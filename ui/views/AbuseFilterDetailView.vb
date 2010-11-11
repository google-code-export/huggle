Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class AbuseFilterDetailView : Inherits Viewer

        Private _Filter As AbuseFilter
        Private AdditionalData As Boolean

        Private Sub _Load() Handles Me.Load
            If DesignMode OrElse DesignTime Then Return

            Views.BeginUpdate()
            Views.Items.Clear()
            Views.Items.Add(Msg("view-abusefilter-general"))
            Views.Items.Add(Msg("view-abusefilter-tags"))
            Views.EndUpdate()
        End Sub

        Public Property Filter As AbuseFilter
            Get
                Return _Filter
            End Get
            Set(ByVal value As AbuseFilter)
                _Filter = value

                If Filter IsNot Nothing AndAlso Not (Filter.IsPrivate AndAlso Filter.Pattern Is Nothing) _
                    AndAlso Filter.RateLimit Is Nothing Then GetDetails()
                UpdateDisplay()
            End Set
        End Property

        Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
            If AdditionalData Then Session.Wiki.Config.SaveLocal()
        End Sub

        Private Sub Description_LinkClicked() Handles Description.LinkClicked
            If Filter IsNot Nothing Then OpenWebBrowser(Session.Wiki.Pages.FromNsAndName(
                Session.Wiki.Spaces.Special, "AbuseFilter/" & Filter.Id.ToStringI).Url)
        End Sub

        Private Sub Views_SelectedIndexChanged() Handles Views.SelectedIndexChanged
            ViewPanel.SuspendLayout()

            For Each control As Control In ViewPanel.Controls
                control.Dock = DockStyle.Fill
                control.Visible = False
            Next control

            If Views.SelectedItem IsNot Nothing Then
                Select Case Views.SelectedItem.ToString
                    Case Msg("view-abusefilter-general") : DescriptionPanel.Visible = True
                    Case Msg("view-abusefilter-notes") : Notes.Visible = True
                    Case Msg("view-abusefilter-pattern") : Pattern.Visible = True
                    Case Msg("view-abusefilter-tags") : TagsPanel.Visible = True
                End Select
            End If

            ViewPanel.ResumeLayout()
        End Sub

        Private Sub GetDetails()
            Dim privilegedSession As Session = App.Sessions.GetUserWithRight(Session.Wiki, "abusefilter-private")
            If privilegedSession Is Nothing Then privilegedSession = Session

            PrivateFilter.Visible = False
            Wait.Visible = True
            Wait.Text = Msg("view-abusefilter-progress")
            Wait.WaitOn(New AbuseFilterDetailQuery(privilegedSession, Filter), AddressOf GotDetails)
        End Sub

        Private Sub GotDetails(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            Wait.Visible = False
            AdditionalData = True
            UpdateDisplay()
        End Sub

        Private Sub UpdateDisplay()
            If Filter Is Nothing Then Return

            Splitter.Panel2Collapsed = Not (Wait.IsRunning OrElse (Filter.IsPrivate AndAlso Filter.Pattern Is Nothing))
            PrivateFilter.Visible = (Filter.IsPrivate AndAlso Filter.Pattern Is Nothing)

            Views.BeginUpdate()

            If Filter.Actions.Contains("tag") Then
                If Not Views.Items.Contains(Msg("view-abusefilter-tags")) _
                    Then Views.Items.Add(Msg("view-abusefilter-tags"))
            Else
                If Views.Items.Contains(Msg("view-abusefilter-tags")) _
                    Then Views.Items.Remove(Msg("view-abusefilter-tags"))
            End If

            If (Filter.IsPrivate AndAlso Filter.Pattern Is Nothing) Then
                If Views.Items.Contains(Msg("view-abusefilter-pattern")) _
                    Then Views.Items.Remove(Msg("view-abusefilter-pattern"))
                If Views.Items.Contains(Msg("view-abusefilter-notes")) _
                    Then Views.Items.Remove(Msg("view-abusefilter-notes"))
            Else
                If Not Views.Items.Contains(Msg("view-abusefilter-pattern")) _
                    Then Views.Items.Add(Msg("view-abusefilter-pattern"))
                If Not Views.Items.Contains(Msg("view-abusefilter-notes")) _
                    Then Views.Items.Add(Msg("view-abusefilter-notes"))
            End If
            
            If Views.SelectedIndex = -1 Then Views.SelectedIndex = 0
            Views.EndUpdate()

            EditFilter.Visible = Session.User.HasRight("abusefilter-modify")

            Actions.Text = Msg("view-abusefilter-actions",
                If(Filter.Actions.Count = 0, Msg("a-none"), Filter.Actions.Join(", ")))
            Description.Text = Filter.Description
            Modified.Text = Msg("view-abusefilter-modified",
                Filter.LastModifiedBy.Name, FullDateString(Filter.LastModified))
            Notes.Text = If(Filter.Notes Is Nothing, Nothing, Filter.Notes.TrimEnd(CR, LF))
            Pattern.Text = If(Filter.Pattern Is Nothing, Nothing, Filter.Pattern.TrimEnd(CR, LF))

            RateLimit.Visible = (Filter.RateLimit IsNot Nothing)
            If RateLimit.Visible Then RateLimit.Text = Msg("view-abusefilter-ratelimit",
                If(Filter.RateLimit Is Huggle.RateLimit.None, Msg("a-none"), Filter.RateLimit.Description))

            Dim rows As New List(Of String())

            If Filter.Tags IsNot Nothing Then
                For Each tag As String In Filter.Tags
                    rows.Add({tag})
                Next tag
            End If

            TagsList.SetItems(rows)

            If Filter.Tags Is Nothing Then
                TagsList.Visible = False
                TagDetail.Text = Msg("view-abusefilter-unknowntags")

            ElseIf Filter.Tags.Count = 0 Then
                TagsList.Visible = False
                TagDetail.Text = Msg("view-abusefilter-notags")

            Else
                TagsList.Visible = True
                TagDetail.Text = Msg("view-abusefilter-tagdetail")
            End If

            Status.Text = Msg("view-abusefilter-status", AbuseFilterView.GetFilterStatus(Filter))
        End Sub

        Private Sub EditFilter_LinkClicked() Handles EditFilter.LinkClicked
            If Filter IsNot Nothing Then
                Dim form As New AbuseFilterEditForm(Session, Filter)
                form.Show()
            End If
        End Sub

    End Class

End Namespace