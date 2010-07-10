Imports Huggle
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class UserRightsForm

    Private Session As Session
    Private User As User

    Public Sub New(ByVal session As Session, ByVal user As User)
        InitializeComponent()
        Me.Session = session
        Me.User = user
    End Sub

    Private Sub _Load() Handles Me.Load
        Try
            Icon = Resources.Icon
            Text = Msg("userrights-title", User.FullName)

            For Each group As UserGroup In User.Wiki.UserGroups.All
                If Not User.IsInGroup(group) AndAlso Session.User.GroupChanges(group).CanAdd _
                    OrElse (User Is Session.User AndAlso Session.User.GroupChanges(group).CanAddSelf) _
                    Then AvailableGroups.Items.Add(group)

                If User.IsInGroup(group) AndAlso Session.User.GroupChanges(group).CanRemove _
                    OrElse (User Is Session.User AndAlso Session.User.GroupChanges(group).CanRemoveSelf) _
                    Then SelectedGroups.Items.Add(group)
            Next group

            SelectedEmptyLabel.Visible = (SelectedGroups.Items.Count > 0)
            AvailableEmptyLabel.Visible = (AvailableGroups.Items.Count > 0)

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

End Class