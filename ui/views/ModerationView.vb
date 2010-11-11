Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class ModerationView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Dim rows As New List(Of String())

            For Each flag As ReviewFlag In Wiki.ReviewFlags.All
                rows.Add({flag.Name, flag.DisplayName, flag.Levels.ToStringForUser,
                    flag.QualityLevel.ToStringForUser, flag.PristineLevel.ToStringForUser})
            Next flag

            List.SortMethods(2) = SortMethod.Integer
            List.SortMethods(3) = SortMethod.Integer
            List.SortMethods(4) = SortMethod.Integer
            List.SetItems(rows)

            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace
