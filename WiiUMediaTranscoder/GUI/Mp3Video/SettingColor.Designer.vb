<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingColor
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
        Me.colorSetting = New System.Windows.Forms.ColorDialog()
        Me.btnSelectColor = New System.Windows.Forms.Button()
        Me.pbColorPreview = New System.Windows.Forms.PictureBox()
        CType(Me.pbColorPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(89, 17)
        Me.lblTitle.TabIndex = 11
        Me.lblTitle.Text = "Color Setting"
        '
        'ttDescription
        '
        Me.ttDescription.AutoPopDelay = 20000
        Me.ttDescription.InitialDelay = 500
        Me.ttDescription.ReshowDelay = 100
        '
        'btnSelectColor
        '
        Me.btnSelectColor.Location = New System.Drawing.Point(217, 5)
        Me.btnSelectColor.Name = "btnSelectColor"
        Me.btnSelectColor.Size = New System.Drawing.Size(124, 23)
        Me.btnSelectColor.TabIndex = 12
        Me.btnSelectColor.Text = "Select Color"
        Me.btnSelectColor.UseVisualStyleBackColor = True
        '
        'pbColorPreview
        '
        Me.pbColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbColorPreview.Location = New System.Drawing.Point(15, 33)
        Me.pbColorPreview.Name = "pbColorPreview"
        Me.pbColorPreview.Size = New System.Drawing.Size(326, 25)
        Me.pbColorPreview.TabIndex = 13
        Me.pbColorPreview.TabStop = False
        '
        'SettingColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.pbColorPreview)
        Me.Controls.Add(Me.btnSelectColor)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "SettingColor"
        Me.Size = New System.Drawing.Size(355, 65)
        CType(Me.pbColorPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents ttDescription As System.Windows.Forms.ToolTip
    Friend WithEvents colorSetting As System.Windows.Forms.ColorDialog
    Friend WithEvents btnSelectColor As System.Windows.Forms.Button
    Friend WithEvents pbColorPreview As System.Windows.Forms.PictureBox

End Class
