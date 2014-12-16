Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Reflection
Imports System.Collections
Imports System.Collections.Generic
Imports System.Threading
Imports System.Security.Permissions
Imports System.ServiceModel
Imports NLog
Imports Common
Imports Common.Mp3Video
Imports Service.Bus
Imports Service.Data
Imports Service.Domain

'Sigh...this could have beeen way better architected using nservicebus, 
'but one of my requirements is not making the user install anything to do with msmq.
'Also I was getting bored so I rushed most of the service

Public Class WiiUMediaTranscodeService
    Inherits System.ServiceProcess.ServiceBase
    Private components As System.ComponentModel.IContainer

    Private _logger As Logger = LogManager.GetCurrentClassLogger()

    Private _gracefulStopWaitHandle As ManualResetEvent

    Private _controller As IConfigureService
    Private _runToken As CancellationTokenSource
    Private _wcfHost As ServiceHost

    Private _io As DeferredFileOperations

    Public Property IsInstantQueueOn As Boolean = False
    Public Property IsNormalQueueOn As Boolean = False

    Public Property WatchDirectories As New Collections.Generic.Dictionary(Of WatchDirectory, FileSystemWatcher)
    Private _fileSystemHandlerDelegates As New List(Of Thread)

    Public Property Mp3Config As Mp3VideoConfiguration
    Public Property Status As ServiceStatus = ServiceStatus.Unconfigured
    Public Property Progress As Integer = 0

    Public Shared Property InstantQueue As New Queue(Of String)
    Public Shared Property NormalQueue As New Queue(Of String)

    Public Shared Sub Main()
        Dim ServicesToRun As System.ServiceProcess.ServiceBase()
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New WiiUMediaTranscodeService()}
        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required method for Designer support - do not modify the contents of this method with the code editor.
    Private Sub InitializeComponent()
        Me.ServiceName = "WiiUMediaTranscodeService"
    End Sub

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    Protected Overrides Sub OnStart(args As String())
        Try
            _controller = New ServiceController(Me)
            _wcfHost = New ServiceModel.ServiceHost(_controller, New Uri() {New Uri("net.pipe://localhost")})
            _wcfHost.AddServiceEndpoint(GetType(IConfigureService), New NetNamedPipeBinding(), "WiiUMediaTranscoder")
            _wcfHost.Open()

            _io = New DeferredFileOperations()

            Using db As New ServiceContext
                'load state to memory
                Dim stateQuery = From state In db.State
                                 Order By state.ServiceStateId Descending
                                 Select state

                For Each state In stateQuery
                    IsInstantQueueOn = state.InstantQueueOn
                    IsNormalQueueOn = state.NormalQueueOn

                    'try blocks to ignore corrupt config, these will be replaced anyway on service stop
                    Try
                        If Not String.IsNullOrEmpty(state.Mp3VideoConfiguration) Then
                            Using stream As New MemoryStream(StringToUTF8ByteArray(state.Mp3VideoConfiguration))
                                Dim xs As New XmlSerializer(GetType(Mp3VideoConfiguration))
                                Mp3Config = CType(xs.Deserialize(stream), Mp3VideoConfiguration)
                            End Using
                        End If
                    Catch ex As Exception
                        _logger.Error("OnStart-Mp3Config: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
                        If ex.InnerException IsNot Nothing Then
                            _logger.Error(ex.InnerException.Message)
                        End If
                        Mp3Config = Nothing
                    End Try

                    Try
                        For Each directory In state.Directories
                            Try
                                Dim wd As New WatchDirectory() With {.DeleteOriginal = directory.DeleteOriginal, .Path = directory.Path, .Recursive = directory.Recursive}
                                Dim fw As New FileSystemWatcher(wd.Path)
                                fw.IncludeSubdirectories = wd.Recursive
                                'fw.InternalBufferSize = 65536 'So we can handle pasting in a bunch of files - we'll instead have the event queue kick off separate directory polling thread when multiple drop detected
                                fw.NotifyFilter = (NotifyFilters.LastAccess Or NotifyFilters.LastWrite Or NotifyFilters.FileName Or NotifyFilters.DirectoryName)

                                AddHandler fw.Changed, Function(sender, e) AddQueueHandler(sender, e, wd.DeleteOriginal)
                                AddHandler fw.Created, Function(sender, e) AddQueueHandler(sender, e, wd.DeleteOriginal)
                                AddHandler fw.Renamed, Function(sender, e) AddQueueHandler(sender, e, wd.DeleteOriginal)
                                AddHandler fw.Error, Function(sender, e) HandleFileSystemWatchError(sender, e, wd.DeleteOriginal)

                                fw.EnableRaisingEvents = True

                                WatchDirectories.Add(wd, fw)
                            Catch ex As Exception
                                _logger.Error("OnStart-LoadWatchDirectories: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
                            End Try
                        Next
                    Catch ex As Exception
                        _logger.Error("OnStart-WatchDirectories: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
                    End Try

                    If Mp3Config IsNot Nothing AndAlso WatchDirectories.Count > 0 Then
                        Status = ServiceStatus.Idle
                    End If
                    Exit For 'only need 1 state, the rest will be discarded
                Next

                'load queue into memory
                Dim queueQuery = From queue In db.Queue
                                Where Not queue.IsInstantComplete Or Not queue.IsNormalComplete
                                Order By queue.ServiceQueueId Descending
                                Select queue

                For Each queue In queueQuery
                    If Not queue.IsInstantComplete Then
                        InstantQueue.Enqueue(queue.Path)
                    End If
                    If Not queue.IsNormalComplete Then
                        NormalQueue.Enqueue(queue.Path)
                    End If
                Next
            End Using

            _runToken = New CancellationTokenSource()
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ProcessQueues), _runToken.Token)
            _gracefulStopWaitHandle = New ManualResetEvent(False)

        Catch ex As Exception
            If _runToken IsNot Nothing Then
                _runToken.Cancel()
            End If

            _logger.Error("OnStart: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
            Status = ServiceStatus.Critical
            Me.Stop()
        End Try
    End Sub

    Public Function AddQueueHandler(source As Object, e As FileSystemEventArgs, deleteOriginal As Boolean) As Boolean
        Try
            'keep operations to bare minimum here, FileSystem buffer overflows quite easily
            Dim newThread As New Thread(AddressOf QueueHandler)
            _fileSystemHandlerDelegates.Add(newThread)
            newThread.Start(New QueueHandlerParameter() With {.FullPath = e.FullPath, .DeleteOriginal = deleteOriginal})
        Catch ex As Exception
            _logger.Error("AddQueueHandler: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
        End Try

        Return True
    End Function

    Class QueueHandlerParameter
        Public Property FullPath As String
        Public Property DeleteOriginal As Boolean
    End Class

    Private Sub QueueHandler(params As QueueHandlerParameter)
        Try
            If IsMusicFile(params.FullPath) Then
                AddToQueue(params.FullPath, params.DeleteOriginal)
            End If
        Catch ex As Exception
            If _runToken IsNot Nothing Then
                _runToken.Cancel()
            End If

            _logger.Error("AddQueueHandler: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
            Status = ServiceStatus.Critical
            Me.Stop()
        End Try
    End Sub

    Public Function HandleFileSystemWatchError(source As Object, e As ErrorEventArgs, deleteOriginal As Boolean) As Boolean
        If TypeOf e.GetException Is InternalBufferOverflowException Then
            _logger.Info("HandleFileSystemWatchError: InternalBufferOverflowException detected. Initiating manual directory scan to pick up missed files.")

            'keep operations to bare minimum here, FileSystem buffer overflows quite easily
            Dim newThread As New Thread(AddressOf FileSystemWatchError)
            _fileSystemHandlerDelegates.Add(newThread)
            newThread.Start(New ErrorQueueHandlerParameter() With {.Source = source, .DeleteOriginal = deleteOriginal})

        Else
            If _runToken IsNot Nothing Then
                _runToken.Cancel()
            End If

            _logger.Error("HandleFileSystemWatchError: " & e.GetException.Message)
            Status = ServiceStatus.Critical
            Me.Stop()
        End If
        Return True
    End Function

    Class ErrorQueueHandlerParameter
        Public Property Source As Object
        Public Property DeleteOriginal As Boolean
    End Class

    Private Sub FileSystemWatchError(params As ErrorQueueHandlerParameter)
        Thread.Sleep(10000) 'We do this because the first chunk of files will make it through to the normal handler. Just giving it plenty of time to throw them into the queue db first
        Dim fsw As FileSystemWatcher = CType(params.Source, FileSystemWatcher)
        ScanDirectoryForChanges(fsw.Path, fsw.IncludeSubdirectories, params.DeleteOriginal)
    End Sub

    Public Sub ScanDirectoryForChanges(path As String, recursive As Boolean, deleteOriginal As Boolean)
        Try
            Dim queueItems As List(Of String)
            Using db As New ServiceContext
                queueItems = From queue In db.Queue.Where(Function(x) Not x.IsInstantComplete Or Not x.IsNormalComplete).Select(Function(x) x.Path).ToList()
            End Using

            Dim files As Dictionary(Of String, Boolean) = GetAllMusicFiles(path, recursive)
            For Each proposedFile In files.Keys
                If Not queueItems.Contains(proposedFile) AndAlso files(proposedFile) = False Then
                    AddToQueue(proposedFile, deleteOriginal)
                End If
            Next
        Catch ex As Exception
            _logger.Error("ScanDirectoryForChanges: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Function GetAllMusicFiles(path As String, recursive As Boolean, Optional parentList As Dictionary(Of String, Boolean) = Nothing) As Dictionary(Of String, Boolean)
        If parentList Is Nothing Then
            parentList = New Dictionary(Of String, Boolean)
        End If

        If Directory.Exists(path) Then
            Dim files As String() = Directory.GetFiles(path)
            For Each f In files
                If IsMusicFile(f) Then
                    parentList.Add(f, (File.GetLastAccessTime(f).Ticks = DateTime.MinValue.Ticks))
                End If
            Next

            If recursive Then
                Dim subDirectories As String() = Directory.GetDirectories(path)
                For Each d In subDirectories
                    GetAllMusicFiles(d, True, parentList)
                Next
            End If
        End If

        Return parentList
    End Function

    Private Function IsMusicFile(fullPath As String) As Boolean
        Dim ext As String = Right(fullPath, 3)

        Select Case ext
            Case "mp3", "aac", "ogg", "wma", "wav"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Sub AddToQueue(fullPath As String, deleteOriginal As Boolean)
        'mark all duplicate complete, add new queue item
        Using db As New ServiceContext
            db.Database.ExecuteSqlCommand(String.Format("DELETE ServiceQueues WHERE Path = '{0}'", fullPath.Replace("'", "''")))

            Dim newQueue As New ServiceQueue() With {.Path = fullPath, .DeleteOriginal = deleteOriginal}
            db.Queue.Add(newQueue)

            db.SaveChanges()
        End Using

        'add to in memory queue if not already (duplicates)
        If Not InstantQueue.Contains(fullPath) Then
            InstantQueue.Enqueue(fullPath)
        End If
        If Not NormalQueue.Contains(fullPath) Then
            NormalQueue.Enqueue(fullPath)
        End If
    End Sub

    Private Sub ProcessQueues()
        Dim tries As Integer = 0

        While Not _runToken.IsCancellationRequested
            Dim filePath As String = Nothing
            Dim isInstant As Boolean = False

            Try
                _fileSystemHandlerDelegates.RemoveAll(Function(x) x.IsAlive = False)

                If Mp3Config IsNot Nothing Then
                    Dim lock As New Object
                    SyncLock lock
                        If IsInstantQueueOn AndAlso InstantQueue.Count > 0 Then
                            isInstant = True
                            filePath = InstantQueue.Dequeue()
                        ElseIf Mp3Config.Fps > 0 AndAlso IsNormalQueueOn AndAlso NormalQueue.Count > 0 Then
                            isInstant = False
                            filePath = NormalQueue.Dequeue()

                            'scrub instant queue if we're skipping it temporarily, we don't want instant queue stuff overwriting normal queue stuff
                            If InstantQueue.Count > 0 Then
                                Dim newInstantQueue As New Queue(Of String)
                                While InstantQueue.Count > 0
                                    Dim tempPath As String = InstantQueue.Dequeue
                                    If tempPath <> filePath Then
                                        newInstantQueue.Enqueue(tempPath)
                                    End If
                                End While
                                InstantQueue = newInstantQueue
                            End If
                        End If
                    End SyncLock
                End If

                If Not String.IsNullOrEmpty(filePath) Then
                    Status = ServiceStatus.Processing

                    Dim video As IMp3Video
                    video = Activator.CreateInstance(Mp3Config.Type)

                    video.Initialize(filePath, Mp3Config)

                    Dim frameDumpPath As String = AppDomain.CurrentDomain.BaseDirectory & Guid.NewGuid().ToString
                    video.GenerateAllFrames(frameDumpPath, isInstant)

                    Dim converter As New Converter()
                    Dim ffmpegArguments As String = ""
                    Dim output As String = converter.ConvertAudio(filePath, frameDumpPath, video.FileNameLength, video.Fps, isInstant, video.TotalSeconds, ffmpegArguments)

                    _logger.Info(String.Format("FFMpeg Arguments: {1}{0}Output: {2}{0}Original: {3}{0}FrameDumpPath: {4}{0}Instant: {5}", vbNewLine, ffmpegArguments, output, filePath, frameDumpPath, isInstant.ToString))

                    HandleConversionComplete(output, filePath, frameDumpPath, isInstant)

                    Thread.Sleep(500) 'prevent cpu pinning. lower sleep time so that if you are doing a batch of a few hundred files we don't waste time needlessly
                ElseIf Not _runToken.IsCancellationRequested Then
                    Status = If(Mp3Config Is Nothing, ServiceStatus.Unconfigured, ServiceStatus.Idle)
                    Thread.Sleep(5000) 'prevent cpu pinning
                End If

                tries = 0 'reset when process succeeds

            Catch ex As Exception
                Status = ServiceStatus.HasError
                _logger.Error("ProcessQueues: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)

                Dim lock As New Object
                SyncLock lock
                    If filePath IsNot Nothing Then
                        If isInstant Then
                            InstantQueue.Enqueue(filePath)
                        Else
                            NormalQueue.Enqueue(filePath)
                        End If
                    End If
                End SyncLock

                tries += 1

                If tries >= 60 Then 'prevent runaway memory leaks and orphaned frame dumps, just shut down.  Service will continue to run, only to show it's critical
                    _runToken.Cancel()
                    Status = ServiceStatus.Critical
                Else
                    Thread.Sleep(5000) 'maybe file locks? wait a minute and try again. TODO: this could leave a lot of frame dump folders orphaned and cause memory leaks.  Need to handle that individually
                End If

            End Try
        End While

        Status = ServiceStatus.Critical
        _gracefulStopWaitHandle.Set()
    End Sub

    Private Sub HandleConversionComplete(tempAudioFile As String, sourceAudioFile As String, sourceFramesPath As String, isInstant As Boolean)
        'mark complete
        Dim deleteOriginal As Boolean = False
        Try
            Using db As New ServiceContext
                Dim queueItems = From queue In db.Queue.Where(Function(x) x.Path.Equals(sourceAudioFile)).ToList

                For Each queue In queueItems
                    deleteOriginal = deleteOriginal Or queue.DeleteOriginal
                    queue.IsInstantComplete = True
                    If Not isInstant Then queue.IsNormalComplete = True

                    db.Queue.Attach(queue)
                    Dim entry = db.Entry(queue)
                    entry.Property(Function(x) x.IsInstantComplete).IsModified = True
                    entry.Property(Function(x) x.IsNormalComplete).IsModified = True

                    db.SaveChanges()
                Next
            End Using
        Catch ex As Exception
            If _runToken IsNot Nothing Then
                _runToken.Cancel()
            End If

            _logger.Error("HandleConversionComplete-EF: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
            Status = ServiceStatus.Critical
            Me.Stop()
        End Try

        'cleanup
        Try
            _io.DeleteFolder(sourceFramesPath, True)
            Dim newFileName As String = Path.GetDirectoryName(sourceAudioFile) & "\" & Path.GetFileNameWithoutExtension(sourceAudioFile) & ".mp4"
            _io.Move(tempAudioFile, newFileName, True)

            'delete file only if configured to do so and if it has been completed in normal queue
            If deleteOriginal AndAlso Not isInstant Then
                _io.Delete(sourceAudioFile, True)
            Else
                _io.MarkFileComplete(sourceAudioFile, True)
            End If
        Catch ex As Exception
            _logger.Error("HandleConversionComplete-CU: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        Try
            If _runToken IsNot Nothing Then
                _runToken.Cancel()
            End If

            'wait for current in process item to complete to stop service gracefully
            'timeout after 10 minutes to ensure it doesn't prevent auto shut down
            If _gracefulStopWaitHandle IsNot Nothing Then
                _gracefulStopWaitHandle.WaitOne(600000)
                _gracefulStopWaitHandle.Dispose()
            End If

            _wcfHost.Close()
            _wcfHost = Nothing

            _io.Dispose()

            'save state to ef db
            Using db As New ServiceContext
                'clear current state
                db.Database.ExecuteSqlCommand("DELETE ServiceWatchDirectories")
                db.Database.ExecuteSqlCommand("DELETE ServiceStates")

                'create new state
                Dim mp3ConfigXml As String = ""
                Using stream As New MemoryStream()
                    Dim xs As New XmlSerializer(GetType(Mp3VideoConfiguration))
                    Dim xw As New XmlTextWriter(stream, Encoding.UTF8)

                    xs.Serialize(xw, Mp3Config)
                    mp3ConfigXml = UTF8ByteArrayToString(CType(xw.BaseStream, MemoryStream).ToArray())
                End Using

                Dim directories As New Collections.Generic.List(Of ServiceWatchDirectory)
                For Each directory In WatchDirectories.Keys
                    Dim swd As New ServiceWatchDirectory() With {.Path = directory.Path, .Recursive = directory.Recursive, .DeleteOriginal = directory.DeleteOriginal}
                    directories.Add(swd)
                Next

                Dim state As New ServiceState() With {.InstantQueueOn = IsInstantQueueOn, .NormalQueueOn = IsNormalQueueOn, .Mp3VideoConfiguration = mp3ConfigXml, .Directories = directories}

                'save state
                db.State.Add(state)
                db.SaveChanges()
            End Using
        Catch ex As Exception
            _logger.Error("OnStop: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace & vbNewLine & vbNewLine & ex.InnerException.Message)
            _logger.Error(ex.InnerException.InnerException.Message)
        End Try

    End Sub

    Private Function StringToUTF8ByteArray(xml As String) As Byte()
        Return New UTF8Encoding().GetBytes(xml)
    End Function

    Private Function UTF8ByteArrayToString(bytes As Byte()) As String
        Return New UTF8Encoding().GetString(bytes)
    End Function
End Class