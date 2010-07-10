Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    Public Class GlobalConfig

        Public Property AnonymousLogin As Boolean
        Public Property AutoUnifiedLogin As Boolean
        Public Property DownloadLocation As String
        Public Property LatestVersion As Version
        Public Property ScaryPattern As Regex
        Public Property TimeZones As New Dictionary(Of String, Integer)
        Public Property TopWiki As Wiki
        Public Property WikiConfigPageName As String

        Private _Loader As LoadGlobalConfig

        Public ReadOnly Property CacheTime() As TimeSpan
            Get
                Return TimeSpan.FromHours(12)
            End Get
        End Property

        Public Property IsDefault() As Boolean = True

        Public ReadOnly Property Loader() As LoadGlobalConfig
            Get
                If _Loader Is Nothing Then _Loader = New LoadGlobalConfig
                Return _Loader
            End Get
        End Property

        Public Sub LoadLocal()
            Try
                If File.Exists(Path) Then
                    If File.GetLastWriteTime(Path).Add(Config.Global.CacheTime) < Date.Now Then NeedsUpdate = True
                    Config.Global.Load(File.ReadAllText(Path, Encoding.UTF8))
                    Log.Debug("Loaded global config [L]")
                Else
                    NeedsUpdate = True
                End If

                FamilyConfig.LoadAll()

            Catch ex As ConfigException
                Log.Write(Result.FromException(ex).LogMessage)

            Catch ex As SystemException
                Log.Write(Result.FromException(ex).LogMessage)
            End Try
        End Sub

        Public ReadOnly Property LocalizationPageName(ByVal code As String) As String
            Get
                Return "Huggle/messages/" & code
            End Get
        End Property

        Public Property NeedsUpdate() As Boolean

        Public ReadOnly Property PageTitle() As String
            Get
                Return "User:Gurch"
            End Get
        End Property

        Private ReadOnly Property Path() As String
            Get
                Return Config.Local.ConfigPath & "global.txt"
            End Get
        End Property

        Public Sub Load(ByVal text As String)
            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig("global", Nothing, text)
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
                            For Each node As KeyValuePair(Of String, String) In Config.ParseConfig("global", key, value)
                                Dim family As Family = App.Families(node.Key)
                                family.Config.Load(node.Value)
                            Next node

                        Case "hide-languages"
                            For Each code As String In value.ToList.Trim
                                App.Languages(code).IsHidden = True
                            Next code

                        Case "hide-wikis"
                            For Each code As String In value.ToList.Trim
                                If App.Wikis.Contains(code) Then App.Wikis(code).IsHidden = True
                            Next code

                        Case "languages"
                            For Each node As KeyValuePair(Of String, String) In Config.ParseConfig("global", key, value)
                                Dim language As Language = App.Languages(node.Key)

                                For Each prop As KeyValuePair(Of String, String) _
                                    In Config.ParseConfig("global", key & ":" & language.Name, node.Value)

                                    Select Case prop.Key
                                        Case "hidden" : language.IsHidden = prop.Value.ToBoolean
                                        Case "localized" : language.IsLocalized = prop.Value.ToBoolean
                                        Case "name" : language.Name = prop.Value
                                    End Select
                                Next prop
                            Next node

                        Case "popular-wikis"
                            For Each wiki As Wiki In App.Wikis.All
                                wiki.IsPopular = False
                            Next wiki

                            For Each code As String In value.ToList.Trim
                                If App.Wikis.Contains(code) Then App.Wikis(code).IsPopular = True
                            Next code

                        Case "scary-pattern" : ScaryPattern = New Regex(value)

                        Case "time-zones"
                            TimeZones.Clear()

                            For Each zone As KeyValuePair(Of String, String) In Config.ParseConfig("global", key, value)
                                TimeZones.Merge(item.Key, CInt(item.Value))
                            Next zone

                        Case "top-wiki" : TopWiki = App.Wikis(value)
                        Case "version" : LatestVersion = New Version(value)
                        Case "wikiconfig-page" : WikiConfigPageName = value
                        Case "wikis" : LoadWikis(key, value)
                    End Select

                Catch ex As SystemException
                    App.ShowError(Result.FromException(ex).Wrap(Msg("error-configvalue", key, "global")))
                End Try
            Next item

            _IsDefault = False
        End Sub

        Private Sub LoadWikis(ByVal key As String, ByVal value As String)
            For Each node As KeyValuePair(Of String, String) In Config.ParseConfig("global", key, value)
                Dim wiki As Wiki = App.Wikis(node.Key)

                Dim props As Dictionary(Of String, String) = _
                    Config.ParseConfig("global", node.Key & ":" & wiki.Code, node.Value)

                'Load Wikimedia defaults
                If Not props.ContainsKey("family") Then
                    wiki.Family = App.Families.Wikimedia

                    If wiki.Code.Contains(".") Then
                        wiki.Channel = "#" & wiki.Code
                        wiki.InternalCode = wiki.Code.ToFirst(".") & _
                            If(wiki.Code.FromFirst(".") = "wikipedia", "wiki", wiki.Code.FromFirst("."))
                        wiki.Language = App.Languages(wiki.Code.ToFirst("."))
                        wiki.Type = wiki.Code.FromFirst(".")

                        wiki.Name = Msg("login-langwikiname", UcFirst(wiki.Type), wiki.Language.Code, wiki.Language.Name)
                        wiki.FileUrl = New Uri(Config.Internal.WikimediaFilePath & wiki.Type & "/" & wiki.Language.Code & "/")
                        wiki.SecureUrl = New Uri(Config.Internal.WikimediaSecurePath & wiki.Type & "/" & wiki.Language.Code & "/w")
                        wiki.Url = New Uri("http://" & wiki.Code & ".org/w")
                    Else
                        wiki.Channel = "#" & wiki.Code & ".wikimedia"
                        wiki.InternalCode = wiki.Code
                        wiki.Type = "special"

                        wiki.Name = UcFirst(wiki.Code)
                        wiki.FileUrl = New Uri(Config.Internal.WikimediaFilePath & "wikipedia/" & wiki.Code & "/")
                        wiki.SecureUrl = New Uri(Config.Internal.WikimediaSecurePath & "wikipedia/" & wiki.Code & "/w")
                        wiki.Url = New Uri("http://" & wiki.Code & ".wikimedia.org/w")
                    End If
                End If

                For Each prop As KeyValuePair(Of String, String) In props
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
                        Case "popular" : wiki.IsPopular = prop.Value.ToBoolean
                        Case "read" : wiki.IsPublicReadable = prop.Value.ToBoolean
                        Case "secure" : wiki.SecureUrl = New Uri(prop.Value)
                        Case "type" : wiki.Type = prop.Value
                        Case "url" : wiki.Url = New Uri(prop.Value)
                    End Select
                Next prop
            Next node
        End Sub

        Public Sub SaveLocal()
            Try
                File.WriteAllText(Path, Config.MakeConfig(WriteConfig(True)), Encoding.UTF8)
                Config.Global.NeedsUpdate = False
                Log.Debug("Saved global config [L]")

                FamilyConfig.SaveAll()

            Catch ex As IOException
                Log.Write(Msg("globalconfig-savefail", ex.Message))
            End Try
        End Sub

        Private Function WriteConfig(ByVal local As Boolean) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            items.Add("anon-login", AnonymousLogin)
            items.Add("auto-unified-login", AutoUnifiedLogin)
            items.Add("download-location", DownloadLocation)
            items.Add("scary-pattern", ScaryPattern)
            If TimeZones.Count > 0 Then items.Add("time-zones", TimeZones.ToDictionary(Of String, Object))
            If TopWiki IsNot Nothing Then items.Add("top-wiki", TopWiki.Code)
            items.Add("version", LatestVersion)
            items.Add("wikiconfig-page", WikiConfigPageName)

            items.Add("default-family-config", App.Families.Default.Config.WriteConfig(True))
            items.Add("default-user-config", App.Wikis.Default.Users.Default.Config.WriteConfig(True))
            items.Add("default-wiki-config", App.Wikis.Default.Config.WriteConfig(True))

            Dim languageConfigs As New Dictionary(Of String, Object)

            For Each language As Language In App.Languages.All
                Dim config As New Dictionary(Of String, Object)

                config.Add("name", language.Name)
                If language.IsHidden Then config.Add("hidden", True)
                If language.IsLocalized Then config.Add("localized", True)

                languageConfigs.Add(language.Code, config)
            Next language

            items.Add("languages", languageConfigs)

            Dim wikiConfigs As New Dictionary(Of String, Object)

            For Each wiki As Wiki In App.Wikis.All
                Dim wikiItems As New Dictionary(Of String, Object)

                If Not wiki.AccountConfirmation Then wikiItems.Add("account-confirmation", False)
                If Not wiki.AnonymousLogin Then wikiItems.Add("anon-login", False)
                If wiki.Channel IsNot Nothing Then wikiItems.Add("channel", wiki.Channel.Remove("#"))
                If wiki.IsCustom Then wikiItems.Add("custom", True)
                If Not wiki.IsPublicEditable Then wikiItems.Add("edit", False)
                wikiItems.Add("family", If(wiki.Family Is Nothing, "none", wiki.Family.Name))
                If wiki.FileUrl IsNot Nothing Then wikiItems.Add("files", wiki.FileUrl.ToString)
                If wiki.IsHidden Then wikiItems.Add("hidden", True)
                If wiki.IsPopular Then wikiItems.Add("popular", True)
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
                        If wiki.Language IsNot Nothing AndAlso wiki.FileUrl.ToString = Config.Internal.WikimediaFilePath _
                            & wiki.Type & "/" & wiki.Language.Code & "/" Then wikiItems.Remove("files")
                        If wiki.Language IsNot Nothing AndAlso wiki.SecureUrl.ToString = Config.Internal.WikimediaSecurePath _
                            & wiki.Type & "/" & wiki.Language.Code & "/w" Then wikiItems.Remove("secure")
                        If wiki.Name = Msg("login-langwikiname", UcFirst(wiki.Type), wiki.Language.Code, wiki.Language.Name) Then wikiItems.Remove("name")
                        If wiki.Type = wiki.Code.FromFirst(".") Then wikiItems.Remove("type")
                        If wiki.Url.ToString = "http://" & wiki.Language.Code & "." & wiki.Type & ".org/w" Then wikiItems.Remove("url")
                    Else
                        If wiki.Channel = "#" & wiki.Code & ".wikimedia" Then wikiItems.Remove("channel")
                        If wiki.FileUrl.ToString = Config.Internal.WikimediaFilePath & "wikipedia/" & wiki.Code & "/" _
                            Then wikiItems.Remove("files")
                        If wiki.SecureUrl.ToString = Config.Internal.WikimediaSecurePath & "wikipedia/" & wiki.Code & "/w" _
                            Then wikiItems.Remove("secure")
                        If wiki.Type = "special" Then wikiItems.Remove("type")
                        If wiki.Url.ToString = "http://" & wiki.Code & ".wikimedia.org/w" Then wikiItems.Remove("url")
                    End If
                End If

                wikiConfigs.Add(wiki.Code, wikiItems)
            Next wiki

            items.Add("wikis", wikiConfigs)

            Return items
        End Function

    End Class

End Namespace
