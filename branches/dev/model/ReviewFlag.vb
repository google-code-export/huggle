Imports System.Collections.Generic

Namespace Huggle

    'Represents a review flag as used by "flagged revisions"

    Public Class ReviewFlag

        Private _DefaultLevel As Integer
        Private _DisplayName As String
        Private _PristineLevel As Integer
        Private _QualityLevel As Integer
        Private _Name As String
        Private _Levels As Integer
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
        End Sub

        Public Property DefaultLevel() As Integer
            Get
                Return _DefaultLevel
            End Get
            Set(ByVal value As Integer)
                _DefaultLevel = value
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return _DisplayName
            End Get
            Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property LevelName(ByVal level As Integer) As String
            Get
                Return Wiki.Message("revreview-" & Name & "-" & CStr(level))
            End Get
        End Property

        Public Property Levels() As Integer
            Get
                Return _Levels
            End Get
            Set(ByVal value As Integer)
                _Levels = value
            End Set
        End Property

        Public Property PristineLevel() As Integer
            Get
                Return _PristineLevel
            End Get
            Set(ByVal value As Integer)
                _PristineLevel = value
            End Set
        End Property

        Public Property QualityLevel() As Integer
            Get
                Return _QualityLevel
            End Get
            Set(ByVal value As Integer)
                _QualityLevel = value
            End Set
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Public Class ReviewFlagCollection

        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of String, ReviewFlag)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of ReviewFlag)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public Sub Clear()
            _All.Clear()
        End Sub

        Default Public ReadOnly Property Item(ByVal name As String) As ReviewFlag
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New ReviewFlag(Wiki, name))
                Return _All(name)
            End Get
        End Property

    End Class

End Namespace
