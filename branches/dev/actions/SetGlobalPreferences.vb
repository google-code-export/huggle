Imports System.Collections.Generic

Namespace Huggle.Actions

    Public Class SetGlobalPreferences : Inherits Process

        Private GlobalUser As GlobalUser
        Private NewPrefs As Dictionary(Of String, String)

        Public Sub New(ByVal user As GlobalUser, ByVal newPrefs As Dictionary(Of String, String))
            Me.GlobalUser = user
            Me.NewPrefs = newPrefs
        End Sub

        Public Overrides Sub Start()
            OnStarted()

            For Each user As User In GlobalUser.Users
                If Not App.Sessions(user).IsActive Then
                    Dim login As New Login(App.Sessions(user), "setglobalpreferences")
                    login.Start()
                    App.WaitFor(Function() login.IsComplete)
                End If
            Next user

            OnSuccess()
        End Sub

    End Class

End Namespace