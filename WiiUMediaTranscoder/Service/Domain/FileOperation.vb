Namespace Domain
    Public Class FileOperation
        Public Property FileOperationId As Integer

        Public Property Source As String
        Public Property Destination As String
        Public Property Operation As OperationType
        Public Property Tries As Integer

        Public Enum OperationType
            Move
            Delete
            DeleteFolder
            MarkComplete
        End Enum

        Public Overrides Function ToString() As String
            Return String.Format("Source: {1}{0}Destination: {2}{0}Operation: {3}", vbNewLine, Source, If(Destination Is Nothing, "NULL", Destination), Operation)
        End Function
    End Class
End Namespace

