<Serializable>
Public Class WatchDirectory
    Public Property Path As String
    Public Property Recursive As Boolean
    Public Property DeleteOriginal As Boolean

    Public Overrides Function ToString() As String
        Return If(Recursive, "All Subfolders, ", "Just this folder, ") & _
                If(DeleteOriginal, "Delete Original Files, ", "Keep Original Files, ") & _
                Path
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Try
            Dim comparison As WatchDirectory = CType(obj, WatchDirectory)
            Return comparison.Path = Path AndAlso comparison.Recursive = Recursive AndAlso comparison.DeleteOriginal = DeleteOriginal
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
