Namespace Huggle.Actions

    Public Class RevertAndWarn

        'Handles revert-warn behaviour

        Private Revert As Revert
        Private Warn As Warning

        Public Sub New(ByVal Revert As Revert, ByVal Warn As Warning)
            Me.Revert = Revert
            Me.Warn = Warn
        End Sub

        Public Sub Start()
            AddHandler Revert.Success, AddressOf RevertSuccess
            Revert.Start()
        End Sub

        Private Sub RevertSuccess()
            Warn.Start()
        End Sub

    End Class

End Namespace