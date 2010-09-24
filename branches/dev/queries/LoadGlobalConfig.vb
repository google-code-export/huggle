Imports Huggle.Queries
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text

Namespace Huggle.Actions

    Public Class LoadGlobalConfig : Inherits Query

        Public Sub New()
            MyBase.New(App.Wikis.Global.Users.Anonymous.Session, Msg("config-desc"))
            Interactive = True
        End Sub

        Public Overrides Sub Start()
            OnStarted()
            OnProgress(Msg("config-progress"))

            Dim configPage As Page = Wiki.Pages(Config.Global.PageTitle)
            Dim languagePage As Page = Wiki.Pages(Config.Global.LocalizationPageName(App.Languages.Current.Code))

            'Parts to global configuration:
            '* the configuration page itself
            '* action=sitematrix through the API
            '* closed.dblist because sitematrix extension is broken and doesn't indicate closed wikis
            '* the global title blacklist

            Dim configRequest As New PageInfoQuery(Session, New Page() {configPage, languagePage}.ToList, Content:=True)

            If Wiki.Family.GlobalTitleBlacklist IsNot Nothing _
                Then configRequest.Pages.Merge(Wiki.Family.GlobalTitleBlacklist.Location)

            Dim wikiRequest As New ApiRequest _
                (App.Wikis("meta").Users.Anonymous.Session, Description, New QueryString("action", "sitematrix"))
            Dim closedRequest As New FileRequest _
                (App.Wikis("meta").Users.Anonymous.Session, Config.Internal.WikimediaClosedWikisPath)

            'Run requests in parallel
            CreateThread(AddressOf configRequest.Start)
            CreateThread(AddressOf wikiRequest.Start)
            CreateThread(AddressOf closedRequest.Start)
            App.WaitFor(Function() configRequest.IsComplete AndAlso wikiRequest.IsComplete AndAlso closedRequest.IsComplete)

            If configRequest.Result.IsError Then OnFail(configRequest.Result) : Return
            If wikiRequest.Result.IsError Then OnFail(wikiRequest.Result) : Return

            If Not configPage.Exists OrElse String.IsNullOrEmpty(configPage.Text) _
                Then OnFail(Msg("globalconfig-notfound", Config.Global.PageTitle, App.Wikis.Global)) : Return

            Try
                Config.Global.Load(configPage.Text)
                Log.Debug("Loaded global config [R]")

            Catch ex As ConfigException
                OnFail(Result.FromException(ex)) : Return
            End Try

            If languagePage.Exists Then
                Try
                    App.Languages.Current.Load(languagePage.Text)
                    Log.Debug("Loaded messages for '{0}' [R]".FormatWith(App.Languages.Current.Name))

                Catch ex As ConfigException
                    Log.Write(Msg("language-loadfail", App.Languages.Current.Name, _
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
                Log.Debug("Error loading closed wiki list: {0}".FormatWith(closedRequest.Result.LogMessage))
            End If

            'Save configuration
            Config.Global.SaveLocal()
            Config.Messages.SaveLocal()

            OnSuccess()
        End Sub

    End Class

End Namespace
