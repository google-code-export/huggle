Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class UserRightsView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Dim rights As List(Of String) = Wiki.UserRights
            rights.Sort()

            List.BeginUpdate()
            List.Items.Clear()

            For Each right As String In Wiki.UserRights
                Dim groups As New List(Of UserGroup)

                For Each group As UserGroup In Wiki.UserGroups.All
                    If group.Rights.Contains(right) Then groups.Add(group)
                Next group

                List.AddRow(right, If(groups.Count = 0, Msg("view-userright-nobody"), groups.Join(", ")))
            Next right

            List.EndUpdate()
            List.SortBy(0)
            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace