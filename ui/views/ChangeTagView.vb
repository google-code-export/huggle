Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class ChangeTagView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            List.BeginUpdate()
            List.Items.Clear()

            For Each tag As ChangeTag In Wiki.ChangeTags.All
                Dim displayName As String = If(Wiki.Config.ChangeTagIdentifier Is Nothing, _
                    tag.DisplayName, tag.DisplayName.Remove(Wiki.Config.ChangeTagIdentifier).Trim)

                List.AddRow(WikiStripMarkup(displayName), WikiStripMarkup(tag.Description), CStr(tag.Hits))
            Next tag

            List.SortMethods.Merge(2, SortMethod.Integer)
            List.SortBy(0)
            List.EndUpdate()
            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace