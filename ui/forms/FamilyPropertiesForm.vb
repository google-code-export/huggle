Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing

Public Class FamilyPropertiesForm

    Private Family As Family

    Public Sub New(ByVal family As Family)
        InitializeComponent()
        Me.Family = family
    End Sub

    Private Sub _Load() Handles Me.Load

        If Family.FileWiki IsNot Nothing Then
            'Load logo
            Dim logo As Media = Family.FileWiki.Media.FromString(Family.Config.Logo)

            If Not logo.ContentKnown Then
                Dim logoQuery As New MediaQuery(App.Sessions(Family.FileWiki), logo, 96)
                Dim form As New WaitForm(Msg("extraconfig-progress"))
                AddHandler logoQuery.Complete, AddressOf form.CloseByProcess
                CreateThread(AddressOf logoQuery.Start)
                form.ShowDialog()
                If form.Cancelled Then Close() : Return
            End If

            Try
                If logo.ThumbKnown(96) Then FamilyLogo.Image = New Bitmap(logo.Thumb(96))
            Catch ex As SystemException
                Log.Write("Logo for {0} not in recognized format".FormatWith(Family.Name))
            End Try
        End If

        Icon = Resources.Icon
        Text = Msg("familyprop-title", Family.Name)

        FamilyName.Text = Family.Name

        WikiCount.Text = Msg("familyprop-wikis", Family.Wikis.All.Count)
        CentralWiki.Text = Msg("familyprop-centralwiki", Family.CentralWiki.Name)

        Dim globalExts As New List(Of String)

        For Each globalExt As String In Config.Internal.GlobalExtensions
            If App.Sessions(Family).Wiki.Extensions.Contains(globalExt) _
                Then globalExts.Add(App.Sessions(Family).Wiki.Extensions(globalExt).Name)
        Next globalExt

        globalExts.Sort()
        Extensions.Text = Msg("familyprop-extensions", globalExts.Join(", "))

        WikiList.BeginUpdate()
        WikiList.Items.Clear()

        For Each wiki As Wiki In Family.Wikis.All
            WikiList.AddRow(wiki.Name)
        Next wiki

        WikiList.EndUpdate()
        WikiList.SortBy(0)
    End Sub

    Private Sub WikiList_Resize() Handles WikiList.Resize
        WikiList.Columns(0).Width = WikiList.Width - 22
    End Sub

    Private Sub FamilyName_LinkClicked() Handles FamilyName.LinkClicked
        OpenWebBrowser(Family.CentralWiki.Url)
    End Sub

End Class