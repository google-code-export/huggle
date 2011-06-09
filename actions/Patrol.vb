Namespace Huggle.Queries

    'Patrol a revision or page

    Class Patrol : Inherits Query

        Private Page As Page
        Private Rev As Revision

        Public Sub New(ByVal session As Session, ByVal rev As Revision)
            MyBase.New(session, Msg("query-patrol-desc"))
            Me.Rev = rev
            Me.Page = rev.Page
            Me.Watch = Watch
        End Sub

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("query-patrol-desc"))
            Me.Page = page
            Me.Watch = watch
        End Sub

        Public Property Watch As WatchAction

        Public Overrides Sub Start()
            OnProgress(Msg("patrol-progress", Page))
            OnStarted()

            If Not Session.User.HasRight("patrol") Then OnFail(Msg("patrol-notallowed")) : Return

            If Not Rev.IsReviewable Then OnFail(Msg("patrol-notfound")) : Return
            If Rev.IsReviewed Then OnFail(Msg("patrol-alreadydone")) : Return

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            Dim rcid As Integer = 0

            If Rev Is Nothing OrElse Rev.Rcid = 0 Then

                'Check whether revision has been patrolled
                Dim checkReq As New ApiRequest(Session, Description, New QueryString(
                    "action", "query",
                    "list", "logevents",
                    "letitle", Page,
                    "letype", "patrol"))

                checkReq.Start()
                If checkReq.IsErrored Then OnFail(checkReq.Result) : Return

                If (Rev Is Nothing AndAlso Page.IsReviewed) OrElse (Rev IsNot Nothing AndAlso Rev.IsReviewed) _
                    Then Cancel() : Return

                'Get rcid needed to patrol the page
                'Can be found through API, but if it is not there, the query takes several minutes
                'on en.wikipedia to go through the whole table.
                'So load the rendered page and look for patrol link instead.

                'Dim rcidReq As New UIRequest(Session, Description, Nothing, Nothing)
                'rcidReq.Start()
                'If rcidReq.IsErrored Then OnFail(rcidReq.Result) : Return
            End If

            'If the page is too old for us to find the rcid, we can't patrol it
            If rcid = 0 Then
                If Rev IsNot Nothing Then Rev.IsReviewable = False
                OnFail(Msg("patrol-notfound")) : Return
            End If

            'Patrol the page
            Dim req As New ApiRequest(Session, Description, New QueryString(
                "action", "patrol",
                "rcid", rcid,
                "token", Session.Tokens("patrol")))

            req.Start()
            If req.Result.IsError Then OnFail(req.Result) : Return

            'Watch page
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, Page, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
