Imports Common
Imports System.IO
Imports System.Security.Permissions
Imports System.ServiceModel
Imports NLog

Namespace Bus
    <ServiceBehavior(ConcurrencyMode:=ConcurrencyMode.Single, InstanceContextMode:=InstanceContextMode.Single)> _
    Public Class ServiceController
        Implements IConfigureService

        Private _logger As Logger = LogManager.GetCurrentClassLogger()
        Private _service As WiiUMediaTranscodeService

        Public Sub New()
        End Sub

        Public Sub New(service As WiiUMediaTranscodeService)
            _service = service
        End Sub

        <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
        Public Sub AddWatchDirectory(directory As WatchDirectory) Implements IConfigureService.AddWatchDirectory
            _service.ScanDirectoryForChanges(directory.Path, directory.Recursive, directory.DeleteOriginal)

            Dim fw As New FileSystemWatcher(directory.Path)
            fw.IncludeSubdirectories = directory.Recursive
            fw.NotifyFilter = (NotifyFilters.LastAccess Or NotifyFilters.LastWrite Or NotifyFilters.FileName Or NotifyFilters.DirectoryName)

            AddHandler fw.Changed, Function(sender, e) _service.AddQueueHandler(sender, e, directory.DeleteOriginal)
            AddHandler fw.Created, Function(sender, e) _service.AddQueueHandler(sender, e, directory.DeleteOriginal)
            AddHandler fw.Renamed, Function(sender, e) _service.AddQueueHandler(sender, e, directory.DeleteOriginal)
            AddHandler fw.Error, Function(sender, e) _service.HandleFileSystemWatchError(sender, e, directory.DeleteOriginal)

            fw.EnableRaisingEvents = True

            _service.WatchDirectories.Add(directory, fw)
        End Sub

        Public Function GetMp3VideoConfiguration() As Mp3Video.Mp3VideoConfiguration Implements IConfigureService.GetMp3VideoConfiguration
            Return _service.Mp3Config
        End Function

        Public Function GetWatchDirectories() As Collections.Generic.List(Of WatchDirectory) Implements IConfigureService.GetWatchDirectories
            Dim directories As New Collections.Generic.List(Of WatchDirectory)
            For Each directory In _service.WatchDirectories.Keys
                directories.Add(directory)
            Next
            Return directories
        End Function

        Public Function IsInstantQueueOn() As Boolean Implements IConfigureService.IsInstantQueueOn
            Return _service.IsInstantQueueOn
        End Function

        Public Function IsNormalQueueOn() As Boolean Implements IConfigureService.IsNormalQueueOn
            Return _service.IsNormalQueueOn
        End Function

        <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
        Public Sub RemoveWatchDirectory(directory As WatchDirectory) Implements IConfigureService.RemoveWatchDirectory
            Try
                Dim myDirectory As WatchDirectory = Nothing
                For Each currentDirectory In _service.WatchDirectories.Keys
                    If currentDirectory.Equals(directory) Then
                        myDirectory = currentDirectory
                        Exit For
                    End If
                Next

                Dim fw As FileSystemWatcher = _service.WatchDirectories(myDirectory)
                fw.EnableRaisingEvents = False
                'TODO: track lambda functions so we can remove handlers in case of memory leak
                fw.Dispose()
                _service.WatchDirectories.Remove(myDirectory)
            Catch ex As Exception
                _logger.Error("RemoveWatchDirectory: " & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
                If ex.InnerException IsNot Nothing Then
                    _logger.Error(ex.InnerException.Message)
                End If
                Throw
            End Try
        End Sub

        Public Sub SetMp3VideoConfiguration(configuration As Mp3Video.Mp3VideoConfiguration) Implements IConfigureService.SetMp3VideoConfiguration
            _service.Mp3Config = configuration
        End Sub

        Public Sub StartInstantQueue() Implements IConfigureService.StartInstantQueue
            _service.IsInstantQueueOn = True
        End Sub

        Public Sub StartNormalQueue() Implements IConfigureService.StartNormalQueue
            _service.IsNormalQueueOn = True
        End Sub

        Public Sub StopInstantQueue() Implements IConfigureService.StopInstantQueue
            _service.IsInstantQueueOn = False
        End Sub

        Public Sub StopNormalQueue() Implements IConfigureService.StopNormalQueue
            _service.IsNormalQueueOn = False
        End Sub

        Public Function GetCurrentStatus() As ServiceStatus Implements IConfigureService.GetCurrentStatus
            Return _service.Status
        End Function

        Public Function GetCurrentProgress() As Integer Implements IConfigureService.GetCurrentProgress
            Return _service.Progress
        End Function

        Public Function GetInstantQueuePending() As Integer Implements IConfigureService.GetInstantQueuePending
            Return _service.InstantQueue.Count
        End Function

        Public Function GetNormalQueuePending() As Integer Implements IConfigureService.GetNormalQueuePending
            Return _service.NormalQueue.Count
        End Function
    End Class

End Namespace
