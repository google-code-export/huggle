Namespace Huggle.Actions

    'Edit an abuse filter
    'TODO: Convert this to use the API if it ever supports editing abuse filters

    Friend Class EditAbuseFilter : Inherits Query

        Private _Filter As AbuseFilter

        Public Sub New(ByVal session As Session, ByVal filter As AbuseFilter)
            MyBase.New(session, Msg("editabusefilter-desc"))
            _Filter = filter
        End Sub

        Public ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Public Overrides Sub Start()
            If Filter Is Nothing Then OnProgress(Msg("editabusefilter-progressnew")) _
                Else OnProgress(Msg("editabusefilter-progress", Filter.Id))
            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result.Wrap(Msg("error-notoken"))) : Return
            End If

            'Not possible with API, must use UI instead
            Dim query As New QueryString(
                "title", "Special:AbuseFilter/" & If(Filter Is Nothing, "new", Filter.Id.ToStringI))

            Dim data As New QueryString(
                "wpFilterActionBlockautopromote", Filter.Actions.Contains("block autopromote"),
                "wpFilterActionBlock", Filter.Actions.Contains("block"),
                "wpFilterActionDegroup", Filter.Actions.Contains("degroup"),
                "wpFilterActionDisallow", Filter.Actions.Contains("disallow"),
                "wpFilterActionRangeblock", Filter.Actions.Contains("rangeblock"),
                "wpFilterActionTag", Filter.Actions.Contains("tag"),
                "wpFilterActionThrottle", Filter.Actions.Contains("throttle"),
                "wpFilterActionWarn", Filter.Actions.Contains("warn"),
                "wpFilterDeleted", Filter.IsDeleted,
                "wpFilterDescription", Filter.Description,
                "wpFilterEnabled", Filter.IsEnabled,
                "wpFilterHidden", Filter.IsPrivate,
                "wpFilterNotes", Filter.Notes,
                "wpFilterRules", Filter.Pattern,
                "wpFilterTags", Filter.Tags.Join(LF),
                "wpFilterThrottleCount", Filter.RateLimit.Count,
                "wpFilterThrottlePeriod", CInt(Filter.RateLimit.Time.TotalSeconds),
                "wpFilterThrottleGroups", Filter.RateLimit.Groups.Join(LF),
                "wpFilterWarnMessage", Filter.WarningMessage,
                "wpFilterWarnMessageOther", Filter.WarningMessage,
                "wpToken", Session.EditToken)

            Dim req As New UIRequest(Session, Description, query, data)
            req.Start()
            If req.Result.IsError Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace