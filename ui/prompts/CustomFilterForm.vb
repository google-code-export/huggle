Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class CustomFilterForm : Inherits HuggleForm

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