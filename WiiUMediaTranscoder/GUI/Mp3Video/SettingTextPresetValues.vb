Imports Common.Mp3Video
Imports System.Text.RegularExpressions

Public Class SettingPresetValues
    Implements ISetting
    Private _setting As Mp3VideoSetting

    Public Sub New(setting As Mp3VideoSetting)
        InitializeComponent()

        Me.Anchor = (AnchorStyles.Top Or AnchorStyles.Left)
        _setting = setting

        lblTitle.Text = setting.Name
        ttDescription.SetToolTip(Me, setting.Description)
        For Each ctrl In Me.Controls
            ttDescription.SetToolTip(ctrl, setting.Description)
        Next

        ddlSetting.DataSource = setting.PresetValues
        ddlSetting.SelectedValue = setting.Value
    End Sub

    Public Sub SaveSetting() Implements ISetting.SaveSetting
        _setting.Value = ddlSetting.SelectedValue
    End Sub

    Private Sub ddlSetting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSetting.SelectedIndexChanged
        _setting.Value = ddlSetting.SelectedValue
    End Sub
End Class
