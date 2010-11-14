Imports System.Collections.Generic
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class ExtensionView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Current As Extension

        Private Sub _Load() Handles Me.Load
            Dim types As New List(Of String)

            For Each extension As Extension In Wiki.Extensions.All
                types.Merge(Msg("view-extension-type-" & extension.Type))
            Next extension

            Type.BeginUpdate()
            Type.Items.Add(Msg("view-extension-type-all"))
            Type.Items.AddRange(types.ToArray)
            Type.ResizeDropDown()
            Type.SelectedIndex = 0
            Type.EndUpdate()

            PopulateList()
        End Sub

        Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
            If List.HasSelectedItems Then
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

        Private Sub PopulateList() Handles Type.SelectedIndexChanged
            Dim rows As New List(Of String())

            For Each extension As Extension In Wiki.Extensions.All
                Dim extensionType As String = Msg("view-extension-type-" & extension.Type)

                If Type.SelectedIndex = 0 OrElse extensionType = Type.SelectedItem.ToString _
                    Then rows.Add({extension.Name, extensionType, extension.Version})
            Next extension

            List.SetItems(rows)
            Count.Text = Msg("view-misc-count", List.Items.Count)
        End Sub

    End Class

End Namespace