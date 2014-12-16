
''' <summary>
'''     Summary description for ProjectInstaller.
''' </summary>
<System.ComponentModel.RunInstaller(True)> _
Public Class ProjectInstaller
    Inherits System.Configuration.Install.Installer
    ''' <summary>
    '''    Required designer variable.
    ''' </summary>
    'private System.ComponentModel.Container components;
    Private serviceInstaller As System.ServiceProcess.ServiceInstaller
    Private serviceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller

    Public Sub New()
        ' This call is required by the Designer.
        InitializeComponent()
    End Sub

    ''' <summary>
    '''    Required method for Designer support - do not modify
    '''    the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.serviceInstaller = New System.ServiceProcess.ServiceInstaller()
        Me.serviceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        ' 
        ' serviceInstaller
        ' 
        Me.serviceInstaller.Description = "Watches Wii U Media directories and converts all audio and soon also video files to h.264 aac mp4, which is the only format readable by the Wii U"
        Me.serviceInstaller.DisplayName = "Wii U Media Converter"
        Me.serviceInstaller.ServiceName = "WiiUMediaTranscodeService"
        Me.serviceInstaller.StartType = ServiceProcess.ServiceStartMode.Automatic
        ' 
        ' serviceProcessInstaller
        ' 
        Me.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.serviceProcessInstaller.Password = Nothing
        Me.serviceProcessInstaller.Username = Nothing
        ' 
        ' ServiceInstaller
        ' 
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.serviceProcessInstaller, Me.serviceInstaller})

    End Sub
End Class
