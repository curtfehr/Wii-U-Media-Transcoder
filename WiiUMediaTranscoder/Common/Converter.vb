Imports System.Collections.ObjectModel
Imports System.IO

Public Class Converter
    Private WithEvents _process As Process
    Private _sourceAudioFile As String
    Private _sourceFramesPath As String
    Private _isInstant As Boolean

    Public Event ConversionComplete(sender As Converter, sourceAudioFile As String, sourceFramesPath As String, isInstant As Boolean)

    Public Sub ConvertVideo(sourceVideoFile As String)
        Throw New NotImplementedException()
    End Sub

    Public Function ConvertAudio(sourceAudioFile As String, sourceFramesPath As String, frameFileFormatLength As Integer, frameRate As Integer, isSingleFrame As Boolean, totalSeconds As Long, ByRef arguments As String) As String
        _sourceAudioFile = sourceAudioFile
        _sourceFramesPath = sourceFramesPath
        _isInstant = isSingleFrame

        Dim outputFile As String = sourceFramesPath & ".mp4"

        If File.Exists(outputFile) Then
            File.Delete(outputFile)
        End If

        _process = New Process()
        _process.StartInfo.UseShellExecute = False
        _process.StartInfo.RedirectStandardOutput = False
        _process.StartInfo.RedirectStandardError = False
        _process.StartInfo.CreateNoWindow = True
        _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        _process.StartInfo.FileName = "lib\ffmpeg"
        _process.EnableRaisingEvents = True

        If isSingleFrame Then
            arguments = " -t " & totalSeconds & " -r 1 -loop 1 -f image2 -i """ & sourceFramesPath & "\instant.jpg"" -i """ & _
                sourceAudioFile & """ -c:v libx264 -c:a aac -strict experimental -b:a 192k -shortest """ & outputFile & """"
        Else
            arguments = " -t " & totalSeconds & " -r " & frameRate.ToString & " -i """ & sourceFramesPath & "\%0" & _
                        frameFileFormatLength.ToString & "d.jpg"" -i """ & sourceAudioFile & _
                        """ -c:v libx264 -c:a aac -strict experimental -b:a 192k -shortest """ & _
                        outputFile & """"
        End If
        _process.StartInfo.Arguments = arguments

        'used to do run ffmpeg async, but this caused issue whereby too many instances of ffmpeg started, causing major performance issues
        'left this here as a reminder and just in case if wanted to revisit from a slight different approach
        'AddHandler _process.Exited, AddressOf Process_Exit

        _process.Start()

        'wait until exit doesn't seem to work 100% of the time, sometimes it just hangs the process until this process is force terminated for some reason
        Do
            Threading.Thread.Sleep(500)
        Loop Until _process.HasExited

        'Process_Exit(Nothing, Nothing)
        _process.Dispose()

        Return outputFile
    End Function

    'Public Sub Process_Exit(sender As Object, e As EventArgs)
    '    _process.Dispose()
    '    RaiseEvent ConversionComplete(Me, _sourceAudioFile, _sourceFramesPath, _isInstant)
    'End Sub
End Class

