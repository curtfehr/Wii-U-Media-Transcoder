<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.lstDirectories = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pbCurrent = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurrentStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tbInstantQueue = New System.Windows.Forms.TrackBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblInstantQueuePending = New System.Windows.Forms.Label()
        Me.btnAddDirectory = New System.Windows.Forms.Button()
        Me.btnRemoveDirectory = New System.Windows.Forms.Button()
        Me.btnConfigureMp3 = New System.Windows.Forms.Button()
        Me.btnConfigureVideo = New System.Windows.Forms.Button()
        Me.FolderDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblNormalQueuePending = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbNormalQueue = New System.Windows.Forms.TrackBar()
        Me.tmrServicePoller = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1.SuspendLayout()
        CType(Me.tbInstantQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNormalQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstDirectories
        '
        Me.lstDirectories.FormattingEnabled = True
        Me.lstDirectories.ItemHeight = 16
        Me.lstDirectories.Location = New System.Drawing.Point(12, 123)
        Me.lstDirectories.Name = "lstDirectories"
        Me.lstDirectories.Size = New System.Drawing.Size(449, 116)
        Me.lstDirectories.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Directories to process media:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbCurrent, Me.ToolStripStatusLabel1, Me.lblCurrentStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 331)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(475, 25)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pbCurrent
        '
        Me.pbCurrent.Name = "pbCurrent"
        Me.pbCurrent.Size = New System.Drawing.Size(150, 19)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(52, 20)
        Me.ToolStripStatusLabel1.Text = "Status:"
        '
        'lblCurrentStatus
        '
        Me.lblCurrentStatus.Name = "lblCurrentStatus"
        Me.lblCurrentStatus.Size = New System.Drawing.Size(34, 20)
        Me.lblCurrentStatus.Text = "Idle"
        '
        'tbInstantQueue
        '
        Me.tbInstantQueue.Location = New System.Drawing.Point(35, 11)
        Me.tbInstantQueue.Maximum = 1
        Me.tbInstantQueue.Name = "tbInstantQueue"
        Me.tbInstantQueue.Size = New System.Drawing.Size(86, 56)
        Me.tbInstantQueue.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(13, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "On"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(113, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Off"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(155, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Instant Queue Pending:"
        '
        'lblInstantQueuePending
        '
        Me.lblInstantQueuePending.Location = New System.Drawing.Point(334, 15)
        Me.lblInstantQueuePending.Name = "lblInstantQueuePending"
        Me.lblInstantQueuePending.Size = New System.Drawing.Size(129, 23)
        Me.lblInstantQueuePending.TabIndex = 11
        Me.lblInstantQueuePending.Text = "0"
        Me.lblInstantQueuePending.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnAddDirectory
        '
        Me.btnAddDirectory.Location = New System.Drawing.Point(388, 245)
        Me.btnAddDirectory.Name = "btnAddDirectory"
        Me.btnAddDirectory.Size = New System.Drawing.Size(75, 23)
        Me.btnAddDirectory.TabIndex = 15
        Me.btnAddDirectory.Text = "Add"
        Me.btnAddDirectory.UseVisualStyleBackColor = True
        '
        'btnRemoveDirectory
        '
        Me.btnRemoveDirectory.Location = New System.Drawing.Point(307, 245)
        Me.btnRemoveDirectory.Name = "btnRemoveDirectory"
        Me.btnRemoveDirectory.Size = New System.Drawing.Size(75, 23)
        Me.btnRemoveDirectory.TabIndex = 16
        Me.btnRemoveDirectory.Text = "Remove"
        Me.btnRemoveDirectory.UseVisualStyleBackColor = True
        '
        'btnConfigureMp3
        '
        Me.btnConfigureMp3.Location = New System.Drawing.Point(16, 288)
        Me.btnConfigureMp3.Name = "btnConfigureMp3"
        Me.btnConfigureMp3.Size = New System.Drawing.Size(236, 33)
        Me.btnConfigureMp3.TabIndex = 17
        Me.btnConfigureMp3.Text = "Configure MP3 Video Transcoding"
        Me.btnConfigureMp3.UseVisualStyleBackColor = True
        '
        'btnConfigureVideo
        '
        Me.btnConfigureVideo.Location = New System.Drawing.Point(258, 288)
        Me.btnConfigureVideo.Name = "btnConfigureVideo"
        Me.btnConfigureVideo.Size = New System.Drawing.Size(205, 33)
        Me.btnConfigureVideo.TabIndex = 18
        Me.btnConfigureVideo.Text = "Configure Video Transcoding"
        Me.btnConfigureVideo.UseVisualStyleBackColor = True
        '
        'lblNormalQueuePending
        '
        Me.lblNormalQueuePending.Location = New System.Drawing.Point(334, 59)
        Me.lblNormalQueuePending.Name = "lblNormalQueuePending"
        Me.lblNormalQueuePending.Size = New System.Drawing.Size(129, 23)
        Me.lblNormalQueuePending.TabIndex = 23
        Me.lblNormalQueuePending.Text = "0"
        Me.lblNormalQueuePending.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(155, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 17)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Normal Queue Pending:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(113, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 17)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Off"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(13, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 17)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "On"
        '
        'tbNormalQueue
        '
        Me.tbNormalQueue.Location = New System.Drawing.Point(35, 55)
        Me.tbNormalQueue.Maximum = 1
        Me.tbNormalQueue.Name = "tbNormalQueue"
        Me.tbNormalQueue.Size = New System.Drawing.Size(86, 56)
        Me.tbNormalQueue.TabIndex = 19
        '
        'tmrServicePoller
        '
        Me.tmrServicePoller.Enabled = True
        Me.tmrServicePoller.Interval = 5000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 356)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblNormalQueuePending)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbNormalQueue)
        Me.Controls.Add(Me.btnConfigureVideo)
        Me.Controls.Add(Me.btnConfigureMp3)
        Me.Controls.Add(Me.btnRemoveDirectory)
        Me.Controls.Add(Me.btnAddDirectory)
        Me.Controls.Add(Me.lblInstantQueuePending)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbInstantQueue)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lstDirectories)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(493, 401)
        Me.MinimumSize = New System.Drawing.Size(493, 401)
        Me.Name = "frmMain"
        Me.Text = "Wii U Media Transcoder Service Configuration"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.tbInstantQueue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNormalQueue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstDirectories As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbCurrent As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblCurrentStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tbInstantQueue As System.Windows.Forms.TrackBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblInstantQueuePending As System.Windows.Forms.Label
    Friend WithEvents btnAddDirectory As System.Windows.Forms.Button
    Friend WithEvents btnRemoveDirectory As System.Windows.Forms.Button
    Friend WithEvents btnConfigureMp3 As System.Windows.Forms.Button
    Friend WithEvents btnConfigureVideo As System.Windows.Forms.Button
    Friend WithEvents FolderDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblNormalQueuePending As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbNormalQueue As System.Windows.Forms.TrackBar
    Friend WithEvents tmrServicePoller As System.Windows.Forms.Timer
End Class
