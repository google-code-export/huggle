Imports System.Collections.Generic

Namespace Huggle.Actions

    'Determine which API modules are available

    Friend Class ApiModuleQuery : Inherits Query

        Private Shared ReadOnly CoreModules As String() = {"help", "paraminfo", "query", "login", "logout",
            "parse", "expandtemplates", "feedwatchlist", "purge", "watch", "edit", "rollback", "delete", "undelete",
            "protect", "block", "unblock", "move", "upload", "emailuser", "patrol", "import", "userrights"}

        Private Shared ReadOnly CoreListModules As String() = {"allpages", "alllinks", "allcategories", "allusers",
            "backlinks", "blocks", "categorymembers", "deletedrevs", "embeddedin", "imageusage", "logevents",
            "recentchanges", "search", "tags", "usercontribs", "watchlist", "watchlistraw", "exturlusage",
            "users", "random", "protectedtitles"}

        Private Shared ReadOnly CoreMetaModules As String() = {"siteinfo", "userinfo", "allmessages"}

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("apimodule-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("userconfig-progress", Session.User.FullName))

            Dim modules As New List(Of String)(CoreModules)

            Dim queryModules As New List(Of String)
            'queryModules.Merge(CoreListModules)
            queryModules.Merge(CoreMetaModules)

            'Extension modules
            If Wiki.Extensions.Contains(Extension.SiteMatrix) Then modules.Merge("sitematrix")
            If Wiki.Extensions.Contains(Extension.OpenSearch) Then modules.Merge("opensearch")

            'Extension query modules
            If Wiki.Extensions.Contains(Extension.AbuseFilter) Then queryModules.Merge({"abuselog", "abusefilters"})
            If Wiki.Extensions.Contains(Extension.GlobalBlocking) Then queryModules.Merge("globalblocks")
            If Wiki.Extensions.Contains(Extension.UnifiedLogin) Then queryModules.Merge("globaluserinfo")

            'Workaround MediaWiki API bug 25248
            modules.Remove("userrights")
            modules.Remove("rollback")

            Dim infoReq As New ApiRequest(Session, Description, New QueryString(
                "action", "paraminfo",
                "modules", modules.Join("|"),
                "querymodules", queryModules.Join("|")))

            infoReq.Start()
            If infoReq.IsFailed Then OnFail(infoReq.Result) : Return

            'Check for disabled modules
            Dim disabledModules As New List(Of ApiModule)

            For Each apiModule As ApiModule In Wiki.ApiModules.All
                If apiModule.IsDisabled Then disabledModules.Add(apiModule)
            Next apiModule

            If disabledModules.Count > 0 Then
                Dim moduleList As String = CRLF & CRLF

                For Each apiModule As ApiModule In disabledModules
                    moduleList &= apiModule.Name & CRLF
                Next apiModule

                App.ShowError(New Result(Msg("wikiconfig-apimodulesdisabled") & moduleList))
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
