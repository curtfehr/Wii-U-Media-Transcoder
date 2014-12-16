<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingRange
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.ttDescription = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblCurrent = New System.Windows.Forms.Label()
        Me.tbSetting = New System.Windows.Forms.TrackBar()
        Me.lblRangeEnd = New System.Windows.Forms.Label()
        Me.lblRangeStart = New System.Windows.Forms.Label()
        CType(Me.tbSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(98, 17)
        Me.lblTitle.TabIndex = 11
        Me.lblTitle.Text = "Range Setting"
        '
        'ttDescription
        '
        Me.ttDescription.AutoPopDelay = 20000
        Me.ttDescription.InitialDelay = 500
        Me.ttDescription.ReshowDelay = 100
        '
        'lblCurrent
        '
        Me.lblCurrent.Location = New System.Drawing.Point(242, 8)
        Me.lblCurrent.Name = "lblCurrent"
        Me.lblCurrent.Size = New System.Drawing.Size(102, 17)
        Me.lblCurrent.TabIndex = 13
        Me.lblCurrent.Text = "#"
        Me.lblCurrent.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbSetting
        '
        Me.tbSetting.Location = New System.Drawing.Point(6, 28)
        Me.tbSetting.Name = "tbSetting"
        Me.tbSetting.Size = New System.Drawing.Size(346, 56)
        Me.tbSetting.TabIndex = 14
        '
        'lblRangeEnd
        '
        Me.lblRangeEnd.Location = New System.Drawing.Point(202, 67)
        Me.lblRangeEnd.Name = "lblRangeEnd"
        Me.lblRangeEnd.Size = New System.Drawing.Size(142, 17)
        Me.lblRangeEnd.TabIndex = 15
        Me.lblRangeEnd.Text = "#"
        Me.lblRangeEnd.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRangeStart
        '
        Me.lblRangeStart.Location = New System.Drawing.Point(12, 67)
        Me.lblRangeStart.Name = "lblRangeStart"
        Me.lblRangeStart.Size = New System.Drawing.Size(142, 17)
        Me.lblRangeStart.TabIndex = 16
        Me.lblRangeStart.Text = "#"
        '
        'SettingRange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblRangeStart)
        Me.Controls.Add(Me.lblRangeEnd)
        Me.Controls.Add(Me.tbSetting)
        Me.Controls.Add(Me.lblCurrent)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "SettingRange"
        Me.Size = New System.Drawing.Size(355, 90)
        CType(Me.tbSetting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents ttDescription As System.Windows.Forms.ToolTip
    Friend WithEvents lblCurrent As System.Windows.Forms.Label
    Friend WithEvents tbSetting As System.Windows.Forms.TrackBar
    Friend WithEvents lblRangeEnd As System.Windows.Forms.Label
    Friend WithEvents lblRangeStart As System.Windows.Forms.Label

End Class
