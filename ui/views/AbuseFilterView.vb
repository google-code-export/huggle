Imports System.Windows.Forms

Namespace Huggle.UI

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
            RestrictedIcon.Image = Resources.mini_no
            CreateFilter.Visible = Session.User.HasRight("abusefilter-modify")
            EditFilter.Visible = Session.User.HasRight("abusefilter-modify")
            UpdateDisplay()
        End Sub

        Private Sub UpdateDisplay() Handles FilterList.SelectedIndexChanged
            If FilterList.SelectedItems.Count = 0 Then
                Splitter.Panel2Collapsed = True
                CurrentFilter = Nothing
            Else
                Splitter.Panel2Collapsed = False
                CurrentFilter = Wiki.AbuseFilters(CInt(FilterList.SelectedItems(0).Text))
                GetDetails()

                Actions.Text = Msg("view-abusefilter-actions",
                    If(CurrentFilter.Actions.Count = 0, Msg("a-none"), CurrentFilter.Actions.Join(", ")))
                Description.Text = CurrentFilter.Description
                Modified.Text = Msg("view-abusefilter-modified",
                    CurrentFilter.LastModifiedBy.Name, FullDateString(CurrentFilter.LastModified))
                Notes.Text = CurrentFilter.Notes
                Pattern.Text = CurrentFilter.Pattern

                RateLimit.Text = Msg("view-abusefilter-ratelimit", If(CurrentFilter.RateLimitCount = 0, Msg("a-none"),
                    Msg("view-abusefilter-limitdetail", CurrentFilter.RateLimitCount,
                    CurrentFilter.RateLimitPeriod, If(CurrentFilter.RateLimitGroups Is Nothing, "",
                    CurrentFilter.RateLimitGroups.Join(", ")))))

                Status.Text = Msg("view-abusefilter-status", GetFilterStatus(CurrentFilter))
            End If
        End Sub

        Private Sub GetDetails()
            Dim full As Boolean = (CurrentFilter.Pattern IsNot Nothing)

            RateLimit.Visible = full
            Progress.Visible = Not full
            Indicator.Visible = Not full
            Restricted.Visible = False
            RestrictedIcon.Visible = False

            If full Then
                Details.Visible = True
                Progress.Text = Nothing
                Indicator.Stop()
            Else
                Dim privilegedSession As Session = App.Sessions.GetUserWithRight(Wiki, "abusefilter-private")

                If CurrentFilter.IsPrivate AndAlso privilegedSession Is Nothing Then
                    Details.Visible = False
                    Restricted.Visible = True
                    RestrictedIcon.Visible = True
                    Return
                End If

                Progress.Text = Msg("view-abusefilter-progress")
                Indicator.Start()

                If privilegedSession Is Nothing Then privilegedSession = Session
                DetailQuery = New Actions.AbuseFilterDetail(privilegedSession, CurrentFilter)
                CreateThread(AddressOf DetailQuery.Start)
            End If
        End Sub

        Private Sub GotDetails() Handles DetailQuery.Complete
            If CurrentFilter Is DetailQuery.Filter Then UpdateDisplay()
        End Sub

        Private Sub PopulateFilterList() _
            Handles VisibilitySel.SelectedIndexChanged, StatusSel.SelectedIndexChanged, ActionSel.SelectedIndexChanged

            FilterList.BeginUpdate()
            FilterList.Items.Clear()

            For Each filter As AbuseFilter In Wiki.AbuseFilters.All
                If MatchesFilter(filter) Then FilterList.AddRow(
                    filter.Id.ToStringForUser, filter.Description, GetFilterStatus(filter),
                    filter.Actions.Join(", "), filter.TotalHits.ToStringForUser)
            Next filter

            FilterList.SortMethods(0) = SortMethod.Integer
            FilterList.SortMethods(4) = SortMethod.Integer
            FilterList.SortBy(0)
            FilterList.EndUpdate()
            FilterCount.Text = Msg("a-count", FilterList.Items.Count)
        End Sub

        Private Shared Function GetFilterStatus(ByVal filter As AbuseFilter) As String
            Return Msg("view-abusefilter-" & If(filter.IsPrivate, "private", "public")) & ", " &
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
            OpenWebBrowser(Wiki.Pages.FromNsAndName(Wiki.Spaces.Special, "AbuseFilter/" & CurrentFilter.Id.ToStringI).Url)
        End Sub

    End Class

End Namespace