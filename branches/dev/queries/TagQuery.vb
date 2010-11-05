'Imports System.Collections.Generic

'Namespace Huggle.Requests.Editing

'    Class TagRequest : Inherits Request

'        'Tag a page

'        Private Page As Page, Tag As Tag, Parameters As New Dictionary(Of String, String)
'        Private Summary, AvoidText As String
'        Private Minor, Watch, Patrol, Append As Boolean, NotifyRequest As Request

'        Friend Sub New(ByVal Page As Page, ByVal Tag As Tag, ByVal Summary As String, _
'            Optional ByVal AvoidText As String = Nothing, Optional ByVal Minor As Boolean = False, _
'            Optional ByVal Watch As Boolean = False, Optional ByVal Patrol As Boolean = False, _
'            Optional ByVal Append As Boolean = False, Optional ByVal NotifyRequest As Request = Nothing)

'            Me.Append = Append
'            Me.AvoidText = AvoidText
'            Me.Minor = Minor
'            Me.NotifyRequest = NotifyRequest
'            Me.Page = Page
'            Me.Patrol = Patrol
'            Me.Summary = Summary
'            Me.Tag = Tag
'            Me.Watch = Watch
'        End Sub

'        Protected Overrides Function Process() As Request
'            Dim FailMsg As String = Msg("tag-fail", Page.Name)

'            LogProgress(Msg("tag-progress", Page.Name))

'            Dim Result As New RevisionRequest(Page)
'            Result.Start()
'            If Result.IsError Then Return Failed(FailMsg, Result.ErrorMessage)

'            Dim Text As String = Result.Edit.Text

'            If Text Is Nothing Then Return Failed(FailMsg, Msg("edit-nopage"))
'            If AvoidText IsNot Nothing AndAlso Text.Contains(AvoidText) _
'                Then Return Failed(FailMsg, Msg("tag-alreadytagged"))

'            'If Tag.Replace Then
'            '    Text = Tag.Build(Parameters)
'            'ElseIf Append Then
'            '    Text = Text & LF & Tag.Build(Parameters)
'            'Else
'            '    Text = Tag.Build(Parameters) & LF & Text
'            'End If

'            Dim EditResult As New EditRequest(Page, Text, Summary, Minor:=Minor, Watch:=Watch, AllowCreate:=False)
'            EditResult.Start()
'            If Result.IsError Then Return Failed(FailMsg, Result.ErrorMessage)

'            If Patrol Then
'                Dim NewRequest As New Actions.PatrolRequest(Page, Watch)
'                NewRequest.Start()
'            End If

'            If NotifyRequest IsNot Nothing Then NotifyRequest.Start()

'            Return Success()
'        End Function

'    End Class

'End Namespace