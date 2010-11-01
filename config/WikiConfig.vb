Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    Public Class WikiConfig : Inherits Config

        Private _ExtraLoader As ExtraWikiConfig

        Private Wiki As Wiki

        Private ReadOnly IgnoredMessages As String() = {
            "bad_image_list", "common.css", "common.js", "monobook.css", "monobook.js",
            "robots.txt", "spam-blacklist", "spam-whitelist", "titleblacklist"
            }

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public Property AbuseFilters As New List(Of Integer)
        Public Property ApiEnabled As Boolean
        Public Property ApiErrorCount As Integer
        Public Property Autoconfirm As Boolean
        Public Property AutoconfirmTime As TimeSpan
        Public Property AutoconfirmEdits As Integer
        Public Property BlockSummary As String
        Public Property BlockTime As String
        Public Property BlockTimeAnon As String
        Public Property BlpTalkTag As String
        Public Property ChangeTagIdentifier As String
        Public Property CustomSpamBlacklists As New List(Of Page)
        Public Property Database As String
        Public Property DatabaseVersion As String
        Public Property DefaultSkin As String
        Public Property EngineRevision As Integer
        Public Property EngineVersion As String
        Public Property EstablishedUsers As Page
        Public Property FirstLetterCaseSensitive As Boolean
        Public Property GadgetIdentifierPattern As Regex = New Regex("''([^']+)(?::''|'':)\ ")
        Public Property Logo As String = "Wiki.png"
        Public Property MessageSummary As String
        Public Property Minor As New List(Of String)
        Public Property QuickReview As Boolean
        Public Property QuickReviewComment As String
        Public Property QuickReviewLevels As Dictionary(Of ReviewFlag, Integer)
        Public Property PageSizeTransition As Integer
        Public Property ParamNorm As Dictionary(Of String, List(Of String))
        Public Property PlatformName As String
        Public Property PlatformVersion As String
        Public Property PriorityQuery As String
        Public Property PriorityCacheTime As TimeSpan
        Public Property [ReadOnly] As Boolean
        Public Property ReadOnlyReason As String
        Public Property ReportDiffs As Boolean
        Public Property ReportPages As New List(Of Page)
        Public Property ReportPatterns As New List(Of Regex)
        Public Property ReportUserPage As Page
        Public Property RevertAlwaysBlank As List(Of Integer) = List(3)
        Public Property RevertBlankTalk As Boolean
        Public Property RevertPatterns As New List(Of Regex)
        Public Property RevertSummaryBlank As String
        Public Property RevertSummaryMultipleRevs As String = "{0} revisions"
        Public Property RevertSummaryMultipleUsers As String = "{0} users"
        Public Property RevertSummaryLastVersion As String = "last version"
        Public Property RevertSummaryPreviousVersion As String = "a previous version"
        Public Property RevertSummaryRollback As String
        Public Property RevertSummaryUndo As String
        Public Property RevertSummaryUnknownRevs As String = "revisions"
        Public Property RevertSummary As String = "Revert {0} by {1} to {2}"
        Public Property ReviewComments As Boolean
        Public Property ReviewFlags As New Dictionary(Of String, ReviewFlag)
        Public Property SanctionLevels As Integer
        Public Property SanctionPatterns As New Dictionary(Of Regex, SanctionType)
        Public Property ServerTimeOffset As TimeSpan
        Public Property SharedTemplates As List(Of Page)
        Public Property SummaryTag As String
        Public Property TagPatterns As List(Of Regex)
        Public Property UnavailableFeatures As New List(Of String)
        Public Property UndoActionSummary As String
        Public Property UseFallback As Boolean
        Public Property UserLink As String
        Public Property UserLinkAnon As String
        Public Property VandalReportDiffs As Integer
        Public Property VandalReportFormat As String
        Public Property VandalReportPage As Page
        Public Property VandalReportReason As String
        Public Property VandalReportSingleNote As String
        Public Property VandalReportSummary As String
        Public Property WarningAge As TimeSpan
        Public Property WarningAutoIncrement As Boolean
        Public Property WarningDefaultType As String
        Public Property WarningLevels As Integer
        Public Property WarningMessages As New Dictionary(Of SanctionType, String)
        Public Property WarningMonthHeadings As Boolean
        Public Property WarningSummaries As New Dictionary(Of Integer, String)

        Public ReadOnly Property ConfigPage() As Page
            Get
                Return Wiki.Pages.FromString(Config.Global.WikiConfigPageTitle)
            End Get
        End Property

        Public ReadOnly Property ExtraLoader() As ExtraWikiConfig
            Get
                If _ExtraLoader Is Nothing Then _ExtraLoader = New ExtraWikiConfig(Wiki)
                Return _ExtraLoader
            End Get
        End Property

        Public Property ExtraConfigLoaded() As Boolean

        Public ReadOnly Property IsMinor(ByVal action As String) As Boolean
            Get
                Return Minor.Contains(action)
            End Get
        End Property

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return Path.Combine("wiki", GetValidFileName(Wiki.Code))
            End Get
        End Property

        Public ReadOnly Property PriorityNeedsUpdate As Boolean
            Get
                Return False
            End Get
        End Property

        Private Function PriorityListLocation() As String
            Return Path.Combine("wiki", "priority-" & GetValidFileName(Wiki.Code))
        End Function

        Public Overrides Sub Load(ByVal text As String)
            MyBase.Load(text)
            If Wiki.IsDefault OrElse text Is Nothing Then Return

            ReportPages.Merge(VandalReportPage)
            ReportPages.Merge(ReportUserPage)

            If RevertSummaryRollback IsNot Nothing Then RevertSummaryRollback = RevertSummaryRollback.FormatWith("$1", "$2")

            If Wiki.Messages.ContainsKey("undo-summary") Then
                RevertPatterns.Add(New Regex(FormatMwMessage(EscapeMwMessage(WikiStripSummary(Wiki.Message("undo-summary"))), _
                    "(?<rvdrev>.+?)", "(?<rvduser>.+?)"), RegexOptions.Compiled))

                If RevertSummaryUndo Is Nothing Then RevertSummaryUndo =
                    FormatMwMessage(Wiki.Message("undo-summary"), "{0}", "{1}")
            End If

            If Wiki.Messages.ContainsKey("revertpage") Then
                RevertPatterns.Add(New Regex(FormatMwMessage(EscapeMwMessage(WikiStripSummary(Wiki.Message("revertpage"))),
                    "(?<olduser>.+?)", "(?<rvduser>.+?)"), RegexOptions.Compiled))
                If RevertSummaryRollback Is Nothing Then RevertSummaryRollback = Wiki.Message("revertpage")
            End If

            If SummaryTag IsNot Nothing Then SummaryTag = " " & SummaryTag.Trim

            Wiki.Users.Anonymous.Groups.Add(Wiki.UserGroups("*"))
            Wiki.Users.Anonymous.Rights.AddRange(Wiki.UserGroups("*").Rights)

            Wiki.IsPublicEditable = Wiki.UserGroups("*").Rights.Contains("edit")
            Wiki.IsPublicReadable = Wiki.UserGroups("*").Rights.Contains("read")

            SetFeedPatterns()
        End Sub

        Public Overrides Sub SaveLocal()
            MyBase.SaveLocal()
            If Wiki.Pages.Priority IsNot Nothing Then Config.SaveFile(PriorityListLocation, Wiki.Pages.Priority.Join(LF))
        End Sub

        Public Sub SaveWiki()

        End Sub

        Protected Overrides Sub ReadConfig(ByVal text As String)
            Dim source As String = "wiki:" & Wiki.Code

            For Each mainProp As KeyValuePair(Of String, String) In Config.ParseConfig(source, Nothing, text)
                Dim key As String = mainProp.Key, value As String = mainProp.Value

                Try
                    Select Case key
                        Case "abuse-filters"
                            Dim items As Dictionary(Of String, String) = Config.ParseConfig(source, key, value)

                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim filter As AbuseFilter = Wiki.AbuseFilters(CInt(item.Key))
                                filter.IsEnabled = False

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "actions" : filter.Actions = List(prop.Value.Split(",")).Trim
                                        Case "deleted" : filter.IsDeleted = prop.Value.ToBoolean
                                        Case "description" : filter.Description = prop.Value
                                        Case "enabled" : filter.IsEnabled = prop.Value.ToBoolean
                                        Case "hits" : filter.TotalHits = CInt(prop.Value)
                                        Case "last-modified" : filter.LastModified = prop.Value.ToDate
                                        Case "last-modified-by" : filter.LastModifiedBy = Wiki.Users.FromString(prop.Value)
                                        Case "notes" : filter.Notes = prop.Value
                                        Case "pattern" : filter.Pattern = prop.Value
                                        Case "private" : filter.IsPrivate = prop.Value.ToBoolean
                                    End Select
                                Next prop
                            Next item

                        Case "change-tag-identifier"
                            ChangeTagIdentifier = value

                        Case "change-tags"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim changeTag As ChangeTag = Wiki.ChangeTags(item.Key)

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "count" : changeTag.Hits = CInt(prop.Value)
                                        Case "description" : changeTag.Description = prop.Value
                                        Case "display-name" : changeTag.DisplayName = prop.Value
                                    End Select
                                Next prop
                            Next item

                        Case "database" : Database = value
                        Case "database-version" : DatabaseVersion = value
                        Case "default-queue" : Wiki.Queues.Default = Wiki.Queues(value)
                        Case "default-skin" : DefaultSkin = value
                        Case "default-user-config" : Wiki.Users.Default.Config.Load(value)
                        Case "engine" : Wiki.Engine = value
                        Case "engine-revision" : EngineRevision = CInt(value)
                        Case "engine-version" : EngineVersion = value
                        Case "established-users" : EstablishedUsers = Wiki.Pages.FromString(value)

                        Case "extensions"
                            Wiki.Extensions.Clear()

                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim extension As Extension = Wiki.Extensions(item.Key)

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "author" : extension.Author = prop.Value
                                        Case "description" : extension.Description = prop.Value
                                        Case "type" : extension.Type = prop.Value
                                        Case "url" : extension.Url = New Uri(prop.Value)
                                        Case "version" : extension.Version = prop.Value
                                    End Select
                                Next prop
                            Next item

                        Case "extra-config-loaded" : ExtraConfigLoaded = value.ToBoolean
                        Case "first-letter-case-sensitive" : FirstLetterCaseSensitive = value.ToBoolean
                        Case "gadget-identifier-pattern" : GadgetIdentifierPattern = New Regex(Config.UnescapeWs(value))

                        Case "gadgets"
                            Wiki.Gadgets.Clear()

                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim gadget As Gadget = Wiki.Gadgets(item.Key)

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "description" : gadget.Description = prop.Value
                                        Case "name" : gadget.Name = prop.Value
                                        Case "type" : gadget.Type = prop.Value
                                        Case "type-desc" : gadget.TypeDesc = prop.Value
                                    End Select
                                Next prop
                            Next item

                        Case "groups"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Dim group As UserGroup = Wiki.UserGroups(item.Key)

                                    Select Case prop.Key
                                        Case "count" : group.Count = CInt(prop.Value)
                                        Case "implicit" : group.IsImplicit = prop.Value.ToBoolean
                                        Case "rights" : group.Rights.AddRange(prop.Value.ToList(","))
                                    End Select
                                Next prop
                            Next item

                        Case "language" : Wiki.Language = App.Languages(value)
                        Case "platform" : PlatformName = value
                        Case "platform-version" : PlatformVersion = value
                        Case "license" : Wiki.License = value
                        Case "license-url" : Wiki.LicenseUrl = New Uri(value)
                        Case "logo" : Logo = value
                        Case "main-page" : Wiki.MainPage = Wiki.Pages.FromString(value)

                        Case "messages"
                            For Each message As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Wiki.Messages.Merge(message.Key, message.Value)
                            Next message

                        Case "namespaces"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim space As Space = Wiki.Spaces(CInt(item.Key))

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "name" : space.Name = prop.Value
                                        Case "content" : space.IsContent = prop.Value.ToBoolean
                                        Case "edit-restricted" : space.IsEditRestricted = prop.Value.ToBoolean
                                        Case "move-restricted" : space.IsMoveRestricted = prop.Value.ToBoolean
                                        Case "movable" : space.IsMovable = prop.Value.ToBoolean
                                        Case "special" : space.IsSpecial = prop.Value.ToBoolean
                                        Case "subpages" : space.HasSubpages = prop.Value.ToBoolean
                                    End Select
                                Next prop
                            Next item

                        Case "page-size-transition" : PageSizeTransition = value.ToInteger
                        Case "preferences" : Wiki.Preferences = value.ToList.Trim
                        Case "priority-cache-time" : PriorityCacheTime = New TimeSpan(0, value.ToInteger, 0)
                        Case "priority-query" : PriorityQuery = value

                        Case "review-flags"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim flag As ReviewFlag = Wiki.ReviewFlags(item.Key)

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "default-level" : flag.DefaultLevel = CInt(prop.Value)
                                        Case "display-name" : flag.DisplayName = prop.Value
                                        Case "levels" : flag.Levels = CInt(prop.Value)
                                        Case "pristine-level" : flag.PristineLevel = CInt(prop.Value)
                                        Case "quality-level" : flag.QualityLevel = CInt(prop.Value)
                                    End Select
                                Next prop
                            Next item

                        Case "queues"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)
                                Dim queue As Queue = Wiki.Queues(item.Key)

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "enabled" : queue.Enabled = prop.Value.ToBoolean
                                        Case "filter" : queue.Filter = prop.Value
                                        Case "list" : If queue.SourceType = QueueSourceType.List Then queue.Source = New ListSource(Nothing)
                                        Case "query" : If queue.SourceType = QueueSourceType.Query Then queue.Source = New QuerySource(prop.Value)
                                        Case "query-re-add" : queue.QueryReAdd = prop.Value.ToBoolean
                                        Case "re-evaluate" : queue.ReEvaluate = prop.Value.ToBoolean
                                        Case "remove-contribs" : queue.RemoveContribs = prop.Value.ToBoolean
                                        Case "remove-history" : queue.RemoveHistory = prop.Value.ToBoolean
                                        Case "remove-viewed" : queue.RemoveViewed = prop.Value.ToBoolean
                                        Case "source"
                                            Select Case prop.Value
                                                Case "rc" : queue.Source = Wiki.Rc
                                                Case "query" : queue.Source = New QuerySource(Nothing)
                                                Case "list" : queue.Source = New ListSource(Nothing)
                                            End Select
                                    End Select
                                Next prop
                            Next item

                        Case "server-time-offset" : ServerTimeOffset = New TimeSpan(CLng(CDbl(value) * 10000000))

                        Case "skins"
                            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig(source, key, value)

                            Next item

                        Case "spam-list" : ReadSpamLists(Wiki.SpamLists, source, value)

                        Case "activeusers" : Wiki.ActiveUsers = CInt(value)
                        Case "files" : Wiki.Files.Count = CInt(value)
                        Case "pages" : Wiki.Pages.Count = CInt(value)
                        Case "revisions" : Wiki.Revisions.Count = CInt(value)
                        Case "updated" : Updated = value.ToDate
                        Case "users" : Wiki.Users.Count = CInt(value)
                    End Select

                Catch ex As SystemException
                    Log.Write(Result.FromException(ex).Wrap(Msg("error-configvalue", key, source)).LogMessage)
                End Try
            Next mainProp
        End Sub

        Public Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
            Dim items As New Dictionary(Of String, Object)

            items.Add("default-queue", Wiki.Queues.Default)

            If Not Wiki.AccountConfirmation Then items.Add("account-confirmation", False)

            If ChangeTagIdentifier IsNot Nothing Then items.Add("change-tag-identifier", ChangeTagIdentifier)
            If PageSizeTransition > 0 Then items.Add("page-size-transition", PageSizeTransition)

            If Wiki.Queues.All.Count > 0 Then
                Dim queues As New Dictionary(Of String, Object)

                Dim defQueue As New Queue(App.Wikis.Default, "default")

                For Each queue As Queue In Wiki.Queues.All
                    Dim item As New Dictionary(Of String, Object)

                    If queue.Enabled <> defQueue.Enabled Then item.Add("enabled", queue.Enabled)
                    If queue.Filter <> defQueue.Filter Then item.Add("filter", queue.Filter)

                    If queue.SourceType = QueueSourceType.List Then item.Add _
                        ("list", CType(queue.Source, ListSource).List.ToStringArray.Join(","))
                    If queue.SourceType = QueueSourceType.Query Then item.Add _
                        ("query", CType(queue.Source, QuerySource).Query)

                    If queue.QueryReAdd <> defQueue.QueryReAdd Then item.Add("query-re-add", queue.QueryReAdd)
                    If queue.ReEvaluate <> defQueue.ReEvaluate Then item.Add("re-evaluate", queue.ReEvaluate)
                    If queue.RemoveContribs <> defQueue.RemoveContribs Then item.Add("remove-contribs", queue.RemoveContribs)
                    If queue.RemoveHistory <> defQueue.RemoveHistory Then item.Add("remove-history", queue.RemoveHistory)
                    If queue.RemoveViewed <> defQueue.RemoveViewed Then item.Add("remove-viewed", queue.RemoveViewed)
                    If queue.SourceType <> defQueue.SourceType Then item.Add("source-type", queue.SourceType.ToString.ToLower)

                    queues.Add(queue.Name, item)
                Next queue

                items.Add("queues", queues)
            End If

            If target = ConfigTarget.Wiki OrElse Wiki.IsDefault Then Return items

            'Cache additional information about the wiki's config that is not Huggle-specific
            'This improves startup time

            'Abuse filters
            If Wiki.AbuseFilters.All.Count > 0 Then
                Dim abuseFilters As New Dictionary(Of String, Object)

                For Each abuseFilter As AbuseFilter In Wiki.AbuseFilters.All
                    Dim item As New Dictionary(Of String, Object)

                    If abuseFilter.Actions.Count > 0 Then item.Add("actions", abuseFilter.Actions.Join(", "))
                    If abuseFilter.Description IsNot Nothing Then item.Add("description", abuseFilter.Description)
                    If abuseFilter.IsDeleted Then item.Add("deleted", True)
                    If abuseFilter.IsEnabled Then item.Add("enabled", True)
                    If abuseFilter.IsPrivate Then item.Add("private", True)
                    If abuseFilter.LastModified > Date.MinValue Then item.Add("last-modified", abuseFilter.LastModified)
                    If abuseFilter.LastModifiedBy IsNot Nothing Then item.Add("last-modified-by", abuseFilter.LastModifiedBy)
                    If abuseFilter.Notes IsNot Nothing Then item.Add("notes", abuseFilter.Notes)
                    If abuseFilter.Pattern IsNot Nothing Then item.Add("pattern", abuseFilter.Pattern)
                    If abuseFilter.TotalHits > -1 Then item.Add("hits", abuseFilter.TotalHits)

                    abuseFilters.Add(CStr(abuseFilter.Id).PadLeft(4, "0"c), item)
                Next abuseFilter

                items.Add("abuse-filters", abuseFilters)
            End If

            'Change tags
            If Wiki.ChangeTags.All.Count > 0 Then
                Dim changeTags As New Dictionary(Of String, Object)

                For Each changeTag As ChangeTag In Wiki.ChangeTags.All
                    Dim item As New Dictionary(Of String, Object)

                    item.Add("count", changeTag.Hits)
                    If Not String.IsNullOrEmpty(changeTag.Description) Then item.Add("description", changeTag.Description)
                    If Not String.IsNullOrEmpty(changeTag.DisplayName) AndAlso changeTag.DisplayName <> changeTag.Name _
                        Then item.Add("display-name", changeTag.DisplayName)

                    changeTags.Add(changeTag.Name, item)
                Next changeTag

                items.Add("change-tags", changeTags)
            End If

            'MediaWiki extensions
            If Wiki.Extensions.All.Count > 0 Then
                Dim extensions As New Dictionary(Of String, Object)

                For Each extension As Extension In Wiki.Extensions.All
                    Dim item As New Dictionary(Of String, Object)

                    If extension.Author IsNot Nothing Then item.Add("author", extension.Author)
                    If extension.Description IsNot Nothing Then item.Add("description", extension.Description)
                    If extension.Type <> "other" Then item.Add("type", extension.Type)
                    If extension.Url IsNot Nothing Then item.Add("url", extension.Url)
                    If extension.Version IsNot Nothing Then item.Add("version", extension.Version)

                    extensions.Add(extension.Name, item)
                Next extension

                items.Add("extensions", extensions)
            End If

            'User groups
            Dim groups As New Dictionary(Of String, Object)

            For Each group As UserGroup In Wiki.UserGroups.All
                Dim item As New Dictionary(Of String, Object)

                If group.Count >= 0 Then item.Add("count", group.Count)
                If group.IsImplicit Then item.Add("implicit", True)
                item.Add("rights", group.Rights.Join(","))

                groups.Add(group.Name, item)
            Next group

            items.Add("groups", groups)

            'Namespaces
            Dim spaces As New Dictionary(Of String, Object)

            For Each space As Space In Wiki.Spaces.All
                Dim item As New Dictionary(Of String, Object)

                If space.IsContent Then item.Add("content", True)
                If space.Name IsNot Nothing Then item.Add("name", space.Name)
                If space.IsEditRestricted Then item.Add("edit-restricted", True)
                If space.IsMoveRestricted Then item.Add("move-restricted", True)
                If Not space.IsMovable Then item.Add("movable", False)
                If space.IsSpecial Then item.Add("special", True)
                If Not space.IsSpecial AndAlso Not space.HasSubpages Then item.Add("subpages", False)

                spaces.Add(If(space.Number < 0, "-", "") & CStr(Math.Abs(space.Number)).PadLeft(4, "0"c), item)
            Next space

            items.Add("namespaces", spaces)

            'MediaWiki interface messages
            If Wiki.Messages.Count > 0 Then
                Dim messages As New Dictionary(Of String, Object)

                For Each msg As KeyValuePair(Of String, String) In Wiki.Messages
                    If Not IgnoredMessages.Contains(msg.Key) Then messages.Add(msg.Key, msg.Value)
                Next msg

                items.Add("messages", messages)
            End If

            'Reviewing configuration
            If Wiki.ReviewFlags.All.Count > 0 Then
                Dim flags As New Dictionary(Of String, Object)

                For Each flag As ReviewFlag In Wiki.ReviewFlags.All
                    Dim item As New Dictionary(Of String, Object)

                    item.Add("default-level", flag.DefaultLevel)
                    item.Add("display-name", flag.DisplayName)
                    item.Add("levels", flag.Levels)
                    item.Add("pristine-level", flag.PristineLevel)
                    item.Add("quality-level", flag.QualityLevel)

                    flags.Add(flag.Name, item)
                Next flag

                items.Add("review-flags", flags)
            End If

            'MediaWiki skins
            If Wiki.Skins.Count > 0 Then
                Dim skins As New Dictionary(Of String, Object)

                For Each skin As WikiSkin In Wiki.Skins.Values
                    skins.Add(skin.Code, skin.Name)
                Next skin

                items.Add("skins", skins)
            End If

            'MediaWiki gadgets
            If Wiki.Gadgets.All.Count > 0 Then
                Dim gadgets As New Dictionary(Of String, Object)

                For Each gadget As Gadget In Wiki.Gadgets.All
                    Dim item As New Dictionary(Of String, Object)

                    item.Add("description", gadget.Description)
                    If gadget.Name <> gadget.Code Then item.Add("name", gadget.Name)
                    item.Add("type", gadget.Type)
                    item.Add("typedesc", gadget.TypeDesc)

                    gadgets.Add(gadget.Code, item)
                Next gadget

                items.Add("gadgets", gadgets)
            End If

            'Miscellaneous
            Dim def As WikiConfig = App.Wikis.Default.Config

            If Database <> def.Database Then items.Add("database", Database)
            If DatabaseVersion <> def.DatabaseVersion Then items.Add("database-version", DatabaseVersion)
            items.Add("default-user-config", Wiki.Users.Default.Config.WriteConfig(ConfigTarget.Local))
            If DefaultSkin IsNot Nothing Then items.Add("default-skin", DefaultSkin)
            If Wiki.Engine <> def.Wiki.Engine Then items.Add("engine", Wiki.Engine)
            If EngineRevision <> def.EngineRevision Then items.Add("engine-revision", EngineRevision)
            If EngineVersion <> def.EngineVersion Then items.Add("engine-version", EngineVersion)
            If FirstLetterCaseSensitive <> def.FirstLetterCaseSensitive _
                Then items.Add("first-letter-case-sensitive", FirstLetterCaseSensitive)
            If GadgetIdentifierPattern IsNot def.GadgetIdentifierPattern _
                Then items.Add("gadget-identifier-pattern", Config.EscapeWs(GadgetIdentifierPattern.ToString))
            If PlatformName <> def.PlatformName Then items.Add("platform", PlatformName)
            If PlatformVersion <> def.PlatformVersion Then items.Add("platform-version", PlatformVersion)
            If Logo <> def.Logo Then items.Add("logo", Logo)
            If Wiki.License <> def.Wiki.License Then items.Add("license", Wiki.License)
            If Wiki.LicenseUrl <> def.Wiki.LicenseUrl Then items.Add("license-url", Wiki.LicenseUrl)
            If Wiki.MainPage IsNot def.Wiki.MainPage Then items.Add("main-page", Wiki.MainPage)
            If Wiki.Preferences IsNot Nothing Then items.Add("preferences", Wiki.Preferences)
            If Updated <> Date.MinValue Then items.Add("updated", Updated)

            If target = ConfigTarget.Local Then
                If ExtraConfigLoaded Then items.Add("extra-config-loaded", True)
                If ServerTimeOffset <> TimeSpan.Zero Then items.Add("server-time-offset", ServerTimeOffset.TotalSeconds)
            End If

            Return items
        End Function

        Public Function Copy(ByVal wiki As Wiki) As WikiConfig
            Dim result As New WikiConfig(wiki)
            result.ReadConfig(Config.MakeConfig(WriteConfig(ConfigTarget.Local)))
            Return result
        End Function

        Private Sub SetFeedPatterns()
            'In order to reliably interpret RC feed messages, I essentially have to be a MediaWiki parser.
            'They honestly couldn't make a less machine-friendly interface if they tried.

            Wiki.FeedPatterns.Clear()
            If Wiki.Messages.Count = 0 Then Return

            Wiki.FeedPatterns.Add("edit", New Regex(Feed.BasePattern.FormatWith("(!?)(M?)(B?)", _
                "[^ ]+diff=(\d+)&oldid=(\d+)(?:&rcid=(\d+))?", "\(\cB?(.+?)\cB?\)", "(.+?)", ""), RegexOptions.Compiled))

            Wiki.FeedPatterns.Add("new", New Regex(Feed.BasePattern.FormatWith("(!?)N(M?)(B?)", _
                "[^ ]+oldid=(\d+)(?:&rcid=(\d+))?", "\((.+?)\)", "(.+?)", ""), RegexOptions.Compiled))

            AddLogPattern(Wiki, "autocreate", "newuserlog-autocreate-entry")
            AddLogPattern(Wiki, "create", "newuserlog-create-entry")
            AddLogPattern(Wiki, "approve", "review-logentry-app", "\cC02([^\cC]+)\cC10", "(\d+)")
            AddLogPattern(Wiki, "block", "blocklogentry", "User:(.+?) \((.+?)\)", "(.+?)", "(.+?)")
            AddLogPattern(Wiki, "create2", "newuserlog-create2-entry", "User:(.+?)")
            AddLogPattern(Wiki, "delete", "deletedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern(Wiki, "modify", "modifiedarticleprotection", "\[\[\cC02([^\cC]+)\cC10\]\]")
            AddLogPattern(Wiki, "move", "1movedto2", "\cC02([^\cC]+)\cC10", "(.+?)")
            AddLogPattern(Wiki, "move_redir", "1movedto2_redir", "\cC02([^\cC]+)\cC10", "(.+?)")
            AddLogPattern(Wiki, "overwrite", "overwroteimage", "\cC02([^\cC]+)\cC10")
            AddLogPattern(Wiki, "patrol", "patrol-log-line", "(r(\d+))", "\[\[\cC02([^\cC]+)\cC10\]\]", "(.+?)")
            AddLogPattern(Wiki, "protect", "protectedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern(Wiki, "reblock", "reblock-logentry", "\cC02User:([^\cC]+)\cC10", "(.+?)", "\((.+?)\)")
            AddLogPattern(Wiki, "restore", "undeletedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern(Wiki, "renameuser", "renameuserlogentry", "(.+?)", "(.+?)", _
                MwMessagePattern(Wiki.Message("renameuser-log"), ".+?", "(.+?)"))
            AddLogPattern(Wiki, "rights", "rightslogentry", "(.+?)", "(.+?)", "(.+?)")
            AddLogPattern(Wiki, "unapprove", "review-logentry-app", "\cC02([^\cC]+)\cC10", "(\d+)")
            AddLogPattern(Wiki, "unblock", "unblocklogentry", "User:(.+?)")
            AddLogPattern(Wiki, "unprotect", "unprotectedarticle", "(.+?)")
            AddLogPattern(Wiki, "upload", "uploadedimage", "\cC02([^\cC]+)\cC10")
        End Sub

        Private Sub AddLogPattern(ByVal wiki As Wiki, ByVal action As String, _
            ByVal message As String, ByVal ParamArray paramPatterns As String())

            If wiki.Message(message) IsNot Nothing Then wiki.FeedPatterns.Add _
                (action, New Regex(Feed.BasePattern.FormatWith(Regex.Escape(action), "", "", MwMessagePattern _
                (wiki.Message(message), paramPatterns)), RegexOptions.Compiled))
        End Sub

    End Class

End Namespace
