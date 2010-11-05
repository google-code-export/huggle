﻿Namespace Huggle.Actions

    'Patrol a revision or page

    Class PatrolQuery : Inherits Query

        Private Page As Page
        Private Rev As Revision

        Friend Sub New(ByVal session As Session, ByVal rev As Revision)
            MyBase.New(session, Msg("query-patrol-desc"))
            Me.Rev = rev
            Me.Page = rev.Page
            Me.Watch = Watch
        End Sub

        Friend Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("query-patrol-desc"))
            Me.Page = page
            Me.Watch = watch
        End Sub

        Friend Property Watch As WatchAction

        Friend Overrides Sub Start()
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

            OnSuccess()
        End Sub

    End Class

End Namespace
