Imports System
Imports System.Drawing

Namespace Huggle.UI
    Public Class GeneralWikiView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            'Logo
            Dim logo As File = Wiki.Files.FromString(Wiki.Config.Logo)

            If logo IsNot Nothing Then
                Try
                    If logo.ThumbKnown(135) Then
                        WikiLogo.Image = New Bitmap(logo.Thumb(135))
                        WikiLogo.Height = WikiLogo.Image.Height
                    End If

                Catch ex As SystemException
                    Log.Write("Logo for {0} not in recognized format".FormatWith(Wiki.Code))
                End Try
            End If

            'General properties
            Dim engineString As String = Wiki.Engine _
                & If(Wiki.Config.EngineVersion Is Nothing, "", " " & Wiki.Config.EngineVersion) _
                & If(Wiki.Config.EngineRevision = 0, "", " (r" & Wiki.Config.EngineRevision & ")")

            Dim platformString As String = Wiki.Config.PlatformName _
                & If(Wiki.Config.PlatformVersion Is Nothing, "", " " & Wiki.Config.PlatformVersion)

            Dim databaseString As String = Wiki.Config.Database _
                & If(Wiki.Config.DatabaseVersion Is Nothing, "", " " & Wiki.Config.DatabaseVersion)

            Dim secureServerString As String =
                If(Wiki.Url.Scheme = "https", Msg("a-yes"),
                If(Wiki.SecureUrl IsNot Nothing AndAlso Wiki.SecureUrl.Scheme = "https", Msg("a-optional"), Msg("a-no")))

            WikiName.Text = Wiki.Name
            Family.Text = Msg("view-wikigeneral-family", If(Wiki.Family Is Nothing, Msg("a-none"), Wiki.Family.Name))
            MainPage.Text = Msg("view-wikigeneral-mainpage", If(Wiki.MainPage Is Nothing, Msg("a-unknown"), Wiki.MainPage.Title))
            ContentLanguage.Text = Msg("view-wikigeneral-language", If(Wiki.Language Is Nothing, Msg("a-unknown"), Wiki.Language.Name))
            ContentLicense.Text = Msg("view-wikigeneral-license", If(Wiki.License Is Nothing, Msg("a-unknown"), Wiki.License))
            SecureServer.Text = Msg("view-wikigeneral-secure", secureServerString)
            Engine.Text = Msg("view-wikigeneral-engine", engineString)
            Platform.Text = Msg("view-wikigeneral-platform", platformString)
            Database.Text = Msg("view-wikigeneral-database", databaseString)

            'Statistics
            StatisticsList.BeginUpdate()
            StatisticsList.Items.Clear()
            StatisticsList.AddRow(Msg("view-wikistats-pages"), If(Wiki.Pages.Count < 0, Msg("a-unknown"), Wiki.Pages.Count.ToString))
            StatisticsList.AddRow(Msg("view-wikistats-contentpages"), If(Wiki.ContentPages < 0, Msg("a-unknown"), Wiki.ContentPages.ToString))
            StatisticsList.AddRow(Msg("view-wikistats-files"), If(Wiki.Files.Count < 0, Msg("a-unknown"), Wiki.Files.Count.ToString))
            StatisticsList.AddRow(Msg("view-wikistats-revisions"), If(Wiki.Revisions.Count < 0, Msg("a-unknown"), Wiki.Revisions.Count.ToString))
            StatisticsList.AddRow(Msg("view-wikistats-users"), If(Wiki.Users.Count < 0, Msg("a-unknown"), Wiki.Users.Count.ToString))
            StatisticsList.AddRow(Msg("view-wikistats-activeusers"), If(Wiki.ActiveUsers < 0, Msg("a-unknown"), Wiki.ActiveUsers.ToString))
            StatisticsList.EndUpdate()
        End Sub

        Private Sub WikiName_LinkClicked() Handles WikiName.LinkClicked
            OpenWebBrowser(Wiki.HomeUrl)
        End Sub

    End Class
End Namespace