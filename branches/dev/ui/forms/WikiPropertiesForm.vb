Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class WikiPropertiesForm : Inherits HuggleForm

        Private LoadedViews As New List(Of Viewer)
        Private Session As Session

        Private WithEvents AbuseFilterView As AbuseFilterView
        Private WithEvents ChangeTagView As ChangeTagView
        Private WithEvents ExtensionView As ExtensionView
        Private WithEvents GadgetView As GadgetView
        Private WithEvents GeneralWikiView As GeneralWikiView
        Private WithEvents ModerationView As ModerationView
        Private WithEvents NamespaceView As NamespaceView
        Private WithEvents SpamListView As SpamListView
        Private WithEvents TitleListView As TitleListView
        Private WithEvents UserGroupView As UserGroupView
        Private WithEvents UserRightView As UserRightView

        Friend Sub New(ByVal session As Session)
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
                If Wiki.Extensions.Contains(Extension.SpamList) Then Views.Items.Add(Msg("view-spamlist-title"))
                If Wiki.Extensions.Contains(Extension.TitleList) Then Views.Items.Add(Msg("view-titlelist-title"))

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
                    If GeneralWikiView Is Nothing Then GeneralWikiView = New GeneralWikiView(Session)
                    SwitchTo(GeneralWikiView)

                Case Msg("view-namespace-title")
                    If NamespaceView Is Nothing Then NamespaceView = New NamespaceView(Session)
                    SwitchTo(NamespaceView)

                Case Msg("view-usergroup-title")
                    If UserGroupView Is Nothing Then UserGroupView = New UserGroupView(Session)
                    SwitchTo(UserGroupView)

                Case Msg("view-userright-title")
                    If UserRightView Is Nothing Then UserRightView = New UserRightView(Session)
                    SwitchTo(UserRightView)

                Case Msg("view-extension-title")
                    If ExtensionView Is Nothing Then ExtensionView = New ExtensionView(Session)
                    SwitchTo(ExtensionView)

                Case Msg("view-titlelist-title")
                    If TitleListView Is Nothing Then TitleListView = New TitleListView(Session)
                    SwitchTo(TitleListView)

                Case Msg("view-gadget-title")
                    If GadgetView Is Nothing Then GadgetView = New GadgetView(Session)
                    SwitchTo(GadgetView)

                Case Msg("view-moderation-title")
                    If ModerationView Is Nothing Then ModerationView = New ModerationView(Session)
                    SwitchTo(ModerationView)

                Case Msg("view-changetag-title")
                    If ChangeTagView Is Nothing Then ChangeTagView = New ChangeTagView(Session)
                    SwitchTo(ChangeTagView)

                Case Msg("view-abusefilter-title")
                    If AbuseFilterView Is Nothing Then AbuseFilterView = New AbuseFilterView(Session)
                    SwitchTo(AbuseFilterView)

                Case Msg("view-spamlist-title")
                    If SpamListView Is Nothing Then SpamListView = New SpamListView(Session)
                    SwitchTo(SpamListView)
            End Select
        End Sub

        Private Sub SwitchTo(ByVal view As Viewer)
            ViewContainer.SuspendLayout()

            For Each control As Control In ViewContainer.Controls
                control.Visible = (control Is view)
            Next control

            If Not ViewContainer.Controls.Contains(view) Then ViewContainer.Controls.Add(view)

            ViewContainer.ResumeLayout()
        End Sub

        Private Sub Views_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles Views.DrawItem
            e.DrawBackground()
            e.DrawFocusRectangle()

            TextRenderer.DrawText(e.Graphics, Views.Items(e.Index).ToString,
                Views.Font, e.Bounds, e.ForeColor, e.BackColor, TextFormatFlags.VerticalCenter)
        End Sub

        Private Sub ViewRight(ByVal sender As Object, ByVal e As EventArgs(Of String)) Handles UserGroupView.ViewRight
            Views.SelectedItem = Msg("view-userright-title")
            UserRightView.SelectedRight = e.Value
        End Sub

    End Class

End Namespace