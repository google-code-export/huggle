Imports Huggle
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class FamilyPropertiesForm

        Private LoadedViews As New List(Of Viewer)
        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Size = New Size(720, 480)
            Me.Session = session
        End Sub

        Private ReadOnly Property Family As Family
            Get
                Return Session.Wiki.Family
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Msg("view-family-title", Family.Name)
                App.Languages.Current.Localize(Me)

                'Core MediaWiki views
                Views.Items.AddRange({
                    Msg("view-familygeneral-title"),
                    Msg("view-globalgroups-title")})

                Views.SelectedIndex = 0

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

        Private Sub Views_SelectedIndexChanged() Handles Views.SelectedIndexChanged
            Select Case Views.SelectedItem.ToString
                Case Msg("view-wikigeneral-title")
                    If Not LoadedViews.ContainsInstance(Of GeneralWikiView)() Then LoadedViews.Add(New GeneralWikiView(Session))
                    ViewInstance(Of GeneralWikiView)()

                Case Msg("view-namespace-title")
                    If Not LoadedViews.ContainsInstance(Of NamespaceView)() Then LoadedViews.Add(New NamespaceView(Session))
                    ViewInstance(Of NamespaceView)()

                Case Msg("view-usergroup-title")
                    If Not LoadedViews.ContainsInstance(Of UserGroupView)() Then LoadedViews.Add(New UserGroupView(Session))
                    ViewInstance(Of UserGroupView)()

                Case Msg("view-userright-title")
                    If Not LoadedViews.ContainsInstance(Of UserRightsView)() Then LoadedViews.Add(New UserRightsView(Session))
                    ViewInstance(Of UserRightsView)()

                Case Msg("view-extension-title")
                    If Not LoadedViews.ContainsInstance(Of ExtensionView)() Then LoadedViews.Add(New ExtensionView(Session))
                    ViewInstance(Of ExtensionView)()

                Case Msg("view-titleblacklist-title")
                    If Not LoadedViews.ContainsInstance(Of TitleBlacklistView)() Then LoadedViews.Add(New TitleBlacklistView(Session))
                    ViewInstance(Of TitleBlacklistView)()

                Case Msg("view-gadget-title")
                    If Not LoadedViews.ContainsInstance(Of GadgetView)() Then LoadedViews.Add(New GadgetView(Session))
                    ViewInstance(Of GadgetView)()

                Case Msg("view-moderation-title")
                    If Not LoadedViews.ContainsInstance(Of ModerationView)() Then LoadedViews.Add(New ModerationView(Session))
                    ViewInstance(Of ModerationView)()

                Case Msg("view-changetag-title")
                    If Not LoadedViews.ContainsInstance(Of ChangeTagView)() Then LoadedViews.Add(New ChangeTagView(Session))
                    ViewInstance(Of ChangeTagView)()

                Case Msg("view-abusefilter-title")
                    If Not LoadedViews.ContainsInstance(Of AbuseFilterView)() Then LoadedViews.Add(New AbuseFilterView(Session))
                    ViewInstance(Of AbuseFilterView)()

                Case Msg("view-spamblacklist-title")
                    If Not LoadedViews.ContainsInstance(Of SpamList)() Then LoadedViews.Add(New SpamListView(Session))
                    ViewInstance(Of SpamListView)()
            End Select
        End Sub

        Private Sub ViewInstance(Of T As Viewer)()
            ViewContainer.Controls.Clear()
            ViewContainer.Controls.Add(LoadedViews.FirstInstance(Of T))
        End Sub

        Private Sub Views_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles Views.DrawItem
            e.DrawBackground()
            e.DrawFocusRectangle()
            e.Graphics.DrawString(Views.Items(e.Index).ToString, Views.Font, _
                New Pen(e.ForeColor).Brush, e.Bounds, New StringFormat With {.LineAlignment = Drawing.StringAlignment.Center})
        End Sub

    End Class

End Namespace