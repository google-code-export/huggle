Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class ChangeTagView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            Dim rows As New List(Of String())

            For Each tag As ChangeTag In Wiki.ChangeTags.All
                Dim displayName As String = If(Wiki.Config.ChangeTagIdentifier Is Nothing, _
                    tag.DisplayName, tag.DisplayName.Remove(Wiki.Config.ChangeTagIdentifier).Trim)

                rows.Add({WikiStripMarkup(displayName), WikiStripMarkup(tag.Description), tag.Hits.ToStringForUser})
            Next tag

            List.SortMethods(2) = SortMethod.Integer
            List.SetItems(rows)

            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace