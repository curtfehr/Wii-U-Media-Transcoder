Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Reflection

Namespace Mp3Video
    <Serializable> _
    <DataContract>
    Public Class Mp3VideoConfiguration
        <DataMember>
        Private _xmlType As String
        <DataMember>
        Property XmlType As String
            Get
                If _type IsNot Nothing Then
                    _xmlType = _type.FullName
                End If
                Return _xmlType
            End Get
            Set(value As String)
                _xmlType = value
                Dim videoAssembly = Assembly.Load("Common")
                _type = videoAssembly.GetType(_xmlType)
            End Set
        End Property

        <IgnoreDataMember()> _
        <XmlIgnore()>
        Private _type As System.Type
        <IgnoreDataMember()> _
        <XmlIgnore()>
        Property Type As System.Type
            Get
                If _type Is Nothing Then
                    Dim videoAssembly = Assembly.Load("Common")
                    _type = videoAssembly.GetType(_xmlType)
                End If
                Return _type
            End Get
            Set(value As System.Type)
                _type = value
                _xmlType = value.FullName
            End Set
        End Property

        <DataMember>
        Property Width As Integer
        <DataMember>
        Property Height As Integer
        <DataMember>
        Property Fps As Integer

        <IgnoreDataMember()> _
        <XmlIgnore()>
        Property Settings As List(Of Mp3VideoSetting)

        'xml serialization simplified
        <DataMember>
        Property XmlSettings As Mp3VideoSetting()
            Get
                Return Settings.ToArray
            End Get
            Set(value As Mp3VideoSetting())
                Settings = New List(Of Mp3VideoSetting)
                For Each setting In value
                    Settings.Add(setting)
                Next
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("{0}XmlType:{6}{0}Type:{1}{0}Width:{2}{0}Height:{3}{0}Fps:{4}{0}Settings:{5}", vbNewLine, If(Type Is Nothing, "NULL", Type.ToString), Width, Height, Fps, If(Settings Is Nothing, 0, Settings.Count), If(_xmlType Is Nothing, "NULL", _xmlType))
        End Function

    End Class
End Namespace

