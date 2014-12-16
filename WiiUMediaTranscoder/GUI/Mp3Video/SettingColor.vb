Imports Common.Mp3Video
Imports System.Text.RegularExpressions

Public Class SettingColor
    Implements ISetting
    Private _setting As Mp3VideoSetting
    Private _currentColor As Color

    Public Sub New(setting As Mp3VideoSetting)
        InitializeComponent()

        Me.Anchor = (AnchorStyles.Top Or AnchorStyles.Left)
        _setting = setting

        lblTitle.Text = setting.Name
        ttDescription.SetToolTip(Me, setting.Description)
        For Each ctrl In Me.Controls
            ttDescription.SetToolTip(ctrl, setting.Description)
        Next

        _currentColor = Color.FromArgb(CInt(setting.Value))
        colorSetting.Color = _currentColor
        pbColorPreview.BackColor = _currentColor
    End Sub

    Public Sub SaveSetting() Implements ISetting.SaveSetting
        _setting.Value = _currentColor.ToArgb().ToString
    End Sub

    Private Sub btnSelectColor_Click(sender As Object, e As EventArgs) Handles btnSelectColor.Click
        If colorSetting.ShowDialog() = DialogResult.OK Then
            _currentColor = colorSetting.Color
            _setting.Value = _currentColor.ToArgb().ToString
            pbColorPreview.BackColor = _currentColor
        End If
    End Sub
End Class
