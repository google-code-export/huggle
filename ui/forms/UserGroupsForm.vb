Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class UserGroupsForm : Inherits HuggleForm

        Private Session As Session
        Private User As User

        Friend Sub New(ByVal session As Session, ByVal user As User)
            InitializeComponent()

            If session Is Nothing Then Throw New ArgumentNullException("session")
            If user Is Nothing Then Throw New ArgumentNullException("user")

            Me.Session = session
            Me.User = user
        End Sub

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Msg("usergroups-title", User.FullName)

                For Each group As UserGroup In User.Wiki.UserGroups.All
                    If Not User.IsInGroup(group) AndAlso Session.User.GroupChanges(group).CanAdd _
                        OrElse (User Is Session.User AndAlso Session.User.GroupChanges(group).CanAddSelf) _
                        Then AvailableGroups.Items.Add(group)

                    If User.IsInGroup(group) AndAlso Session.User.GroupChanges(group).CanRemove _
                        OrElse (User Is Session.User AndAlso Session.User.GroupChanges(group).CanRemoveSelf) _
                        Then SelectedGroups.Items.Add(group)
                Next group

                SelectedEmptyLabel.Visible = (SelectedGroups.Items.Count = 0)
                AvailableEmptyLabel.Visible = (AvailableGroups.Items.Count = 0)

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Private Sub OK_Click() Handles OK.Click
            Dim action As New Actions.UserRights(Session, User, Comment.Text)

            action.RemoveGroups = New List(Of UserGroup)
            action.AddGroups = New List(Of UserGroup)

            For Each groupObject As Object In AvailableGroups.Items
                Dim group As UserGroup = CType(groupObject, UserGroup)
                If User.IsInGroup(group) Then action.RemoveGroups.Add(group)
            Next groupObject

            For Each groupObject As Object In SelectedGroups.Items
                Dim group As UserGroup = CType(groupObject, UserGroup)
                If Not User.IsInGroup(group) Then action.AddGroups.Add(group)
            Next groupObject

            App.UserWaitForProcess(action)
            If action.IsFailed Then App.ShowError(action.Result)
            Close()
        End Sub

        Private Sub AvailableGroups_SelectedIndexChanged() Handles AvailableGroups.SelectedIndexChanged
            AddGroup.Enabled = (AvailableGroups.SelectedIndex > -1)
        End Sub

        Private Sub SelectedGroups_SelectedIndexChanged() Handles SelectedGroups.SelectedIndexChanged
            RemoveGroup.Enabled = (SelectedGroups.SelectedIndex > -1)
        End Sub

        Private Sub AddGroup_Click() Handles AddGroup.Click
            For Each item As Object In AvailableGroups.SelectedItems.ToList(Of Object)()
                SelectedGroups.Items.Add(item)
                AvailableGroups.Items.Remove(item)
            Next item
        End Sub

        Private Sub RemoveGroup_Click() Handles RemoveGroup.Click
            For Each item As Object In SelectedGroups.SelectedItems.ToList(Of Object)()
                AvailableGroups.Items.Add(item)
                SelectedGroups.Items.Remove(item)
            Next item
        End Sub

    End Class

End Namespace