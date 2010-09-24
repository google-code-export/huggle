Imports Huggle
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Public Class ExtensionView : Inherits Viewer

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        InitializeComponent()
    End Sub

    Private Current As Extension

    Private Sub _Load() Handles Me.Load
        List.BeginUpdate()
        List.Items.Clear()

        For Each extension As Extension In Wiki.Extensions.All
            List.AddRow(extension.Name, Msg("view-extension-type" & If(extension.Type, "general")), extension.Version)
        Next extension

        List.EndUpdate()
        List.SortBy(0)
        Count.Text = Msg("a-count", List.Items.Count)
    End Sub

    Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
        If List.SelectedItems.Count = 0 Then
            Current = Nothing
        Else
            Image.Visible = True
            Properties.Visible = True

            Current = Wiki.Extensions(List.SelectedItems(0).Text)
            ExtensionName.Text = Current.Name
            Description.Text = If(Current.Description Is Nothing, _
                Msg("view-general-nodescription"), WikiStripSummary(HtmlDecode(Current.Description)))
            If Current.Version IsNot Nothing Then Version.Text = Msg("view-extension-version", Current.Version)
            If Current.Author IsNot Nothing Then Author.Text = _
                Msg("view-extension-" & If(Current.Author.Contains(","), "authors", "author"), Current.Author)

            Author.Visible = (Current.Author IsNot Nothing)
            Version.Visible = (Current.Version IsNot Nothing)

            If Current.Url Is Nothing Then ExtensionName.LinkArea = New LinkArea(0, 0) _
                Else ExtensionName.LinkArea = New LinkArea(0, ExtensionName.Text.Length)
        End If
    End Sub

    Private Sub ExtensionName_LinkClicked() Handles ExtensionName.LinkClicked
        If Current.Url IsNot Nothing Then OpenWebBrowser(Current.Url)
    End Sub

End Class
