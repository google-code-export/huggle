Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle.UI

    Friend Class GeneralFamilyView : Inherits Viewer

        Private Family As Family

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            Family = session.Wiki.Family
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            'Logo
            If Family.FileWiki IsNot Nothing Then
                Dim logo As File = Family.FileWiki.Files.FromString(Family.Config.Logo)

                If Not logo.ContentKnown Then
                    Dim logoQuery As New MediaQuery(App.Sessions(Family.FileWiki), logo, 96)
                    App.UserWaitForProcess(logoQuery)
                    If logoQuery.IsFailed Then Return
                End If

                Try
                    If logo.ThumbKnown(96) Then FamilyLogo.Image = New Bitmap(logo.Thumb(96))
                Catch ex As SystemException
                    Log.Write("Logo for {0} not in recognized format".FormatI(Family.Name))
                End Try
            End If

            Title.Text = Msg("familyprop-title", Family.Name)
            FamilyName.Text = Family.Name
            Count.Text = Msg("a-count", Family.Wikis.All.Count)
            CentralWiki.Text = Msg("familyprop-centralwiki", Family.CentralWiki.Name)

            Dim globalExts As New List(Of String)

            For Each globalExt As String In InternalConfig.GlobalExtensions
                If App.Sessions(Family).Wiki.Extensions.Contains(globalExt) _
                    Then globalExts.Add(App.Sessions(Family).Wiki.Extensions(globalExt).Name)
            Next globalExt

            globalExts.Sort()
            Extensions.Text = Msg("familyprop-extensions", globalExts.Join(", "))

            Dim rows As New List(Of String())

            For Each wiki As Wiki In Family.Wikis.All
                rows.Add({wiki.Name})
            Next wiki

            WikiList.SetItems(rows)
        End Sub

        Private Sub FamilyName_LinkClicked() Handles FamilyName.LinkClicked
            OpenWebBrowser(Family.CentralWiki.Url)
        End Sub

    End Class

End Namespace