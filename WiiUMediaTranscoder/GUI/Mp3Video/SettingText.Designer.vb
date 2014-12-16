<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingText
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
        Me.txtSetting = New System.Windows.Forms.TextBox()
        Me.ttDescription = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(83, 17)
        Me.lblTitle.TabIndex = 11
        Me.lblTitle.Text = "Text Setting"
        '
        'txtSetting
        '
        Me.txtSetting.Location = New System.Drawing.Point(15, 28)
        Me.txtSetting.Name = "txtSetting"
        Me.txtSetting.Size = New System.Drawing.Size(327, 22)
        Me.txtSetting.TabIndex = 12
        '
        'ttDescription
        '
        Me.ttDescription.AutoPopDelay = 20000
        Me.ttDescription.InitialDelay = 500
        Me.ttDescription.ReshowDelay = 100
        '
        'SettingText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.txtSetting)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "SettingText"
        Me.Size = New System.Drawing.Size(355, 62)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtSetting As System.Windows.Forms.TextBox
    Friend WithEvents ttDescription As System.Windows.Forms.ToolTip

End Class
