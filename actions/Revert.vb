Imports System.Collections.Generic

Namespace Huggle.Actions

    Friend Class Revert : Inherits Query

        'Handles the "business logic" of reverting a page to an earlier revision

        Private _Rev As Revision
        Private _Summary As String

        Private BlankPage As Boolean
        Private IsFailedRollback As Boolean
        Private IsRollbackStyle As Boolean
        Private IsSelf As Boolean
        Private IsUndo As Boolean
        Private Source As Revision
        Private Target As Revision
        Private Top As Revision

        Private ConfirmMessages As New List(Of String)
        Private ConfirmOptions As New List(Of String)

        Public Sub New(ByVal session As Session, ByVal rev As Revision, Optional ByVal self As Boolean = False, _
            Optional ByVal undo As Boolean = False, Optional ByVal summary As String = Nothing)

            MyBase.New(session, Msg("revert-desc"))
            Me.IsSelf = self
            Me.IsUndo = undo
            _Rev = rev
            _Summary = summary
        End Sub

        Public ReadOnly Property Rev() As Revision
            Get
                Return _Rev
            End Get
        End Property

        Public Property Summary() As String
            Get
                Return _Summary
            End Get
            Private Set(ByVal value As String)
                _Summary = value
            End Set
        End Property

        Public Overrides Sub Start()
            'Abort if blocked
            If User.IsBlocked Then OnFail(Msg("error-blocked", User.CurrentBlock.Comment)) : Return

            'Check account permissions
            If User.Wiki IsNot Rev.Wiki OrElse Not User.HasRight("edit") _
                OrElse Not User.Can("revert") Then OnFail(Msg("error-account")) : Return

            CreateThread(AddressOf GetRevisionInfo, AddressOf CheckRevision)
        End Sub

        Private Sub GetRevisionInfo()
            'Need certain minimal information about the revision before
            'we can decide what to do with it and how to prompt the user
            If Not Rev.InHistory OrElse Rev.User Is Nothing OrElse Rev.Page Is Nothing _
                OrElse Rev.Prev Is Nothing OrElse Rev.Prev.User Is Nothing Then

                Dim request As New ApiRequest(Session, Description, New QueryString( _
                    "action", "query", _
                    "prop", "info|revisions", _
                    "titles", Rev.Page, _
                    "rvstartid", Rev.Page.LatestKnownRev, _
                    "rvendid", Rev.Prev, _
                    "rvdir", "older", _
                    "rvlimit", "max", _
                    "rvprop", "ids|flags|timestamp|user|size|comment"))

                request.Start()
                If request.Result.IsError Then OnFail(request.Result.Message) : Return
            End If
        End Sub

        Private Sub CheckRevision()
            CheckAlreadyDone()
            If IsFailed Then Return

            'Note last revision to page so we can check if the page has been edited
            Top = Rev.Page.LastRev
            Target = Rev.Prev
            If IsUndo Then Source = Target Else Source = Top

            Dim canRollback As Boolean = (Rev.CanRollback AndAlso User.HasRight("rollback"))

            'Check if all revisions to be reverted are ours
            Dim allMine As Boolean = True
            Dim r As Revision = Source

            While r IsNot Rev
                r = r.Prev
                If Not r.User Is User Then allMine = False
            End While

            'Check if all users to be reverted are ignored
            Dim allIgnored As Boolean = True
            r = Source

            While r IsNot Rev
                r = r.Prev
                If Not r.User.IsIgnored Then allIgnored = False
            End While

            'Check if all revisions to be reverted are by the same user
            Dim allSame As Boolean = True
            r = Source

            While r IsNot Rev
                r = r.Prev
                If r.User IsNot Rev.User Then allSame = False
            End While

            'Check if the page has a single author
            Dim singleAuthor As Boolean = True
            r = Source

            While Revision.IsKnown(r)
                r = r.Prev
                If r.User IsNot Source.User Then singleAuthor = False
            End While

            If r Is Nothing Then singleAuthor = False

            'See if there's anything to prompt for confirmation about
            ConfirmOptions.Add(Msg("revert-continue"))

            Dim decisionNeeded As Boolean

            'Self-reversion
            If User.Config.ConfirmSelf AndAlso Not IsSelf AndAlso Not Rev.IsCreation AndAlso allMine Then
                ConfirmMessages.Add(Msg("revert-confirm-self"))
            End If

            'Reverting an ignored user
            If User.Config.ConfirmIgnoredUser AndAlso Not Rev.User Is User AndAlso allIgnored Then
                ConfirmMessages.Add(Msg("revert-confirm-ignoreduser", Rev.User))
                ConfirmOptions.Add(Msg("revert-unignore-user", Rev.User))
            End If

            'Reverting an ignored page
            If User.Config.ConfirmIgnoredPage AndAlso Not IsSelf AndAlso Rev.Page.IsIgnored Then
                ConfirmMessages.Add(Msg("revert-confirm-ignoredpage", Rev.Page))
                ConfirmOptions.Add(Msg("revert-unignore-page", Rev.Page))
            End If

            'Reverting a semi-ignored user
            If User.Config.ConfirmSemiIgnored AndAlso Rev.IsTop AndAlso Not Rev.User.IsIgnored _
                AndAlso Rev.User.IgnoreCount >= User.Config.SemiIgnoreAfter Then

                ConfirmMessages.Add(Msg("revert-confirm-semiignored", Rev.User))
            End If

            'Check whether page is to be automatically blanked
            BlankPage = Rev.IsCreation AndAlso _
                (User.Config.RevertAlwaysBlank AndAlso Wiki.Config.RevertAlwaysBlank.Contains(Rev.Page.Space.Number))

            'Reverting page creator or first edit to page, except when automatically blanked
            If (Rev.User Is Rev.Page.Creator OrElse Rev.IsCreation) AndAlso Not BlankPage Then

                If Rev.IsCreation Then
                    ConfirmMessages.Add(Msg("revert-first"))
                ElseIf singleAuthor Then
                    ConfirmMessages.Add(Msg("revert-onlyauthor", Rev.User))
                Else
                    ConfirmMessages.Add(Msg("revert-creator", Rev.User))
                End If

                'Several options, depending on wiki setup and user permissions
                ConfirmOptions.Clear()
                If Rev.IsCreation OrElse Not Revision.IsKnown(Target) Then ConfirmOptions.Remove(Msg("revert-continue"))
                If Not Rev.IsCreation AndAlso Not Revision.IsKnown(Target) Then ConfirmOptions.Add(Msg("revert-revertone"))
                If Wiki.Config.RevertBlankTalk AndAlso Rev.Page.IsTalkPage Then ConfirmOptions.Add(Msg("revert-blank"))
                If User.Can("delete") AndAlso User.Config.RevertDelete Then ConfirmOptions.Add(Msg("revert-delete"))
                If User.Can("speedy") AndAlso User.Config.RevertSpeedy Then ConfirmOptions.Add(Msg("revert-speedy"))

                If ConfirmOptions.Count = 0 Then
                    'If we get here, the revision is a page creation, we can't blank the page, 
                    'we can't delete it and there's no speedy deletion process defined. So there's nothing we can do
                    OnFail(Msg("revert-creationfail"))
                    App.ShowError(Result)
                    Return
                End If
            End If

            If Not IsSelf AndAlso Not IsUndo Then
                If User.Config.ConfirmPartialSeries AndAlso Not Rev.IsTop AndAlso Rev.CanUserRevert Then

                    'Reverting to the middle of a series of revisions by one user
                    decisionNeeded = True
                    ConfirmMessages.Add(Msg("revert-confirm-series", Rev.User))
                    ConfirmOptions.Remove(Msg("revert-continue"))
                    ConfirmOptions.Add(Msg(If(allSame, "revert-series-allby", "revert-series-all"), Rev.User))
                    ConfirmOptions.Add(Msg("revert-series-here", Rev.User))

                ElseIf User.Config.ConfirmRollback AndAlso User.Config.RevertRollback _
                    AndAlso Rev.IsTop AndAlso Rev.CanUserRevert Then

                    'Reverting multiple revisions by the same user
                    ConfirmMessages.Add(Msg("revert-confirm-multipleby", Rev.User))
                    ConfirmMessages.Add(Msg("revert-revertone", Rev.User))

                ElseIf User.Config.ConfirmSameUser AndAlso Rev.Page.LastRev IsNot Nothing _
                    AndAlso Rev.Page.LastRev.User Is Target.User AndAlso Rev.User IsNot User Then

                    'Reverting to another revision by the same user
                    decisionNeeded = True
                    ConfirmMessages.Add(Msg("revert-confirm-same", Rev.User))
                    ConfirmOptions.Remove(Msg("revert-continue"))
                    ConfirmOptions.Add(Msg("revert-revertone", Rev.User))
                    ConfirmOptions.Add(Msg("revert-revertall", Rev.User))

                ElseIf User.Config.ConfirmMultiple AndAlso Not Rev.IsTop Then

                    'Reverting multiple revisions
                    decisionNeeded = True
                    ConfirmMessages.Add(Msg("revert-confirm-multiple"))
                    ConfirmOptions.Add(Msg("revert-revertone"))
                End If
            End If

            'If the user's response to the confirmation dialog affects which revision we revert to,
            'then before we can warn the user of any problems with that revision, we need to know what
            'their choice is. So show the confirmation dialog early...
            If decisionNeeded AndAlso Interactive Then
                GetConfirmation()
                CheckAlreadyDone()
                If IsCancelled Then Return
            End If

            'Implicitly decide to do rollback-like behaviour if we can
            If Not User.Config.ConfirmRollback AndAlso User.Config.RevertRollback _
                AndAlso Rev.IsTop AndAlso Not IsSelf Then IsRollbackStyle = True

            'If the user has decided to revert to a revision that we don't know about, we
            'need to find that revision first before we can warn them of problems with it.
            'For rollbacks, though, this slows things down, since the target revision
            'isn't actually needed for rollback. Thus in such cases it is optional.
            If IsRollbackStyle AndAlso Rev.UserRevertTarget Is Nothing _
                AndAlso Not (Rev.CanRollback AndAlso User.HasRight("rollback") _
                AndAlso Not User.Config.RevertCheckRollbackTarget) Then

                CreateThread(AddressOf GetTargetRevision, AddressOf CheckTarget)
            Else
                'Construct summary if none is supplied
                If Summary Is Nothing Then Summary = GetSummary()
                DoRevert()
            End If
        End Sub

        Private Sub GetConfirmation()
            ConfirmOptions.Add(Msg("cancel"))

            'Show prompt to user
            Select Case ConfirmOptions(App.ShowPrompt _
                (Msg("revert-action"), MakeConfirmation(ConfirmMessages), Nothing, 1, ConfirmOptions.ToArray) - 1)

                Case Msg("revert-blank") : BlankPage = True
                Case Msg("revert-continue") : Exit Select
                Case Msg("revert-delete") : DoDelete()
                Case Msg("revert-revertall", Rev.User) : IsRollbackStyle = True
                Case Msg("revert-revertone", Rev.User) : IsUndo = True
                Case Msg("revert-series-here") : IsRollbackStyle = False
                Case Msg("revert-series-allby", Rev.User) : IsRollbackStyle = True
                Case Msg("revert-series-all", Rev.User) : IsRollbackStyle = True
                Case Msg("revert-speedy") : DoSpeedy()
                Case Msg("cancel") : OnFail(Msg("error-cancelled"))
            End Select

            'Reset the confirmation dialog in case it's needed again later on
            ConfirmMessages.Clear()
            ConfirmOptions.Clear()
            ConfirmOptions.Add(Msg("revert-continue"))
        End Sub

        Private Sub GetTargetRevision()
            'If we're going to be manually saving this revision later,
            'we might as well get the content. If not, we don't need it.
            Dim prop As String = "ids|user"

            'Need the most recent revision not by the same user
            Dim request As New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "prop", "revisions", _
                "titles", Rev.Page, _
                "rvdir", "older", _
                "rvexcludeuser", Rev.User, _
                "rvlimit", 1, _
                "rvprop", prop, _
                "rvstartid", Rev))

            request.Start()

            If request.Result.IsError Then OnFail(request.Result.Message) : Return
            If Not Rev.Page.Exists Then OnFail(Msg("error-pagemissing")) : Return

            Target = request.Items.FirstInstance(Of Revision)()
        End Sub

        Private Sub CheckTarget()
            CheckAlreadyDone()
            If IsCancelled Then Return

            If Target Is Nothing Then
                'Turns out this was the page's only author after all
                'Return to first sequence of checks and this will be picked up and prompted for
                CheckRevision()
                Return
            End If

            If User.Config.ConfirmUseful AndAlso Not IsSelf Then
                'Trying to avoid reversion of useful changes: check for intermediate revisions
                'by ignored users that are not tags, warnings, reverts, or reverted revisions
                Dim r As Revision = Rev.Page.LastRev

                While Revision.IsKnown(r) AndAlso r IsNot Target
                    If r.User.IsIgnored AndAlso Not r.IsRevert AndAlso Not r.IsReverted _
                        AndAlso Not r.IsTag AndAlso Not r.IsSanction AndAlso Not r.User Is User Then

                        'Reverting revisions that appear to be content changes by ignored users
                        ConfirmMessages.Add(Msg("revert-confirm-useful"))
                        Exit While
                    End If

                    r = r.Prev
                End While
            End If

            'Now check for problems with the target revision
            If User.Config.ConfirmWarnedRev AndAlso Not IsSelf _
                AndAlso Target.User.IsWarned AndAlso Target.IsSanctioned _
                AndAlso Target.WarnedForBy IsNot Nothing AndAlso Target.WarnedForBy.User.IsIgnored _
                AndAlso Target.WarnedForBy.Sanction.IsWarning Then

                'Reverting to a revision that was warned for
                ConfirmMessages.Add(Msg("revert-confirm-warned", Target.User))

            ElseIf User.Config.ConfirmRevertedRev AndAlso Not IsSelf _
                AndAlso Target.IsReverted AndAlso Target.RevertedBy IsNot Nothing _
                AndAlso Target.RevertedBy.User.IsIgnored Then

                'Reverting to a previously reverted revision
                ConfirmMessages.Add(Msg("revert-confirm-reverted", Target.User))

            ElseIf User.Config.ConfirmWarnedUser AndAlso Not IsSelf AndAlso Target.User IsNot Nothing _
                AndAlso Not Target.User.IsIgnored AndAlso (Target.User.IsWarned OrElse Target.User.IsBlocked) Then

                'Reverting to a revision by a warned or blocked user
                ConfirmMessages.Add(Msg("revert-confirm-warneduser", Target.User))

            ElseIf User.Config.ConfirmRevertedUser AndAlso Not IsSelf _
                AndAlso Target.User IsNot Nothing AndAlso Not Target.User.IsIgnored _
                AndAlso Target.User.IsReverted Then

                'Reverting to a revision by a reverted user
                ConfirmMessages.Add(Msg("revert-confirm-reverteduser", Target.User))
            End If

            If User.Config.ConfirmRange AndAlso Not IsSelf _
                AndAlso Target.User IsNot Nothing AndAlso Target.User.Range IsNot Nothing _
                AndAlso Target.User.Range = Rev.User.Range Then

                'Reverting to a revision by an anonymous user in same /16 block as user being reverted
                ConfirmMessages.Add(Msg("revert-confirm-range", Rev.User, Target.User))
            End If

            'Prompt for confirmation
            If ConfirmMessages.Count > 0 AndAlso Interactive Then
                GetConfirmation()
                CheckAlreadyDone()
                If IsCancelled Then Return
            End If

            'Construct summary if none is supplied
            If Summary Is Nothing Then Summary = GetSummary()

            'If we made it this far, go ahead and revert
            CreateThread(AddressOf DoRevert)
        End Sub

        Private Function GetSummary() As String

            'Rollback accepts its own summary with placeholder parameters that are automatically filled
            If User.HasRight("rollback") AndAlso IsRollbackStyle AndAlso Not IsUndo AndAlso Rev.CanRollback _
                Then Return Wiki.Config.RevertSummaryRollback & Wiki.Config.SummaryTag

            If IsSelf Then Return User.Config.RevertSelfSummary
            If IsUndo Then Return Wiki.Config.RevertSummaryUndo.FormatForUser(Rev.Id, Rev.User) & Wiki.Config.SummaryTag

            Dim count As Integer = 0
            Dim users As New List(Of User)
            Dim r As Revision = Target.Page.LastRev

            While Revision.IsKnown(r) AndAlso r IsNot Target
                users.Merge(r.User)
                count += 1
                r = r.Prev
            End While

            Dim countStr As String = If(r Is Target, Wiki.Config.RevertSummaryMultipleRevs, _
                Wiki.Config.RevertSummaryUnknownRevs).FormatForUser(count)

            Dim usersStr As String = NaturalLanguageList(Rev.Wiki.Language, users)

            Dim targetStr As String = If(users.Contains(Rev.User), Wiki.Config.RevertSummaryPreviousVersion, _
                Wiki.Config.RevertSummaryLastVersion).FormatForUser(Rev.User)

            'Ensure the summary isn't too long
            If (Wiki.Config.RevertSummary.FormatForUser(countStr, usersStr, targetStr) & Wiki.Config.SummaryTag).Length > 255 _
                Then usersStr = Wiki.Config.RevertSummaryMultipleUsers.FormatForUser(users.Count)

            Return Wiki.Config.RevertSummary.FormatForUser(countStr, usersStr, targetStr) & Wiki.Config.SummaryTag
        End Function

        Private Sub DoRevert()
            CheckAlreadyDone()
            If IsCancelled Then Return

            'Use rollback in preference to undoing the most recent revision
            If IsUndo AndAlso Rev Is Top AndAlso Target Is Rev.Prev AndAlso CanRollback Then
                IsUndo = False
                IsRollbackStyle = True
            End If

            'Use multi-revision undo in preference to manually reverting
            If Target IsNot Nothing AndAlso Target.Next IsNot Nothing AndAlso Not CanRollback Then
                IsRollbackStyle = False
                IsUndo = True
            End If

            OnStarted()
            OnProgress(Msg("revert-progress", Rev.Page))


            If BlankPage Then
                'Blank the page
                Dim blankEdit As New Edit(Session, Rev.Page, "", Nothing)

                If Rev.User Is User Then
                    blankEdit.Summary = User.Config.RevertSelfSummary
                    blankEdit.Minor = Wiki.Config.IsMinor("undo")
                    blankEdit.Watch = If(User.Config.IsWatch("undo"), WatchAction.Watch, WatchAction.NoChange)
                Else
                    blankEdit.Summary = Wiki.Config.RevertSummaryBlank
                    blankEdit.Minor = Wiki.Config.IsMinor("revert")
                    blankEdit.Watch = If(User.Config.IsWatch("revert"), WatchAction.Watch, WatchAction.NoChange)
                End If

                blankEdit.Start()
                If blankEdit.IsFailed Then OnFail(blankEdit.Result) Else OnSuccess()
                Return
            End If

            If IsRollbackStyle AndAlso CanRollback Then
                'Rollback
                If Not Session.RollbackTokens.ContainsKey(Rev) Then

                    'Get rollback token
                    Dim tokenreq As New ApiRequest(Session, Description, New QueryString( _
                        "action", "query", _
                        "prop", "revisions", _
                        "titles", Rev.Page, _
                        "rvlimit", 1, _
                        "rvtoken", "rollback"))

                    tokenreq.Start()
                    If tokenreq.Result.IsError Then OnFail(tokenreq.Result.Message)

                    If Not Session.RollbackTokens.ContainsKey(Rev) Then
                        'No rollback token could be found. Revert manually instead
                        'This may require fetching the target revision, so start method from the top
                        IsFailedRollback = True

                        If Target Is Nothing Then GetTargetRevision()
                        DoRevert()
                        Return
                    End If
                End If

                CheckAlreadyDone()
                If IsFailed Then Return

                'Rollback the page
                Dim req As New ApiRequest(Session, Description, New QueryString( _
                    "action", "rollback", _
                    "title", Rev.Page, _
                    "from", Rev.User, _
                    "token", Session.RollbackTokens(Rev), _
                    "summary", Summary))

                req.Start()
                If req.Result.IsError Then OnFail(req.Result.Message) : Return

                'Watch the page
                If User.Config.IsWatch("revert") AndAlso Not Rev.Page.IsWatchedBy(User) Then
                    Dim query As New Watch(Session, Rev.Page, WatchAction.Watch)
                    query.Start()
                End If

                OnSuccess()
            End If

            If IsUndo Then
                'Undo
                If Session.EditToken Is Nothing Then
                    'Get token
                    Dim query As New TokenQuery(Session)
                    query.Start()
                    If query.Result.IsError Then OnFail(query.Result.Message) : Return
                End If

                CheckAlreadyDone()
                If IsFailed Then Return

                Dim req As New ApiRequest(Session, Description, New QueryString( _
                    "action", "edit", _
                    "title", Rev.Page, _
                    "summary", Summary, _
                    "undo", Rev.Id, _
                    "token", Session.EditToken, _
                    "watch", User.Config.IsWatch("revert")))

                If Target IsNot Rev.Prev Then req.Query.Add("undoafter", Target.Next)
                req.Start()

                If req.Result.IsError Then
                    If True Then
                        'Undo failed due to conflicting intermediate edits, try manually reverting
                        IsUndo = False
                        DoRevert()
                        Return
                    Else
                        OnFail(req.Result.Message) : Return
                    End If
                End If

                OnSuccess()
                Return
            End If

            'Need the target revision content to manually revert
            If Target.Text Is Nothing Then
                'Dim targetQuery As New RevisionInfoQuery(User, Target)
                'If targetQuery.Do.IsError Then OnFail(targetQuery.Result.Message) : Return
                If Target.Text Is Nothing Then OnFail(Msg("error-text")) : Return

                CheckAlreadyDone()
                If IsFailed Then Return
            End If

            'Cannot revert to a revision we aren't allowed to see
            If Target.TextHidden Then OnFail(Msg("revert-texthidden")) : Return

            'Edit the page and overwrite with the target revision's content
            Dim edit As New Edit(Session, Target.Page, Target.Text, Summary)
            edit.AllowCreate = False
            edit.Conflict = ConflictAction.Ignore
            edit.Watch = If(User.Config.IsWatch("revert"), WatchAction.Watch, WatchAction.NoChange)

            edit.Start()
            If edit.IsFailed Then OnFail(edit.Result)

            OnSuccess()
        End Sub

        Private Sub CheckAlreadyDone()
            'Give up if the revert we wanted to do was already done while we were
            'running checks or waiting for user response
            If Top IsNot Rev.Page.LastRev AndAlso Rev.Page.LastRev.RevertTo Is Target _
                Then OnFail(Msg("error-alreadydone", Rev.Page.LastRev.User))
        End Sub

        Private Sub DoDelete()
            '
        End Sub

        Private Sub DoSpeedy()
            '
        End Sub

        Private ReadOnly Property CanRollback() As Boolean
            Get
                Return (Rev.CanRollback AndAlso User.HasRight("rollback") AndAlso Target Is Rev.UserRevertTarget)
            End Get
        End Property

    End Class

End Namespace