Imports System
Imports System.Collections.Generic

Namespace Huggle

    'Represents a filter from MediaWiki's AbuseFilter extension

    <Diagnostics.DebuggerDisplay("{Id}")>
    Friend Class AbuseFilter

        Private _Hits As List(Of Abuse)
        Private ReadOnly _Id As Integer
        Private ReadOnly _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal id As Integer)
            _IsEnabled = True
            _Id = id
            _TotalHits = -1
            _Wiki = wiki
        End Sub

        Public Property Actions() As New List(Of String)

        Public Property Description() As String

        Public ReadOnly Property Hits() As List(Of Abuse)
            Get
                If _Hits Is Nothing Then _Hits = New List(Of Abuse)
                Return _Hits
            End Get
        End Property

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public Property IsDeleted() As Boolean

        Public Property IsEnabled() As Boolean

        Public Property IsPrivate() As Boolean

        Public Property LastModified() As Date

        Public Property LastModifiedBy() As User

        Public Property LastRevision As AbuseFilterRevision

        Public Property Notes() As String

        Public ReadOnly Property Page As Page
            Get
                Return Wiki.Pages.FromNsAndName(Wiki.Spaces.Special, "AbuseFilter/" & Id.ToStringI)
            End Get
        End Property

        Public Property Pattern() As String

        Public Property RateLimit As RateLimit

        Public Property Revisions As List(Of AbuseFilterRevision)

        Public Property Tags() As List(Of String)

        Public Property TotalHits() As Integer

        Public Property WarningMessage() As String

        Public ReadOnly Property Wiki() As Wiki
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

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of AbuseFilter)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property FromID(ByVal id As Integer) As AbuseFilter
            Get
                If Not _All.ContainsKey(id) Then _All.Add(id, New AbuseFilter(Wiki, id))
                Return _All(id)
            End Get
        End Property

    End Class

End Namespace
