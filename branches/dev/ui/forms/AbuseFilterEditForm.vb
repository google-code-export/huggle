﻿Imports Huggle.Queries
Imports System

Namespace Huggle.UI

    Friend Class AbuseFilterEditForm : Inherits HuggleForm

        Private Filter As AbuseFilter
        Private Session As Session

        Public Sub New(ByVal session As Session, Optional ByVal filter As AbuseFilter = Nothing)
            ThrowNull(session, "session")
            Me.Session = session
            Me.Filter = If(filter Is Nothing, New AbuseFilter(App.Wikis.Default, 0), filter)

            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Dim getter As New AbuseFilterDetailQuery(Session, Filter)
            App.UserWaitForProcess(getter)
            If getter.IsCancelled Then Close() : Return

            BlockCheck.Visible = Session.Wiki.Config.AbuseFilterActions.Contains("block")
            BlockAutopromoteCheck.Visible = Session.Wiki.Config.AbuseFilterActions.Contains("blockautopromote")
            DegroupCheck.Visible = Session.Wiki.Config.AbuseFilterActions.Contains("degroup")
            DisallowCheck.Visible = Session.Wiki.Config.AbuseFilterActions.Contains("disallow")
            RangeblockCheck.Visible = Session.Wiki.Config.AbuseFilterActions.Contains("rangeblock")

            If Not Session.Wiki.Config.AbuseFilterActions.Contains("warn") Then Tabs.TabPages(1).Hide()
            If Not Session.Wiki.Config.AbuseFilterActions.Contains("throttle") Then Tabs.TabPages(2).Hide()
            If Not Session.Wiki.Config.AbuseFilterActions.Contains("tag") Then Tabs.TabPages(3).Hide()

            If Filter.Id > 0 Then
                Text = Msg("abusefilteredit-title", Filter.Id)

                Description.Text = Filter.Description
                Condition.Text = Filter.Pattern
                Notes.Text = Filter.Notes
                PrivateCheck.Checked = Filter.IsPrivate
                DisabledOption.Checked = True
                DeletedOption.Checked = Filter.IsDeleted
                EnabledOption.Checked = Filter.IsEnabled
                DisallowCheck.Checked = Filter.Actions.Contains("disallow")
                BlockCheck.Checked = Filter.Actions.Contains("block")
                BlockAutopromoteCheck.Checked = Filter.Actions.Contains("blockautopromote")
                DegroupCheck.Checked = Filter.Actions.Contains("degroup")
                RangeblockCheck.Checked = Filter.Actions.Contains("rangeblock")
                RateLimitCheck.Checked = Filter.Actions.Contains("throttle")
                RateLimitCount.Text = Filter.RateLimit.Count.ToStringI
                RateLimitTime.Text = CInt(Filter.RateLimit.Time.TotalSeconds).ToStringI
                RateLimitGroup.Text = Filter.RateLimit.Groups.Join(CRLF)
                Tags.Text = Filter.Tags.Join(CRLF)
            Else
                Text = Msg("abusefilteredit-newtitle")
            End If
        End Sub

        Private Sub CancelButton_Click() Handles CancelBtn.Click
            Close()
        End Sub

        Private Sub OK_Click() Handles OK.Click

            Dim newFilter As New AbuseFilter(App.Wikis.Default, Filter.Id)

            If BlockCheck.Checked Then newFilter.Actions.Add("block")
            If BlockAutopromoteCheck.Checked Then newFilter.Actions.Add("blockautopromote")
            If DegroupCheck.Checked Then newFilter.Actions.Add("degroup")
            If DisallowCheck.Checked Then newFilter.Actions.Add("disallow")
            If RangeblockCheck.Checked Then newFilter.Actions.Add("rangeblock")
            If RateLimitCheck.Checked Then newFilter.Actions.Add("throttle")
            If WarningSelector.Text IsNot Nothing Then newFilter.Actions.Add("warn")

            newFilter.Description = Description.Text
            newFilter.IsDeleted = DeletedOption.Checked
            newFilter.IsEnabled = EnabledOption.Checked
            newFilter.IsPrivate = PrivateCheck.Checked
            newFilter.Notes = Notes.Text
            newFilter.Pattern = Condition.Text
            If WarningSelector.Text IsNot Nothing Then newFilter.WarningMessage = WarningSelector.Text

            If RateLimitCheck.Checked Then newFilter.RateLimit = New RateLimit(
                Nothing, Nothing, RateLimitGroup.Text.Split(CRLF).ToList, CInt(RateLimitCount.Text),
                New TimeSpan(0, 0, CInt(RateLimitTime.Text)))

            If Tags.Text.Length > 0 Then newFilter.Tags = Tags.Text.Split(CRLF).ToList

            Dim editFilter As New EditAbuseFilter(Session, newFilter)
            App.UserWaitForProcess(editFilter)
            If editFilter.IsCancelled Then Close()
        End Sub

        Private Sub RateLimitCheck_CheckedChanged() Handles RateLimitCheck.CheckedChanged
            RateLimitCount.Enabled = RateLimitCheck.Checked
            RateLimitGroup.Enabled = RateLimitCheck.Checked
            RateLimitTime.Enabled = RateLimitCheck.Checked
        End Sub

    End Class

End Namespace