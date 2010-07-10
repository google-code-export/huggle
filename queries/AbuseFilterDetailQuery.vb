Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Retrieve details of an abuse filter
    'This includes information that is not currently available through the MediaWiki API

    Public Class AbuseFilterDetail : Inherits Query

        Private _AllowedActions As New List(Of String)
        Private _Filter As AbuseFilter

        Public Sub New(ByVal wiki As Wiki, ByVal filter As AbuseFilter)
            MyBase.New(App.Sessions.GetUserWithRight(wiki, "abusefilter-view"), Msg("abusefilterdetail-desc", filter))
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
            If Session Is Nothing Then OnFail(Msg("error-rights", "abusefilter-view")) : Return

            OnStarted()

            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:AbuseFilter/" & _
                If(Filter.Id = 0, "new", CStr(Filter.Id))), Nothing)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            Dim notes As String = GetHtmlInputValue(req.Response, "wpFilterNotes")
            Dim pattern As String = GetHtmlInputValue(req.Response, "wpFilterRules")
            Dim rateLimitCount As String = GetHtmlInputValue(req.Response, "wpFilterThrottleCount")
            Dim rateLimitPeriod As String = GetHtmlInputValue(req.Response, "wpFilterThrottlePeriod")
            Dim rateLimitGroups As String = GetHtmlInputValue(req.Response, "wpFilterThrottleGroups")
            Dim warning As String = GetHtmlInputValue(req.Response, "wpFilterWarnMessageOther")

            If notes IsNot Nothing Then Filter.Notes = notes
            If pattern IsNot Nothing Then Filter.Pattern = pattern
            If rateLimitCount IsNot Nothing Then Filter.RateLimitCount = CInt(rateLimitCount)
            If rateLimitGroups IsNot Nothing Then Filter.RateLimitGroups = List(rateLimitGroups.Split(LF))
            If rateLimitPeriod IsNot Nothing Then Filter.RateLimitPeriod = New TimeSpan(0, 0, CInt(rateLimitPeriod))
            If warning IsNot Nothing Then Filter.WarningMessage = warning

            For Each item As String In List( _
                "blockautopromote", "block", "degroup", "disallow", "tag", "throttle", "warn", "rangeblock")

                If req.Response.Contains("wpFilterAction" & UcFirst(item)) Then AllowedActions.Add(item)
            Next item

            OnSuccess()
        End Sub

    End Class

End Namespace
