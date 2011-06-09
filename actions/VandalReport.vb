'Imports Huggle.Queries
'Imports Huggle.Wikitext
'Imports System
'Imports System.Collections.Generic
'Imports System.Text.RegularExpressions

'Namespace Huggle.Queries

'    Friend Class VandalReport : Inherits Query

'        Private Reason As String
'        Private Target As User
'        Private Type As String

'        Public Sub New(ByVal session As Session, ByVal user As User, ByVal type As String)
'            MyBase.New(session, Msg("vandalreport-desc"))
'            Me.Target = user
'            Me.Type = type
'        End Sub

'        Protected Overrides ReadOnly Property FailMessage() As String
'            Get
'                Return Msg("report-fail", User)
'            End Get
'        End Property

'        Public Overrides Sub Start()
'            CreateThread(AddressOf GetDetails, AddressOf GetConfirmation)
'        End Sub

'        Private Sub GetDetails()
'            'Fetch details if necessary
'            If Not User.ExtendedInfoKnown Then
'                Dim Query As New UserDetailQuery(User, User)
'                Query.Start()

'                If Query.Result.IsError Then
'                    OnFail(Query.Result.Message)
'                    Return
'                End If
'            End If

'            'Check whether user is blocked
'            If User.IsBlocked Then OnFail(Msg("report-alreadyblocked"))
'        End Sub

'        Private Sub GetConfirmation()
'            If IsCancelled Then Return

'            Dim ConfirmMessages As New List(Of String)
'            Dim ConfirmOptions As New List(Of String)
'            ConfirmOptions.Add(Msg("report-continue"))

'            'Reporting a user who does not have a final warning
'            If Not User.Sanction.IsFinal AndAlso User.Config.ConfirmReportUnwarned Then
'                ConfirmMessages.Add(Msg("report-unwarned"))
'            End If

'            'Reporting an ignored user
'            If User.IsIgnored AndAlso User.Config.ConfirmReportIgnored Then
'                ConfirmMessages.Add(Msg("report-ignored"))
'                ConfirmOptions.Remove(Msg("report-continue"))
'                ConfirmOptions.Add(Msg("report-unignore"))
'            End If

'            If ConfirmMessages.Count > 0 AndAlso Interactive Then
'                ConfirmOptions.Add(Msg("cancel"))

'                'Show prompt to user
'                Select Case Prompt.Show(Msg("block-action"), MakeConfirmation(ConfirmMessages), _
'                    Nothing, 1, Msg("block-continue"), Msg("cancel"))

'                    Case 1 : Exit Select
'                    Case 2 : OnFail(Msg("error-cancelled")) : Return
'                End Select
'            End If

'            CreateThread(AddressOf DoReport)
'        End Sub

'        Private Sub DoReport()
'            OnStarted()

'            If Wiki.Config.VandalReportFormat Is Nothing Then FailUndefined("vandal-report-format") : Return
'            If Wiki.Config.VandalReportPage Is Nothing Then FailUndefined("vandal-report-page") : Return
'            If Wiki.Config.VandalReportSummary Is Nothing Then FailUndefined("vandal-report-summary") : Return

'            If Reason Is Nothing Then Reason = Wiki.Config.VandalReportReason

'            Dim Vandalism As New List(Of Revision)

'            For Each rev As Revision In User.Edits
'                If rev.IsReverted Then Vandalism.Add(rev)
'            Next rev

'            'Get text of report page(s)
'            Dim TextQuery As New RevisionDetailQuery(User, Wiki.Config.ReportPages, True)
'            TextQuery.Start()

'            If TextQuery.Result.IsError Then
'                OnFail(TextQuery.Result.Message)
'                Return
'            End If

'            Dim document As New Document(Wiki.Config.VandalReportPage)

'            'Check for existing reports
'            For Each Pattern As Regex In Wiki.Config.RevertPatterns
'                For Each Item As Match In Pattern.Matches(document.Text)
'                    Dim User As User = Wiki.Users.FromString(Item.Groups(1).Value)

'                    If User IsNot Nothing Then
'                        If Not User.IsReported Then
'                            User.Sanctions.Add(New Sanction(CDate(Item.Groups(2).Value), User, _
'                                Wiki.Users.FromString(Item.Groups(3).Value), New SanctionType("report", Nothing, 0), Nothing))
'                        End If

'                        If User Is Me.User Then
'                            OnFail(Msg("report-alreadyreported"))
'                            Return
'                        End If
'                    End If
'                Next Item
'            Next Pattern

'            Dim Report As String = CRLF, SingleNote As String = ""

'            'Check warnings
'            Dim RecentWarnings As Integer = 0

'            For Each Item As Sanction In User.Sanctions
'                If Item.Time.Add(Wiki.Config.WarningAge) > Wiki.ServerTime Then RecentWarnings += 1
'            Next Item

'            If RecentWarnings = 0 Then
'                OnFail(Msg("report-notwarned"))
'                Return
'            End If

'            If RecentWarnings = 1 AndAlso Wiki.Config.VandalReportSingleNote IsNot Nothing _
'                Then SingleNote = " – " & Wiki.Config.VandalReportSingleNote

'            'Create report
'            document.Text &= Wiki.Config.VandalReportFormat.FormatWith(document.UserLink(User), Reason, SingleNote, LinkDiffs())

'            Dim edit As New Edit(Session, document.Page, document.Text, Wiki.Config.VandalReportSummary)
'            edit.Conflict = ConflictAction.Retry
'            edit.Minor = Wiki.Config.IsMinor("report")

'            Do
'                edit.Start()
'            Loop Until Not edit.ConflictRetry

'            If edit.IsFailed Then OnFail(edit.Result) : Return

'            OnSuccess()
'        End Sub

'        'Link to instances of vandalism from this user
'        Private Function LinkDiffs() As String

'            If Wiki.Config.VandalReportDiffs = 0 Then Return Nothing

'            Dim linkRevs As New List(Of Revision)

'            'Revisions by this user that have been warned for
'            For Each sanction As Sanction In User.Sanctions
'                If sanction.IsWarning AndAlso sanction.Regarding IsNot Nothing Then linkRevs.Merge(sanction.Regarding)
'            Next sanction

'            'Any other revisions by this user that we know have been reverted
'            For Each rev As Revision In User.Edits
'                If rev.IsReverted Then linkRevs.Merge(rev)
'            Next rev

'            If linkRevs.Count = 0 Then Return Nothing

'            Dim result As String = "<span class=""plainlinks"">"
'            Dim resultCount As Integer = Math.Min(linkRevs.Count, 6)

'            For i As Integer = 0 To resultCount - 1
'                result &= "[" & WikiUrl(User.Wiki, linkRevs(i).Page.Title, "diff", CStr(linkRevs(i).Id)).ToString & " " & CStr(i + 1) & "]"
'                If i < resultCount - 1 Then result &= ", "
'            Next i


'            Return result & "</span>"
'        End Function

'    End Class

'End Namespace
