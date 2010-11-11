Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class GlobalUserPropertiesForm : Inherits HuggleForm

        Private Session As Session
        Private HoveredItem As ListViewItem.ListViewSubItem

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Me.Session = session
        End Sub

        Private ReadOnly Property GlobalUser As GlobalUser
            Get
                Return Session.User.GlobalUser
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                'Finish loading extra config
                If Not GlobalUser.Config.Extra Then
                    If GlobalUser.Config.ExtraLoader Is Nothing Then GlobalUser.Config.ExtraLoader = _
                        New ExtraGlobaluserConfig(GlobalUser)
                    App.UserWaitForProcess(GlobalUser.Config.ExtraLoader)
                    If GlobalUser.Config.ExtraLoader.IsErrored Then App.ShowError(GlobalUser.Config.ExtraLoader.Result)

                    If GlobalUser.Config.ExtraLoader.IsFailed Then
                        GlobalUser.Config.ExtraLoader = Nothing
                        Close() : Return
                    End If
                End If

                Icon = Resources.Icon
                Text = Msg("globaluserprop-title", GlobalUser.Name)

                Dim contribs As Integer = 0

                For Each wikiUser As User In GlobalUser.Users
                    If wikiUser.Contributions >= 0 Then contribs += wikiUser.Contributions
                Next wikiUser

                Username.Text = GlobalUser.Name

                UserContributions.Text = Msg("globaluserprop-contribs", contribs)
                UserCreated.Text = Msg("globaluserprop-created", If(GlobalUser.Created = Date.MinValue, Msg("a-unknown"), FullDateString(GlobalUser.Created)))
                UserHome.Text = Msg("globaluserprop-home", If(GlobalUser.Home Is Nothing, Msg("a-unknown"), GlobalUser.Home.Name))
                UserId.Text = Msg("globaluserprop-id", CStr(GlobalUser.Id))

                Dim status As New List(Of String)
                If GlobalUser.IsHidden Then status.Add(Msg("globaluserprop-hidden"))
                If GlobalUser.IsLocked Then status.Add(Msg("globaluserprop-locked"))

                UserStatus.Text = Msg("globaluserprop-status", If(status.Count = 0, Msg("globaluserprop-normal"), status.Join(", ")))

                Dim groups As New List(Of String)

                For Each group As GlobalGroup In GlobalUser.GlobalGroups
                    groups.Add(group.Name)
                Next group

                UserGroups.Text = Msg("globaluserprop-groups", If(groups.Count = 0, Msg("a-none"), groups.Join(", ")))

                Dim rows As New List(Of String())

                For Each linkedUser As User In GlobalUser.Users
                    rows.Add({linkedUser.Wiki.Name, linkedUser.UnificationMethod, _
                        FullDateString(linkedUser.UnificationDate), linkedUser.Contributions.ToStringI})
                Next linkedUser

                WikiList.SortMethods.Add(2, SortMethod.Date)
                WikiList.SortMethods.Add(3, SortMethod.Integer)
                WikiList.SetItems(rows)

                WikiCount.Text = Msg("globaluserprop-wikis", GlobalUser.Users.Count)

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

        Private Sub SetGlobalPreferences_LinkClicked() Handles SetGlobalPreferences.LinkClicked
            Dim form As New GlobalPreferencesForm(App.Sessions(GlobalUser))
            form.Show()
        End Sub

    End Class

End Namespace