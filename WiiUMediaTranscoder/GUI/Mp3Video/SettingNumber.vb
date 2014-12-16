Imports Common.Mp3Video
Imports System.Text.RegularExpressions

Public Class SettingNumber
    Implements ISetting
    Private _setting As Mp3VideoSetting
    Private _lastSetting As String

    Public Sub New(setting As Mp3VideoSetting)
        InitializeComponent()

        Me.Anchor = (AnchorStyles.Top Or AnchorStyles.Left)
        _setting = setting

        lblTitle.Text = setting.Name
        ttDescription.SetToolTip(Me, setting.Description)
        For Each ctrl In Me.Controls
            ttDescription.SetToolTip(ctrl, setting.Description)
        Next

        txtSetting.Text = setting.Value
        _lastSetting = setting.Value
    End Sub

    Public Sub SaveSetting() Implements ISetting.SaveSetting
        _setting.Value = txtSetting.Text
    End Sub

    Private Sub txtSetting_TextChanged(sender As Object, e As EventArgs) Handles txtSetting.TextChanged
        Dim regex As New Regex("^-?\d*$")
        If Not regex.IsMatch(txtSetting.Text) Then
            txtSetting.Text = _lastSetting
        End If
        _setting.Value = txtSetting.Text
        _lastSetting = txtSetting.Text
    End Sub
End Class
