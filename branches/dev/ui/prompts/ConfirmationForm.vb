Imports Huggle
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class ConfirmationForm

    Private _Confirmation As Confirmation

    Public Sub New(ByVal confirmation As Confirmation)
        InitializeComponent()
        _Confirmation = confirmation
    End Sub

    Private Sub _Load() Handles Me.Load
        Try
            ConfirmImage.Image = Confirmation.Image

            'Fit confirmation code to form
            Width = 300 + Math.Max(0, ConfirmImage.Image.Width - ConfirmImage.Width)
            ConfirmRefresh.Left = ConfirmImage.Left + (ConfirmImage.Width \ 2) _
                + (ConfirmImage.Image.Width \ 2) - ConfirmRefresh.Width

        Catch ex As SystemException
            App.ShowError(Result.FromException(ex))
            DialogResult = DialogResult.Abort
            Close()
        End Try
    End Sub

    Private Sub ConfirmRefresh_LinkClicked() Handles ConfirmRefresh.LinkClicked

    End Sub

    Public ReadOnly Property Confirmation As Confirmation
        Get
            Return _Confirmation
        End Get
    End Property

End Class