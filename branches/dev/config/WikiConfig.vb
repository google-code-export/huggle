Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    Friend Class WikiConfig : Inherits Config

        Private _ExtraLoader As ExtraWikiConfig

        Private Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend Property AbuseFilters As New List(Of Integer)
        Friend Property ApiEnabled As Boolean
        Friend Property ApiErrorCount As Integer
        Friend Property Autoconfirm As Boolean
        Friend Property AutoconfirmTime As TimeSpan
        Friend Property AutoconfirmEdits As Integer
        Friend Property BlockSummary As String
        Friend Property BlockTime As String
        Friend Property BlockTimeAnon As String
        Friend Property BlpTalkTag As String
        Friend Property ChangeTagIdentifier As String
        Friend Property CustomSpamBlacklists As New List(Of Page)
        Friend Property Database As String
        Friend Property DatabaseVersion As String
        Friend Property DefaultSkin As String
        Friend Property EngineRevision As Integer
        Friend Property EngineVersion As String
        Friend Property EstablishedUsers As Page
        Friend Property FirstLetterCaseSensitive As Boolean
        Friend Property GadgetIdentifierPattern As Regex = New Regex("''([^']+)(?::''|'':)\ ")
        Friend Property Logo As String = "Wiki.png"
        Friend Property MessageSummary As String
        Friend Property Minor As New List(Of String)
        Friend Property QuickReview As Boolean
        Friend Property QuickReviewComment As String
        Friend Property QuickReviewLevels As Dictionary(Of ReviewFlag, Integer)
        Friend Property PageSizeTransition As Integer
        Friend Property ParamNorm As Dictionary(Of String, List(Of String))
        Friend Property PlatformName As String
        Friend Property PlatformVersion As String
        Friend Property PriorityQuery As String
        Friend Property PriorityCacheTime As TimeSpan
        Friend Property [ReadOnly] As Boolean
        Friend Property ReadOnlyReason As String
        Friend Property ReportDiffs As Boolean
        Friend Property ReportPages As New List(Of Page)
        Friend Property ReportPatterns As New List(Of Regex)
        Friend Property ReportUserPage As Page
        Friend Property RevertAlwaysBlank As List(Of Integer) = List(3)
        Friend Property RevertBlankTalk As Boolean
        Friend Property RevertPatterns As New List(Of Regex)
        Friend Property RevertSummaryBlank As String
        Friend Property RevertSummaryMultipleRevs As String = "{0} revisions"
        Friend Property RevertSummaryMultipleUsers As String = "{0} users"
        Friend Property RevertSummaryLastVersion As String = "last version"
        Friend Property RevertSummaryPreviousVersion As String = "a previous version"
        Friend Property RevertSummaryRollback As String
        Friend Property RevertSummaryUndo As String
        Friend Property RevertSummaryUnknownRevs As String = "revisions"
        Friend Property RevertSummary As String = "Revert {0} by {1} to {2}"
        Friend Property ReviewComments As Boolean
        Friend Property ReviewFlags As New Dictionary(Of String, ReviewFlag)
        Friend Property SanctionLevels As Integer
        Friend Property SanctionPatterns As New Dictionary(Of Regex, SanctionType)
        Friend Property ServerTimeOffset As TimeSpan
        Friend Property SharedTemplates As List(Of Page)
        Friend Property SummaryTag As String
        Friend Property TagPatterns As List(Of Regex)
        Friend Property UnavailableFeatures As New List(Of String)
        Friend Property UndoActionSummary As String
        Friend Property UseFallback As Boolean
        Friend Property UserLink As String
        Friend Property UserLinkAnon As String
        Friend Property VandalReportDiffs As Integer
        Friend Property VandalReportFormat As String
        Friend Property VandalReportPage As Page
        Friend Property VandalReportReason As String
        Friend Property VandalReportSingleNote As String
        Friend Property VandalReportSummary As String
        Friend Property WarningAge As TimeSpan
        Friend Property WarningAutoIncrement As Boolean
        Friend Property WarningDefaultType As String
        Friend Property WarningLevels As Integer
        Friend Property WarningMessages As New Dictionary(Of SanctionType, String)
        Friend Property WarningMonthHeadings As Boolean
        Friend Property WarningSummaries As New Dictionary(Of Integer, String)

        Friend ReadOnly Property ConfigPage() As Page
            Get
                Return Wiki.Pages.FromString(Config.Global.WikiConfigPageTitle)
            End Get
        End Property

        Friend ReadOnly Property ExtraLoader() As ExtraWikiConfig
            Get
                If _ExtraLoader Is Nothing Then _ExtraLoader = New ExtraWikiConfig(Wiki)
                Return _ExtraLoader
            End Get
        End Property

        Friend Property ExtraConfigLoaded() As Boolean

        Friend ReadOnly Property IsMinor(ByVal action As String) As Boolean
            Get
                Return Minor.Contains(action)
            End Get
        End Property

        Protected Overrides ReadOnly Property Location() As String
            Get
                Return Path.Combine("wiki", GetValidFileName(Wiki.Code))
            End Get
        End Property

        Friend ReadOnly Property PriorityNeedsUpdate As Boolean
            Get
                Return False
            End Get
        End Property

        Private Function PriorityListLocation() As String
            Return Path.Combine("wiki", "priority-" & GetValidFileName(Wiki.Code))
        End Function

        Friend Overrides Sub Load(ByVal text As String)
            MyBase.Load(text)
            If Wiki.IsDefault OrElse text Is Nothing Then Return

            ReportPages.Merge(VandalReportPage)
            ReportPages.Merge(ReportUserPage)

            If RevertSummaryRollback IsNot Nothing Then RevertSummaryRollback = RevertSummaryRollback.FormatForUser("$1", "$2")

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

        Friend Overrides Sub SaveLocal()
            MyBase.SaveLocal()
            If Wiki.Pages.Priority IsNot Nothing Then Config.SaveFile(PriorityListLocation, Wiki.Pages.Priority.Join(LF))
        End Sub

        Friend Sub SaveWiki()

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
                                Dim count As Integer
                                Dim desc As String = Nothing
                                Dim displayName As String = Nothing
                                Dim name As String = Nothing

                                For Each prop As KeyValuePair(Of String, String) In Config.ParseConfig(source, key & ":" & item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "count" : count = CInt(prop.Value)
                                        Case "description" : desc = prop.Value
                                        Case "display-name" : displayName = prop.Value
                                        Case "name" : name = prop.Value
                                    End Select
                                Next prop

                                If Not String.IsNullOrEmpty(name) Then
                                    Dim changeTag As ChangeTag = Wiki.ChangeTags(name)
                                    changeTag.Description = desc
                                    changeTag.DisplayName = If(displayName, changeTag.Name)
                                    changeTag.Hits = count
                                End If
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

                        Case "page-size-transition" : PageSizeTransition = CInt(value)
                        Case "preferences" : Wiki.Preferences = value.ToList.Trim
                        Case "priority-cache-time" : PriorityCacheTime = New TimeSpan(0, CInt(value), 0)
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
                        Case "contentpages" : Wiki.ContentPages = CInt(value)
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

        Friend Overrides Function WriteConfig(ByVal target As ConfigTarget) As Dictionary(Of String, Object)
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
                    If queue.SourceType <> defQueue.SourceType Then item.Add("source-type", queue.SourceType.ToString.ToLowerI)

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

                    If abuseFilter.IsEnabled AndAlso abuseFilter.Pattern IsNot Nothing _
                        Then item.Add("pattern", abuseFilter.Pattern)

                    If abuseFilter.TotalHits > -1 Then item.Add("hits", abuseFilter.TotalHits)

                    abuseFilters.Add(CStr(abuseFilter.Id).PadLeft(4, "0"c), item)
                Next abuseFilter

                items.Add("abuse-filters", abuseFilters)
            End If

            'Change tags
            If Wiki.ChangeTags.All.Count > 0 Then
                Dim changeTags As New Dictionary(Of String, Object)

                For i As Integer = 0 To Wiki.ChangeTags.All.Count - 1
                    Dim item As New Dictionary(Of String, Object)
                    Dim changeTag As ChangeTag = Wiki.ChangeTags.All(i)

                    item.Add("count", changeTag.Hits)
                    If Not String.IsNullOrEmpty(changeTag.Description) Then item.Add("description", changeTag.Description)
                    If Not String.IsNullOrEmpty(changeTag.DisplayName) AndAlso changeTag.DisplayName <> changeTag.Name _
                        Then item.Add("display-name", changeTag.DisplayName)
                    item.Add("name", changeTag.Name)

                    changeTags.Add(i.ToStringI.PadLeft(4, "0"c), item)
                Next i

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
                    If InternalConfig.WikiMessages.Contains(msg.Key) Then
                        messages.Add(msg.Key, msg.Value)
                    Else
                        For Each msgGroup As String In InternalConfig.MessageGroups
                            If msg.Key.StartsWithI(msgGroup.ToFirst("*")) Then
                                messages.Add(msg.Key, msg.Value)
                                Exit For
                            End If
                        Next msgGroup
                    End If
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

            If Wiki.ActiveUsers > -1 Then items.Add("activeusers", Wiki.ActiveUsers)
            If Wiki.ContentPages > -1 Then items.Add("contentpages", Wiki.ContentPages)
            If Wiki.Files.Count > -1 Then items.Add("files", Wiki.Files.Count)
            If Wiki.Pages.Count > -1 Then items.Add("pages", Wiki.Pages.Count)
            If Wiki.Revisions.Count > -1 Then items.Add("revisions", Wiki.Revisions.Count)
            If Wiki.Users.Count > -1 Then items.Add("users", Wiki.Users.Count)

            If target = ConfigTarget.Local Then
                If ExtraConfigLoaded Then items.Add("extra-config-loaded", True)
                If ServerTimeOffset <> TimeSpan.Zero Then items.Add("server-time-offset", ServerTimeOffset.TotalSeconds)
            End If

            Return items
        End Function

        Friend Function Copy(ByVal wiki As Wiki) As WikiConfig
            Dim result As New WikiConfig(wiki)
            result.ReadConfig(Config.MakeConfig(WriteConfig(ConfigTarget.Local)))
            Return result
        End Function

        Private Sub SetFeedPatterns()
            'In order to reliably interpret RC feed messages, I essentially have to be a MediaWiki parser.
            'They honestly couldn't make a less machine-friendly interface if they tried.

            Wiki.FeedPatterns.Clear()
            If Wiki.Messages.Count = 0 Then Return

            Wiki.FeedPatterns.Add("edit", New Regex(Feed.BasePattern.FormatI("(!?)(M?)(B?)",
                "[^ ]+diff=(\d+)&oldid=(\d+)(?:&rcid=(\d+))?", "\(\cB?(.+?)\cB?\)", "(.+?)", ""), RegexOptions.Compiled))

            Wiki.FeedPatterns.Add("new", New Regex(Feed.BasePattern.FormatI("(!?)N(M?)(B?)",
                "[^ ]+oldid=(\d+)(?:&rcid=(\d+))?", "\((.+?)\)", "(.+?)", ""), RegexOptions.Compiled))

            AddLogPattern("autocreate", "newuserlog-autocreate-entry")
            AddLogPattern("create", "newuserlog-create-entry")
            AddLogPattern("approve", "review-logentry-app", "\cC02([^\cC]+)\cC10", "(\d+)")
            AddLogPattern("block", "blocklogentry", "User:(.+?) \((.+?)\)", "(.+?)", "(.+?)")
            AddLogPattern("create2", "newuserlog-create2-entry", "User:(.+?)")
            AddLogPattern("delete", "deletedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern("modify", "modifiedarticleprotection", "\[\[\cC02([^\cC]+)\cC10\]\]")
            AddLogPattern("move", "1movedto2", "\cC02([^\cC]+)\cC10", "(.+?)")
            AddLogPattern("move_redir", "1movedto2_redir", "\cC02([^\cC]+)\cC10", "(.+?)")
            AddLogPattern("overwrite", "overwroteimage", "\cC02([^\cC]+)\cC10")
            AddLogPattern("patrol", "patrol-log-line", "(r(\d+))", "\[\[\cC02([^\cC]+)\cC10\]\]", "(.+?)")
            AddLogPattern("protect", "protectedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern("reblock", "reblock-logentry", "\cC02User:([^\cC]+)\cC10", "(.+?)", "\((.+?)\)")
            AddLogPattern("restore", "undeletedarticle", "\cC02([^\cC]+)\cC10")
            AddLogPattern("renameuser", "renameuserlogentry", "(.+?)", "(.+?)",
                MwMessagePattern(Wiki.Message("renameuser-log"), ".+?", "(.+?)"))
            AddLogPattern("rights", "rightslogentry", "(.+?)", "(.+?)", "(.+?)")
            AddLogPattern("unapprove", "review-logentry-app", "\cC02([^\cC]+)\cC10", "(\d+)")
            AddLogPattern("unblock", "unblocklogentry", "User:(.+?)")
            AddLogPattern("unprotect", "unprotectedarticle", "(.+?)")
            AddLogPattern("upload", "uploadedimage", "\cC02([^\cC]+)\cC10")
        End Sub

        Private Sub AddLogPattern(ByVal action As String,
            ByVal message As String, ByVal ParamArray paramPatterns As String())

            If Wiki.Message(message) IsNot Nothing Then Wiki.FeedPatterns.Add(
                action, New Regex(Feed.BasePattern.FormatI(Regex.Escape(action), "", "", MwMessagePattern(
                Wiki.Message(message), paramPatterns)), RegexOptions.Compiled))
        End Sub

    End Class

End Namespace
