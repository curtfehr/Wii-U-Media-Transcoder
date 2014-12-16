Imports Common.Mp3Video

Public Class SettingTrueFalse
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

        cbSetting.Checked = CBool(setting.Value)
    End Sub

    Public Sub SaveSetting() Implements ISetting.SaveSetting
        _setting.Value = cbSetting.Checked.ToString
    End Sub

    Private Sub cbSetting_CheckedChanged(sender As Object, e As EventArgs) Handles cbSetting.CheckedChanged
        _setting.Value = cbSetting.Checked.ToString
    End Sub
End Class
