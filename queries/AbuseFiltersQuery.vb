Namespace Huggle.Actions

    'Get a list of abuse filters

    Class AbuseFiltersQuery : Inherits Query

        Private _Limit As Integer

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("abusefilters-desc"))
        End Sub

        Public Property Limit() As Integer
            Get
                Return _Limit
            End Get
            Set(ByVal value As Integer)
                _Limit = value
            End Set
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("abusefilters-progress"))
            OnStarted()

            Dim query As New QueryString( _
                "action", "query", _
                "list", "abusefilters", _
                "abflimit", "max", _
                "abfprop", "id|description|pattern|actions|hits|lasteditor|lastedittime|status|private")

            Dim req As New ApiRequest(Session, Description, query)
            req.Limit = Limit
            req.DoMultiple()
            If req.IsFailed Then OnFail(req.Result)

            OnSuccess()
        End Sub

    End Class

End Namespace
