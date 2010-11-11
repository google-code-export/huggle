Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class RevisionView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            RevisionList.SortMethods(0) = SortMethod.Integer
            RevisionList.SortMethods(4) = SortMethod.Integer
            AddHandler Wiki.Action, AddressOf Action
        End Sub

        Private Sub CustomFilter_Click() Handles CustomFilter.Click
            Using form As New CustomFilterForm
                form.ShowDialog()
            End Using
        End Sub

        Private Sub Action(ByVal sender As Object, ByVal e As EventArgs(Of QueueItem))
            Dim rev As Revision = TryCast(e.Value, Revision)

            Dim statuses As New List(Of String)
            If rev.IsMinor Then statuses.Add("minor")
            If rev.IsCreation Then statuses.Add("new")
            If rev.IsBot Then statuses.Add("bot")
            If rev.IsAssisted Then statuses.Add("assisted")
            If rev.IsRevert Then statuses.Add("revert")
            If rev.IsSanction Then statuses.Add("sanction")

            If rev IsNot Nothing Then
                RevisionList.AddItem({rev.Id.ToStringI, rev.Time.ToLongTimeString, rev.User.Name,
                    rev.Page.Title, statuses.Join(", "), rev.Change.ToStringForUser, rev.Summary})
            End If
        End Sub

    End Class

End Namespace
