Imports Common
Imports System.IO
Imports Common.Mp3Video.Type

Public Class tester
    Private _video As PulseVideo

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim starttime As Date = Now
        'DoubleBuffered = True
        'Dim sourceFile As String = Path.GetDirectoryName(Application.ExecutablePath) & "\lib\sample.mp3"

        '_video = New PulseVideo()
        '_video.Initialize(sourceFile)
        '_video.Fps = 60
        '_video.Width = 1920
        '_video.Height = 1080
        'Dim sourceFramesPath As String = _video.GenerateAllFrames(Path.GetDirectoryName(Application.ExecutablePath), False)

        'Dim converter As New Converter()
        'Dim arguments As String = ""
        'converter.ConvertAudio(sourceFile, sourceFramesPath, _video.FileNameLength, _video.Fps, False, arguments)

        'Dim endtime As Date = Now

        'MsgBox("Done. Elapsed Time: " & endtime.Subtract(starttime).ToString)
        'Timer1.Interval = 1000 / _video.Fps
        'Timer1.Enabled = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim previous As Image = BackgroundImage
        'BackgroundImage = _video.GetNextFrame()
        'If previous IsNot Nothing Then previous.Dispose()
    End Sub
End Class
