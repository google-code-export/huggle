Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Retrieve details of an abuse filter
    'This includes information that is not currently available through the MediaWiki API

    Public Class AbuseFilterDetail : Inherits Query

        Private _AllowedActions As New List(Of String)
        Private _Filter As AbuseFilter

        Public Sub New(ByVal session As Session, ByVal filter As AbuseFilter)
            MyBase.New(session, Msg("abusefilterdetail-desc", filter))
            _Filter = filter
        End Sub

        Public ReadOnly Property AllowedActions() As List(Of String)
            Get
                Return _AllowedActions
            End Get
        End Property

        Public ReadOnly Property Filter() As AbuseFilter
            Get
                Return _Filter
            End Get
        End Property

        Public Overrides Sub Start()
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

                If rateLimitCount IsNot Nothing Then Filter.RateLimitCount = CInt(rateLimitCount)
                If rateLimitGroups IsNot Nothing Then Filter.RateLimitGroups = List(rateLimitGroups.Split(LF))
                If rateLimitPeriod IsNot Nothing Then Filter.RateLimitPeriod = New TimeSpan(0, 0, CInt(rateLimitPeriod))
            End If
            
            For Each item As String In
                {"blockautopromote", "block", "degroup", "disallow", "tag", "throttle", "warn", "rangeblock"}

                If req.Response.Contains("wpFilterAction" & UcFirst(item)) Then AllowedActions.Merge(item)
            Next item

            OnSuccess()
        End Sub

    End Class

End Namespace
