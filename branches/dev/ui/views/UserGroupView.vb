Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class UserGroupView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            GroupList.BeginUpdate()
            GroupList.Items.Clear()

            For Each group As UserGroup In Wiki.UserGroups.All
                Dim groupCountString As String

                If group.IsImplicit Then groupCountString = Msg("view-usergroup-implicit") _
                    Else groupCountString = If(group.Count < 0, Msg("a-unknown"), group.Count.ToStringForUser)

                GroupList.AddRow(group.Description, groupCountString)
            Next group

            GroupList.SortMethods(1) = SortMethod.Integer
            GroupList.SortBy(0)
            GroupList.SelectedIndices.Add(0)
            GroupList.EndUpdate()

            GroupCount.Text = Msg("a-count", GroupList.Items.Count)
        End Sub

        Private Sub List_SelectedIndexChanged() Handles GroupList.SelectedIndexChanged
            If GroupList.SelectedItems.Count > 0 Then
                Dim selectedGroup As UserGroup = Nothing

                For Each group As UserGroup In Wiki.UserGroups.All
                    If group.Description = GroupList.SelectedItems(0).Text Then
                        selectedGroup = group
                        Exit For
                    End If
                Next group

                If selectedGroup IsNot Nothing Then
                    GroupName.Text = selectedGroup.Description

                    RightsList.BeginUpdate()
                    RightsList.Items.Clear()

                    For Each right As String In selectedGroup.Rights
                        RightsList.AddRow(right)
                    Next right

                    RightsCount.Text = Msg("a-count", RightsList.Items.Count)
                    RightsList.EndUpdate()
                End If
            End If
        End Sub

    End Class

End Namespace