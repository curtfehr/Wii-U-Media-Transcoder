Imports System
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports Common

Public Class ServiceController
    Private _serviceStatus As ServiceStatus

    Public Event OnServiceStart()
    Public Event OnServiceStop()

    Private _proxy As IConfigureService
    Public ReadOnly Property Proxy As IConfigureService
        Get
            VerifyServiceController()
            Return _proxy
        End Get
    End Property

    Public Sub New()
        CreateChannel()
    End Sub

    Private Sub CreateChannel()
        Dim factory As New ChannelFactory(Of IConfigureService)(New NetNamedPipeBinding(), New EndpointAddress("net.pipe://localhost/WiiUMediaTranscoder"))
        _proxy = factory.CreateChannel()

        Try
            _proxy.IsInstantQueueOn()
        Catch ex As Exception
            _proxy = New ServiceOfflineController
        End Try

        _serviceStatus = _proxy.GetCurrentStatus()
    End Sub

    Private Sub VerifyServiceController()
        'If previous status was online, and service went offline, getcurrentstatus in the try will fail.
        'If we're offline, we'll attempt to go back online every request.
        'TODO: test for mem leaks here
        Try
            If _serviceStatus = ServiceStatus.Offline Then
                CreateChannel()
                If _proxy.GetCurrentStatus <> ServiceStatus.Offline Then
                    RaiseEvent OnServiceStart()
                End If
            End If
            _serviceStatus = _proxy.GetCurrentStatus

        Catch ex As Exception
            CreateChannel()
            If _proxy.GetCurrentStatus = ServiceStatus.Offline AndAlso _serviceStatus <> ServiceStatus.Offline Then
                RaiseEvent OnServiceStop()
            End If
            _serviceStatus = _proxy.GetCurrentStatus
        End Try
    End Sub
End Class
