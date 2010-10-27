Namespace Huggle.UI

    Public Class SpamListView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            If Wiki.SpamLists.All.Count = 0 Then
                Entries.Clear()
                EmptyNote.Visible = True
                EditBlacklist.Visible = (Wiki.Extensions.Contains("SpamBlacklist"))

                If Wiki.Family IsNot Nothing AndAlso Wiki.Family.CentralWiki.Extensions.Contains("SpamBlacklist") Then
                    GlobalListNote.Text = Msg("view-spamblacklist-global", Wiki.Family.Name)
                    GlobalListNote.Visible = True
                End If
            Else
                TypeFilter.Enabled = True
                TypeFilter.Items.AddRange({
                    Msg("view-spamblacklist-all"),
                    Msg("view-spamblacklist-blacklist"),
                    Msg("view-spamblacklist-whitelist")})

                If Wiki.Config.CustomSpamBlacklists.Count > 0 Then TypeFilter.Items.Add(Msg("view-spamblacklist-custom"))
                TypeFilter.SelectedIndex = 0
            End If
        End Sub

        Private Sub Filter_SelectedIndexChanged() Handles TypeFilter.SelectedIndexChanged
            If Not TypeFilter.Enabled Then Return

            Entries.BeginUpdate()
            Entries.Clear()



            Entries.EndUpdate()
            Count.Text = Msg("a-count", Entries.Items.Count)
        End Sub

        Private Sub EditBlacklist_LinkClicked() Handles EditBlacklist.LinkClicked

        End Sub

        Private Sub Filter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeFilter.SelectedIndexChanged

        End Sub
    End Class

End Namespace