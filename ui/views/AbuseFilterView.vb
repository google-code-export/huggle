Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AbuseFilterView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
            FilterDetailView.Session = session
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)
            CreateFilter.Visible = Session.User.HasRight("abusefilter-modify")

            For Each visibility As String In {"visibilityall", "public", "private"}
                VisibilitySel.Items.Add(Msg("view-abusefilter-" & visibility))
            Next visibility

            For Each status As String In {"statusall", "enabled", "disabled", "deleted"}
                StatusSel.Items.Add(Msg("view-abusefilter-" & status))
            Next status

            For Each action As String In
                {"all", "tag", "warn", "disallow", "throttle", "blockautopromote", "degroup", "block", "rangeblock"}

                If action = "all" OrElse Wiki.Config.AbuseFilterActions Is Nothing _
                    OrElse Wiki.Config.AbuseFilterActions.Contains(action) _
                    Then ActionSel.Items.Add(Msg("view-abusefilter-action-" & action))
            Next action

            VisibilitySel.SelectedIndex = 0
            StatusSel.SelectedIndex = 0
            ActionSel.SelectedIndex = 0
        End Sub

        Public Property SelectedFilter As AbuseFilter
            Get
                If FilterList.SelectedValue Is Nothing Then Return Nothing
                Return Wiki.AbuseFilters(CInt(FilterList.SelectedValue))
            End Get
            Set(ByVal value As AbuseFilter)
                If Wiki.AbuseFilters.All.Contains(value) Then FilterList.SelectedValue = value.Id.ToStringI
            End Set
        End Property

        Public Shared Function GetFilterStatus(ByVal filter As AbuseFilter) As String
            Return Msg("view-abusefilter-" & If(filter.IsPrivate, "private", "public")).ToLowerI & ", " &
                Msg("view-abusefilter-" & If(filter.IsDeleted, "deleted",
                If(filter.IsEnabled, "enabled", "disabled"))).ToLowerI
        End Function

        Private Sub CreateFilter_LinkClicked() Handles CreateFilter.LinkClicked
            Dim form As New AbuseFilterEditForm(Session)
            form.Show()
        End Sub

        Private Sub PopulateFilterList() _
            Handles VisibilitySel.SelectedIndexChanged, StatusSel.SelectedIndexChanged, ActionSel.SelectedIndexChanged

            Dim lastFilter As AbuseFilter = SelectedFilter
            Dim rows As New List(Of String())

            For Each filter As AbuseFilter In Wiki.AbuseFilters.All
                Dim actions As IList(Of String) =
                    filter.Actions.Map(Function(action As String) Msg("view-abusefilter-action-" & action).ToLowerI)

                If MatchesFilter(filter) Then rows.Add({
                    filter.Id.ToStringForUser,
                    filter.Description,
                    GetFilterStatus(filter),
                    actions.Join(", "),
                    filter.TotalHits.ToStringForUser
                })
            Next filter

            FilterList.SortMethods(0) = SortMethod.Integer
            FilterList.SortMethods(4) = SortMethod.Integer
            FilterList.SetItems(rows)

            FilterCount.Text = Msg("a-count", rows.Count)

            If lastFilter IsNot Nothing AndAlso FilterList.Contains(lastFilter.Id.ToStringI) Then
                FilterList.SelectedValue = lastFilter.Id.ToStringI
            ElseIf FilterList.Items.Count > 0 Then
                FilterList.SelectedIndices.Add(0)
            End If
        End Sub

        Private Sub UpdateDisplay() Handles FilterList.SelectedIndexChanged
            If FilterList.SelectedValue IsNot Nothing Then
                Splitter.Panel2Collapsed = False
                FilterDetailView.Filter = SelectedFilter
            End If
        End Sub

        Private Function MatchesFilter(ByVal filter As AbuseFilter) As Boolean
            If filter.IsPrivate AndAlso VisibilitySel.SelectedIndex = 1 Then Return False
            If Not filter.IsPrivate AndAlso VisibilitySel.SelectedIndex = 2 Then Return False

            If Not filter.IsEnabled AndAlso StatusSel.SelectedIndex = 1 Then Return False
            If (filter.IsEnabled OrElse filter.IsDeleted) AndAlso StatusSel.SelectedIndex = 2 Then Return False
            If Not filter.IsDeleted AndAlso StatusSel.SelectedIndex = 3 Then Return False

            If ActionSel.SelectedItem Is Nothing Then Return True

            Select Case ActionSel.SelectedItem.ToString
                Case Msg("view-abusefilter-action-tag") : Return filter.Actions.Contains("tag")
                Case Msg("view-abusefilter-action-warn") : Return filter.Actions.Contains("warn")
                Case Msg("view-abusefilter-action-throttle") : Return filter.Actions.Contains("throttle")
                Case Msg("view-abusefilter-action-disallow") : Return filter.Actions.Contains("disallow")
                Case Msg("view-abusefilter-action-blockautopromote") : Return filter.Actions.Contains("blockautopromote")
                Case Msg("view-abusefilter-action-degroup") : Return filter.Actions.Contains("degroup")
                Case Msg("view-abusefilter-action-block") : Return filter.Actions.Contains("block")
                Case Msg("view-abusefilter-action-rangeblock") : Return filter.Actions.Contains("rangeblock")
            End Select

            Return True
        End Function

    End Class

End Namespace