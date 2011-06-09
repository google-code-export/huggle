Imports Huggle.Queries
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Restore a page

    Class Undelete : Inherits Query

        Private _Page As Page
        Private _Summary As String

        Public Sub New(ByVal session As Session, ByVal page As Page, ByVal summary As String)

            MyBase.New(session, Msg("undelete-desc", page))

            ThrowNull(page, "page")
            ThrowNull(summary, "summary")
            _Page = page
            _Summary = summary
        End Sub

        Public ReadOnly Property Page As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Summary As String
            Get
                Return _Summary
            End Get
        End Property

        Public Property Timestamps As List(Of Date)

        Public Property Watch As WatchAction

        Public Overrides Sub Start()
            OnProgress(Msg("undelete-progress", Page))
            OnStarted()

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString(
                "action", "undelete",
                "title", Page.Name,
                "reason", Summary,
                "token", Session.Tokens("undelete"))

            If Timestamps IsNot Nothing Then query.Add("timestamps", Timestamps.ToStringArray.Join("|"))

            'Restore the page
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Watch user's userpage
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, Page, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
