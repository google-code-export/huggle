Imports System
Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Load essential wiki and user configuration only --
    'non-essential configuration is loaded later by ExtraWikiConfig

    Friend Class LoadUserConfig : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("userconfig-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("userconfig-progress", Session.User.FullName))

            Dim updateWiki As Boolean = (Not Wiki.Config.IsCurrent)
            Dim updateUser As Boolean = (Not Wiki.Config.IsCurrent)
            Dim updateGlobalUser As Boolean = (User.IsUnified AndAlso Not User.GlobalUser.Config.IsCurrent)

            'Get cached config from the cloud
            If InternalConfig.UseCloud Then
                Dim loaders As New List(Of Process)

                If updateWiki Then loaders.Add(New GeneralProcess("", AddressOf Wiki.Config.LoadCloud))
                If updateUser Then loaders.Add(New GeneralProcess("", AddressOf User.Config.LoadCloud))
                If updateGlobalUser Then loaders.Add(New GeneralProcess("", AddressOf User.GlobalUser.Config.LoadCloud))

                App.DoParallel(loaders)

                If updateWiki Then
                    If Wiki.Config.IsCurrent Then Wiki.Config.SaveLocal() _
                        Else Log.Debug("Outdated in cloud: wiki\{0}".FormatI(Wiki.Code))
                End If

                If updateUser Then
                    If User.Config.IsCurrent Then User.Config.SaveLocal() _
                        Else Log.Debug("Outdated in cloud: user\{0}".FormatI(User.FullName))
                End If

                If updateGlobalUser Then
                    If User.GlobalUser.Config.IsCurrent Then User.GlobalUser.Config.SaveLocal() _
                        Else Log.Debug("Outdated in cloud: globaluser\{0}".FormatI(User.GlobalUser.FullName))
                End If
            End If

            Dim query As New QueryString("action", "query")
            Dim lists As New List(Of String)
            Dim meta As New List(Of String)
            Dim titles As New List(Of String)

            If Not User.Config.IsCurrent Then
                Log.Debug("Load from wiki: {0}".FormatI("user\" & User.FullName))

                lists.Add("users")
                meta.Add("userinfo")
                query.Add("uiprop", "blockinfo|groups|rights|changeablegroups|options|editcount|ratelimits|email")
                query.Add("usprop", "registration|emailable|gender")
                query.Add("ususers", User)
            End If

            If User.IsUnified AndAlso Not User.GlobalUser.Config.IsCurrent Then
                If User.IsUnified Then Log.Debug("Load from wiki: {0}".FormatI(
                        "globaluser\" & User.GlobalUser.FullName))

                meta.Add("globaluserinfo")
                query.Add("guiprop", "groups|rights")
            End If

            If Not Wiki.Config.IsCurrent Then
                Log.Debug("Load from wiki: {0}".FormatI("wiki\" & Wiki.Code))

                lists.Add("tags")
                meta.Add("siteinfo", "allmessages")
                query.Add("amlang", Wiki.Language.Code)
                query.Add("ammessages", "*")
                query.Add("siprop",
                    "general|namespaces|namespacealiases|extensions|fileextensions|rightsinfo|statistics|usergroups")
                query.Add("sinumberingroup", True)
                query.Add("tgprop", "name|displayname|description|hitcount")
                query.Add("tglimit", "max")
                titles.Add(Config.Global.WikiConfigPageTitle)

                If Wiki.Extensions.All.Count = 0 OrElse Wiki.Extensions.Contains(Extension.AbuseFilter) Then
                    lists.Add("abusefilters")
                    query.Add("abflimit", "max")
                    query.Add("abfprop", "id|description|pattern|actions|hits|comments|lasteditor|lastedittime|status|private")
                End If
            End If

            If meta.Count > 0 Then query.Add("meta", meta)
            If lists.Count > 0 Then query.Add("list", lists)

            If titles.Count > 0 Then
                query.Add("titles", titles)
                query.Add("prop", "info|revisions")
                query.Add("rvprop", "ids|content|user")
            End If

            If query.Values.Count = 0 Then OnSuccess() : Return

            OnStarted()

            Dim processes As New List(Of Process)
            Dim mainReq As New ApiRequest(Session, Description, query)
            processes.Add(mainReq)

            If Not Wiki.Config.IsCurrent Then
                Dim moduleReq As New ApiModuleQuery(Session)
                processes.Add(moduleReq)
            End If

            App.DoParallel(processes)

            For Each process As Process In processes
                If process.IsFailed Then OnFail(process.Result) : Return
            Next process

            'Load wiki config
            Wiki.Config.Load(Wiki.Pages(Config.Global.WikiConfigPageTitle).Text)

            If Not User.Config.IsCurrent Then
                User.Config.Updated = Date.UtcNow
                User.Config.SaveLocal()
                If InternalConfig.UseCloud Then User.Config.SaveCloud()
            End If

            If Not Wiki.Config.IsCurrent Then
                Wiki.Config.Updated = Date.UtcNow
                Wiki.Config.SaveLocal()
                If InternalConfig.UseCloud Then Wiki.Config.SaveCloud()
            End If

            If User.IsUnified AndAlso Not User.GlobalUser.Config.IsCurrent Then
                User.GlobalUser.Config.Updated = Date.UtcNow
                User.GlobalUser.Config.SaveLocal()
                If InternalConfig.UseCloud Then User.GlobalUser.Config.SaveCloud()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
