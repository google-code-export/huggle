Imports Huggle
Imports System.Windows.Forms

Public Class ModerationView : Inherits Viewer

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        InitializeComponent()
    End Sub

    Private Sub _Load() Handles Me.Load
        List.BeginUpdate()
        List.Items.Clear()

        For Each flag As ReviewFlag In Wiki.ReviewFlags.All
            List.AddRow(flag.Name, flag.DisplayName, _
                flag.Levels.ToString, flag.QualityLevel.ToString, flag.PristineLevel.ToString)
        Next flag

        List.EndUpdate()
        List.SortMethods.Merge(2, SortMethod.Integer)
        List.SortMethods.Merge(3, SortMethod.Integer)
        List.SortMethods.Merge(4, SortMethod.Integer)
        List.SortBy(0)
        Count.Text = Msg("a-count", List.Items.Count)
    End Sub

End Class
