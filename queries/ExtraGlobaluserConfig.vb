﻿Namespace Huggle.Actions

    'Load non-essential global user configuration

    Public Class ExtraGlobaluserConfig : Inherits Query

        Private GlobalUser As GlobalUser

        Public Sub New(ByVal globalUser As GlobalUser)
            MyBase.New(App.Sessions(globalUser.Family), Msg("globaluserconfig-desc", globalUser.FullName))
            Me.GlobalUser = globalUser
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("globaluserconfig-progress", GlobalUser.FullName))

            Dim globalUsers As New ApiRequest(Session, Description, New QueryString( _
                "action", "query", _
                "meta", "globaluserinfo", _
                "guiprop", "groups|rights|merged|unattached", _
                "guiuser", GlobalUser.Name))

            globalUsers.Start()
            If globalUsers.IsFailed Then OnFail(globalUsers.Result) : Return

            GlobalUser.Config.Extra = True
            GlobalUser.Config.SaveLocal()

            OnSuccess()
        End Sub

    End Class

End Namespace