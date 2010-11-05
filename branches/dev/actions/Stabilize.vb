Namespace Huggle.Actions

    Friend Class Stabilize : Inherits Query

        Private _AutoReview As Boolean
        Private _Page As Page
        Private _RestrictAutoReview As Boolean
        Private _StableFlag As StableFlag
        Private _Summary As String
        Private _Watch As Boolean

        Friend Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("stabilize-desc", page))
            _Page = page
        End Sub

        Friend Property AutoReview() As Boolean
            Get
                Return _AutoReview
            End Get
            Set(ByVal value As Boolean)
                _AutoReview = value
            End Set
        End Property

        Friend Property Page() As Page
            Get
                Return _Page
            End Get
            Set(ByVal value As Page)
                _Page = value
            End Set
        End Property

        Friend Property RestrictAutoReview() As Boolean
            Get
                Return _RestrictAutoReview
            End Get
            Set(ByVal value As Boolean)
                _RestrictAutoReview = value
            End Set
        End Property

        Friend Property StableFlag() As StableFlag
            Get
                Return _StableFlag
            End Get
            Set(ByVal value As StableFlag)
                _StableFlag = value
            End Set
        End Property

        Friend Property Summary() As String
            Get
                Return _Summary
            End Get
            Set(ByVal value As String)
                _Summary = value
            End Set
        End Property

        Friend Property Watch() As Boolean
            Get
                Return _Watch
            End Get
            Set(ByVal value As Boolean)
                _Watch = value
            End Set
        End Property

        Friend Overrides Sub Start()
            OnProgress(Msg("stabilize-progress", Page.Title))
            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            Dim query As New QueryString( _
                "action", "stabilize", _
                "title", Page, _
                "reason", Summary)

            If StableFlag = Actions.StableFlag.Latest Then query.Add("default", "latest") Else query.Add("default", "stable")

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