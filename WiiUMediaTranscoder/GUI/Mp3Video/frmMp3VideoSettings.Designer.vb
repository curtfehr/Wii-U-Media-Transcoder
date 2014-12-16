<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMp3VideoSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.ddlMp3Video = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.imgPreview = New System.Windows.Forms.PictureBox()
        Me.ddlVideoSizeProfile = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbFramesPerSecond = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblFramesPerSecond = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.NumericUpDown()
        Me.txtHeight = New System.Windows.Forms.NumericUpDown()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlCustomSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.tmrPreview = New System.Windows.Forms.Timer(Me.components)
        Me.lblQualityWarning = New System.Windows.Forms.Label()
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFramesPerSecond, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ddlMp3Video
        '
        Me.ddlMp3Video.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlMp3Video.FormattingEnabled = True
        Me.ddlMp3Video.Location = New System.Drawing.Point(12, 36)
        Me.ddlMp3Video.Name = "ddlMp3Video"
        Me.ddlMp3Video.Size = New System.Drawing.Size(358, 24)
        Me.ddlMp3Video.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Animation Type"
        '
        'imgPreview
        '
        Me.imgPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.imgPreview.Location = New System.Drawing.Point(410, 13)
        Me.imgPreview.Name = "imgPreview"
        Me.imgPreview.Size = New System.Drawing.Size(372, 210)
        Me.imgPreview.TabIndex = 2
        Me.imgPreview.TabStop = False
        '
        'ddlVideoSizeProfile
        '
        Me.ddlVideoSizeProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlVideoSizeProfile.FormattingEnabled = True
        Me.ddlVideoSizeProfile.Location = New System.Drawing.Point(410, 251)
        Me.ddlVideoSizeProfile.Name = "ddlVideoSizeProfile"
        Me.ddlVideoSizeProfile.Size = New System.Drawing.Size(372, 24)
        Me.ddlVideoSizeProfile.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(410, 226)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Video Size Profile"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(410, 282)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Width"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(552, 282)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Height"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(410, 332)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 17)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Frames Per Second"
        '
        'tbFramesPerSecond
        '
        Me.tbFramesPerSecond.Location = New System.Drawing.Point(410, 352)
        Me.tbFramesPerSecond.Maximum = 60
        Me.tbFramesPerSecond.Name = "tbFramesPerSecond"
        Me.tbFramesPerSecond.Size = New System.Drawing.Size(372, 56)
        Me.tbFramesPerSecond.TabIndex = 10
        Me.tbFramesPerSecond.Value = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(410, 429)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Description"
        '
        'lblDescription
        '
        Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDescription.Location = New System.Drawing.Point(413, 450)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(369, 119)
        Me.lblDescription.TabIndex = 12
        '
        'lblFramesPerSecond
        '
        Me.lblFramesPerSecond.Location = New System.Drawing.Point(682, 332)
        Me.lblFramesPerSecond.Name = "lblFramesPerSecond"
        Me.lblFramesPerSecond.Size = New System.Drawing.Size(100, 17)
        Me.lblFramesPerSecond.TabIndex = 13
        Me.lblFramesPerSecond.Text = "10"
        Me.lblFramesPerSecond.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtWidth
        '
        Me.txtWidth.Increment = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtWidth.Location = New System.Drawing.Point(413, 302)
        Me.txtWidth.Maximum = New Decimal(New Integer() {1920, 0, 0, 0})
        Me.txtWidth.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(120, 22)
        Me.txtWidth.TabIndex = 14
        Me.txtWidth.Value = New Decimal(New Integer() {1920, 0, 0, 0})
        '
        'txtHeight
        '
        Me.txtHeight.Increment = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtHeight.Location = New System.Drawing.Point(555, 302)
        Me.txtHeight.Maximum = New Decimal(New Integer() {1080, 0, 0, 0})
        Me.txtHeight.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(120, 22)
        Me.txtHeight.TabIndex = 15
        Me.txtHeight.Value = New Decimal(New Integer() {1080, 0, 0, 0})
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(707, 578)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(626, 578)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlCustomSettings
        '
        Me.pnlCustomSettings.AutoScroll = True
        Me.pnlCustomSettings.AutoSize = True
        Me.pnlCustomSettings.ColumnCount = 1
        Me.pnlCustomSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.pnlCustomSettings.Location = New System.Drawing.Point(12, 77)
        Me.pnlCustomSettings.MaximumSize = New System.Drawing.Size(392, 521)
        Me.pnlCustomSettings.Name = "pnlCustomSettings"
        Me.pnlCustomSettings.RowCount = 1
        Me.pnlCustomSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.pnlCustomSettings.Size = New System.Drawing.Size(392, 521)
        Me.pnlCustomSettings.TabIndex = 18
        '
        'tmrPreview
        '
        Me.tmrPreview.Interval = 83
        '
        'lblQualityWarning
        '
        Me.lblQualityWarning.AutoSize = True
        Me.lblQualityWarning.ForeColor = System.Drawing.Color.Red
        Me.lblQualityWarning.Location = New System.Drawing.Point(410, 384)
        Me.lblQualityWarning.Name = "lblQualityWarning"
        Me.lblQualityWarning.Size = New System.Drawing.Size(372, 34)
        Me.lblQualityWarning.TabIndex = 19
        Me.lblQualityWarning.Text = "Warning: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The Wii U cannot sustain playback at 1080p above 40fps."
        Me.lblQualityWarning.Visible = False
        '
        'frmMp3VideoSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 610)
        Me.Controls.Add(Me.lblQualityWarning)
        Me.Controls.Add(Me.pnlCustomSettings)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtHeight)
        Me.Controls.Add(Me.txtWidth)
        Me.Controls.Add(Me.lblFramesPerSecond)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbFramesPerSecond)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ddlVideoSizeProfile)
        Me.Controls.Add(Me.imgPreview)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ddlMp3Video)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(769, 655)
        Me.Name = "frmMp3VideoSettings"
        Me.Text = "Mp3 Video Settings"
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFramesPerSecond,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtWidth,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtHeight,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ddlMp3Video As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents imgPreview As System.Windows.Forms.PictureBox
    Friend WithEvents ddlVideoSizeProfile As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbFramesPerSecond As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblFramesPerSecond As System.Windows.Forms.Label
    Friend WithEvents txtWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlCustomSettings As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tmrPreview As System.Windows.Forms.Timer
    Friend WithEvents lblQualityWarning As System.Windows.Forms.Label
End Class
