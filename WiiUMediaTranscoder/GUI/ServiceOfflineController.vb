Imports Common

Public Class ServiceOfflineController
    Implements IConfigureService

    Private Sub DisplayOfflineMessage()
        MsgBox("The Service is offline.  Your changes will not be saved", MsgBoxStyle.Exclamation, "Wii U Media Transcoder")
    End Sub

    Public Sub AddWatchDirectory(directory As WatchDirectory) Implements IConfigureService.AddWatchDirectory
        DisplayOfflineMessage()
    End Sub

    Public Function GetCurrentProgress() As Integer Implements IConfigureService.GetCurrentProgress
        Return 0
    End Function

    Public Function GetCurrentStatus() As ServiceStatus Implements IConfigureService.GetCurrentStatus
        Return ServiceStatus.Offline
    End Function

    Public Function GetInstantQueuePending() As Integer Implements IConfigureService.GetInstantQueuePending
        Return 0
    End Function

    Public Function GetMp3VideoConfiguration() As Mp3Video.Mp3VideoConfiguration Implements IConfigureService.GetMp3VideoConfiguration
        Return Nothing
    End Function

    Public Function GetNormalQueuePending() As Integer Implements IConfigureService.GetNormalQueuePending
        Return 0
    End Function

    Public Function GetWatchDirectories() As List(Of WatchDirectory) Implements IConfigureService.GetWatchDirectories
        Return New List(Of WatchDirectory)
    End Function

    Public Function IsInstantQueueOn() As Boolean Implements IConfigureService.IsInstantQueueOn
        Return False
    End Function

    Public Function IsNormalQueueOn() As Boolean Implements IConfigureService.IsNormalQueueOn
        Return False
    End Function

    Public Sub RemoveWatchDirectory(directory As WatchDirectory) Implements IConfigureService.RemoveWatchDirectory
        DisplayOfflineMessage()
    End Sub

    Public Sub SetMp3VideoConfiguration(configuration As Mp3Video.Mp3VideoConfiguration) Implements IConfigureService.SetMp3VideoConfiguration
        DisplayOfflineMessage()
    End Sub

    Public Sub StartInstantQueue() Implements IConfigureService.StartInstantQueue
        DisplayOfflineMessage()
    End Sub

    Public Sub StartNormalQueue() Implements IConfigureService.StartNormalQueue
        DisplayOfflineMessage()
    End Sub

    Public Sub StopInstantQueue() Implements IConfigureService.StopInstantQueue
        DisplayOfflineMessage()
    End Sub

    Public Sub StopNormalQueue() Implements IConfigureService.StopNormalQueue
        DisplayOfflineMessage()
    End Sub
End Class
