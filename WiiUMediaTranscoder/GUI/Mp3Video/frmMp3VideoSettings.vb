Imports Common.Mp3Video
Imports Common.Mp3Video.Type
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

Public Class frmMp3VideoSettings
    Const PreviewWidth As Integer = 372
    Const PreviewHeight As Integer = 210

    Private _video As IMp3Video
    Private _serviceController As ServiceController

    Private _sampleAudioSource As String

    Public Sub New(serviceController As ServiceController)
        ' This call is required by the designer.
        InitializeComponent()

        _serviceController = serviceController

        pnlCustomSettings.Padding = New Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0)
        _sampleAudioSource = Path.GetDirectoryName(Application.ExecutablePath) & "\lib\sample.mp3"

        Dim mp3VideoTypes As New Dictionary(Of String, String)
        mp3VideoTypes.Add("", "Please Select")
        mp3VideoTypes.Add("Common.Mp3Video.Type.PulseVideo", "Pulse Video")

        ddlMp3Video.DisplayMember = "Value"
        ddlMp3Video.ValueMember = "Key"
        ddlMp3Video.DataSource = New BindingSource(mp3VideoTypes, Nothing)

        Dim videoProfiles As New Dictionary(Of String, VideoProfile)
        videoProfiles.Add("Custom", Nothing)
        videoProfiles.Add("1080p - FAST RENDER", New VideoProfile() With {.Width = 1920, .Height = 1080, .Fps = 10})
        videoProfiles.Add("1080p - NTSC", New VideoProfile() With {.Width = 1920, .Height = 1080, .Fps = 24})
        videoProfiles.Add("1080p - PAL", New VideoProfile() With {.Width = 1920, .Height = 1080, .Fps = 30})
        videoProfiles.Add("720p - FAST RENDER", New VideoProfile() With {.Width = 1280, .Height = 720, .Fps = 10})
        videoProfiles.Add("720p - SUPER QUALITY", New VideoProfile() With {.Width = 1280, .Height = 720, .Fps = 45})
        videoProfiles.Add("720p - NTSC", New VideoProfile() With {.Width = 1280, .Height = 720, .Fps = 24})
        videoProfiles.Add("720p - PAL", New VideoProfile() With {.Width = 1280, .Height = 720, .Fps = 30})
        videoProfiles.Add("480p Wide - FAST RENDER", New VideoProfile() With {.Width = 854, .Height = 480, .Fps = 10})
        videoProfiles.Add("480p Wide - SUPER QUALITY", New VideoProfile() With {.Width = 854, .Height = 480, .Fps = 60})
        videoProfiles.Add("480p Wide - NTSC", New VideoProfile() With {.Width = 854, .Height = 480, .Fps = 24})
        videoProfiles.Add("480p Wide - PAL", New VideoProfile() With {.Width = 854, .Height = 480, .Fps = 30})

        ddlVideoSizeProfile.DisplayMember = "Key"
        ddlVideoSizeProfile.ValueMember = "Value"
        ddlVideoSizeProfile.DataSource = New BindingSource(videoProfiles, Nothing)
        ddlVideoSizeProfile.SelectedIndex = 1

        Dim mp3Config = _serviceController.Proxy.GetMp3VideoConfiguration()
        If mp3Config IsNot Nothing Then
            ddlVideoSizeProfile.SelectedIndex = 0
            tbFramesPerSecond.Value = mp3Config.Fps
            txtHeight.Value = mp3Config.Height
            txtWidth.Value = mp3Config.Width

            _video = Activator.CreateInstance(mp3Config.Type)
            _video.Initialize(_sampleAudioSource, mp3Config)

            If mp3VideoTypes.ContainsKey(mp3Config.Type.FullName) Then
                ddlMp3Video.Tag = "skip"
                ddlMp3Video.SelectedValue = mp3Config.Type.FullName
            End If

            SetupVideo()
        End If
    End Sub

    Private Sub ddlMp3Video_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMp3Video.SelectedIndexChanged
        If ddlMp3Video.Tag = "skip" Then
            ddlMp3Video.Tag = ""
            Exit Sub
        End If
        pnlCustomSettings.Controls.Clear()
        lblDescription.Text = ""

        If ddlMp3Video.SelectedValue <> "" Then
            Dim videoAssembly = Assembly.Load("Common")
            _video = Activator.CreateInstance(videoAssembly.GetType(ddlMp3Video.SelectedValue))
        Else
            tmrPreview.Enabled = False
            imgPreview.Image = Nothing
            If _video IsNot Nothing Then _video.Dispose()
            _video = Nothing
        End If

        If _video IsNot Nothing Then
            _video.Initialize(_sampleAudioSource)

            _video.Fps = tbFramesPerSecond.Value

            SetupVideo()
        End If
    End Sub

    Private Sub SetupVideo()
        'hard coded preview window size
        _video.Width = PreviewWidth
        _video.Height = PreviewHeight

        lblDescription.Text = _video.Description

        tmrPreview.Interval = 1000 / _video.Fps

        If _video.Settings IsNot Nothing Then
            For Each setting As Mp3VideoSetting In _video.Settings
                Dim settingControl As ISetting
                Select Case setting.Type
                    Case Mp3VideoSetting.SettingType.Color
                        settingControl = New SettingColor(setting)
                    Case Mp3VideoSetting.SettingType.Number
                        settingControl = New SettingNumber(setting)
                    Case Mp3VideoSetting.SettingType.PresetValues
                        settingControl = New SettingPresetValues(setting)
                    Case Mp3VideoSetting.SettingType.Range
                        settingControl = New SettingRange(setting)
                    Case Mp3VideoSetting.SettingType.TrueFalse
                        settingControl = New SettingTrueFalse(setting)
                    Case Else
                        settingControl = New SettingText(setting)
                End Select

                pnlCustomSettings.Controls.Add(settingControl)
            Next
        End If

        tmrPreview.Enabled = True
    End Sub

    Private Sub tmrPreview_Tick(sender As Object, e As EventArgs) Handles tmrPreview.Tick
        If _video IsNot Nothing Then
            imgPreview.Image = _video.GetNextFrame()
        Else
            tmrPreview.Enabled = False
            imgPreview.Image = Nothing
        End If
    End Sub

    Private Sub ddlVideoSizeProfile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVideoSizeProfile.SelectedIndexChanged
        If ddlVideoSizeProfile.SelectedIndex > 0 Then
            Dim profile As VideoProfile = CType(ddlVideoSizeProfile.SelectedValue, VideoProfile)

            txtWidth.Value = profile.Width
            txtHeight.Value = profile.Height
            tbFramesPerSecond.Value = profile.Fps
        End If
    End Sub

    Private Sub tbFramesPerSecond_ValueChanged(sender As Object, e As EventArgs) Handles tbFramesPerSecond.ValueChanged
        lblFramesPerSecond.Text = tbFramesPerSecond.Value

        If tbFramesPerSecond.Value = 0 Then
            tmrPreview.Interval = 100
        Else
            tmrPreview.Interval = 1000 / tbFramesPerSecond.Value
        End If

        If _video IsNot Nothing Then
            _video.Fps = tbFramesPerSecond.Value
        End If
        If tbFramesPerSecond.Focused Then ddlVideoSizeProfile.SelectedIndex = 0
        CheckIfVideoSettingsTooHighAndWarnUser()
    End Sub

    Private Sub txtWidth_ValueChanged(sender As Object, e As EventArgs) Handles txtWidth.ValueChanged
        If txtWidth.Focused Then ddlVideoSizeProfile.SelectedIndex = 0
        If txtWidth.Value Mod 2 > 0 Then txtWidth.Value += 1
        CheckIfVideoSettingsTooHighAndWarnUser()
    End Sub

    Private Sub txtHeight_ValueChanged(sender As Object, e As EventArgs) Handles txtHeight.ValueChanged
        If txtHeight.Focused Then ddlVideoSizeProfile.SelectedIndex = 0
        If txtHeight.Value Mod 2 > 0 Then txtHeight.Value += 1
        CheckIfVideoSettingsTooHighAndWarnUser()
    End Sub

    Private Sub CheckIfVideoSettingsTooHighAndWarnUser()
        Dim qualityWarning As String = "Warning:" & vbNewLine & "The Wii U cannot sustain playback at 1080p above 40fps."
        Dim zeroFpsWarning As String = "Warning:" & vbNewLine & "The normal queue will not run at 0 FPS even if enabled."

        If txtWidth.Value = 1920 AndAlso txtHeight.Value = 1080 AndAlso tbFramesPerSecond.Value > 40 Then
            lblQualityWarning.Text = qualityWarning
            lblQualityWarning.Visible = True
        ElseIf tbFramesPerSecond.Value = 0 Then
            lblQualityWarning.Text = zeroFpsWarning
            lblQualityWarning.Visible = True
        Else
            lblQualityWarning.Visible = False
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If MsgBox("Are you sure you want to save your changes?", MsgBoxStyle.YesNo, "Save Changes") = MsgBoxResult.No Then Exit Sub

        If _video IsNot Nothing Then
            Dim config As Mp3VideoConfiguration = _video.Configuration
            config.Width = txtWidth.Value
            config.Height = txtHeight.Value

            _serviceController.Proxy.SetMp3VideoConfiguration(config)
            _video.Dispose()
        Else
            _serviceController.Proxy.SetMp3VideoConfiguration(Nothing)
        End If

        Close()
    End Sub

    Private Function UTF8ByteArrayToString(bytes As Byte()) As String
        Return New UTF8Encoding().GetString(bytes)
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If _video IsNot Nothing Then
            _video.Dispose()
        End If

        Close()
    End Sub

    Class VideoProfile
        Public Property Width As Integer
        Public Property Height As Integer
        Public Property Fps As Integer
    End Class

End Class