Imports System.Text.RegularExpressions

Namespace Mp3Video
    Public MustInherit Class BaseMp3Video
        Implements IMp3Video

        Private _currentFrame As Long = -1 'start at -1 so that first frame will be 0
        Protected ReadOnly Property GetCurrentFrame As Long
            Get
                _currentFrame += 1
                If _currentFrame > FrameCount Then _currentFrame = 0

                Return _currentFrame
            End Get
        End Property

        Private _isResized As Boolean = False
        Protected ReadOnly Property IsResized As Boolean
            Get
                Return _isResized
            End Get
        End Property

        Private _mp3 As TagLib.File
        Protected ReadOnly Property Mp3 As TagLib.File
            Get
                Return _mp3
            End Get
        End Property

        Private _totalSeconds As Long
        Public ReadOnly Property TotalSeconds As Long Implements IMp3Video.TotalSeconds
            Get
                Return _totalSeconds
            End Get
        End Property

        Public Property Configuration As Mp3VideoConfiguration Implements IMp3Video.Configuration
            Get
                'builds config and returns
                Dim config As New Mp3VideoConfiguration With {.Type = Me.GetType(), .Width = _width, .Height = _height, .Fps = _fps, .Settings = _settings}
                Return config
            End Get
            Set(value As Mp3VideoConfiguration)
                If value IsNot Nothing Then
                    Width = value.Width
                    Height = value.Height
                    Fps = value.Fps
                    _settings = value.Settings
                End If
            End Set
        End Property

        Private _width As Integer = 1920
        Public Property Width As Integer Implements IMp3Video.Width
            Get
                Return _width
            End Get
            Set(value As Integer)
                If value Mod 2 = 1 Then value += 1 'ffmpeg needs height and width divisible by 2 for some reason...
                _width = value
                _isResized = True
            End Set
        End Property

        Private _height As Integer = 1080
        Public Property Height As Integer Implements IMp3Video.Height
            Get
                Return _height
            End Get
            Set(value As Integer)
                If value Mod 2 = 1 Then value += 1 'ffmpeg needs height and width divisible by 2 for some reason...
                _height = value
                _isResized = True
            End Set
        End Property

        Private _fps As Integer = 20
        Public Property Fps As Integer Implements IMp3Video.Fps
            Get
                Return _fps
            End Get
            Set(value As Integer)
                'first calculate new current frame, so we can change fps on the fly
                If _currentFrame > 0 Then _currentFrame = CInt((_currentFrame / _fps) * value)
                _fps = value
                SetCounterPad()
            End Set
        End Property

        Private _counterPad As String
        Protected ReadOnly Property CounterPad As String
            Get
                Return _counterPad
            End Get
        End Property

        Public ReadOnly Property FileNameLength As Integer Implements IMp3Video.FileNameLength
            Get
                Return _counterPad.Length
            End Get
        End Property

        Private _settings As List(Of Mp3VideoSetting)
        Public ReadOnly Property Settings As List(Of Mp3VideoSetting) Implements IMp3Video.Settings
            Get
                Return _settings
            End Get
        End Property

        Public ReadOnly Property FrameCount As Long Implements IMp3Video.FrameCount
            Get
                Return _totalSeconds * _fps
            End Get
        End Property

        Public MustOverride ReadOnly Property Name As String Implements IMp3Video.Name
        Public MustOverride ReadOnly Property Description As String Implements IMp3Video.Description
        Public MustOverride Sub Initialize(mp3Path As String, Optional configuration As Mp3VideoConfiguration = Nothing) Implements IMp3Video.Initialize
        Public MustOverride Function GenerateAllFrames(frameDumpPath As String, isInstantQueue As Boolean) As String Implements IMp3Video.GenerateAllFrames
        Public MustOverride Function GetNextFrame() As Drawing.Image Implements IMp3Video.GetNextFrame
        Public MustOverride Function GetInstantQueueFrame() As Drawing.Image Implements IMp3Video.GetInstantQueueFrame
        Public MustOverride Sub SetDefaultConfiguration() Implements IMp3Video.SetDefaultConfiguration

        Public Sub InitializeBase(mp3FilePath As String, Optional configuration As Mp3VideoConfiguration = Nothing)
            Me.Configuration = configuration
            _mp3 = TagLib.File.Create(mp3FilePath)
            _totalSeconds = CLng(Mp3.Properties.Duration.TotalSeconds)

            If configuration IsNot Nothing Then
                SetCounterPad()
            End If
        End Sub

        Public Sub Dispose() Implements IMp3Video.Dispose
            If _mp3 IsNot Nothing Then
                _mp3.Dispose()
            End If
        End Sub

        Protected Sub ApplySettings(newSettings As List(Of Mp3VideoSetting))
            _settings = newSettings
        End Sub

        Private Sub SetCounterPad()
            _counterPad = New Regex(".").Replace(FrameCount.ToString(), "0")
        End Sub
    End Class
End Namespace

