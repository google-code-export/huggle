Namespace Huggle.Queries

    'Patrol a revision or page

    Class PatrolQuery : Inherits OldQuery

        Private Rev As Revision, Page As Page, Watch As Boolean

        Public Sub New(ByVal Account As User, ByVal Rev As Revision, ByVal Watch As Boolean)
            MyBase.New(Account)
            Me.Rev = Rev
            Me.Page = Rev.Page
            Me.Watch = Watch
        End Sub

        Public Sub New(ByVal Account As User, ByVal Page As Page, ByVal Watch As Boolean)
            MyBase.New(Account)
            Me.Page = Page
            Me.Watch = Watch
        End Sub

        Protected Overrides Function Process() As Result
            'Dim FailMsg As String = Msg("patrol-fail", Page)

            'DoProgress(Msg("patrol-progress", Page))

            'If Not Account.HasRight("patrol") _
            '    Then Return Result.Fail("notallowed", Msg("patrol-notallowed")).FailWith(FailMsg)

            'If Not Rev.IsReviewable Then Return Result.Fail("notfound", Msg("patrol-notfound")).FailWith(FailMsg)
            'If Rev.IsReviewed Then Return Result.Cancel(Msg("patrol-alreadydone")).FailWith(FailMsg)

            ''Get token
            'If Account.Token Is Nothing Then
            '    Result = (New TokenQuery).Do
            '    If Result.IsError Then Return Result.FailWith(FailMsg)
            'End If

            'Dim Rcid As Integer = 0

            'If Rev Is Nothing OrElse Rev.Rcid = 0 Then

            '    'Check whether revision has been patrolled
            '    Result = DoApiRequest(New QueryString( _
            '        "action", "query", _
            '        "list", "logevents", _
            '        "letitle", Page, _
            '        "letype", "patrol"))

            '    If Result.IsError Then Return Result.FailWith(FailMsg)

            '    If (Rev Is Nothing AndAlso Page.IsReviewed) OrElse (Rev IsNot Nothing AndAlso Rev.IsReviewed) _
            '        Then Return Result.Cancel(Msg("patrol-alreadydone")).FailWith(FailMsg)

            '    'Get rcid needed to patrol the page
            '    'Can be found through API, but if it is not there, the query takes several minutes
            '    'on en.wikipedia to go through the whole table.
            '    'So load the rendered page and look for patrol link instead.

            '    Result = (New HtmlQuery(Page.LastEdit)).Do
            '    If Result.IsError Then Return Result.FailWith(FailMsg)
            'End If

            ''If the page is too old for us to find the rcid, we can't patrol it
            'If Rcid = 0 Then
            '    If Rev IsNot Nothing Then Rev.IsReviewable = False
            '    Return Result.Fail("notfound", Msg("patrol-notfound")).FailWith(FailMsg)
            'End If

            ''Patrol the page
            'Result = DoApiRequest(New QueryString( _
            '    "action", "patrol", _
            '    "rcid", Rcid, _
            '    "token", Account.Token))

            'If Result.IsError Then Return Result.FailWith(FailMsg)

            ''Watch the page
            'If Watch AndAlso Not Account.Watchlist.Contains(Page.Title) Then Result = New WatchQuery(Page).Do

            'DoProgress(Msg("patrol-done", Page))

            Return Result.Success
        End Function

    End Class

End Namespace
