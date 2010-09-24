Imports Huggle
Imports System.Windows.Forms

Public Class AbuseFilterView : Inherits Viewer

    Private CurrentFilter As AbuseFilter
    Private WithEvents DetailQuery As Actions.AbuseFilterDetail

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        InitializeComponent()
    End Sub

    Private Sub _Load() Handles Me.Load
        App.Languages.Current.Localize(Me)
        VisibilitySel.SelectedIndex = 0
        StatusSel.SelectedIndex = 0
        ActionSel.SelectedIndex = 0
    End Sub

    Private Sub UpdateDetails() Handles FilterList.SelectedIndexChanged
        If FilterList.SelectedItems.Count = 0 Then
            AbuseFilterSplit.Panel2Collapsed = True
            CurrentFilter = Nothing
        Else
            AbuseFilterSplit.Panel2Collapsed = False
            CurrentFilter = Wiki.AbuseFilters(CInt(FilterList.SelectedItems(0).Text))

            Dim full As Boolean = (CurrentFilter.Pattern IsNot Nothing)

            RateLimit.Visible = full
            Progress.Visible = Not full
            Indicator.Visible = Not full

            If full Then
                Tabs.TabPages(1).Show()
                Tabs.TabPages(2).Show()
                Progress.Text = Nothing
                Indicator.BackgroundImage = Nothing
                Indicator.Stop()
            Else
                Tabs.TabPages(1).Hide()
                Tabs.TabPages(2).Hide()

                Dim abfPrivateSession As Session = App.Sessions.GetUserWithRight(Wiki, "abusefilter-private")

                If abfPrivateSession Is Nothing Then
                    Progress.Text = Msg("view-abusefilter-restricted")
                    Indicator.BackgroundImage = Resources.mini_no
                Else
                    DetailQuery = New Actions.AbuseFilterDetail(abfPrivateSession.Wiki, CurrentFilter)
                    CreateThread(AddressOf DetailQuery.Start)
                    Progress.Text = Msg("view-abusefilter-progress")
                    Indicator.BackgroundImage = Nothing
                    Indicator.Start()
                End If
            End If

            Actions.Text = Msg("view-abusefilter-actions", _
                If(CurrentFilter.Actions.Count = 0, Msg("a-none"), CurrentFilter.Actions.Join(", ")))
            Description.Text = CurrentFilter.Description
            Modified.Text = Msg("view-abusefilter-modified", _
                CurrentFilter.LastModifiedBy.Name, FullDateString(CurrentFilter.LastModified))
            Notes.Text = CurrentFilter.Notes
            Pattern.Text = CurrentFilter.Pattern
            RateLimit.Text = Msg("view-abusefilter-ratelimit", If(CurrentFilter.RateLimitCount = 0, Msg("a-none"), _
                Msg("view-abusefilter-limitdetail", CurrentFilter.RateLimitCount, _
                CurrentFilter.RateLimitPeriod, CurrentFilter.RateLimitGroups.Join(", "))))
            Status.Text = Msg("view-abusefilter-status", GetFilterStatus(CurrentFilter))

            CreateFilter.Visible = Session.User.HasRight("abusefilter-modify")
            EditFilter.Visible = Session.User.HasRight("abusefilter-modify")
        End If
    End Sub

    Private Sub Details() Handles DetailQuery.Complete
        If CurrentFilter Is DetailQuery.Filter Then UpdateDetails()
    End Sub

    Private Sub PopulateFilterList() _
        Handles VisibilitySel.SelectedIndexChanged, StatusSel.SelectedIndexChanged, ActionSel.SelectedIndexChanged

        FilterList.BeginUpdate()
        FilterList.Items.Clear()

        For Each filter As AbuseFilter In Wiki.AbuseFilters.All
            If MatchesFilter(filter) Then FilterList.AddRow _
                (filter.Id.ToString, filter.Description, GetFilterStatus(filter), _
                filter.Actions.Join(", "), filter.TotalHits.ToString)
        Next filter

        FilterList.SortMethods(0) = SortMethod.Integer
        FilterList.SortMethods(4) = SortMethod.Integer
        FilterList.SortBy(0)
        FilterList.EndUpdate()
        FilterCount.Text = Msg("a-count", FilterList.Items.Count)
    End Sub

    Private Function GetFilterStatus(ByVal filter As AbuseFilter) As String
        Return Msg("view-abusefilter-" & If(filter.IsPrivate, "private", "public")) & ", " & _
            Msg("view-abusefilter-" & If(filter.IsDeleted, "deleted", If(filter.IsEnabled, "enabled", "disabled")))
    End Function

    Private Function MatchesFilter(ByVal filter As AbuseFilter) As Boolean
        If filter.IsPrivate AndAlso VisibilitySel.SelectedIndex = 1 Then Return False
        If Not filter.IsPrivate AndAlso VisibilitySel.SelectedIndex = 2 Then Return False

        If Not filter.IsEnabled AndAlso StatusSel.SelectedIndex = 1 Then Return False
        If (filter.IsEnabled OrElse filter.IsDeleted) AndAlso StatusSel.SelectedIndex = 2 Then Return False
        If Not filter.IsDeleted AndAlso StatusSel.SelectedIndex = 3 Then Return False

        If Not filter.Actions.Contains("tag") AndAlso ActionSel.SelectedIndex = 1 Then Return False
        If Not filter.Actions.Contains("warn") AndAlso ActionSel.SelectedIndex = 2 Then Return False
        If Not filter.Actions.Contains("throttle") AndAlso ActionSel.SelectedIndex = 3 Then Return False
        If Not filter.Actions.Contains("disallow") AndAlso ActionSel.SelectedIndex = 4 Then Return False
        If Not filter.Actions.Contains("blockautopromote") AndAlso ActionSel.SelectedIndex = 5 Then Return False
        If Not filter.Actions.Contains("degroup") AndAlso ActionSel.SelectedIndex = 6 Then Return False
        If Not filter.Actions.Contains("block") AndAlso ActionSel.SelectedIndex = 7 Then Return False
        If Not filter.Actions.Contains("rangeblock") AndAlso ActionSel.SelectedIndex = 8 Then Return False

        Return True
    End Function

    Private Sub AbuseFilterEdit_LinkClicked() Handles EditFilter.LinkClicked
        Dim form As New AbuseFilterEditForm(Session, CurrentFilter)
        form.Show()
    End Sub

    Private Sub AbuseFilterCreate_LinkClicked() Handles CreateFilter.LinkClicked
        Dim form As New AbuseFilterEditForm(Session)
        form.Show()
    End Sub

    Private Sub Description_LinkClicked() Handles Description.LinkClicked
        OpenWebBrowser(Wiki.Pages.FromNsAndName(Wiki.Spaces.Special, "AbuseFilter/" & CurrentFilter.Id.ToString).Url)
    End Sub

End Class