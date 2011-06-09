Namespace Huggle.Queries

    'Get a list of abuse filters

    Class AbuseFiltersQuery : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("abusefilters-desc"))
        End Sub

        Public Property Limit() As Integer

        Public Overrides Sub Start()
            OnProgress(Msg("abusefilters-progress"))
            OnStarted()

            Dim query As New QueryString(
                "action", "query",
                "list", "abusefilters",
                "abflimit", "max",
                "abfprop", "id|description|pattern|actions|hits|lasteditor|lastedittime|status|private")

            Dim req As New ApiRequest(Session, Description, query)
            req.Limit = Limit
            req.DoMultiple()
            If req.IsFailed Then OnFail(req.Result)

            OnSuccess()
        End Sub

    End Class

End Namespace
