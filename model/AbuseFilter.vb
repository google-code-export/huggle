Imports System
Imports System.Collections.Generic

Namespace Huggle

    'Represents a filter from MediaWiki's AbuseFilter extension

    <Diagnostics.DebuggerDisplay("{Id}")>
    Friend Class AbuseFilter

        Private _Hits As List(Of Abuse)
        Private ReadOnly _Id As Integer
        Private ReadOnly _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal id As Integer)
            _IsEnabled = True
            _Id = id
            _TotalHits = -1
            _Wiki = wiki
        End Sub

        Friend Property Actions() As New List(Of String)

        Friend Property Description() As String

        Friend ReadOnly Property Hits() As List(Of Abuse)
            Get
                If _Hits Is Nothing Then _Hits = New List(Of Abuse)
                Return _Hits
            End Get
        End Property

        Friend ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Friend Property IsDeleted() As Boolean

        Friend Property IsEnabled() As Boolean

        Friend Property IsPrivate() As Boolean

        Friend Property LastModified() As Date

        Friend Property LastModifiedBy() As User

        Friend Property Notes() As String

        Friend Property Pattern() As String

        Friend Property RateLimit As RateLimit

        Friend Property Tags() As List(Of String)

        Friend Property TotalHits() As Integer

        Friend Property WarningMessage() As String

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Id.ToStringI
        End Function

    End Class

    Friend Class AbuseFilterCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, AbuseFilter)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As IList(Of AbuseFilter)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal id As Integer) As AbuseFilter
            Get
                If Not _All.ContainsKey(id) Then _All.Add(id, New AbuseFilter(Wiki, id))
                Return _All(id)
            End Get
        End Property

    End Class

End Namespace
