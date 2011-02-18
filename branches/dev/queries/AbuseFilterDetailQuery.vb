﻿Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Retrieve details of an abuse filter
    'This includes information that is not currently available through the MediaWiki API

    Friend Class AbuseFilterDetailQuery : Inherits Query

        Private _Filter As AbuseFilter

        Public Sub New(ByVal session As Session, ByVal filter As AbuseFilter)
            MyBase.New(session, Msg("abusefilterdetail-desc", filter))
            _Filter = filter
        End Sub

        Public ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Public Overrides Sub Start()
            OnStarted()

            Dim detailReq As New UIRequest(Session, Description, New QueryString("title", "Special:AbuseFilter/" &
                If(Filter.Id = 0, "new", Filter.Id.ToStringI)), Nothing)

            Dim historyReq As New Lists.LogsQuery(Session)
            historyReq.SetOption("logtype", "abusefilter")

            App.DoParallel({detailReq, historyReq})

            If detailReq.IsFailed Then OnFail(detailReq.Result) : Return
            If historyReq.IsFailed Then OnFail(historyReq.Result) : Return

            Dim notes As String = GetHtmlTextFromName(detailReq.Response, "wpFilterNotes")
            Dim pattern As String = GetHtmlTextFromName(detailReq.Response, "wpFilterRules")
            Dim warning As String = GetHtmlAttributeFromName(detailReq.Response, "wpFilterWarnMessageOther", "value")

            If notes IsNot Nothing Then Filter.Notes = notes
            If pattern IsNot Nothing Then Filter.Pattern = pattern
            If warning IsNot Nothing Then Filter.WarningMessage = warning

            If GetHtmlAttributeFromName(detailReq.Response, "wpFilterActionThrottle", "checked") IsNot Nothing Then
                Dim rateLimitCount As String = GetHtmlAttributeFromName(detailReq.Response, "wpFilterThrottleCount", "value")
                Dim rateLimitPeriod As String = GetHtmlAttributeFromName(detailReq.Response, "wpFilterThrottlePeriod", "value")
                Dim rateLimitGroups As String = GetHtmlAttributeFromName(detailReq.Response, "wpFilterThrottleGroups", "value")

                If rateLimitCount IsNot Nothing Then Filter.RateLimit = New RateLimit(
                    Nothing, Nothing, If(rateLimitGroups Is Nothing, Nothing, rateLimitGroups.Split(LF).ToList),
                    CInt(rateLimitCount), New TimeSpan(0, 0, CInt(rateLimitPeriod)))
            Else
                Filter.RateLimit = RateLimit.None
            End If

            Dim tagText As String = GetHtmlTextFromName(detailReq.Response, "wpFilterTags")
            Dim tags As New List(Of String)
            If tagText IsNot Nothing Then tags.AddRange(tagText.Split(LF))
            Filter.Tags = tags

            Dim allowedActions As New List(Of String)

            For Each action As String In
                {"blockautopromote", "block", "degroup", "disallow", "tag", "throttle", "warn", "rangeblock"}

                If detailReq.Response.Contains("wpFilterAction" & UcFirst(action)) Then allowedActions.Merge(action)
            Next action

            Wiki.Config.AbuseFilterActions = allowedActions

            OnSuccess()
        End Sub

    End Class

End Namespace