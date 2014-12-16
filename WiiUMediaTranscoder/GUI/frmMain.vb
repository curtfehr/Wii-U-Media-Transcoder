Imports Common

Public Class frmMain
    Private _watchDirectories As List(Of WatchDirectory)
    Private _serviceController As ServiceController

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        _serviceController = New ServiceController
        AddHandler _serviceController.OnServiceStart, AddressOf HandleServiceStart
        AddHandler _serviceController.OnServiceStop, AddressOf HandleServiceStop

        tmrServicePoller_Tick(Nothing, Nothing)

        RefreshWatchDirectories()
    End Sub

    Private Sub HandleServiceStart()
        RefreshWatchDirectories()
    End Sub

    Private Sub HandleServiceStop()
        RefreshWatchDirectories()
    End Sub

    Private Sub RefreshWatchDirectories()
        _watchDirectories = _serviceController.Proxy.GetWatchDirectories()
        lstDirectories.DataSource = Nothing
        lstDirectories.DataSource = _watchDirectories
    End Sub

    Private Sub btnConfigureMp3_Click(sender As Object, e As EventArgs) Handles btnConfigureMp3.Click
        Dim mp3VideoForm As New frmMp3VideoSettings(_serviceController)
        mp3VideoForm.Show(Me)

    End Sub

    Private Sub btnAddDirectory_Click(sender As Object, e As EventArgs) Handles btnAddDirectory.Click
        If FolderDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim recursive As Boolean = MsgBox("Would you like to include all sub folders?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes
            Dim deleteOriginal As Boolean = MsgBox("Would you like to keep the original files?", MsgBoxStyle.YesNo) = MsgBoxResult.No

            Dim directory As New WatchDirectory() With {.Path = FolderDialog.SelectedPath, .Recursive = recursive, .DeleteOriginal = deleteOriginal}

            _serviceController.Proxy.AddWatchDirectory(directory)
            RefreshWatchDirectories()
        End If
    End Sub

    Private Sub btnRemoveDirectory_Click(sender As Object, e As EventArgs) Handles btnRemoveDirectory.Click
        If lstDirectories.SelectedItem IsNot Nothing AndAlso _
            MsgBox("Are you sure you want to remove the following directory from processing?" & vbNewLine & lstDirectories.SelectedItem.ToString, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            
            Dim directory As WatchDirectory = CType(lstDirectories.SelectedItem, WatchDirectory)

            _serviceController.Proxy.RemoveWatchDirectory(directory)
            RefreshWatchDirectories()
        End If
    End Sub

    Private Sub tbInstantQueue_Scroll(sender As Object, e As EventArgs) Handles tbInstantQueue.Scroll
        If tbInstantQueue.Value = 0 Then
            _serviceController.Proxy.StartInstantQueue()
        Else
            _serviceController.Proxy.StopInstantQueue()
        End If
    End Sub

    Private Sub tbNormalQueue_Scroll(sender As Object, e As EventArgs) Handles tbNormalQueue.Scroll
        If tbNormalQueue.Value = 0 Then
            _serviceController.Proxy.StartNormalQueue()
        Else
            _serviceController.Proxy.StopNormalQueue()
        End If
    End Sub

    Private Sub tmrServicePoller_Tick(sender As Object, e As EventArgs) Handles tmrServicePoller.Tick
        tbInstantQueue.Value = If(_serviceController.Proxy.IsInstantQueueOn, 0, 1)
        tbNormalQueue.Value = If(_serviceController.Proxy.IsNormalQueueOn, 0, 1)

        lblInstantQueuePending.Text = _serviceController.Proxy.GetInstantQueuePending()
        lblNormalQueuePending.Text = _serviceController.Proxy.GetNormalQueuePending()
        lblCurrentStatus.Text = _serviceController.Proxy.GetCurrentStatus().ToString()
        pbCurrent.Value = _serviceController.Proxy.GetCurrentProgress()

        RefreshWatchDirectories()
    End Sub

    Private Sub btnConfigureVideo_Click(sender As Object, e As EventArgs) Handles btnConfigureVideo.Click
        MsgBox("Feature coming soon!", MsgBoxStyle.Information, "Wii U Media Transcoder")
    End Sub
End Class