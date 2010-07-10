Imports System
Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Load essential wiki and user configuration only --
    'non-essential configuration is loaded later by LoadExtraWikiConfig

    Public Class LoadUserConfig : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("userconfig-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("userconfig-progress", Session.User.FullName))

            Dim query As New QueryString("action", "query")
            Dim list As New List(Of String)
            Dim meta As New List(Of String)
            Dim titles As New List(Of String)

            If User.Config.NeedsUpdate Then
                list.Add("users")
                meta.Add("userinfo")
                query.Add("uiprop", "blockinfo|groups|rights|changeablegroups|options|editcount|ratelimits|email")
                query.Add("usprop", "registration|emailable|gender")
                query.Add("ususers", User)
            End If

            If User.IsUnified AndAlso User.GlobalUser.Config.NeedsUpdate Then
                meta.Add("globaluserinfo")
                query.Add("guiprop", "groups|rights")
            End If

            If Wiki.Config.NeedsUpdate Then
                list.Add("tags")
                meta.Add("siteinfo", "allmessages")
                query.Add("ammessages", Config.Internal.WikiMessages)
                query.Add("siprop", "general|namespaces|namespacealiases|extensions|rightsinfo|usergroups")
                query.Add("tgprop", "name|displayname|description|hitcount")
                query.Add("tglimit", "max")
                titles.Add(Config.Global.WikiConfigPageName)

                If Wiki.Extensions.All.Count = 0 OrElse Wiki.Extensions.Contains("AbuseFilter") Then
                    list.Add("abusefilters")
                    query.Add("abflimit", "max")
                    query.Add("abfprop", "id|description|actions|hits|lasteditor|lastedittime|status|private")
                End If
            End If

            If meta.Count > 0 Then query.Add("meta", meta)
            If list.Count > 0 Then query.Add("list", list)

            If titles.Count > 0 Then
                query.Add("titles", titles)
                query.Add("prop", "info|revisions")
                query.Add("rvprop", "ids|content|user")
            End If

            OnStarted()

            Dim req As New ApiRequest(Session, Description, query)
            req.DoMultiple()
            If req.IsErrored Then OnFail(req.Result) : Return

            'Load wiki config
            Wiki.Config.Load(Wiki.Pages(Config.Global.WikiConfigPageName).Text)

            If User.Config.NeedsUpdate Then
                User.Config.IsLocalCopy = False
                Log.Debug("Loaded user details for {0} [R]".FormatWith(User.FullName))
            End If

            If Wiki.Config.NeedsUpdate Then
                Wiki.Config.IsLocalCopy = False
                Log.Debug("Loaded wiki details for {0} [R]".FormatWith(Wiki))
            End If

            If User.IsUnified AndAlso User.GlobalUser.Config.NeedsUpdate Then
                User.GlobalUser.Config.IsLocalCopy = False
                Log.Debug("Loaded global user details for {0} [R]".FormatWith(User.GlobalUser.FullName))
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
