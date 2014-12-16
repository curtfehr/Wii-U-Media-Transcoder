Imports Common
Imports Common.Mp3Video

Namespace Domain
    Public Class ServiceState
        Public Property ServiceStateId As Integer

        Public Property InstantQueueOn As Boolean
        Public Property NormalQueueOn As Boolean

        'the below are serialized and must be deserialized in service to be consumed
        Public Overridable Property Directories As Collections.Generic.List(Of ServiceWatchDirectory)
        Public Property Mp3VideoConfiguration As String

    End Class
End Namespace

