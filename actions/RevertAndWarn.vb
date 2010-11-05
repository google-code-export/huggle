Imports System

Namespace Huggle.Actions

    Friend Class RevertAndWarn

        'Handles revert-warn behaviour

        Private Revert As Revert
        Private Warn As Warning

        Friend Sub New(ByVal revert As Revert, ByVal warn As Warning)
            Me.Revert = revert
            Me.Warn = warn
        End Sub

        Friend Sub Start()
            AddHandler Revert.Success, AddressOf RevertSuccess
            Revert.Start()
        End Sub

        Private Sub RevertSuccess(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            Warn.Start()
        End Sub

    End Class

End Namespace