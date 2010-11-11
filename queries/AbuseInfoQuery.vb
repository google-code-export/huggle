Namespace Huggle.Actions

    'Retrieve abuse log entries

    Class AbuseInfoQuery : Inherits Query

        Private _Filter As AbuseFilter
        Private _Limit As Integer
        Private _Page As Page
        Private _Target As User

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("abuseinfo-desc"))
        End Sub

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("abuseinfo-descp", page))
            _Page = page
        End Sub

        Public Sub New(ByVal session As Session, ByVal target As User)
            MyBase.New(session, Msg("abuseinfo-descu", target))
            _Target = User
        End Sub

        Public Sub New(ByVal session As Session, ByVal filter As AbuseFilter)
            MyBase.new(session, Msg("abuseinfo-descf", filter))
            _Target = User
        End Sub

        Public ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Public Property Limit() As Integer
            Get
                Return _Limit
            End Get
            Set(ByVal value As Integer)
                _Limit = value
            End Set
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Target() As User
            Get
                Return _Target
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("abuseinfo-progress"))
            OnStarted()

            Dim query As New QueryString( _
                "action", "query", _
                "list", "abuselog", _
                "afllimit", "max", _
                "afltitle", Page, _
                "afluser", User, _
                "aflfilter", Filter, _
                "aflprop", "ids|filter|user|ip|title|action|result|timestamp")

            Dim req As New ApiRequest(Session, Description, query)
            req.Limit = Limit
            req.DoMultiple()
            If req.IsFailed Then OnFail(req.Result)

            OnSuccess()
        End Sub

    End Class

End Namespace
