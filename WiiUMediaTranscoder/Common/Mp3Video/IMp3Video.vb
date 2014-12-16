Imports System.Drawing

Namespace Mp3Video
    Public Interface IMp3Video

        'Base Class
        Property Configuration As Mp3VideoConfiguration
        Property Width As Integer
        Property Height As Integer
        Property Fps As Integer
        ReadOnly Property FrameCount As Long
        ReadOnly Property FileNameLength As Integer
        ReadOnly Property Settings As List(Of Mp3VideoSetting)
        ReadOnly Property TotalSeconds As Long
        Sub Dispose()

        'User Implemented
        'To create an mp3 video type, inherit BaseMp3Video, implment IMp3Video, 
        'constructor with mp3 file path passed to mybase, implement the below
        ReadOnly Property Name As String
        ReadOnly Property Description As String
        Sub Initialize(mp3Path As String, Optional configuration As Mp3VideoConfiguration = Nothing)
        Sub SetDefaultConfiguration()
        Function GenerateAllFrames(frameDumpPath As String, isInstantQueue As Boolean) As String
        Function GetNextFrame() As Image
        Function GetInstantQueueFrame() As Image
    End Interface
End Namespace

