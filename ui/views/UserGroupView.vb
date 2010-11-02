Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class UserGroupView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Dim rights As List(Of String) = Wiki.UserRights
            rights.Sort()

            List.BeginUpdate()
            List.Items.Clear()

            For Each group As UserGroup In Wiki.UserGroups.All
                Dim groupCountString As String

                If group.IsImplicit _
                    Then groupCountString = Msg("view-usergroup-implicit") _
                    Else groupCountString = If(group.Count < 0, Msg("a-unknown"), group.Count.ToStringForUser)

                List.AddRow(group.Name, group.Description, groupCountString)
            Next group

            List.SortMethods(1) = SortMethod.Integer
            List.SortBy(0)
            List.SelectedIndices.Add(0)
            List.EndUpdate()

            Count.Text = Msg("view-usergroup-count", List.Items.Count)
        End Sub

        Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
            If List.SelectedItems.Count > 0 Then
                Dim selectedGroup As UserGroup = Wiki.UserGroups(List.SelectedItems(0).Text)
                GroupName.Text = selectedGroup.Name

                RightsList.BeginUpdate()
                RightsList.Items.Clear()

                For Each right As String In selectedGroup.Rights
                    RightsList.AddRow(right)
                Next right

                RightsCount.Text = Msg("view-usergroup-rightscount", RightsList.Items.Count)
                RightsList.EndUpdate()
            End If
        End Sub

    End Class

End Namespace