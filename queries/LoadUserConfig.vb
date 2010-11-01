﻿Imports System
Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Load essential wiki and user configuration only --
    'non-essential configuration is loaded later by ExtraWikiConfig

    Public Class LoadUserConfig : Inherits Query

        Public Sub New(ByVal session As Session)
            MyBase.New(session, Msg("userconfig-desc", session.User.FullName))
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("userconfig-progress", Session.User.FullName))

            'Get cached config from the cloud
            If InternalConfig.UseCloud Then
                Wiki.Config.LoadCloud()

                If Wiki.Config.IsCurrent Then Wiki.Config.SaveLocal() _
                    Else Log.Debug("Outdated in cloud: wiki\{0}".FormatWith(Wiki.Code))
            End If

            Dim query As New QueryString("action", "query")
            Dim lists As New List(Of String)
            Dim meta As New List(Of String)
            Dim titles As New List(Of String)

            If Not User.Config.IsCurrent Then
                Log.Debug("Load from wiki: {0}".FormatWith("user\" & User.FullName))

                lists.Add("users")
                meta.Add("userinfo")
                query.Add("uiprop", "blockinfo|groups|rights|changeablegroups|options|editcount|ratelimits|email")
                query.Add("usprop", "registration|emailable|gender")
                query.Add("ususers", User)
            End If

            If User.IsUnified AndAlso Not User.GlobalUser.Config.IsCurrent Then
                If User.IsUnified Then Log.Debug("Load from wiki: {0}".FormatWith(
                        "globaluser\" & User.GlobalUser.FullName))

                meta.Add("globaluserinfo")
                query.Add("guiprop", "groups|rights")
            End If

            If Not Wiki.Config.IsCurrent Then
                Log.Debug("Load from wiki: {0}".FormatWith("wiki\" & Wiki.Code))

                lists.Add("tags")
                meta.Add("siteinfo", "allmessages")
                query.Add("ammessages", InternalConfig.WikiMessages)
                query.Add("siprop",
                    "general|namespaces|namespacealiases|extensions|fileextensions|rightsinfo|statistics|usergroups")
                query.Add("sinumberingroup", True)
                query.Add("tgprop", "name|displayname|description|hitcount")
                query.Add("tglimit", "max")
                titles.Add(Config.Global.WikiConfigPageTitle)

                If Wiki.Extensions.All.Count = 0 OrElse Wiki.Extensions.Contains(Extension.AbuseFilter) Then
                    lists.Add("abusefilters")
                    query.Add("abflimit", "max")
                    query.Add("abfprop", "id|description|actions|hits|lasteditor|lastedittime|status|private")
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
            CreateThread(AddressOf mainReq.DoMultiple)

            If Not Wiki.Config.IsCurrent Then
                Dim moduleReq As New ApiModuleQuery(Session)
                processes.Add(moduleReq)
                CreateThread(AddressOf moduleReq.Start)
            End If

            App.WaitFor(Function() processes.TrueForAll(Function(p As Process) p.IsComplete))

            For Each process As Process In processes
                If process.IsFailed Then OnFail(process.Result) : Return
            Next process

            'Load wiki config
            Wiki.Config.Load(Wiki.Pages(Config.Global.WikiConfigPageTitle).Text)

            User.Config.Updated = Date.UtcNow
            User.Config.SaveLocal()
            Wiki.Config.Updated = Date.UtcNow
            Wiki.Config.SaveLocal()

            If User.IsUnified Then
                User.GlobalUser.Config.Updated = Date.UtcNow
                User.GlobalUser.Config.SaveLocal()
            End If

            Wiki.Config.SaveCloud()

            OnSuccess()
        End Sub

    End Class

End Namespace
