Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Public Class GlobalConfig : Inherits Config

        Public Property AnonymousLogin As Boolean
        Public Property AutoUnifiedLogin As Boolean
        Public Property DownloadLocation As String
        Public Property LatestVersion As Version
        Public Property ScaryPattern As Regex
        Public Property TimeZones As Dictionary(Of String, Integer)
        Public Property TopWiki As Wiki
        Public Property WikiConfigPageTitle As String

        Private _Loader As LoadGlobalConfig

        Public Sub New()
            DownloadLocation = InternalConfig.DownloadUrl.ToString
            LatestVersion = App.Version
            WikiConfigPageTitle = "Project:Huggle/Config"
        End Sub

        Public ReadOnly Property Loader As LoadGlobalConfig
            Get
                If _Loader Is Nothing Then _Loader = New LoadGlobalConfig
                Return _Loader
            End Get
        End Property

        Public ReadOnly Property LocalizationPageName(ByVal code As String) As String
            Get
                Return "Huggle/messages/" & code
            End Get
        End Property

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return "global"
            End Get
        End Property

        Public ReadOnly Property PageTitle() As String
            Get
                Return "Huggle/test config"
            End Get
        End Property

        Protected Overrides Sub ReadConfig(ByVal text As String)
            For Each item As KVP In Config.ParseConfig("global", Nothing, text)
                Dim key As String = item.Key
                Dim value As String = item.Value

                Try
                    Select Case key
                        Case "anon-login" : AnonymousLogin = value.ToBoolean
                        Case "auto-unified-login" : AutoUnifiedLogin = value.ToBoolean
                        Case "default-user-config" : App.Wikis.Default.Users.Default.Config.Load(value)
                        Case "default-wiki-config" : App.Wikis.Default.Config.Load(value)
                        Case "download-location" : DownloadLocation = value

                        Case "families"
                            For Each node As KVP In Config.ParseConfig("global", key, value)
                                Dim family As Family = App.Families(node.Key)
                                family.Config.Load(node.Value)
                            Next node

                        Case "languages"
                            For Each node As KVP In Config.ParseConfig("global", key, value)
                                Dim language As Language = App.Languages(node.Key)

                                For Each prop As KVP In Config.ParseConfig("global", key & ":" & language.Name, node.Value)
                                    Select Case prop.Key
                                        Case "ignored" : language.IsIgnored = prop.Value.ToBoolean
                                        Case "localized" : language.IsLocalized = prop.Value.ToBoolean
                                        Case "name" : language.Name = prop.Value
                                    End Select
                                Next prop
                            Next node

                        Case "scary-pattern" : ScaryPattern = New Regex(value)

                        Case "time-zones"
                            Dim zones As New Dictionary(Of String, Integer)

                            For Each zone As KVP In Config.ParseConfig("global", key, value)
                                zones.Merge(zone.Key, CInt(zone.Value))
                            Next zone

                            TimeZones = zones

                        Case "top-wiki" : TopWiki = App.Wikis(value)
                        Case "updated" : Updated = value.ToDate
                        Case "version" : LatestVersion = New Version(value)
                        Case "wikiconfig-page" : WikiConfigPageTitle = value
                        Case "wikis" : LoadWikis(key, value)
                    End Select

                Catch ex As SystemException
                    App.ShowError(Result.FromException(ex).Wrap(Msg("error-configvalue", key, "global")))
                End Try
            Next item
        End Sub

        Private Sub LoadWikis(ByVal key As String, ByVal value As String)
            For Each node As KVP In Config.ParseConfig("global", key, value)
                Dim wiki As Wiki = App.Wikis(node.Key)
                wiki.Exists = True

                Dim props As Dictionary(Of String, String) = _
                    Config.ParseConfig("global", node.Key & ":" & wiki.Code, node.Value)

                'Load Wikimedia defaults
                If Not props.ContainsKey("family") Then
                    wiki.Family = App.Families.Wikimedia

                    If wiki.Code.Contains(".") Then
                        wiki.Channel = "#" & wiki.Code
                        wiki.Language = App.Languages(wiki.Code.ToFirst("."))
                        wiki.Type = wiki.Code.FromFirst(".")

                        wiki.Name = wiki.Code
                        wiki.FileUrl = New Uri(InternalConfig.WikimediaFilePath & wiki.Type & "/" & wiki.Language.Code & "/")
                        wiki.SecureUrl = New Uri(InternalConfig.WikimediaSecurePath & wiki.Type & "/" & wiki.Language.Code & "/w/")
                        wiki.Url = New Uri("http://" & wiki.Code & ".org/w/")
                    Else
                        wiki.Channel = "#" & wiki.Code & ".wikimedia"
                        wiki.Type = "special"

                        wiki.Name = UcFirst(wiki.Code)
                        wiki.FileUrl = New Uri(InternalConfig.WikimediaFilePath & "wikipedia/" & wiki.Code & "/")
                        wiki.SecureUrl = New Uri(InternalConfig.WikimediaSecurePath & "wikipedia/" & wiki.Code & "/w/")
                        wiki.Url = New Uri("http://" & wiki.Code & ".wikimedia.org/w/")
                    End If
                End If

                For Each prop As KVP In props
                    Select Case prop.Key
                        Case "anon-login" : wiki.AnonymousLogin = prop.Value.ToBoolean
                        Case "account-confirmation" : wiki.AccountConfirmation = prop.Value.ToBoolean
                        Case "channel" : wiki.Channel = "#" & prop.Value
                        Case "edit" : wiki.IsPublicEditable = prop.Value.ToBoolean
                        Case "family" : If prop.Value = "none" Then wiki.Family = Nothing Else wiki.Family = App.Families(prop.Value)
                        Case "files" : wiki.FileUrl = New Uri(prop.Value)
                        Case "hidden" : wiki.IsHidden = prop.Value.ToBoolean
                        Case "language" : wiki.Language = App.Languages(prop.Value)
                        Case "name" : wiki.Name = prop.Value
                        Case "read" : wiki.IsPublicReadable = prop.Value.ToBoolean
                        Case "secure" : wiki.SecureUrl = New Uri(prop.Value)
                        Case "type" : wiki.Type = prop.Value
                        Case "url" : wiki.Url = New Uri(prop.Value)
                    End Select
                Next prop
            Next node

            For Each wiki As Wiki In App.Wikis.All.ToList
                If Not wiki.Exists Then App.Wikis.Remove(wiki)
            Next wiki
        End Sub

        Public Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            items.Add("anon-login", AnonymousLogin)
            items.Add("auto-unified-login", AutoUnifiedLogin)
            items.Add("download-location", DownloadLocation)
            items.Add("scary-pattern", ScaryPattern)
            If TimeZones IsNot Nothing Then items.Add("time-zones", TimeZones.ToDictionary(Of String, Object))
            If TopWiki IsNot Nothing Then items.Add("top-wiki", TopWiki.Code)
            items.Add("version", LatestVersion)
            items.Add("wikiconfig-page", WikiConfigPageTitle)
            items.Add("default-user-config", App.Wikis.Default.Users.Default.Config.WriteConfig(target))
            items.Add("default-wiki-config", App.Wikis.Default.Config.WriteConfig(target))
            items.Add("updated", Updated.ToLongTimeString)

            Dim familyConfigs As New Dictionary(Of String, Object)

            For Each family As Family In App.Families.All
                Dim config As New Dictionary(Of String, Object)
                config.Add("name", family.Name)

                familyConfigs.Add(family.Code, config)
            Next family

            items.Add("families", familyConfigs)

            Dim languageConfigs As New Dictionary(Of String, Object)

            For Each language As Language In App.Languages.All
                Dim config As New Dictionary(Of String, Object)

                config.Add("name", language.Name)
                If language.IsIgnored Then config.Add("ignored", True)
                If language.IsLocalized Then config.Add("localized", True)

                languageConfigs.Add(language.Code, config)
            Next language

            items.Add("languages", languageConfigs)

            Dim wikiConfigs As New Dictionary(Of String, Object)

            For Each wiki As Wiki In App.Wikis.All
                If wiki.IsDefault Then Continue For
                If wiki.IsCustom AndAlso target <> ConfigTarget.Local Then Continue For

                Dim wikiItems As New Dictionary(Of String, Object)

                If Not wiki.AccountConfirmation Then wikiItems.Add("account-confirmation", False)
                If Not wiki.AnonymousLogin Then wikiItems.Add("anon-login", False)
                If wiki.Channel IsNot Nothing Then wikiItems.Add("channel", wiki.Channel.Remove("#"))
                If wiki.IsCustom Then wikiItems.Add("custom", True)
                If Not wiki.IsPublicEditable Then wikiItems.Add("edit", False)
                wikiItems.Add("family", If(wiki.Family Is Nothing, "none", wiki.Family.Name))
                If wiki.FileUrl IsNot Nothing Then wikiItems.Add("files", wiki.FileUrl.ToString)
                If wiki.IsHidden Then wikiItems.Add("hidden", True)
                If wiki.Language IsNot Nothing Then wikiItems.Add("language", wiki.Language.Code)
                If wiki.Name <> wiki.Code Then wikiItems.Add("name", wiki.Name)
                If Not wiki.IsPublicReadable Then wikiItems.Add("read", False)
                If wiki.SecureUrl IsNot Nothing Then wikiItems.Add("secure", wiki.SecureUrl.ToString)
                If wiki.Type IsNot Nothing Then wikiItems.Add("type", wiki.Type)
                wikiItems.Add("url", wiki.Url)

                'For Wikimedia wikis suppress predicable values to reduce configuration file size
                If wiki.IsWikimedia Then
                    wikiItems.Remove("family")

                    If wiki.Code.Contains(".") Then
                        If wiki.Channel = "#" & wiki.Code Then wikiItems.Remove("channel")
                        If wiki.Language IsNot Nothing AndAlso wiki.Language.Code = wiki.Code.ToFirst(".") Then wikiItems.Remove("language")
                        If wiki.Language IsNot Nothing AndAlso wiki.FileUrl.ToString = InternalConfig.WikimediaFilePath _
                            & wiki.Type & "/" & wiki.Language.Code & "/" Then wikiItems.Remove("files")
                        If wiki.Language IsNot Nothing AndAlso wiki.SecureUrl.ToString = InternalConfig.WikimediaSecurePath _
                            & wiki.Type & "/" & wiki.Language.Code & "/w/" Then wikiItems.Remove("secure")
                        If wiki.Name = wiki.Code Then wikiItems.Remove("name")
                        If wiki.Type = wiki.Code.FromFirst(".") Then wikiItems.Remove("type")
                        If wiki.Url.ToString = "http://" & wiki.Language.Code & "." & wiki.Type & ".org/w/" Then wikiItems.Remove("url")
                    Else
                        If wiki.Channel = "#" & wiki.Code & ".wikimedia" Then wikiItems.Remove("channel")
                        If wiki.FileUrl.ToString = InternalConfig.WikimediaFilePath & "wikipedia/" & wiki.Code & "/" _
                            Then wikiItems.Remove("files")
                        If wiki.SecureUrl.ToString = InternalConfig.WikimediaSecurePath & "wikipedia/" & wiki.Code & "/w/" _
                            Then wikiItems.Remove("secure")
                        If wiki.Type = "special" Then wikiItems.Remove("type")
                        If wiki.Url.ToString = "http://" & wiki.Code & ".wikimedia.org/w/" Then wikiItems.Remove("url")
                    End If
                End If

                wikiConfigs.Add(wiki.Code, wikiItems)
            Next wiki

            items.Add("wikis", wikiConfigs)

            Return items
        End Function

    End Class

End Namespace
