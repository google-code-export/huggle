'Imports System.Collections.Generic

'Namespace Huggle.Queries

'    'Review a revision using "Flagged revisions" extension

'    Class ReviewQuery : Inherits OldQuery

'        Private Comment As String, Rev As Revision, Levels As Dictionary(Of ReviewFlag, Integer), Watch As Boolean

'        Public Sub New(ByVal Account As User, ByVal Rev As Revision, ByVal Levels As Dictionary(Of ReviewFlag, Integer), _
'            ByVal Comment As String, ByVal Watch As Boolean)

'            MyBase.New(Account)
'            Me.Comment = Comment
'            Me.Rev = Rev
'            Me.Levels = Levels
'            Me.Watch = Watch
'        End Sub

'        Protected Overrides Function Process() As Result

'            Dim FailMsg As String = Msg("review-fail", Rev.Page)

'            DoProgress(Msg("review-progress", Rev.Page), "review-" & Rev.Page.Name)

'            'Get token
'            If User.Token Is Nothing Then
'                Result = (New TokenQuery(User)).Do
'                If Result.IsError Then Return Result.FailWith(FailMsg)
'            End If

'            'Create query string
'            Dim Qs As New QueryString( _
'                "action", "review", _
'                "revid", Rev, _
'                "comment", Comment, _
'                "token", User.Token)

'            For Each Item As KeyValuePair(Of ReviewFlag, Integer) In Levels
'                Qs.Add("flag_" & Item.Key.Name, Item.Value)
'            Next Item

'            'Submit the review
'            Result = DoApiRequest(Qs)
'            If Result.IsError Then Return Result.FailWith(FailMsg)

'            'Watch the page
'            If Watch AndAlso Not Rev.Page.IsWatchedBy(User) Then Result = (New WatchQuery(User, Rev.Page)).Do

'            Return Result.Success
'        End Function

'    End Class

'End Namespace
