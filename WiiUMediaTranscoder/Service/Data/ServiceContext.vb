Imports System.Data.Entity
Imports Service.Domain

Namespace Data
    Public Class ServiceContext
        Inherits DbContext

        Public Property State As DbSet(Of ServiceState)
        Public Property Queue As DbSet(Of ServiceQueue)
        Public Property WatchDirectory As DbSet(Of ServiceWatchDirectory)
        Public Property FileOperation As DbSet(Of FileOperation)

    End Class
End Namespace

