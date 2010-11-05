Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Retrieve details of an abuse filter
    'This includes information that is not currently available through the MediaWiki API

    Friend Class AbuseFilterDetailQuery : Inherits Query

        Private _AllowedActions As New List(Of String)
        Private _Filter As AbuseFilter

        Friend Sub New(ByVal session As Session, ByVal filter As AbuseFilter)
            MyBase.New(session, Msg("abusefilterdetail-desc", filter))
            _Filter = filter
        End Sub

        Friend ReadOnly Property AllowedActions() As List(Of String)
            Get
                Return _AllowedActions
            End Get
        End Property

        Friend ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Friend Overrides Sub Start()
            OnStarted()

            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:AbuseFilter/" &
                If(Filter.Id = 0, "new", Filter.Id.ToStringI)), Nothing)

            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            Dim notes As String = GetHtmlTextFromName(req.Response, "wpFilterNotes")
            Dim pattern As String = GetHtmlTextFromName(req.Response, "wpFilterRules")
            Dim warning As String = GetHtmlAttributeFromName(req.Response, "wpFilterWarnMessageOther", "value")

            If notes IsNot Nothing Then Filter.Notes = notes
            If pattern IsNot Nothing Then Filter.Pattern = pattern
            If warning IsNot Nothing Then Filter.WarningMessage = warning

            If GetHtmlAttributeFromName(req.Response, "wpFilterActionThrottle", "checked") IsNot Nothing Then
                Dim rateLimitCount As String = GetHtmlAttributeFromName(req.Response, "wpFilterThrottleCount", "value")
                Dim rateLimitPeriod As String = GetHtmlAttributeFromName(req.Response, "wpFilterThrottlePeriod", "value")
                Dim rateLimitGroups As String = GetHtmlAttributeFromName(req.Response, "wpFilterThrottleGroups", "value")

                If rateLimitCount IsNot Nothing Then Filter.RateLimit = New RateLimit(
                    Nothing, Nothing, If(rateLimitGroups Is Nothing, Nothing, rateLimitGroups.Split(LF).ToList),
                    CInt(rateLimitCount), New TimeSpan(0, 0, CInt(rateLimitPeriod)))
            Else
                Filter.RateLimit = RateLimit.None
            End If

            For Each item As String In
                {"blockautopromote", "block", "degroup", "disallow", "tag", "throttle", "warn", "rangeblock"}

                If req.Response.Contains("wpFilterAction" & UcFirst(item)) Then AllowedActions.Merge(item)
            Next item

            OnSuccess()
        End Sub

    End Class

End Namespace
