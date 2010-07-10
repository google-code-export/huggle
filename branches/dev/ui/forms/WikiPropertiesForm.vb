Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Web.HttpUtility
Imports System.Windows.Forms

Public Class WikiPropertiesForm

    Private _Wiki As Wiki

    Private CurrentExtension As Extension
    Private CurrentGadget As Gadget

    Public Sub New(ByVal wiki As Wiki)
        InitializeComponent()
        _Wiki = wiki
    End Sub

    Public ReadOnly Property Wiki() As Wiki
        Get
            Return _Wiki
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

            Dim logo As Media = Wiki.Media.FromString(Wiki.Config.Logo)

            Try
                If logo.ThumbKnown(96) Then WikiLogo.Image = New Bitmap(logo.Thumb(96))
            Catch ex As SystemException
                Log.Write("Logo for {0} not in recognized format".FormatWith(Wiki.Code))
            End Try

            If Wiki.Media.FromString(Wiki.Config.Logo).ThumbKnown(96) Then

            End If

            WikiName.Text = Wiki.Name

            'Hide tabs that aren't relevant for the selected wiki
            If Not Wiki.Extensions.Contains("Gadgets") Then Tabs.TabPages.RemoveAt(7)
            If Not Wiki.Extensions.Contains("Flagged Revisions") Then Tabs.TabPages.RemoveAt(6)

            If Not Wiki.Extensions.Contains("Abuse Filter") Then
                Tabs.TabPages.RemoveAt(5)
                Tabs.TabPages.RemoveAt(4)
            End If

            Dim engineString As String = Wiki.Engine _
                & If(Wiki.Config.EngineVersion Is Nothing, "", " " & Wiki.Config.EngineVersion) _
                & If(Wiki.Config.EngineRevision = 0, "", " (r" & Wiki.Config.EngineRevision & ")")

            Dim languageString As String = Wiki.Config.LanguageName _
                & If(Wiki.Config.LanguageVersion Is Nothing, "", " " & Wiki.Config.LanguageVersion)

            Dim databaseString As String = Wiki.Config.Database _
                & If(Wiki.Config.DatabaseVersion Is Nothing, "", " " & Wiki.Config.DatabaseVersion)

            GeneralList.BeginUpdate()
            GeneralList.Items.Clear()
            GeneralList.AddRow(Msg("wikiprop-name"), Wiki.Name)
            GeneralList.AddRow(Msg("wikiprop-language"), If(Wiki.Language Is Nothing, "(" & Msg("a-unknown") & ")", Wiki.Language.Name))
            GeneralList.AddRow(Msg("wikiprop-family"), If(Wiki.Family Is Nothing, Msg("a-none"), Wiki.Family.Name))
            GeneralList.AddRow(Msg("wikiprop-secure"), If(Wiki.SecureUrl Is Nothing, Msg("a-no"), Msg("a-yes")))
            GeneralList.AddRow(Msg("wikiprop-license"), If(Wiki.License Is Nothing, "(" & Msg("a-unknown") & ")", Wiki.License))
            GeneralList.AddRow(Msg("wikiprop-type"), Wiki.Type)
            GeneralList.AddRow(Msg("wikiprop-engine"), engineString)
            GeneralList.AddRow(Msg("wikiprop-language"), languageString)
            GeneralList.AddRow(Msg("wikiprop-database"), databaseString)
            GeneralList.AddRow(Msg("wikiprop-mainpage", If(Wiki.MainPage Is Nothing, "(" & Msg("a-unknown") & ")", Wiki.MainPage.Title)))
            GeneralList.EndUpdate()

            SpacesList.BeginUpdate()
            SpacesList.Items.Clear()

            For Each space As Space In Wiki.Spaces.All
                Dim props As New List(Of String)
                If space.IsSpecial Then props.Add("special")
                If space.IsContent Then props.Add("content")
                If space.IsCustom Then props.Add("custom")
                If space.IsEditRestricted Then props.Add("protected")
                If Not space.IsEditRestricted AndAlso space.IsMoveRestricted Then props.Add("move-protected")
                If Not space.IsMovable Then props.Add("unmovable")
                If space.HasSubpages Then props.Add("subpages")
                SpacesList.AddRow(CStr(space.Number), space.Name, props.Join(", "))
            Next space

            SpacesList.EndUpdate()
            SpacesList.SortMethods(0) = "integer"
            SpacesList.SortBy(0)
            SpacesCount.Text = Msg("a-count", SpacesList.Items.Count)

            ExtensionsList.BeginUpdate()
            ExtensionsList.Items.Clear()

            For Each extension As Extension In Wiki.Extensions.All
                ExtensionsList.AddRow(extension.Name, extension.Type)
            Next extension

            ExtensionsList.EndUpdate()
            ExtensionsList.SortBy(0)
            ExtensionsCount.Text = Msg("a-count", ExtensionsList.Items.Count)

            UserGroupsList.BeginUpdate()
            UserGroupsList.Items.Clear()

            For Each group As UserGroup In Wiki.UserGroups.All
                UserGroupsList.AddRow(group.Name, group.Rights.Join(", "))
            Next group

            UserGroupsList.EndUpdate()
            UserGroupsList.SortBy(0)
            UserGroupsCount.Text = Msg("a-count", UserGroupsList.Items.Count)

            ChangeTagsList.BeginUpdate()
            ChangeTagsList.Items.Clear()

            For Each tag As ChangeTag In Wiki.ChangeTags.All
                Dim displayName As String = If(Wiki.Config.ChangeTagIdentifier Is Nothing, _
                    tag.DisplayName, tag.DisplayName.Remove(Wiki.Config.ChangeTagIdentifier).Trim)

                ChangeTagsList.AddRow(WikiStripMarkup(displayName), WikiStripMarkup(tag.Description), CStr(tag.Hits))
            Next tag

            ChangeTagsList.SortMethods(2) = "integer"
            ChangeTagsList.SortBy(0)
            ChangeTagsList.EndUpdate()
            ChangeTagsCount.Text = Msg("a-count", ChangeTagsList.Items.Count)

            FlagsList.BeginUpdate()
            FlagsList.Items.Clear()

            For Each flag As ReviewFlag In Wiki.ReviewFlags.All
                FlagsList.AddRow(flag.Name, flag.DisplayName, CStr(flag.Levels), CStr(flag.QualityLevel), CStr(flag.PristineLevel))
            Next flag

            FlagsList.EndUpdate()
            FlagsList.SortBy(0)
            FlagsCount.Text = Msg("a-count", FlagsList.Items.Count)

            GadgetsList.BeginUpdate()
            GadgetsList.Items.Clear()

            For Each gadget As Gadget In Wiki.Gadgets.All
                GadgetsList.AddRow(UcFirst(gadget.Name.Replace("_", " ")), If(gadget.TypeDesc, gadget.Type), WikiStripSummary(HtmlDecode(gadget.Description)))
            Next gadget

            GadgetsList.EndUpdate()
            GadgetsList.SortBy(0)
            GadgetsCount.Text = Msg("a-count", GadgetsList.Items.Count)

        Catch ex As SystemException
            App.ShowError(Result.FromException(ex))
            Close()
        End Try
    End Sub

    Private Sub _Resize() Handles Me.Resize
        GeneralList.Columns(1).Width = GeneralList.Width - 22 - GeneralList.Columns(0).Width
        SpacesList.Columns(1).Width = SpacesList.Width - 22 - SpacesList.Columns(0).Width - SpacesList.Columns(2).Width
        ExtensionsList.Columns(0).Width = ExtensionsList.Width - 22 - ExtensionsList.Columns(1).Width - ExtensionsList.Columns(2).Width
        UserGroupsList.Columns(1).Width = UserGroupsList.Width - 22 - UserGroupsList.Columns(0).Width
        ChangeTagsList.Columns(1).Width = ChangeTagsList.Width - 22 - ChangeTagsList.Columns(0).Width - ChangeTagsList.Columns(2).Width
        GadgetsList.Columns(2).Width = GadgetsList.Width - 22 - GadgetsList.Columns(0).Width - GadgetsList.Columns(1).Width
    End Sub

    Private Sub ExtensionsList_SelectedIndexChanged() Handles ExtensionsList.SelectedIndexChanged
        If ExtensionsList.SelectedItems.Count = 0 Then
            CurrentExtension = Nothing
        Else
            ExtImage.Visible = True
            ExtProps.Visible = True

            CurrentExtension = Wiki.Extensions(ExtensionsList.SelectedItems(0).Text)
            ExtName.Text = CurrentExtension.Name
            ExtDescription.Text = If(String.IsNullOrEmpty(CurrentExtension.Description), _
                Msg("wikiprop-nodescription"), WikiStripSummary(HtmlDecode(CurrentExtension.Description)))
            If CurrentExtension.Version IsNot Nothing Then ExtVersion.Text = Msg("wikiprop-ext-version", CurrentExtension.Version)
            If CurrentExtension.Author IsNot Nothing Then ExtAuthor.Text = _
                Msg("wikiprop-ext-" & If(CurrentExtension.Author.Contains(","), "authors", "author"), CurrentExtension.Author)

            ExtAuthor.Visible = Not String.IsNullOrEmpty(CurrentExtension.Author)
            ExtVersion.Visible = Not String.IsNullOrEmpty(CurrentExtension.Version)

            If CurrentExtension.Url Is Nothing Then ExtName.LinkArea = New LinkArea(0, 0) _
                Else ExtName.LinkArea = New LinkArea(0, ExtName.Text.Length)
        End If
    End Sub

    Private Sub ExtName_LinkClicked() Handles ExtName.LinkClicked
        If CurrentExtension.Url IsNot Nothing Then OpenWebBrowser(CurrentExtension.Url)
    End Sub

    Private Sub GadgetsList_SelectedIndexChanged() Handles GadgetsList.SelectedIndexChanged
        If GadgetsList.SelectedItems.Count = 0 Then
            CurrentGadget = Nothing
        Else
            Dim item As ListViewItem = GadgetsList.SelectedItems(0)

            For Each gadget As Gadget In Wiki.Gadgets.All
                If UcFirst(gadget.Name) = item.Text Then CurrentGadget = gadget
            Next gadget

            GadgetName.Text = item.Text
            GadgetType.Text = Msg("wikiprop-gadget-type", item.SubItems(1).Text)
            GadgetDescription.Text = item.SubItems(2).Text

            GadgetImage.Visible = True
            GadgetProps.Visible = True
        End If
    End Sub

    Private Sub WikiName_LinkClicked() Handles WikiName.LinkClicked
        OpenWebBrowser(Wiki.HomeUrl)
    End Sub

    Private Sub GadgetName_LinkClicked() Handles GadgetName.LinkClicked
        If CurrentGadget.Pages.Count > 0 Then OpenWebBrowser(CurrentGadget.Pages(0).Url)
    End Sub

End Class