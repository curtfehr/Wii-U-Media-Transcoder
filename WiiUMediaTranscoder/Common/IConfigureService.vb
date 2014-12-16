Imports System
Imports System.ServiceModel
Imports Common.Mp3Video

<ServiceContract>
Public Interface IConfigureService
    <OperationContract>
    Function IsInstantQueueOn() As Boolean
    <OperationContract>
    Function IsNormalQueueOn() As Boolean

    <OperationContract>
    Sub StartInstantQueue()
    <OperationContract>
    Sub StartNormalQueue()

    <OperationContract>
    Sub StopInstantQueue()
    <OperationContract>
    Sub StopNormalQueue()

    <OperationContract>
    Function GetInstantQueuePending() As Integer
    <OperationContract>
    Function GetNormalQueuePending() As Integer
    <OperationContract>
    Function GetCurrentStatus() As ServiceStatus
    <OperationContract>
    Function GetCurrentProgress() As Integer

    <OperationContract>
    Function GetWatchDirectories() As List(Of WatchDirectory)
    <OperationContract>
    Sub AddWatchDirectory(directory As WatchDirectory)
    <OperationContract>
    Sub RemoveWatchDirectory(directory As WatchDirectory)

    <OperationContract>
    Function GetMp3VideoConfiguration() As Mp3VideoConfiguration
    <OperationContract>
    Sub SetMp3VideoConfiguration(configuration As Mp3VideoConfiguration)
End Interface
