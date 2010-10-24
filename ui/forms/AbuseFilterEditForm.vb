Imports Huggle
Imports Huggle.Actions
Imports System

Public Class AbuseFilterEditForm

    Private Filter As AbuseFilter
    Private Session As Session

    Public Sub New(ByVal session As Session, Optional ByVal filter As AbuseFilter = Nothing)
        InitializeComponent()
        Me.Filter = If(filter Is Nothing, New AbuseFilter(App.Wikis.Default, 0), filter)
        Me.Session = session
    End Sub

    Private Sub _Load() Handles Me.Load
        Dim getter As New AbuseFilterDetail(Session, Filter)
        App.UserWaitForProcess(getter)
        If getter.IsCancelled Then Close() : Return

        BlockCheck.Visible = getter.AllowedActions.Contains("block")
        BlockAutopromoteCheck.Visible = getter.AllowedActions.Contains("blockautopromote")
        DegroupCheck.Visible = getter.AllowedActions.Contains("degroup")
        DisallowCheck.Visible = getter.AllowedActions.Contains("disallow")
        RangeblockCheck.Visible = getter.AllowedActions.Contains("rangeblock")

        If Not getter.AllowedActions.Contains("warn") Then Tabs.TabPages(1).Hide()
        If Not getter.AllowedActions.Contains("throttle") Then Tabs.TabPages(2).Hide()
        If Not getter.AllowedActions.Contains("tag") Then Tabs.TabPages(3).Hide()

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
            RateLimitCount.Text = CStr(Filter.RateLimitCount)
            RateLimitTime.Text = CStr(CInt(Filter.RateLimitPeriod.TotalSeconds))
            RateLimitGroup.Text = Filter.RateLimitGroups.Join(CRLF)
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

        If RateLimitCheck.Checked Then
            newFilter.RateLimitCount = CInt(RateLimitCount.Text)
            newFilter.RateLimitGroups = RateLimitGroup.Text.Split(CRLF).ToList
            newFilter.RateLimitPeriod = New TimeSpan(0, 0, CInt(RateLimitTime.Text))
        End If

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