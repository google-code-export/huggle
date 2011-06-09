Imports System

Namespace Huggle.Queries

    'Load non-essential global user configuration

    Friend Class ExtraGlobaluserConfig : Inherits Query

        Private GlobalUser As GlobalUser

        Public Sub New(ByVal globalUser As GlobalUser)
            MyBase.New(App.Sessions(globalUser.Family), Msg("globaluserconfig-desc", globalUser.FullName))
            Me.GlobalUser = globalUser
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("globaluserconfig-progress", GlobalUser.FullName))

            Dim globalUsers As New ApiRequest(Session, Description, New QueryString(
                "action", "query",
                "meta", "globaluserinfo",
                "guiprop", "groups|rights|merged|unattached",
                "guiuser", GlobalUser.Name))

            globalUsers.Timeout = New TimeSpan(0, 0, 30)
            globalUsers.Start()
            If globalUsers.IsFailed Then OnFail(globalUsers.Result) : Return

            Log.Debug("Load from wiki: globaluser\{0}".FormatI(GlobalUser.FullName))
            GlobalUser.Config.Updated = Date.UtcNow
            GlobalUser.Config.SaveLocal()

            OnSuccess()
        End Sub

    End Class

End Namespace
