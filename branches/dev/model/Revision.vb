Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Id}")> _
    Public Class Revision : Inherits QueueItem

        'Represents a page revision

        Private Random As Integer
        Private IsProcessed As Boolean
        Private TextProcessed As Boolean

        Private _ApproxTime As Date
        Private _Change As Integer
        Private _Id As Integer
        Private _InHistory As Boolean
        Private _IsReviewed As Boolean
        Private _IsSanctioned As Boolean
        Private _Length As Integer
        Private WithEvents _Page As Page
        Private _Review As Review
        Private _Tags As List(Of ChangeTag)
        Private _Text As String
        Private WithEvents _User As User
        Private _UserHidden As Boolean
        Private _Wiki As Wiki

        Private Shared ReadOnly InfoboxPattern As New Regex( _
            "\{\{ ?([Ii]nfobox|[Tt]axobox)", RegexOptions.Compiled)

        Private Shared ReadOnly TrimInterval As Integer = 60000

        Private Shared WithEvents Timer As New Windows.Forms.Timer

        Public Shared Null As New Revision(Nothing, -1)

        Public Event StateChanged As SimpleEventHandler(Of Revision)

        Public Shared Event [New] As SimpleEventHandler(Of Revision)
        Public Shared Event Processed As SimpleEventHandler(Of Revision)
        Public Shared Event Sighted As SimpleEventHandler(Of Revision)

        Shared Sub New()
            Timer.Interval = TrimInterval
            Timer.Start()
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal id As Integer)
            Random = App.Randomness.Next
            _Wiki = wiki
            _Id = id
            _IsReviewable = True
            _Change = Integer.MinValue
            _Bytes = Integer.MinValue
        End Sub

        Public Property Abuse() As Abuse

        Public Property ApproxTime() As Date
            Get
                If Time > Date.MinValue Then Return Time Else Return _ApproxTime
            End Get
            Set(ByVal value As Date)
                _ApproxTime = value
            End Set
        End Property

        Public Property Bytes() As Integer

        Public ReadOnly Property CanRollback() As Boolean
            Get
                If Page.LastRev Is Nothing Then Return False

                Dim rev As Revision = Page.LastRev

                While rev IsNot Me
                    If Not IsKnown(rev) Then Return False
                    If rev.User IsNot User Then Return False
                    rev = rev.Prev
                End While

                Return True
            End Get
        End Property

        Public ReadOnly Property CanUserRevert() As Boolean
            Get
                Return (Prev IsNot Nothing AndAlso Prev.User Is User)
            End Get
        End Property

        Public Property Change() As Integer
            Get
                If Not IsProcessed Then Process()
                Return _Change
            End Get
            Set(ByVal value As Integer)
                _Change = value
            End Set
        End Property

        Public Property DetailsKnown() As Boolean

        Public Property Exists() As TS

        Public Property Html() As String

        Public Property HtmlCacheState() As CacheState

        Public Overrides ReadOnly Property Icon() As Image
            Get
                If IsBlank Then Return Icons.Blanked
                If IsReplace Then Return Icons.Replaced
                If IsReport Then Return Icons.Report
                If IsRevert Then Return Icons.Revert

                If User IsNot Nothing Then
                    If User.IsBlocked Then Return Icons.Blocked
                    If User IsNot Nothing AndAlso User.IsWarned _
                        AndAlso User.Sanction IsNot Nothing Then Return User.Sanction.Icon
                End If

                If IsTag Then Return Icons.Tag
                If IsRedirect Then Return Icons.Redirect
                If IsAbusive Then Return Icons.Filter
                If IsCreation Then Return Icons.[New]
                If IsBot Then Return Icons.Bot

                If IsAssisted Then _
                    If User.IsIgnored Then Return Icons.IgnoredAssisted Else Return Icons.NoneAssisted

                If User IsNot Nothing Then
                    If User.IsBot Then Return Icons.Bot
                    If User.IsIgnored Then Return Icons.Ignored
                    If User.IsAbusive Then _
                        If User.IsAnonymous Then Return Icons.AnonFilter Else Return Icons.NoneFilter
                    If User.IsReverted Then Return Icons.Reverted
                    If User.IsAnonymous Then Return Icons.Anon
                End If

                Return Icons.None
            End Get
        End Property

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public ReadOnly Property InHistory() As Boolean
            Get
                If Page Is Nothing Then Return False

                Dim rev As Revision = Page.LastRev

                While IsKnown(rev)
                    rev = rev.Prev
                    If rev Is Me Then Return True
                End While

                Return False
            End Get
        End Property

        Public ReadOnly Property IsAbusive() As Boolean
            Get
                If Abuse Is Nothing Then Return False
                Return Wiki.Config.AbuseFilters.Contains(Abuse.Id)
            End Get
        End Property

        Public ReadOnly Property IsAssisted() As Boolean
            Get
                Return (Tool IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property IsBlank() As Boolean
            Get
                Return (Bytes = 0)
            End Get
        End Property

        Public Property IsBot() As Boolean

        Public Property IsCreation() As Boolean
            Get
                Return (_Prev Is Null)
            End Get
            Private Set(ByVal value As Boolean)
                If value Then _Prev = Null Else _Prev = Nothing
            End Set
        End Property

        Public Property IsDeleted() As Boolean

        Public ReadOnly Property IsHidden() As Boolean
            Get
                Return (SummaryHidden OrElse UserHidden OrElse TextHidden)
            End Get
        End Property

        Public Property IsMinor() As Boolean

        Public ReadOnly Property IsOwnUserspace() As Boolean
            Get
                Return (_Page.Owner Is _User)
            End Get
        End Property

        Public Property IsReplace() As Boolean

        Public Property IsReport() As Boolean

        Public Property IsRevert() As Boolean

        Public ReadOnly Property IsReverted() As Boolean
            Get
                Return (RevertedBy IsNot Nothing)
            End Get
        End Property

        Public Property IsReviewable() As Boolean

        Public Property IsReviewed() As Boolean
            Get
                Return _IsReviewed
            End Get
            Set(ByVal value As Boolean)
                _IsReviewed = value
                If IsReviewed Then IsReviewable = False
            End Set
        End Property

        Public Property IsRedirect() As Boolean

        Public Property IsSanction() As Boolean

        Public ReadOnly Property IsSanctioned() As Boolean
            Get
                Return (WarnedForBy IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property IsSection() As Boolean
            Get
                Return (Section IsNot Nothing)
            End Get
        End Property

        Public Property IsTag() As Boolean

        Public ReadOnly Property IsTop() As Boolean
            Get
                Return (Page IsNot Nothing AndAlso Page.LastRev Is Me)
            End Get
        End Property

        Public ReadOnly Property IsWarnedFor() As Boolean
            Get
                Return (WarnedForBy IsNot Nothing)
            End Get
        End Property

        Public Overrides ReadOnly Property Key() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public ReadOnly Property Length() As Integer
            Get
                If Text Is Nothing Then Return -1 Else Return Text.Length
            End Get
        End Property

        Public Property [Next]() As Revision

        Public Property NextByUser() As Revision

        Public Property Page() As Page
            Get
                Return _Page
            End Get
            Set(ByVal value As Page)
                _Page = value
            End Set
        End Property

        Public Property Prev() As Revision

        Public Property PrevByUser() As Revision

        Public Overrides ReadOnly Property Label() As String
            Get
                If Page Is Nothing Then Return String.Empty Else Return Page.Name
            End Get
        End Property

        Public Property Rcid() As Integer

        Public Property RevertedBy() As Revision

        Public Property RevertOf() As List(Of Revision)

        Public Property RevertTo() As Revision

        Public Property Review() As Review
            Get
                Return _Review
            End Get
            Set(ByVal value As Review)
                _Review = value
                If _Review IsNot Nothing Then _IsReviewed = True
            End Set
        End Property

        Public ReadOnly Property Reviewer() As User
            Get
                If Review IsNot Nothing Then Return Review.User Else Return Nothing
            End Get
        End Property

        Public Property Sanction() As Sanction

        Public Property WarnedForBy() As Revision

        Public ReadOnly Property Section() As String
            Get
                If _Summary Is Nothing Then Return Nothing
                If _Summary.StartsWithI("/* ") AndAlso Summary.Contains(" */") _
                    Then Return _Summary.FromFirst("/* ").ToFirst(" */") Else Return Nothing
            End Get
        End Property

        Public Property StartTime() As Date

        Public Property Summary() As String

        Public Property SummaryHidden() As Boolean

        Public ReadOnly Property Tags() As List(Of ChangeTag)
            Get
                Return _Tags
            End Get
        End Property

        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
                ProcessText(value)
                If Page IsNot Nothing AndAlso Page.Space Is Wiki.Spaces.UserTalk Then ProcessUserTalk()
            End Set
        End Property

        Public Property TextCacheState() As CacheState

        Public Property TextHidden() As Boolean

        Public Property Time() As Date

        Public Property Tool() As Tool

        Public Property User() As User
            Get
                Return _User
            End Get
            Set(ByVal value As User)
                _User = value
            End Set
        End Property

        Public Property UserHidden() As Boolean
            Get
                Return _UserHidden
            End Get
            Set(ByVal value As Boolean)
                _UserHidden = value
                If User Is Nothing Then User = Wiki.Users.Hidden
            End Set
        End Property

        Public ReadOnly Property UserRevertTarget() As Revision
            Get
                Dim Result As Revision = Me

                While IsKnown(Result) AndAlso Result.User Is User
                    Result = Result.Prev
                End While

                If Result.User Is User Then Return Nothing Else Return Result.Prev
            End Get
        End Property

        Public Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Private Sub OnStateChanged(ByVal o As Object)
            RaiseEvent StateChanged(Me, New EventArgs(Of Revision)(Me))
        End Sub

        Public Sub Process()
            'Automatic summaries
            If Summary IsNot Nothing Then
                If Summary = Wiki.Message("autosumm-blank") Then _Bytes = 0

                Dim redirMatch As Match = MessageMatch(Wiki.Message("autoredircomment"), Summary)

                If redirMatch.Success Then
                    IsRedirect = True
                    Page.Target = Wiki.Pages(redirMatch.Groups(1).Value)
                End If

                Dim replaceMatch As Match = MessageMatch(Wiki.Message("autosumm-replace"), Summary)

                If replaceMatch.Success Then
                    IsReplace = True
                    If Not replaceMatch.Groups(1).Value.EndsWithI("...") _
                        AndAlso Text Is Nothing Then Text = replaceMatch.Groups(1).Value
                End If

                Dim creationMatch As Match = MessageMatch(Wiki.Message("autosumm-new"), Summary)

                If (Prev Is Nothing OrElse IsCreation) AndAlso creationMatch.Success Then
                    IsCreation = True
                    If Not creationMatch.Groups(1).Value.EndsWithI("...") _
                        AndAlso Text Is Nothing Then Text = creationMatch.Groups(1).Value
                End If
            End If

            'Update revision properties
            If Text IsNot Nothing Then _Bytes = Encoding.UTF8.GetByteCount(Text)

            If Prev Is Null Then Page.FirstRev = Me
            If PrevByUser Is Null Then User.FirstEdit = Me
            If Prev IsNot Nothing Then Prev.Next = Me
            If PrevByUser IsNot Nothing Then PrevByUser.NextByUser = Me

            If Prev IsNot Nothing Then
                Prev.Page = Page
                If _Change > Integer.MinValue AndAlso _Bytes >= 0 Then Prev._Bytes = _Bytes - _Change
                If Prev._Bytes >= 0 AndAlso _Change > Integer.MinValue Then _Bytes = Prev._Bytes + _Change
                If Prev._Bytes >= 0 AndAlso _Bytes >= 0 Then _Change = _Bytes - Prev._Bytes
            End If

            If [Next] IsNot Nothing Then
                [Next].Page = Page
                If [Next]._Bytes >= 0 AndAlso [Next]._Change > Integer.MinValue _
                    Then _Bytes = [Next]._Bytes - [Next]._Change
            End If

            If _Bytes = -1 AndAlso Id >= Wiki.Config.PageSizeTransition Then
                If [Next]._Bytes > -1 Then _Bytes = [Next]._Bytes
                If Prev._Bytes > -1 Then _Bytes = Prev._Bytes
            End If

            If _Bytes = 0 Then Text = ""

            If User IsNot Nothing AndAlso Not User.IsWarned Then ProcessRevert()

            If Summary IsNot Nothing Then
                Dim plainSummary As String = WikiStripSummary(Summary)

                'Assisted summaries
                For Each tool As Tool In tool.All
                    If tool.Pattern IsNot Nothing AndAlso tool.Pattern.IsMatch(Summary) Then
                        Me.Tool = tool
                        Exit For
                    End If
                Next tool

                'Infer sanction from summary
                If Page.Space Is Wiki.Spaces.UserTalk AndAlso Not Page.IsSubpage _
                    AndAlso (User IsNot Page.Owner) AndAlso Not IsRevert Then

                    For Each pattern As KeyValuePair(Of Regex, SanctionType) In Wiki.Config.SanctionPatterns
                        If pattern.Key.IsMatch(plainSummary) AndAlso User.IsIgnored Then

                            Sanction = New Sanction(Time, Page.Owner, User, pattern.Value, Nothing)
                            Page.Owner.Sanctions.Add(Sanction)
                            Exit For
                        End If
                    Next pattern
                End If

                'Reports
                If Wiki.Config.ReportPages.Contains(Page) Then

                    For Each pattern As Regex In Wiki.Config.ReportPatterns
                        Dim match As Match = pattern.Match(plainSummary)

                        If match.Success Then
                            IsReport = True
                            Dim reportedUser As User = Wiki.Users.FromString(match.Groups(1).Value)

                            If reportedUser IsNot Nothing Then
                                Dim type As String = Nothing

                                If Page Is Wiki.Config.VandalReportPage Then type = "vandal"
                                If Page Is Wiki.Config.ReportUserPage Then type = "user"

                                Sanction = New Sanction(Time, reportedUser, User, New SanctionType("report", type, 0), Nothing)
                                reportedUser.Sanctions.Add(Sanction)
                            End If
                        End If
                    Next pattern
                End If

                'Tags
                If Sanction Is Nothing AndAlso Wiki.Config.TagPatterns IsNot Nothing Then
                    For Each pattern As Regex In Wiki.Config.TagPatterns
                        If pattern.IsMatch(plainSummary) Then
                            IsTag = True
                            Exit For
                        End If
                    Next pattern
                End If
            End If

            If Not IsRevert AndAlso _Change > Integer.MinValue AndAlso (-_Change / Prev._Bytes) > 0.9 Then IsReplace = True

            IsProcessed = True
            RaiseEvent Processed(Me, New EventArgs(Of Revision)(Me))
        End Sub

        Public Sub ProcessRevert()
            Dim plainSummary As String = WikiStripSummary(Summary)

            For Each pattern As Regex In Wiki.Config.RevertPatterns
                Dim match As Match = pattern.Match(plainSummary)
                If Not match.Success Then Continue For

                IsRevert = True
                Page.Reverted = True

                Dim oldId, rvdId, count As Integer

                Dim oldRev As Revision = If(Integer.TryParse(match.Groups("oldrev").Value, oldId), Wiki.Revisions(oldId), Nothing)
                Dim rvdRev As Revision = If(Integer.TryParse(match.Groups("rvdrev").Value, rvdId), Wiki.Revisions(rvdId), Nothing)
                Integer.TryParse(match.Groups("rvcount").Value, count)

                Dim oldUser As User = Wiki.Users.FromString(match.Groups("olduser").Value)
                Dim rvdUser As User = Wiki.Users.FromString(match.Groups("rvduser").Value)

                'Various ways in which the old revision of the revert
                'might be specified, in order of preference:

                If IsKnown(oldRev) Then
                    'Old revision
                    RevertTo = oldRev
                    If IsKnown(Prev) AndAlso oldRev Is Prev.Prev Then Prev.RevertedBy = Me

                ElseIf IsKnown(rvdRev) Then
                    'Reverted revision
                    rvdRev.RevertedBy = Me
                    If rvdRev Is Prev AndAlso IsKnown(rvdRev.Prev) Then RevertTo = rvdRev.Prev

                ElseIf count > 0 Then
                    'Number of reverted revisions
                    Dim rev As Revision = Me
                    Dim ok As Boolean = True

                    For i As Integer = 1 To count
                        If Not IsKnown(rev.Prev) Then ok = False Else rev = rev.Prev
                    Next i

                    If ok Then
                        RevertTo = rev
                        rev = Me

                        For i As Integer = 1 To count
                            rev = rev.Prev
                            If rev.RevertedBy Is Nothing Then rev.RevertedBy = Me
                        Next i
                    End If

                ElseIf oldUser IsNot Nothing OrElse rvdUser IsNot Nothing Then
                    'Old user and/or reverted user
                    Dim rev As Revision = Prev

                    While IsKnown(rev) AndAlso _
                        (rvdUser Is Nothing AndAlso oldUser IsNot rev.User OrElse rvdUser Is rev.User)

                        rev = rev.Prev
                    End While

                    If IsKnown(rev) AndAlso (oldUser Is Nothing OrElse oldUser Is rev.User) Then
                        RevertTo = rev
                        rev = Prev

                        While IsKnown(rev) AndAlso _
                            (rvdUser Is Nothing AndAlso oldUser IsNot rev.User OrElse rvdUser Is rev.User)

                            If rev.RevertedBy Is Nothing Then rev.RevertedBy = Me
                        End While
                    End If

                ElseIf IsKnown(Prev) Then
                    'No information, assume only previous revision was reverted
                    Prev.RevertedBy = Me
                    If IsKnown(Prev.Prev) Then RevertTo = Prev
                End If

                Exit For
            Next pattern
        End Sub

        Public Sub ProcessNew()
            If User Is Nothing OrElse Page Is Nothing Then Return

            Page.Exists = True

            'Update revision properties
            If Page.LastRev IsNot Nothing Then Prev = Page.LastRev
            Page.LastRev = Me
            If User.LastEdit IsNot Nothing Then PrevByUser = User.LastEdit
            User.LastEdit = Me
            Prev.Next = Me
            If PrevByUser IsNot Nothing Then PrevByUser.NextByUser = Me

            'Reverts
            ProcessRevert()

            'Reverts with custom summary
            'If Config.CustomReverts.ContainsKey(Page) Then
            '    Dim crv As CustomRevert = Config.CustomReverts(Page)

            '    If User.IsUsed AndAlso Summary = crv.Summary Then
            '        IsRevert = True
            '        crv.Rev.RevertedBy = Me

            '        If crv.Target Is Nothing Then
            '            'Revert was a rollback to unknown target, try to find it
            '            Dim rev As Revision = Prev

            '            While IsKnown(rev) AndAlso rev.User Is Prev.User
            '                rev = rev.Prev
            '            End While

            '            If IsKnown(rev) Then crv.Target = rev
            '        End If

            '        If RevertTo Is Nothing Then RevertTo = crv.Target
            '    End If
            'End If

            'Config.CustomReverts.Remove(Page)

            If Prev Is Null Then Page.EditCount = 0
            If PrevByUser Is Null Then User.Contributions = 0

            User.SessionEdits += 1
            If User.Contributions > -1 Then User.Contributions += 1
            Page.SessionEdits += 1
            If Page.EditCount > -1 Then Page.EditCount += 1

            Page.OnEdit(Me)
            User.OnEdit(Me)

            RaiseEvent [New](Me, New EventArgs(Of Revision)(Me))
        End Sub

        Public Sub ProcessHtml()
            If Html Is Nothing Then Return

            'Rcid
            If Html.Contains("<div class='patrollink'>") Then
                IsReviewable = True
                Rcid = CInt(Html.FromFirst("<div class='patrollink'>").FromFirst("&amp;rcid=").ToFirst(""""))
            End If

            If Page.LastRev Is Me Then
                'Categories
                If Not Page.CategoriesKnown Then
                    If Html.Contains("<div id='catlinks'") Then
                        Dim cats As String() = Html.FromFirst("<div id='catlinks'").ToFirst("<!-- end content -->").Split("title=""")

                        For i As Integer = 2 To cats.Length - 1
                            Page.Categories.Merge(Wiki.Categories(HtmlDecode(cats(i).FromFirst(">").ToFirst("<"))))
                        Next i
                    End If

                    Page.CategoriesKnown = True
                End If

                'Sections
                If Not Page.SectionsKnown Then
                    Page.Sections.Clear()
                    Dim pos As Integer = Html.IndexOfI("<h2>")

                    While pos > -1
                        Dim header As String = Html.Substring(pos).FromFirst("<h2>").ToFirst("</h2>")

                        If header.Contains("<span class=""mw-headline"">") _
                            Then Page.Sections.Add(HtmlDecode(header.FromFirst("<span class=""mw-headline"">").ToFirst("</span>")))

                        pos = Html.IndexOfI("<h2>", Html.IndexOfI("</h2>", pos))
                    End While

                    Page.SectionsKnown = True
                End If
            End If
        End Sub

        Private Sub ProcessText(ByVal text As String)
            If text Is Nothing Then Return

            If Not TextProcessed Then
                Bytes = Encoding.UTF8.GetByteCount(text)

                If Page IsNot Nothing AndAlso Page.LastRev Is Me Then
                    'Redirect
                    If Not Page.TargetKnown Then
                        Static redirectRegex As New Regex("#redirect:? *\[\[([^\]]+)\]\].*", RegexOptions.IgnoreCase)

                        Dim match As Match = redirectRegex.Match(text)

                        Page.IsRedirect = match.Success
                        Page.Target = If(match.Success, Wiki.Pages.FromString(match.Groups(1).Value), Nothing)
                    End If

                    'Sections
                    If Not Page.SectionsKnown Then
                        Static sectionsRegex As New Regex("(^|\n)=+(.+?)=+($|\n)")

                        Dim sections As MatchCollection = sectionsRegex.Matches(text)

                        For Each match As Match In sections
                            Dim name As String = match.Groups(2).Value.Trim
                            If name.Remove("=").Length > 0 Then Page.Sections.Add(name)
                        Next match

                        'Page.SectionsKnown = True
                    End If
                End If

                TextProcessed = True
            End If
        End Sub

        Private Sub ProcessUserTalk()

            'Shared IP address
            If Page.Owner.IsAnonymous AndAlso Wiki.Config.SharedTemplates IsNot Nothing Then
                For Each item As Page In Wiki.Config.SharedTemplates
                    Dim i As Integer = Text.IndexOfIgnoreCase("{{" & Page.Name & "|")

                    If i >= 0 Then
                        Page.Owner.IsShared = True

                        Dim registeredTo As String = Text.Substring(i + 1)
                        If registeredTo.Contains("}}") Then registeredTo = registeredTo.ToFirst("}}")
                        If registeredTo.Contains("|") Then registeredTo = registeredTo.ToFirst("|")
                        Page.Owner.RegisteredTo = registeredTo
                    End If
                Next item
            End If
        End Sub

        Private Sub ProcessStateChange() Handles _Page.StateChanged, _User.StateChanged
            OnStateChanged(Nothing)
        End Sub

        Public Overrides Function ToString() As String
            Return Id.ToStringI
        End Function

        Public Shared Function CompareByPageName(ByVal x As Revision, ByVal y As Revision) As Integer
            Return String.Compare(x._Page.Name, y._Page.Name, StringComparison.Ordinal)
        End Function

        Public Shared Function CompareByQuality(ByVal x As Revision, ByVal y As Revision) As Integer

            If x Is Nothing Then Return 1
            If y Is Nothing Then Return -1

            If x.Page.IsIgnored AndAlso Not y.Page.IsIgnored Then Return 1
            If y.Page.IsIgnored AndAlso Not x.Page.IsIgnored Then Return -1

            If x.User.IsIgnored AndAlso Not y.User.IsIgnored Then Return 1
            If y.User.IsIgnored AndAlso Not x.User.IsIgnored Then Return -1

            If x.IsBot AndAlso Not y.IsBot Then Return 1
            If y.IsBot AndAlso Not x.IsBot Then Return -1

            If x.User.Sanction IsNot Nothing AndAlso y.User.Sanction IsNot Nothing Then
                If x.User.Sanction.Type.Level > y.User.Sanction.Type.Level Then Return -1
                If y.User.Sanction.Type.Level > x.User.Sanction.Type.Level Then Return 1
            End If

            If x.IsBlank AndAlso Not y.IsBlank Then Return -1
            If y.IsBlank AndAlso Not x.IsBlank Then Return 1

            If x.IsReplace AndAlso Not y.IsReplace Then Return -1
            If y.IsReplace AndAlso Not x.IsReplace Then Return 1

            If x.IsAbusive AndAlso Not y.IsAbusive Then Return -1
            If y.IsAbusive AndAlso Not x.IsAbusive Then Return 1

            If x.User.IsAbusive AndAlso Not y.User.IsAbusive Then Return -1
            If y.User.IsAbusive AndAlso Not x.User.IsAbusive Then Return 1

            If x.User.IsReverted AndAlso Not y.User.IsReverted Then Return -1
            If y.User.IsReverted AndAlso Not x.User.IsReverted Then Return 1

            If x.Page.IsPriority AndAlso Not y.Page.IsPriority Then Return -1
            If y.Page.IsPriority AndAlso Not x.Page.IsPriority Then Return 1

            If x.Page.IsPriorityTalk AndAlso Not y.Page.IsPriorityTalk Then Return -1
            If y.Page.IsPriorityTalk AndAlso Not x.Page.IsPriorityTalk Then Return 1

            If x.User.IsAnonymous AndAlso Not y.User.IsAnonymous Then Return -1
            If y.User.IsAnonymous AndAlso Not x.User.IsAnonymous Then Return 1

            If x.Page.Space.Number = 0 AndAlso y.Page.Space.Number > 0 Then Return -1
            If y.Page.Space.Number = 0 AndAlso x.Page.Space.Number > 0 Then Return 1

            If x.IsCreation AndAlso Not y.IsCreation Then Return -1
            If y.IsCreation AndAlso Not x.IsCreation Then Return 1

            If x.Random <> y.Random Then Return Math.Sign(y.Random - x.Random)

            Return String.Compare(x.Page.Name, y.Page.Name, StringComparison.Ordinal)
        End Function

        Public Shared Function CompareByTime(ByVal x As Revision, ByVal y As Revision) As Integer
            Return Date.Compare(y._Time, x._Time)
        End Function

        Public Shared Function CompareByTimeReverse(ByVal x As Revision, ByVal y As Revision) As Integer
            Return Date.Compare(x._Time, y._Time)
        End Function

        Public Shared Function IsKnown(ByVal rev As Revision) As Boolean
            Return (rev IsNot Nothing AndAlso rev IsNot Revision.Null)
        End Function

        Public Overrides ReadOnly Property LabelBackColor() As Color
            Get
                Return Page.LabelBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property LabelStyle() As FontStyle
            Get
                If Page.IsPriority OrElse Page.IsPriorityTalk Then Return FontStyle.Bold Else Return FontStyle.Regular
            End Get
        End Property

        Public Overrides ReadOnly Property FilterVars() As Dictionary(Of String, Object)
            Get
                Return New Object() {
                    "type", "edit",
                    "page", Page,
                    "user", User,
                    "summary", Summary,
                    "section", If(Section, ""),
                    "change", If(Change = Integer.MinValue, Nothing, Change)
                    }.ToDictionary(Of String, Object)()
            End Get
        End Property

    End Class

    Public Class RevisionCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, Revision)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Integer, Revision)
            Get
                Return _All
            End Get
        End Property

        Public Property Count() As Integer = -1

        Default Public ReadOnly Property Item(ByVal id As Integer) As Revision
            Get
                If Not All.ContainsKey(id) Then All.Add(id, New Revision(Wiki, id))
                Return All(id)
            End Get
        End Property

    End Class

End Namespace