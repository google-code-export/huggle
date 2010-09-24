Imports Huggle
Imports System.Collections.Generic
Imports System.Windows.Forms

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
                Else groupCountString = If(group.Count < 0, Msg("a-unknown"), group.Count.ToString)

            List.AddRow(group.Name, groupCountString, group.Rights.Join(", "))
        Next group

        List.EndUpdate()
        List.SortMethods(1) = SortMethod.Integer
        List.SortBy(0)
        Count.Text = Msg("a-count", List.Items.Count)
    End Sub

End Class