Imports System.Drawing

Public Class ConfirmationForm

    Public Sub New(ByVal confirmation As Image)
        InitializeComponent()
        ConfirmationImage.Image = confirmation
    End Sub

    Public ReadOnly Property ConfirmationCode() As String
        Get
            Return ConfirmationInput.Text
        End Get
    End Property

End Class