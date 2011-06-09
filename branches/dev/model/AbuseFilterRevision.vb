Imports System
Imports System.Collections.Generic

Namespace Huggle

    'Represents a revision to an abuse filter

    <Diagnostics.DebuggerDisplay("{AbuseFilterRevId}")>
    Friend Class AbuseFilterRevision : Inherits LogItem

        Private _AbuseFilterRevId As Integer

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property AbuseFilterRevId() As Integer
            Get
                Return _AbuseFilterRevId
            End Get
            Set(ByVal value As Integer)
                _AbuseFilterRevId = value
                Wiki.AbuseFilterRevisions.SetAbfRevID(Me)
            End Set
        End Property

        Public Property Filter As AbuseFilter

        Public Property Prev As AbuseFilterRevision

        Public Property [Next] As AbuseFilterRevision

        Public Overrides ReadOnly Property Target As String
            Get
                If Filter Is Nothing Then Return Nothing
                Return Msg("log-abusefilter", Filter.Id)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Id.ToStringI
        End Function

    End Class

    Friend Class AbuseFilterRevisionCollection

        Private Wiki As Wiki

        Private ReadOnly _ByLogID As New Dictionary(Of Integer, AbuseFilterRevision)
        Private ReadOnly _ByAbfRevID As New Dictionary(Of Integer, AbuseFilterRevision)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of AbuseFilterRevision)
            Get
                Return _ByLogID.Values.ToList.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property FromAbfRevID(ByVal id As Integer) As AbuseFilterRevision
            Get
                If Not _ByAbfRevID.ContainsKey(id) Then Return Nothing
                Return _ByAbfRevID(id)
            End Get
        End Property

        Default Public ReadOnly Property FromLogID(ByVal id As Integer) As AbuseFilterRevision
            Get
                If Not _ByLogID.ContainsKey(id) Then _ByLogID.Add(id, New AbuseFilterRevision(id, Wiki))
                Return _ByLogID(id)
            End Get
        End Property

        Public Sub SetAbfRevID(ByVal rev As AbuseFilterRevision)
            _ByAbfRevID.Merge(rev.AbuseFilterRevId, rev)
        End Sub

    End Class

End Namespace
