Imports System.Collections.Generic

Namespace Huggle.Queries

    'Determine which API modules are available

    Friend Class ApiModuleQuery : Inherits Query

        Private Shared ReadOnly CoreModules As String() = {
            "block",
            "compare",
            "delete",
            "edit",
            "emailuser",
            "expandtemplates",
            "feedwatchlist",
            "filerevert",
            "help",
            "import",
            "login",
            "logout",
            "move",
            "opensearch",
            "paraminfo",
            "parse",
            "patrol",
            "protect",
            "purge",
            "query",
            "rollback",
            "rsd",
            "unblock",
            "undelete",
            "upload",
            "userrights",
            "watch"
        }

        Private Shared ReadOnly CoreListModules As String() = {
            "allcategories",
            "allimages",
            "allpages",
            "alllinks",
            "allusers",
            "backlinks",
            "blocks",
            "categorymembers",
            "deletedrevs",
            "embeddedin",
            "exturlusage",
            "filearchive",
            "imageusage",
            "iwbacklinks",
            "langbacklinks",
            "logevents",
            "protectedtitles",
            "querypage",
            "random",
            "recentchanges",
            "search",
            "tags",
            "usercontribs",
            "users",
            "watchlist",
            "watchlistraw"
            }

        Private Shared ReadOnly CoreMetaModules As String() = {
            "allmessages",
            "siteinfo",
            "userinfo"
            }

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("apimodule-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("userconfig-progress", Session.User.FullName))

            Dim modules As New List(Of String)(CoreModules)

            Dim queryModules As New List(Of String)
            queryModules.Merge(CoreListModules)
            queryModules.Merge(CoreMetaModules)

            modules.Merge(GetExtensionModules)
            queryModules.Merge(GetExtensionQueryModules)

            Dim infoReq As New ApiRequest(Session, Description, New QueryString(
                "action", "paraminfo",
                "modules", modules.Join("|"),
                "querymodules", queryModules.Join("|")))

            infoReq.Start()
            If infoReq.IsFailed Then OnFail(infoReq.Result) : Return

            Wiki.ApiModules.Checked = True

            OnSuccess()
        End Sub

        Private Function GetExtensionModules() As List(Of String)
            Dim result As New List(Of String)

            If Wiki.Extensions.Contains(CommonExtension.SiteMatrix) Then result.Add("sitematrix")
            If Wiki.Extensions.Contains(CommonExtension.Moderation) Then result.Add("flagconfig", "review", "stabilize")

            Return result
        End Function

        Private Function GetExtensionQueryModules() As List(Of String)
            Dim result As New List(Of String)

            If Wiki.Extensions.Contains(CommonExtension.AbuseFilter) Then result.Merge({"abuselog", "abusefilters"})
            If Wiki.Extensions.Contains(CommonExtension.GlobalBlocking) Then result.Merge("globalblocks")
            If Wiki.Extensions.Contains(CommonExtension.UnifiedLogin) Then result.Merge("globaluserinfo")

            Return result
        End Function

    End Class

End Namespace
