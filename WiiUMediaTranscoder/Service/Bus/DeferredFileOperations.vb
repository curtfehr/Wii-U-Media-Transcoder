Imports System.Threading
Imports System.Collections.Generic
Imports System.Linq
Imports NLog
Imports Service.Domain
Imports Service.Data

Namespace Bus
    'mitigates file operations and cleanup that may be otherwise interrupted via temporary issues like file locks
    Public Class DeferredFileOperations
        Private _queue As Queue(Of FileOperation)
        Private _runToken As CancellationTokenSource
        Private _gracefulStopWaitHandle As ManualResetEvent
        Private _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New()
            'load state
            _queue = New Queue(Of FileOperation)
            Using db As New ServiceContext
                Dim fileOpQuery = From fileOp In db.FileOperation
                                    Order By fileOp.FileOperationId Descending
                                    Select fileOp

                For Each fileOperation In fileOpQuery
                    _queue.Enqueue(fileOperation)
                Next
            End Using

            'start thread
            _runToken = New CancellationTokenSource()
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Process), _runToken.Token)
            _gracefulStopWaitHandle = New ManualResetEvent(False)
        End Sub

        Public Sub Move(source As String, destination As String, isAsync As Boolean)
            If isAsync Then
                _queue.Enqueue(New FileOperation() With {.Source = source, .Destination = destination, .Operation = FileOperation.OperationType.Move})
            Else
                Dim success As Boolean = False
                Dim tries As Integer = 0
                While Not success
                    Try
                        If IO.File.Exists(destination) Then
                            IO.File.Delete(destination)
                        End If
                        If IO.File.Exists(source) Then
                            IO.File.Move(source, destination)
                        End If

                        success = True
                    Catch ex As Exception
                        tries += 1
                        If tries > 10 Then
                            Throw
                        End If
                        Thread.Sleep(2000)
                    End Try
                End While
            End If
        End Sub

        Public Sub Delete(source As String, isAsync As Boolean)
            If isAsync Then
                _queue.Enqueue(New FileOperation() With {.Source = source, .Operation = FileOperation.OperationType.Delete})
            Else
                Dim success As Boolean = False
                Dim tries As Integer = 0
                While Not success
                    Try
                        If IO.File.Exists(source) Then
                            IO.File.Delete(source)
                        End If

                        success = True
                    Catch ex As Exception
                        tries += 1
                        If tries > 10 Then
                            Throw
                        End If
                        Thread.Sleep(2000)
                    End Try
                End While
            End If
        End Sub

        Public Sub DeleteFolder(source As String, isAsync As Boolean)
            If isAsync Then
                _queue.Enqueue(New FileOperation() With {.Source = source, .Operation = FileOperation.OperationType.DeleteFolder})
            Else
                Dim success As Boolean = False
                Dim tries As Integer = 0
                While Not success
                    Try
                        If IO.Directory.Exists(source) Then
                            IO.Directory.Delete(source, True)
                        End If

                        success = True
                    Catch ex As Exception
                        tries += 1
                        If tries > 10 Then
                            Throw
                        End If
                        Thread.Sleep(2000)
                    End Try
                End While
            End If
        End Sub

        Public Sub MarkFileComplete(source As String, isAsync As Boolean)
            If isAsync Then
                _queue.Enqueue(New FileOperation() With {.Source = source, .Operation = FileOperation.OperationType.MarkComplete})
            Else
                Dim success As Boolean = False
                Dim tries As Integer = 0
                While Not success
                    Try
                        If IO.File.Exists(source) Then
                            IO.File.SetLastAccessTime(source, DateTime.MinValue)
                        End If

                        success = True
                    Catch ex As Exception
                        tries += 1
                        If tries > 10 Then
                            Throw
                        End If
                        Thread.Sleep(2000)
                    End Try
                End While
            End If
        End Sub

        Public Sub Dispose()
            _runToken.Cancel()
            _gracefulStopWaitHandle.WaitOne(600000)
            _gracefulStopWaitHandle.Dispose()

            Using db As New ServiceContext
                db.Database.ExecuteSqlCommand("DELETE FileOperations")

                While _queue.Count > 0
                    db.FileOperation.Add(_queue.Dequeue)
                    db.SaveChanges()
                End While
            End Using
        End Sub

        Private Sub Process()
            While Not _runToken.IsCancellationRequested
                If _queue.Count > 0 Then
                    Dim op = _queue.Dequeue()

                    Try
                        Select Case op.Operation
                            Case FileOperation.OperationType.Move
                                If IO.File.Exists(op.Destination) Then
                                    IO.File.Delete(op.Destination)
                                End If
                                If IO.File.Exists(op.Source) Then
                                    IO.File.Move(op.Source, op.Destination)
                                End If
                            Case FileOperation.OperationType.Delete
                                If IO.File.Exists(op.Source) Then
                                    IO.File.Delete(op.Source)
                                End If
                            Case FileOperation.OperationType.DeleteFolder
                                If IO.Directory.Exists(op.Source) Then
                                    IO.Directory.Delete(op.Source, True)
                                End If
                            Case FileOperation.OperationType.MarkComplete
                                If IO.File.Exists(op.Source) Then
                                    IO.File.SetLastAccessTime(op.Source, DateTime.MinValue)
                                End If
                        End Select
                    Catch ex As Exception
                        op.Tries += 1
                        If op.Tries > 5000 Then
                            _logger.Error("Process: File operation attempts exceeded maximum try count." & vbNewLine & op.ToString & vbNewLine & ex.Message & vbNewLine & vbNewLine & ex.StackTrace)
                        Else
                            _queue.Enqueue(op)
                        End If
                    End Try
                End If

                Thread.Sleep(5000)
            End While
            _gracefulStopWaitHandle.Set()
        End Sub

    End Class

End Namespace
