Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class UserRightView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Public Property SelectedRight As String
            Get
                Return RightsList.SelectedValue
            End Get
            Set(ByVal value As String)
                RightsList.Select()
                If Wiki.UserRights.Contains(value) Then RightsList.SelectedValue = value
            End Set
        End Property

        Private Sub _Load() Handles Me.Load
            Dim rows As New List(Of String())

            For Each right As String In Wiki.UserRights
                Dim groups As New List(Of UserGroup)

                For Each group As UserGroup In Wiki.UserGroups.All
                    If group.Rights.Contains(right) Then groups.Add(group)
                Next group

                rows.Add({right, WikiStripMarkup(Wiki.Message("right-" & right)),
                    If(groups.Count = 0, Msg("view-userright-nobody"), groups.Join(", "))})
            Next right

            RightsList.SetItems(rows)
            Count.Text = Msg("a-count", RightsList.Items.Count)
        End Sub

    End Class

End Namespace