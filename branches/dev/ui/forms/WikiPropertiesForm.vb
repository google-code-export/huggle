Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class WikiPropertiesForm

        Private LoadedViews As New List(Of Viewer)
        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Size = New Size(720, 480)
            Me.Session = session
        End Sub

        Private ReadOnly Property Wiki As Wiki
            Get
                Return Session.Wiki
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                'Finish loading extra config
                If Not Wiki.Config.ExtraConfigLoaded Then
                    App.UserWaitForProcess(Wiki.Config.ExtraLoader)
                    If Wiki.Config.ExtraLoader.IsFailed Then Close() : Return
                End If

                Icon = Resources.Icon
                Text = Msg("wikiprop-title", Wiki.Name)
                App.Languages.Current.Localize(Me)

                'Core MediaWiki views
                Views.Items.AddRange({
                    Msg("view-wikigeneral-title"),
                    Msg("view-namespace-title"),
                    Msg("view-usergroup-title"),
                    Msg("view-userright-title"),
                    Msg("view-extension-title")})

                Views.SelectedIndex = 0

                'Extension views
                If Wiki.Extensions.Contains(Extension.Moderation) Then Views.Items.Add(Msg("view-moderation-title"))
                If Wiki.Extensions.Contains(Extension.Gadgets) Then Views.Items.Add(Msg("view-gadget-title"))
                If Wiki.Extensions.Contains(Extension.SpamList) Then Views.Items.Add(Msg("view-spamblacklist-title"))
                If Wiki.Extensions.Contains(Extension.TitleList) Then Views.Items.Add(Msg("view-titleblacklist-title"))

                If Wiki.Extensions.Contains(Extension.AbuseFilter) Then
                    Views.Items.Add(Msg("view-abusefilter-title"))
                    Views.Items.Add(Msg("view-changetag-title"))
                End If

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