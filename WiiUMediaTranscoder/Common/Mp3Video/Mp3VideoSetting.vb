Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Mp3Video
    <Serializable> _
    <DataContract>
    Public Class Mp3VideoSetting
        Public Enum SettingType
            Number
            TrueFalse
            Range
            Text
            PresetValues
            Color
        End Enum

        <DataMember>
        Private _name As String
        <DataMember>
        Private _description As String
        <DataMember>
        Private _type As SettingType
        <DataMember>
        Private _rangeStart As Integer
        <DataMember>
        Private _rangeEnd As Integer
        <IgnoreDataMember()> _
        <XmlIgnore()>
        Private _presetValues As List(Of String)
        <DataMember>
        Private _value As String

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Public Property Type As SettingType
            Get
                Return _type
            End Get
            Set(value As SettingType)
                _type = value
            End Set
        End Property

        Public Property RangeStart As Integer
            Get
                Return _rangeStart
            End Get
            Set(value As Integer)
                _rangeStart = value
            End Set
        End Property

        Public Property RangeEnd As Integer
            Get
                Return _rangeEnd
            End Get
            Set(value As Integer)
                _rangeEnd = value
            End Set
        End Property

        <IgnoreDataMember()> _
        <XmlIgnore()>
        Public Property PresetValues As List(Of String)
            Get
                Return _presetValues
            End Get
            Set(value As List(Of String))
                _presetValues = value
            End Set
        End Property

        <DataMember>
        Property XmlPresetValues As String()
            Get
                Return _presetValues.ToArray
            End Get
            Set(value As String())
                _presetValues = New List(Of String)
                For Each setting In value
                    _presetValues.Add(setting)
                Next
            End Set
        End Property

        Public Event ValueChanged()
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(value As String)
                If IsNewValueValid(value) Then
                    RaiseEvent ValueChanged()
                    _value = value
                Else
                    Throw New Exception("Value attempted to be set is invalid. Value:[" & value & "] on " & _name & " of type " & _type.ToString("G"))
                End If
            End Set
        End Property

        Private Function IsNewValueValid(value As String) As Boolean
            Select Case _type
                Case SettingType.Number
                    Dim regex As New Regex("^-?\d+$")
                    Return regex.IsMatch(value)
                Case SettingType.TrueFalse
                    Dim regex As New Regex("^((True)|(False))$")
                    Return regex.IsMatch(value)
                Case SettingType.PresetValues
                    Return _presetValues.Contains(value)
                Case SettingType.Range
                    Dim regex As New Regex("^-?\d+$")
                    If regex.IsMatch(value) AndAlso CInt(value) >= RangeStart AndAlso CInt(value) <= RangeEnd Then
                        Return True
                    Else
                        Return False
                    End If
                Case SettingType.Color
                    Dim regex As New Regex("^-?\d+$")
                    Try
                        If regex.IsMatch(value) Then
                            Dim color As Color = color.FromArgb(CInt(value))
                            Return True
                        End If
                    Catch ex As Exception
                    End Try
                    Return False
                Case Else
                    Return True
            End Select
        End Function

        Public Sub New()
            _presetValues = New List(Of String)
        End Sub

        Public Sub New(name As String, Optional description As String = "")
            _presetValues = New List(Of String)
            _type = SettingType.Text
            _value = ""
            _name = name
            _description = description
            CheckNameAndDescription()
        End Sub

        Public Sub New(defaultColor As Color, name As String, Optional description As String = "")
            _presetValues = New List(Of String)
            _type = SettingType.Color
            _value = defaultColor.ToArgb().ToString
            _name = name
            _description = description
            CheckNameAndDescription()
        End Sub

        Public Sub New(rangeStart As Integer, rangeEnd As Integer, name As String, Optional description As String = "")
            _presetValues = New List(Of String)
            _type = SettingType.Range
            _rangeStart = rangeStart
            _rangeEnd = rangeEnd
            _value = _rangeStart
            _name = name
            _description = description
            CheckNameAndDescription()
        End Sub

        Public Sub New(numberIsBoolean As Boolean, name As String, Optional description As String = "")
            _presetValues = New List(Of String)
            If numberIsBoolean Then
                _type = SettingType.TrueFalse
                _value = "False"
            Else
                _type = SettingType.Number
                _value = "0"
            End If
            _name = name
            _description = description
            CheckNameAndDescription()
        End Sub

        Public Sub New(presetValues As List(Of String), name As String, Optional description As String = "")
            If presetValues Is Nothing OrElse presetValues.Count = 0 Then
                Throw New Exception("Cannot create a preset value setting without any preset values defined!")
            End If
            _type = SettingType.PresetValues
            _presetValues = presetValues
            _name = name
            _description = description
            _value = _presetValues(0)
            CheckNameAndDescription()
        End Sub

        Private Sub CheckNameAndDescription()
            If String.IsNullOrEmpty(_name) Then
                Throw New Exception("Setting Name cannot be blank.")
            End If
            If _description Is Nothing Then
                _description = ""
            End If
        End Sub
    End Class
End Namespace

