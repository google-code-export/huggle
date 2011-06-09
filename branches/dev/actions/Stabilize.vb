Namespace Huggle.Queries

    Friend Class Stabilize : Inherits Query

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("stabilize-desc", page))
            _Page = page
        End Sub

        Public Property AutoReview As Boolean

        Public Property Page As Page

        Public Property RestrictAutoReview As Boolean

        Public Property StableFlag As StableFlag

        Public Property Summary As String

        Public Property Watch As Boolean

        Public Overrides Sub Start()
            OnProgress(Msg("stabilize-progress", Page.Title))
            OnStarted()

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            Dim query As New QueryString(
                "action", "stabilize",
                "title", Page,
                "token", Session.Tokens("stabilize"),
                "reason", Summary)

            If StableFlag = StableFlag.Latest Then query.Add("default", "latest") Else query.Add("default", "stable")

            Select Case StableFlag
                Case StableFlag.LatestStable : query.Add("precedence", "latest")
                Case StableFlag.LatestPristine : query.Add("precedence", "pristine")
                Case StableFlag.LatestQuality : query.Add("precedence", "quality")
            End Select

            If AutoReview Then query.Add("review", 1)
            If RestrictAutoReview Then query.Add("autoreview", "sysop")
            If Watch Then query.Add("watch", 1)

            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.Result.IsError Then OnFail(req.Result.Message) : Return

            OnSuccess()
        End Sub

    End Class

    Friend Enum StableFlag As Integer
        : Latest : LatestStable : LatestQuality : LatestPristine
    End Enum

End Namespace