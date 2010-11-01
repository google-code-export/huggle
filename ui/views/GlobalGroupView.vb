Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class GlobalGroupView : Inherits Viewer

        Private Family As Family

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            Family = session.Wiki.Family
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            If Family.GlobalGroups.All.Count = 0 Then
                Wait.WaitOn(New Actions.GlobalGroupsQuery(Family), AddressOf QueryComplete)
            Else
                Display()
            End If
        End Sub

        Private Sub QueryComplete(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            If e.Sender.IsSuccess Then
                Wait.Visible = False
                Display()
            End If
        End Sub

        Private Sub Display()
            List.Visible = True
            Count.Visible = True
            Title.Visible = True

            List.BeginUpdate()
            List.Items.Clear()

            For Each group As GlobalGroup In Family.GlobalGroups.All
                List.AddRow(group.Name, group.DisplayName)
            Next group

            List.EndUpdate()
            List.SortBy(0)
            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace
