Namespace Huggle.Actions

    Friend Class SaveConfig : Inherits Query

        Private Config As WikiConfig
        Private Protect As Boolean

        Public Sub New(ByVal session As Session, ByVal config As WikiConfig, ByVal protect As Boolean)
            MyBase.New(session, Msg("saveconfig-desc"))
            Me.Config = config
            Me.Protect = protect
        End Sub

        Public Overrides Sub Start()
            If User.HasRight("protect") Then
                If Protect AndAlso Not User.Wiki.Config.ConfigPage.IsProtected Then
                    'Need to protect the page
                    Dim query As New Protect(Session, User.Wiki.Config.ConfigPage, "")
                    query.Levels.Add("edit", New ProtectionPart(Date.MaxValue, "sysop"))
                    query.Levels.Add("move", New ProtectionPart(Date.MaxValue, "sysop"))
                    query.Start()

                ElseIf Not Protect AndAlso User.Wiki.Config.ConfigPage.IsProtected Then
                    'Need to unprotect the page
                    Dim query As New Protect(Session, User.Wiki.Config.ConfigPage, "")
                    query.Start()
                End If
            End If

            'Save the config
            Config.SaveWiki()
        End Sub

    End Class

End Namespace
