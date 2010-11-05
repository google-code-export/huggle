'Imports System.Collections.Generic

'Namespace Huggle.Queries.Actions

'    'Restore a page

'    Class RestoreQuery : Inherits OldQuery

'        Private Page As Page, Comment As String, Watch As Boolean, Timestamps As List(Of Date)

'        Friend Sub New(ByVal Account As User, ByVal Page As Page, ByVal Comment As String, ByVal Watch As Boolean, _
'            Optional ByVal Timestamps As List(Of Date) = Nothing)

'            MyBase.New(Account)
'            Me.Comment = Comment
'            Me.Page = Page
'            Me.Timestamps = Timestamps
'            Me.Watch = Watch
'        End Sub

'        Protected Overrides Function Process() As Result
'            Dim FailMsg As String = Msg("restore-fail", Page)

'            DoProgress(Msg("restore-progress", Page), Page)

'            'Get token
'            If User.Token Is Nothing Then
'                Result = (New TokenQuery(User)).Do
'                If Result.IsError Then Return Result.FailWith(FailMsg)
'            End If

'            'Create query string
'            Dim Qs As New QueryString( _
'                "action", "undelete", _
'                "title", Page.Name, _
'                "reason", Comment, _
'                "token", User.Token)

'            If Timestamps IsNot Nothing Then Qs.Add("timestamps", Timestamps.ToStringArray.Join("|"))

'            'Restore the page
'            Result = DoApiRequest(Qs)
'            If Result.IsError Then Return Result.FailWith(FailMsg)

'            'Watch the page
'            If Watch AndAlso Not Page.IsWatchedBy(User) Then Result = New WatchQuery(User, Page).Do

'            Return Result.Success
'        End Function

'    End Class

'End Namespace
