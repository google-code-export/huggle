Namespace Huggle.Actions

    'Edit an abuse filter
    'TODO: Convert this to use the API if it ever supports editing abuse filters

    Public Class EditAbuseFilter : Inherits Query

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
            Dim query As New QueryString("title", "Special:AbuseFilter/" & If(Filter Is Nothing, "new", CStr(Filter.Id)))

            Dim data As New QueryString( _
                "wpFilterActionBlockautopromote", Filter.Actions.Contains("block autopromote"), _
                "wpFilterActionBlock", Filter.Actions.Contains("block"), _
                "wpFilterActionDegroup", Filter.Actions.Contains("degroup"), _
                "wpFilterActionDisallow", Filter.Actions.Contains("disallow"), _
                "wpFilterActionRangeblock", Filter.Actions.Contains("rangeblock"), _
                "wpFilterActionTag", Filter.Actions.Contains("tag"), _
                "wpFilterActionThrottle", Filter.Actions.Contains("throttle"), _
                "wpFilterActionWarn", Filter.Actions.Contains("warn"), _
                "wpFilterDeleted", Filter.IsDeleted, _
                "wpFilterDescription", Filter.Description, _
                "wpFilterEnabled", Filter.IsEnabled, _
                "wpFilterHidden", Filter.IsPrivate, _
                "wpFilterNotes", Filter.Notes, _
                "wpFilterRules", Filter.Pattern, _
                "wpFilterTags", Filter.Tags.Join(LF), _
                "wpFilterThrottleCount", Filter.RateLimitCount, _
                "wpFilterThrottlePeriod", CInt(Filter.RateLimitPeriod.TotalSeconds), _
                "wpFilterThrottleGroups", Filter.RateLimitGroups.Join(LF), _
                "wpFilterWarnMessage", Filter.WarningMessage, _
                "wpFilterWarnMessageOther", Filter.WarningMessage, _
                "wpToken", Session.EditToken)

            Dim req As New UIRequest(Session, Description, query, data)
            req.Start()
            If req.Result.IsError Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace