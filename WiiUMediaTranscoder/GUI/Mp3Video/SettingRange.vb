Imports Common.Mp3Video
Imports System.Text.RegularExpressions

Public Class SettingRange
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

        lblRangeStart.Text = setting.RangeStart
        lblRangeEnd.Text = setting.RangeEnd
        lblCurrent.Text = setting.Value

        tbSetting.SetRange(setting.RangeStart, setting.RangeEnd)
        tbSetting.Value = CInt(setting.Value)
    End Sub

    Public Sub SaveSetting() Implements ISetting.SaveSetting
        _setting.Value = tbSetting.Value
    End Sub

    Private Sub tbSetting_Scroll(sender As Object, e As EventArgs) Handles tbSetting.Scroll
        lblCurrent.Text = tbSetting.Value
        _setting.Value = tbSetting.Value
    End Sub
End Class
