Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class CustomFilterForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            Try

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Public Property Filter As String
            Get
                Return FilterInput.Text
            End Get
            Set(ByVal value As String)
                FilterInput.Text = value
            End Set
        End Property

    End Class

End Namespace