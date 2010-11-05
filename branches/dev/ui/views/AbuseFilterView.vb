Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AbuseFilterView : Inherits Viewer

        Friend Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
            FilterDetail.Session = session
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)
            VisibilitySel.SelectedIndex = 0
            StatusSel.SelectedIndex = 0
            ActionSel.SelectedIndex = 0
            CreateFilter.Visible = Session.User.HasRight("abusefilter-modify")
            If FilterList.Items.Count > 0 Then FilterList.SelectedIndices.Add(0)
        End Sub

        Friend Property SelectedFilter As AbuseFilter
            Get
                If FilterList.SelectedValue Is Nothing Then Return Nothing
                Return Wiki.AbuseFilters(CInt(FilterList.SelectedValue))
            End Get
            Set(ByVal value As AbuseFilter)
                If Wiki.AbuseFilters.All.Contains(value) Then FilterList.SelectedValue = value.Id.ToStringI
            End Set
        End Property

        Private Sub UpdateDisplay() Handles FilterList.SelectedIndexChanged
            If FilterList.SelectedValue IsNot Nothing Then
                Splitter.Panel2Collapsed = False
                FilterDetail.Filter = SelectedFilter
            End If
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

        Friend Shared Function GetFilterStatus(ByVal filter As AbuseFilter) As String
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

        Private Sub CreateFilter_LinkClicked() Handles CreateFilter.LinkClicked
            Dim form As New AbuseFilterEditForm(Session)
            form.Show()
        End Sub

    End Class

End Namespace