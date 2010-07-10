Namespace Huggle.Actions

    'Get details of an abuse log item
    'TODO: Update this to use the API if information is ever made available that way

    Class AbuseDetailQuery : Inherits Query

        Private _Abuse As Abuse

        Public Sub New(ByVal session As Session, ByVal abuse As Abuse)
            MyBase.New(session, Msg("abusedetail-desc", abuse))
            _Abuse = abuse
        End Sub

        Public ReadOnly Property Abuse() As Abuse
            Get
                Return _Abuse
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("abusedetail-progress", Abuse))
            OnStarted()

            Dim query As New QueryString( _
                "title", "Special:AbuseLog", _
                "details", Abuse)

            Dim req As New UIRequest(Session, Description, query, Nothing)
            req.Start()
            If req.IsFailed Then OnFail(req.Result)

            'Extract diff
            If Not (req.Response.Contains("<table class='diff'>") AndAlso req.Response.Contains("</table>")) _
                Then OnFail(Msg("error-unknown")) : Return
            Abuse.Diff = req.Response.FromFirst("<table class='diff'>", True).ToFirst("</table>", True)

            OnSuccess()
        End Sub

    End Class

End Namespace
