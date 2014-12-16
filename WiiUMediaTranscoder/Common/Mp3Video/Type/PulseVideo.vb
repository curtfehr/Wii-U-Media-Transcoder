Imports System.Drawing
Imports System.Text
Imports System.IO
Imports System.Drawing.Drawing2D

Namespace Mp3Video.Type
    Public Class PulseVideo
        Inherits BaseMp3Video
        Implements IMp3Video

        'keep a reference of last frame to dispose it properly to avoid memory leaks
        Private _lastFrame As Image

        Private _r As Integer
        Private _g As Integer
        Private _b As Integer

        Private _pulseIndex As Integer = 0
        Private _pulseDirection As Integer = 1

        Private _metaData As String

        'the below are contained within the list, separated here for private consumption
        Private _pulseRate As Mp3VideoSetting
        Private _color As Mp3VideoSetting
        Private _randomColor As Mp3VideoSetting
        Private _randomColorThreshold As Mp3VideoSetting

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Pulsing Gradient"
            End Get
        End Property

        Public Overrides ReadOnly Property Description As String
            Get
                Return "The background pulses with color, while id3 tag information and a progress slider sits in the foreground."
            End Get
        End Property

        Public Overrides Sub Initialize(mp3FilePath As String, Optional configuration As Mp3VideoConfiguration = Nothing)
            InitializeBase(mp3FilePath, configuration)

            If configuration Is Nothing Then
                SetDefaultConfiguration()
            Else
                For Each setting In configuration.Settings
                    Select Case setting.Name
                        Case "Pulse Rate"
                            _pulseRate = setting
                        Case "Random Color Threshold"
                            _randomColorThreshold = setting
                        Case "Background Pulse Color"
                            _color = setting
                        Case "Random Color"
                            _randomColor = setting
                    End Select
                Next
            End If

            _metaData = GetMetaDataString(mp3FilePath)

            CalculateRandomPulseColor()

            _pulseDirection = 1
            _pulseIndex = 0
        End Sub

        Private Sub CalculateRandomPulseColor()
            Dim rand As New Random()
            _r = rand.Next(CInt(_randomColorThreshold.Value))
            _g = rand.Next(CInt(_randomColorThreshold.Value))
            _b = rand.Next(CInt(_randomColorThreshold.Value))
        End Sub

        Public Overrides Sub SetDefaultConfiguration()
            _pulseRate = New Mp3VideoSetting(1, 500, "Pulse Rate", "The amount of frames it takes for the background to pulse")
            _pulseRate.Value = "33"

            _randomColorThreshold = New Mp3VideoSetting(10, 255, "Random Color Threshold", "The amount of depth of color allowed for a random color")
            _randomColorThreshold.Value = "70"

            'listens to changed values, can be done when configuring on the fly.
            'In this case we need to calculate a new random color since this setting applies to how that is calculated
            AddHandler _randomColorThreshold.ValueChanged, New Mp3VideoSetting.ValueChangedEventHandler(AddressOf CalculateRandomPulseColor)

            _color = New Mp3VideoSetting(Color.DarkTurquoise, "Background Pulse Color", "The pulse color of the top and bottom when random color is disabled.")

            _randomColor = New Mp3VideoSetting(True, "Random Color", "Enables or disables random background pulse color.")
            _randomColor.Value = True
            AddHandler _randomColor.ValueChanged, New Mp3VideoSetting.ValueChangedEventHandler(AddressOf CalculateRandomPulseColor)

            Dim newSettings As New List(Of Mp3VideoSetting)
            newSettings.Add(_pulseRate)
            newSettings.Add(_randomColor)
            newSettings.Add(_randomColorThreshold)
            newSettings.Add(_color)

            ApplySettings(newSettings)
        End Sub

        'returns full path to frames
        Public Overrides Function GenerateAllFrames(frameDumpPath As String, isInstantQueue As Boolean) As String
            If Directory.Exists(frameDumpPath) Then
                Directory.Delete(frameDumpPath, True)
            End If
            Directory.CreateDirectory(frameDumpPath)

            If isInstantQueue Then
                Dim image As Image = GetInstantQueueFrame()
                image.Save(frameDumpPath & "\instant.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                image.Dispose()
            Else
                For i As Integer = 1 To FrameCount
                    Dim image As Image = GetFrame(i)
                    image.Save(frameDumpPath & "\" & Right(CounterPad & i.ToString, CounterPad.Length) & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                    image.Dispose()
                Next
            End If

            'reset
            _pulseDirection = 1
            _pulseIndex = 0

            Return frameDumpPath
        End Function

        Public Overrides Function GetNextFrame() As Image
            If _lastFrame IsNot Nothing Then _lastFrame.Dispose()
            _lastFrame = GetFrame(GetCurrentFrame)
            Return _lastFrame
        End Function

        Public Overrides Function GetInstantQueueFrame() As Image
            If _lastFrame IsNot Nothing Then _lastFrame.Dispose()
            _lastFrame = GetFrame(1, True)
            Return _lastFrame
        End Function

        Private Function GetFrame(currentFrame As Long, Optional isInstantQueueFrame As Boolean = False) As Image
            If Fps = 0 Then isInstantQueueFrame = True
            If isInstantQueueFrame Then _pulseIndex = 0
            Dim color As Color = GetBackgroundColor()

            Dim image As Image = New Bitmap(1920, 1080)
            Dim drawing As Graphics = Graphics.FromImage(image)
            Dim textBrush As Brush = New SolidBrush(color.White)
            Dim rand As New Random()
            Dim gradientBrush As New LinearGradientBrush(New Rectangle(0, 0, 1920, 1080), color, System.Drawing.Color.Black, LinearGradientMode.Vertical)
            Dim solidBrush As New SolidBrush(System.Drawing.Color.White)

            gradientBrush.SetSigmaBellShape(0.5F)

            drawing.FillRectangle(gradientBrush, New Rectangle(0, 0, 1920, 1080)) 'pulsing background
            drawing.DrawString(_metaData, New Font("Arial", 26), textBrush, 10, 300) 'id3 text

            If Not isInstantQueueFrame Then
                'progress slider and time
                drawing.FillRectangle(solidBrush, New Rectangle(10, 725, 1900, 6))
                Dim position As Long = 10 + ((1900 - 5) * (currentFrame / FrameCount))
                drawing.FillRectangle(solidBrush, New Rectangle(position, 725 - 10, 5, 26))

                Dim currentTimeSpan As New TimeSpan((currentFrame / Fps) * 10000000)
                Dim format As String = "mm':'ss"
                If currentTimeSpan.Hours > 0 Then
                    format = "hh':'mm':'ss"
                End If
                drawing.DrawString("   " & currentTimeSpan.ToString(format), New Font("Arial", 26), textBrush, 10, 755)
            End If

            drawing.Save()

            textBrush.Dispose()
            drawing.Dispose()
            gradientBrush.Dispose()
            solidBrush.Dispose()

            If IsResized Then
                Dim resizedImage As New Bitmap(image, New Size(Width, Height))
                image.Dispose()
                Return resizedImage
            Else
                Return image
            End If

        End Function

        Private Function GetBackgroundColor() As Color
            Dim returnColor As Color

            'this can happen when configuring the video in real time
            If _pulseIndex > _pulseRate.Value Then
                _pulseIndex = 0
                _pulseDirection = 1
            End If

            If CBool(_randomColor.Value) Then
                Dim cR As Integer = _r - ((_pulseIndex / CInt(_pulseRate.Value)) * _r)
                Dim cG As Integer = _g - ((_pulseIndex / CInt(_pulseRate.Value)) * _g)
                Dim cB As Integer = _b - ((_pulseIndex / CInt(_pulseRate.Value)) * _b)

                returnColor = Color.FromArgb(cR, cG, cB)
            Else
                returnColor = Color.FromArgb(CInt(_color.Value))

                Dim cR As Integer = returnColor.R - ((_pulseIndex / CInt(_pulseRate.Value)) * returnColor.R)
                Dim cG As Integer = returnColor.G - ((_pulseIndex / CInt(_pulseRate.Value)) * returnColor.G)
                Dim cB As Integer = returnColor.B - ((_pulseIndex / CInt(_pulseRate.Value)) * returnColor.B)

                returnColor = Color.FromArgb(cR, cG, cB)
            End If

            If _pulseDirection = -1 AndAlso _pulseIndex = 0 Then
                _pulseDirection = 1
            ElseIf _pulseDirection = 1 AndAlso _pulseIndex = CInt(_pulseRate.Value) Then
                _pulseDirection = -1
            End If
            _pulseIndex += _pulseDirection

            Return returnColor
        End Function

        Private Function GetMetaDataString(mp3FilePath As String) As String
            Dim metaData As New StringBuilder()

            metaData.AppendLine("   File Name:")
            metaData.AppendLine(Path.GetFileNameWithoutExtension(mp3FilePath))
            metaData.AppendLine()
            metaData.AppendLine("   Title:" & vbTab & vbTab & Mp3.Tag.Title)
            metaData.AppendLine("   Artist:" & vbTab & vbTab & Mp3.Tag.FirstPerformer)
            metaData.AppendLine("   Album:" & vbTab & Mp3.Tag.Album)
            metaData.AppendLine("   Year:" & vbTab & vbTab & If(Mp3.Tag.Year > 0, Mp3.Tag.Year.ToString, ""))

            Dim lengthFormat As String = "mm':'ss"
            If Mp3.Properties.Duration.Hours > 0 Then
                lengthFormat = "hh':'mm':'ss"
            End If
            metaData.AppendLine("   Length:" & vbTab & Mp3.Properties.Duration.ToString(lengthFormat))

            Return metaData.ToString
        End Function

    End Class
End Namespace