Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text

Namespace Huggle.Actions

    Public Class LoadGlobalConfig : Inherits Query

        Public Sub New()
            MyBase.New(App.Sessions(App.Wikis.Global.Users.Anonymous), Msg("config-desc"))
            Interactive = True
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("config-progress"))

            'Get cached config from the cloud
            If InternalConfig.UseCloud Then
                Config.Global.LoadCloud()

                If Config.Global.IsCurrent Then
                    Config.Global.SaveLocal()
                    OnSuccess()
                    Return
                Else
                    Log.Debug("Outdated in cloud: global")
                End If
            End If

            Dim configPage As Page = Wiki.Pages(GlobalConfig.PageTitle)
            Dim languagePage As Page = Wiki.Pages(GlobalConfig.LocalizationPageName(App.Languages.Current.Code))

            'Parts to global configuration:
            '* the configuration page itself
            '* action=sitematrix through the API
            '* closed.dblist because sitematrix extension is broken and doesn't indicate closed wikis
            '* the global title blacklist

            Dim configRequest As New PageInfoQuery(Session, New Page() {configPage, languagePage}.ToList, Content:=True)

            If Wiki.Family.GlobalTitleBlacklist IsNot Nothing _
                Then configRequest.Pages.Merge(Wiki.Family.GlobalTitleBlacklist.Location)

            Dim wikiRequest As New ApiRequest(
                App.Sessions(App.Wikis.Global.Users.Anonymous), Description, New QueryString("action", "sitematrix"))
            Dim closedRequest As New FileRequest(
                App.Sessions(App.Wikis.Global.Users.Anonymous), InternalConfig.WikimediaClosedWikisPath)

            App.DoParallel({configRequest, wikiRequest, closedRequest})
            
            If configRequest.Result.IsError Then OnFail(configRequest.Result) : Return
            If wikiRequest.Result.IsError Then OnFail(wikiRequest.Result) : Return

            If Not configPage.Exists OrElse String.IsNullOrEmpty(configPage.Text) _
                Then OnFail(Msg("config-globalnotfound")) : Return

            Try
                Config.Global.Load(configPage.Text)
                Log.Debug("Load from wiki: global")

            Catch ex As ConfigException
                OnFail(Result.FromException(ex)) : Return
            End Try

            If languagePage.Exists Then
                Try
                    App.Languages.Current.GetConfig.Load(languagePage.Text)
                    Log.Debug("Loaded messages for '{0}' from wiki".FormatI(App.Languages.Current.Name))

                Catch ex As ConfigException
                    Log.Write(Msg("language-loadfail", App.Languages.Current.Name,
                        Msg("error-config", Nothing, "messages-" & App.Languages.Current.Code)))
                End Try
            End If

            If closedRequest.IsSuccess Then
                For Each code As String In Encoding.ASCII.GetString(closedRequest.File.ToArray).Split(LF)
                    Dim wiki As Wiki = App.Wikis.FromInternalCode(code)

                    If wiki IsNot Nothing Then
                        wiki.IsPublicEditable = False
                        wiki.IsPublicReadable = False
                    End If
                Next code
            Else
                Log.Debug("Error loading closed wiki list: {0}".FormatI(closedRequest.Result.LogMessage))
            End If

            'Save configuration
            Config.Global.Updated = Date.UtcNow
            Config.Global.SaveLocal()

            For Each lang As Language In App.Languages.All
                If lang.IsLocalized Then lang.GetConfig.SaveLocal()
            Next lang

            'Store config to the cloud
            If InternalConfig.UseCloud Then CreateThread(AddressOf Config.Global.SaveCloud)

            OnSuccess()
        End Sub

    End Class

End Namespace
