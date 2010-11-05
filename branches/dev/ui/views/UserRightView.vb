Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class UserRightView : Inherits Viewer

        Friend Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Friend Property SelectedRight As String
            Get
                Return RightsList.SelectedValue
            End Get
            Set(ByVal value As String)
                RightsList.Select()
                If Wiki.UserRights.Contains(value) Then RightsList.SelectedValue = value
            End Set
        End Property

        Private Sub _Load() Handles Me.Load
            Dim rights As List(Of String) = Wiki.UserRights
            rights.Sort()

            RightsList.BeginUpdate()
            RightsList.Items.Clear()

            For Each right As String In Wiki.UserRights
                Dim groups As New List(Of UserGroup)

                For Each group As UserGroup In Wiki.UserGroups.All
                    If group.Rights.Contains(right) Then groups.Add(group)
                Next group

                RightsList.AddRow(right, WikiStripMarkup(Wiki.Message("right-" & right)), If(groups.Count = 0, Msg("view-userright-nobody"), groups.Join(", ")))
            Next right

            RightsList.EndUpdate()
            RightsList.SortBy(0)
            Count.Text = Msg("a-count", RightsList.Items.Count)
        End Sub

    End Class

End Namespace