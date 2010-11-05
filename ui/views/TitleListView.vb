Namespace Huggle.UI

    Friend Class TitleListView : Inherits Viewer

        Friend Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            If Wiki.TitleList Is Nothing OrElse Wiki.TitleList.Entries Is Nothing OrElse Wiki.TitleList.Entries.Count = 0 Then
                Entries.Clear()
                EmptyNote.Visible = True
                EditBlacklist.Visible = (Wiki.TitleList IsNot Nothing)

                If Wiki.Family IsNot Nothing AndAlso Wiki.Family.GlobalTitleBlacklist IsNot Nothing Then
                    GlobalListNote.Text = Msg("view-titleblacklist-global", Wiki.Family.Name)
                    GlobalListNote.Visible = True
                End If
            Else
                Filter.Enabled = True
                Filter.Items.AddRange({
                    Msg("view-titleblacklist-all"),
                    Msg("view-titleblacklist-create"),
                    Msg("view-titleblacklist-move"),
                    Msg("view-titleblacklist-newuser"),
                    Msg("view-titleblacklist-upload"),
                    Msg("view-titleblacklist-reupload")})

                Filter.SelectedIndex = 0
            End If
        End Sub

        Private Sub Filter_SelectedIndexChanged() Handles Filter.SelectedIndexChanged
            If Not Filter.Enabled Then Return

            Entries.BeginUpdate()
            Entries.Clear()

            For Each entry As TitleListEntry In Wiki.TitleList.Entries
                Select Case Filter.SelectedItem.ToString
                    Case Msg("view-titleblacklist-edit")
                        If Not entry.Options.Contains(TitleListOption.NoEdit) Then Continue For

                    Case Msg("view-titleblacklist-create")
                        If entry.Options.Contains(TitleListOption.MoveOnly) OrElse
                            entry.Options.Contains(TitleListOption.NewAccountOnly) Then Continue For

                    Case Msg("view-titleblacklist-move")
                        If entry.Options.Contains(TitleListOption.NewAccountOnly) Then Continue For

                    Case Msg("view-titleblacklist-newuser")
                        If entry.Options.Contains(TitleListOption.MoveOnly) Then Continue For

                    Case Msg("view-titleblacklist-upload")
                        If entry.Options.Contains(TitleListOption.MoveOnly) OrElse
                            entry.Options.Contains(TitleListOption.NewAccountOnly) Then Continue For

                    Case Msg("view-titleblacklist-reupload")
                        If entry.Options.Contains(TitleListOption.MoveOnly) OrElse
                            entry.Options.Contains(TitleListOption.NewAccountOnly) OrElse
                            entry.Options.Contains(TitleListOption.ReUpload) Then Continue For
                End Select

                Entries.AddRow(entry.Pattern, entry.Options.ToStringArray.Join(", "), entry.ErrorMessage)
            Next entry

            Entries.EndUpdate()
            Count.Text = Msg("a-count", Entries.Items.Count)
        End Sub

        Private Sub EditBlacklist_LinkClicked() Handles EditBlacklist.LinkClicked
            OpenWebBrowser(Wiki.TitleList.Location.Url)
        End Sub

    End Class

End Namespace