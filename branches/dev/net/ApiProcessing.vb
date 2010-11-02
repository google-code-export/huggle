Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Xml

Namespace Huggle

    Partial Public Class ApiRequest

        Private LastRev As Revision

        Private Sub ProcessApi(ByVal rootNode As XmlNode)
            Try
                If rootNode.Name <> "api" Then OnFail(Msg("error-apiresponse"), "badroot")

                For Each node As XmlNode In rootNode.ChildNodes
                    Select Case node.Name
                        Case "block" 'Action

                        Case "changerights" 'Action

                        Case "delete" 'Action

                        Case "edit"
                            If node.Attribute("result") = "Success" Then
                                Dim rev As Revision = Wiki.Revisions(CInt(node.Attribute("newrevid")))
                                Dim old As Revision = Wiki.Revisions(CInt(node.Attribute("oldrevid")))
                                Dim page As Page = Wiki.Pages(node.Attribute("title"))

                                page.Id = CInt(node.Attribute("pageid"))
                                rev.Prev = old
                                old.Next = rev
                            End If

                        Case "emailuser" 'Action

                        Case "error"
                            Dim errorCode As String = node.Attribute("code")
                            Dim errorInfo As String = node.Attribute("info")

                            'Tidy internal API error messages
                            If errorCode.StartsWithI("internal_api_error_") Then
                                errorCode = errorCode.FromFirst("internal_api_error_").ToLowerI
                                errorInfo = errorInfo.Remove("Exception Caught: ")

                                OnFail(Msg("error-internal", "Internal API error"), errorInfo) : Return

                            ElseIf errorCode = "moduledisabled" Then
                                errorInfo = Msg("error-apimoduledisabled", errorInfo.FromFirst("``").ToFirst("'"))

                            ElseIf errorCode = "help" Then
                                errorCode = "noquery"
                                errorInfo = Msg("error-noquery")
                            End If

                            OnFail(errorInfo, errorCode) : Return

                        Case "expandtemplates"
                            Dim sources As List(Of String) = Query("text").ToString.Split(ExpandTemplatesQuery.Separator).ToList
                            Dim results As List(Of String) = node.Value.Split(ExpandTemplatesQuery.Separator).ToList

                            For i As Integer = 0 To sources.Count - 1
                                Wiki.ExpansionCache.Merge(sources(i), results(i))
                            Next i

                        Case "flagconfig"
                            Wiki.ReviewFlags.Clear()

                            For Each flagNode As XmlNode In node.ChildNodes
                                Dim flag As ReviewFlag = Wiki.ReviewFlags(flagNode.Attribute("name"))

                                flag.DefaultLevel = 0
                                flag.Levels = CInt(flagNode.Attribute("levels"))
                                flag.PristineLevel = CInt(flagNode.Attribute("tier3"))
                                flag.QualityLevel = CInt(flagNode.Attribute("tier2"))
                            Next flagNode

                        Case "import" 'Action

                        Case "limits"
                            '''

                        Case "login"
                            _LoginResponse = New LoginResponse

                            If node.HasAttribute("result") Then LoginResponse.Result = node.Attribute("result").ToLowerI
                            If LoginResponse.Result = "wrongpluginpass" Then LoginResponse.Result = "wrongpass"
                            If node.HasAttribute("token") Then LoginResponse.Token = node.Attribute("token")
                            If node.HasAttribute("wait") Then LoginResponse.Wait = New TimeSpan(0, 0, CInt(node.Attribute("wait")))

                            'Clear saved password if it's wrong
                            If LoginResponse.Result = "wrongpass" Then User.Password = Nothing

                        Case "move" 'Action

                        Case "paraminfo" : ProcessParamInfo(node)
                        Case "parse" : ProcessParse(node)

                        Case "patrol"
                            Dim rcid As Integer = CInt(node.Attribute("rcid"))

                            If Wiki.RecentChanges.ContainsKey(rcid) _
                                Then CType(Wiki.RecentChanges(rcid), Revision).IsReviewed = True

                        Case "protect" 'Action

                        Case "purge" 'Action

                        Case "query" : ProcessQuery(node)

                        Case "query-continue"
                            Continues.Clear()

                            For Each qcNode As XmlNode In node.ChildNodes
                                Dim name As String = qcNode.Attributes(0).Name
                                Dim value As String = qcNode.Attributes(0).Value

                                If name = "rvstartid" AndAlso LastRev IsNot Nothing _
                                    Then LastRev.Prev = Wiki.Revisions(CInt(value))

                                Continues.Merge(name, value)
                            Next qcNode

                        Case "review" 'Action

                        Case "rollback"
                            Dim rev As Revision = Wiki.Revisions(CInt(node.Attribute("revid")))
                            Dim old As Revision = Wiki.Revisions(CInt(node.Attribute("old_revid")))
                            Dim last As Revision = Wiki.Revisions(CInt(node.Attribute("last_revid")))
                            Dim page As Page = Wiki.Pages(node.Attribute("title"))

                            page.Id = CInt(node.Attribute("pageid"))
                            rev.Summary = node.Attribute("summary")
                            rev.Page = page : old.Page = page : last.Page = page

                            rev.RevertTo = last

                            If rev IsNot old Then
                                rev.Prev = old
                                old.Next = rev
                                old.RevertedBy = rev
                            End If

                        Case "sitematrix" : ProcessSiteMatrix(node)

                        Case "stabilize" 'Action

                        Case "undelete" 'Action

                        Case "upload" 'Action

                        Case "warnings"
                            Warnings.Clear()

                            For Each warningNode As XmlNode In node.ChildNodes
                                Warnings.Add(warningNode.InnerText)
                            Next warningNode

                        Case "watch" 'Action

                        Case Else : Log.Debug(Msg("error-apiunrecognized", "result", node.Name))
                    End Select
                Next node

            Catch ex As Exception
                OnFail(Result.FromException(ex).Wrap(Msg("error-apiresponse")))
            End Try
        End Sub

        Private Sub ProcessQuery(ByVal queryNode As XmlNode)
            For Each node As XmlNode In queryNode.ChildNodes
                Select Case node.Name
                    Case "abusefilters" : ProcessAbuseFilters(node)

                    Case "abuselog" : ProcessAbuseLog(node)

                    Case "allcategories"
                        For Each c As XmlNode In node.ChildNodes
                            AssertApi(c.Name, "c")

                            If IsSimple Then
                                Strings.Add(c.Value)
                            Else
                                Dim category As Category = Wiki.Categories(c.Value)
                                category.Count = CInt(c.Attribute("size"))
                                category.SubcatCount = CInt(c.Attribute("subcats"))
                                Items.Add(category.Page)
                            End If
                        Next c

                    Case "alllinks"
                        '''

                    Case "allmessages"
                        For Each message As XmlNode In node.ChildNodes
                            AssertApi(message.Name, "message")
                            Wiki.Messages.Merge(message.Attribute("name"), message.InnerText)
                        Next message

                    Case "allpages"
                        For Each p As XmlNode In node.ChildNodes
                            AssertApi(p.Name, "p")

                            If IsSimple Then
                                Strings.Add(p.Attribute("title"))
                            Else
                                Dim page As Page = Wiki.Pages(CInt(p.Attribute("ns")), p.Attribute("title"))
                                page.Id = CInt(p.Attribute("pageid"))
                                Items.Add(page)
                            End If
                        Next p

                    Case "allusers"
                        For Each u As XmlNode In node.ChildNodes
                            AssertApi(u.Name, "u")

                            If IsSimple Then
                                Strings.Add(u.Attribute("name"))
                            Else
                                Dim user As User = Wiki.Users(u.Attribute("name"))
                                If u.HasAttribute("editcount") Then user.Contributions = CInt(u.Attribute("editcount"))
                                If u.HasAttribute("registration") Then user.Created = CDate(u.Attribute("registration"))
                                Items.Add(user)
                            End If
                        Next u

                    Case "articleassessment"
                        '''

                    Case "backlinks"
                        Dim sourcePage As Page = Wiki.Pages.FromString(CStr(Query("bltitle")))

                        For Each bl As XmlNode In node.ChildNodes
                            AssertApi(bl.Name, "bl")
                            Dim page As Page = Wiki.Pages(CInt(bl.Attribute("ns")), bl.Attribute("title"))
                            page.Id = CInt(bl.Attribute("pageid"))

                            If bl.HasAttribute("redirect") Then
                                page.IsRedirect = True
                                page.Target = sourcePage
                                sourcePage.Redirects.Merge(page)
                            End If

                            Items.Add(page)
                        Next bl

                        If CStr(Query("blfilterredir")) = "redirects" Then sourcePage.RedirectsKnown = True

                    Case "badrevids"
                        For Each revNode As XmlNode In node.ChildNodes
                            AssertApi(revNode.Name, "rev")
                            Dim id As Integer = CInt(revNode.Attribute("revid"))

                            If id > 0 Then
                                Dim rev As Revision = Wiki.Revisions(id)
                                rev.Exists = TS.False
                            End If
                        Next revNode

                    Case "blocks"
                        For Each bk As XmlNode In node.ChildNodes
                            AssertApi(bk.Name, "bk")

                            Dim block As New Block(
                                Action:="block",
                                anonOnly:=bk.HasAttribute("anononly"),
                                autoBlock:=bk.HasAttribute("autoblock"),
                                automatic:=bk.HasAttribute("automatic"),
                                blockCreation:=bk.HasAttribute("nocreate"),
                                blockEmail:=bk.HasAttribute("emailblocked"),
                                blockTalk:=Not (bk.HasAttribute("allowusertalk")),
                                Comment:=bk.Attribute("reason"),
                                duration:=Nothing,
                                expires:=If(bk.Attribute("expiry") = "infinity",
                                    Date.MaxValue, CDate(bk.Attribute("expiry"))),
                                id:=CInt(bk.Attribute("id")),
                                rcid:=0,
                                target:=bk.Attribute("user"),
                                time:=CDate(bk.Attribute("timestamp")),
                                User:=Wiki.Users(bk.Attribute("by")))

                            If Not bk.Attribute("user").Contains("/") Then
                                Dim user As User = Wiki.Users(bk.Attribute("user"))
                                user.Blocks.Merge(block)
                            End If

                            Items.Add(block)
                        Next bk

                    Case "categorymembers"
                        For Each cm As XmlNode In node.ChildNodes
                            AssertApi(cm.Name, "cm")

                            If IsSimple Then
                                Strings.Add(cm.Attribute("title"))
                            Else
                                Dim page As Page = Wiki.Pages(CInt(cm.Attribute("ns")), cm.Attribute("title"))
                                If cm.HasAttribute("pageid") Then page.Id = CInt(cm.Attribute("pageid"))
                                Items.Add(page)
                            End If
                        Next cm

                    Case "dbrepllag"
                        Dim maxLag As Integer = 0

                        For Each db As XmlNode In node.ChildNodes
                            AssertApi(db.Name, "db")
                            maxLag = Math.Max(maxLag, CInt(db.Attribute("lag")))
                        Next db

                        Wiki.Lag = maxLag

                    Case "deletedrevs"
                        For Each pg As XmlNode In node.ChildNodes
                            AssertApi(pg.Name, "pg")
                            Dim page As Page = Wiki.Pages(CInt(pg.Attribute("ns")), pg.Attribute("title"))

                            For Each dr As XmlNode In pg.ChildNodes
                                'TODO: Find some way to store this given ID of deleted revisions is not exposed
                            Next dr
                        Next pg

                    Case "embeddedin"
                        For Each ei As XmlNode In node.ChildNodes
                            AssertApi(ei.Name, "ei")

                            Dim page As Page = Wiki.Pages(CInt(ei.Attribute("ns")), ei.Attribute("title"))
                            page.Id = CInt(ei.Attribute("pageid"))
                            Items.Add(page)
                        Next ei

                    Case "extensions"
                        Wiki.Extensions.Clear()

                        For Each ext As XmlNode In node.ChildNodes
                            AssertApi(ext.Name, "ext")
                            Dim extension As Extension = Wiki.Extensions(ext.Attribute("name"))

                            If ext.HasAttribute("author") Then extension.Author = ext.Attribute("author")
                            If ext.HasAttribute("description") Then extension.Description = ext.Attribute("description")
                            If ext.HasAttribute("type") Then extension.Type = ext.Attribute("type")
                            If ext.HasAttribute("url") Then extension.Url = New Uri(ext.Attribute("url"))
                            If ext.HasAttribute("version") Then extension.Version = ext.Attribute("version")
                        Next ext

                    Case "filearchive"
                        '''

                    Case "fileextensions"
                        Wiki.FileExtensions.Clear()

                        For Each fe As XmlNode In node.ChildNodes
                            AssertApi(fe.Name, "fe")
                            Wiki.FileExtensions.Merge(node.Attribute("ext"))
                        Next fe

                    Case "general" : ProcessSiteInfo(node)

                    Case "globalblocks"
                        For Each block As XmlNode In node.ChildNodes
                            AssertApi(block.Name, "block")

                            Dim globalBlock As New GlobalBlock( _
                                Action:="block", _
                                anonOnly:=block.Attribute("anononly").ToBoolean, _
                                expires:=CDate(block.Attribute("expires")), _
                                Family:=Wiki.Family, _
                                id:=CInt(block.Attribute("id")), _
                                rcid:=0, _
                                Comment:=block.Attribute("reason"), _
                                target:=block.Attribute("address"), _
                                time:=CDate(block.Attribute("timestamp")), _
                                User:=App.Wikis(block.Attribute("bywiki")).Users(block.Attribute("by")))

                            Items.Add(globalBlock)
                        Next block

                    Case "globaluserinfo" : ProcessGlobalUserInfo(node)

                    Case "imageusage"
                        Dim media As File = Wiki.Files(CStr(Query("iutitle")))
                        media.Uses.Clear()

                        For Each iu As XmlNode In node.ChildNodes
                            AssertApi(iu.Name, "iu")

                            Dim page As Page = Wiki.Pages(CInt(iu.Attribute("ns")), iu.Attribute("title"))
                            page.Id = CInt(iu.Attribute("pageid"))
                            media.Uses.Merge(page)
                        Next iu

                    Case "interwikimap"
                        For Each iw As XmlNode In node.ChildNodes
                            AssertApi(iw.Name, "iw")
                            'ignore
                        Next iw

                    Case "languages"
                        '''

                    Case "logevents"
                        For Each item As XmlNode In node.ChildNodes
                            AssertApi(item.Name, "item")
                            ProcessLogItem(item)
                        Next item

                    Case "magicwords"
                        For Each magicword As XmlNode In node.ChildNodes
                            AssertApi(magicword.Name, "magicword")

                            Dim name As String = magicword.Attribute("name")

                            For Each aliases As XmlNode In magicword.ChildNodes
                                AssertApi(aliases.Name, "aliases")

                                If aliases.ChildNodes.Count > 0 Then
                                    'Assume first item in the list returned is the preferred localised form
                                    '(the last always seems to be the canonical non-localised form)
                                    'TODO: MediaWiki should be more explicit about this
                                    Wiki.MagicWords.Merge(name, aliases.ChildNodes(0).Value.ToLowerI)

                                    For Each aliasNode As XmlNode In aliases.ChildNodes
                                        AssertApi(aliasNode.Name, "alias")
                                        Wiki.MagicWordAliases.Merge(aliasNode.Value, name)
                                    Next aliasNode
                                End If
                            Next aliases
                        Next magicword

                    Case "namespaces"
                        For Each ns As XmlNode In node.ChildNodes
                            AssertApi(ns.Name, "ns")

                            Dim space As Space = Wiki.Spaces(CInt(ns.Attribute("id")))
                            space.Name = If(ns.FirstChild Is Nothing, "", ns.FirstChild.Value)
                            If ns.HasAttribute("canonical") Then space.CanonicalName = ns.Attribute("canonical")
                            space.IsContent = ns.HasAttribute("content")
                            space.HasSubpages = ns.HasAttribute("subpages")
                            space.Aliases.Merge(space.CanonicalName)
                        Next ns

                    Case "namespacealiases"
                        For Each ns As XmlNode In node.ChildNodes
                            AssertApi(ns.Name, "ns")

                            Wiki.Spaces(CInt(ns.Attribute("id"))).Aliases.Merge(ns.Value)
                        Next ns

                    Case "normalized"
                        '''

                    Case "oldreviewedpages", "reviewedpages", "unreviewedpages"
                        For Each p As XmlNode In node.ChildNodes
                            AssertApi(p.Name, "p")

                            Dim page As Page = Wiki.Pages(CInt(p.Attribute("ns")), p.Attribute("title"))
                            page.Id = CInt(p.Attribute("pageid"))

                            If p.HasAttribute("revid") Then
                                page.LastRev = Wiki.Revisions(CInt(p.Attribute("revid")))

                                If p.HasAttribute("stable_revid") Then
                                    Dim reviewed As Revision = Wiki.Revisions(CInt(p.Attribute("stable_revid")))
                                    reviewed.Page = page
                                    reviewed.IsReviewed = True
                                    If reviewed IsNot page.LastRev Then page.LastRev.IsReviewed = False
                                End If

                                Items.Add(page.LastRev)
                            End If
                        Next p

                    Case "pages" : ProcessPages(node)

                    Case "protectedtitles"
                        For Each pt As XmlNode In node.ChildNodes
                            AssertApi(pt.Name, "pt")

                            Dim page As Page = Wiki.Pages(CInt(pt.Attribute("ns")), pt.Attribute("title"))
                            page.Exists = False
                            page.IsProtected = True
                        Next pt

                    Case "random"
                        '''

                    Case "recentchanges" : ProcessRecentChanges(node)

                    Case "redirects"
                        For Each r As XmlNode In node.ChildNodes
                            AssertApi(r.Name, "r")
                            Wiki.Pages(r.Attribute("from")).Target = Wiki.Pages(r.Attribute("to"))
                        Next r

                    Case "rightsinfo"
                        If Not String.IsNullOrEmpty(node.Attribute("text")) Then Wiki.License = node.Attribute("text")
                        If Not String.IsNullOrEmpty(node.Attribute("url")) Then Wiki.LicenseUrl = New Uri(node.Attribute("url"))

                    Case "searchinfo"
                        '''

                    Case "search"
                        '''

                    Case "specialpagealiases"
                        '''

                    Case "statistics"
                        Wiki.ActiveUsers = CInt(node.Attribute("activeusers"))
                        Wiki.Administrators = CInt(node.Attribute("admins"))
                        Wiki.ContentPages = CInt(node.Attribute("articles"))
                        Wiki.Files.Count = CInt(node.Attribute("images"))
                        Wiki.Pages.Count = CInt(node.Attribute("pages"))
                        Wiki.Revisions.Count = CInt(node.Attribute("edits"))
                        Wiki.Users.Count = CInt(node.Attribute("users"))

                    Case "tags"
                        Wiki.ChangeTags.Clear()

                        For Each tagNode As XmlNode In node.ChildNodes
                            AssertApi(tagNode.Name, "tag")

                            Dim tag As ChangeTag = Wiki.ChangeTags(tagNode.Attribute("name"))

                            If tagNode.HasAttribute("description") Then tag.Description = tagNode.Attribute("description")
                            If tagNode.HasAttribute("displayname") Then tag.DisplayName = tagNode.Attribute("displayname")
                            If tagNode.HasAttribute("hitcount") Then tag.Hits = CInt(tagNode.Attribute("hitcount"))
                        Next tagNode

                    Case "threads" : ProcessThreads(node)
                    Case "usercontribs" : ProcessUserContribs(node)

                    Case "usergroups"
                        Wiki.UserGroups.Reset()
                        Dim groupCounts As Boolean

                        For Each groupNode As XmlNode In node.ChildNodes
                            AssertApi(groupNode.Name, "group")

                            Dim group As UserGroup = Wiki.UserGroups(groupNode.Attribute("name"))

                            If {"*", "user", "autoconfirmed"}.Contains(group.Name) Then group.IsImplicit = True

                            If group.Name = "user" Then
                                group.Count = Wiki.Users.Count
                            ElseIf groupNode.HasAttribute("number") AndAlso Not group.IsImplicit Then
                                groupCounts = True
                                group.Count = CInt(groupNode.Attribute("number"))
                            End If

                            For Each rights As XmlNode In groupNode.ChildNodes
                                If rights.Name = "rights" Then
                                    For Each permission As XmlNode In rights.ChildNodes
                                        group.Rights.Merge(permission.FirstChild.Value)
                                    Next permission
                                End If
                            Next rights

                            group.Rights.Sort()
                        Next groupNode

                        If groupCounts Then
                            For Each group As UserGroup In Wiki.UserGroups.All
                                If group.Count < 0 Then group.IsImplicit = True
                            Next group
                        End If

                    Case "userinfo" : ProcessUserInfo(node)

                    Case "users" : ProcessUsers(node)

                    Case "watchlist"
                        '''

                    Case "watchlistraw"
                        '''

                    Case Else : Log.Write(Msg("error-apiunrecognized", "query", node.Name))
                End Select
            Next node
        End Sub

        Private Sub ProcessAbuseFilters(ByVal abuseNode As XmlNode)
            For Each node As XmlNode In abuseNode.ChildNodes
                AssertApi(node.Name, "filter")

                Dim filter As AbuseFilter = Wiki.AbuseFilters(CInt(node.Attribute("id")))

                filter.Actions = List(node.Attribute("actions").Split(","))
                filter.Description = node.Attribute("description")
                filter.IsDeleted = node.HasAttribute("deleted")
                filter.IsEnabled = node.HasAttribute("enabled")
                filter.IsPrivate = node.HasAttribute("private")
                filter.LastModifiedBy = Wiki.Users.FromName(node.Attribute("lasteditor"))
                filter.LastModified = node.Attribute("lastedittime").ToDate
                filter.Notes = node.Attribute("comments")
                filter.Pattern = node.Attribute("pattern")
                filter.TotalHits = CInt(node.Attribute("hits"))
            Next node
        End Sub

        Private Sub ProcessAbuseLog(ByVal logNode As XmlNode)
            For Each node As XmlNode In logNode.ChildNodes
                AssertApi(node.Name, "item")

                If Wiki.Logs.All.ContainsKey(CInt(node.Attribute("id"))) Then Continue For

                Dim abuse As New Abuse(
                    Id:=CInt(node.Attribute("id")),
                    Filter:=Wiki.AbuseFilters(CInt(node.Attribute("filter_id"))),
                    Page:=Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title")),
                    rcid:=0,
                    Result:=node.Attribute("result"),
                    Time:=node.Attribute("timestamp").ToDate,
                    User:=Wiki.Users(node.Attribute("user")),
                    UserAction:=node.Attribute("action"))

                If node.HasAttribute("filter") Then abuse.Filter.Description = node.Attribute("filter")

                NewItems.Add(abuse)
            Next node

            If Query.Contains("user") Then Wiki.Users(CStr(Query("user"))).AbuseKnown = True
        End Sub

        Private Sub ProcessGlobalUserInfo(ByVal guiNode As XmlNode)
            Dim globalUser As GlobalUser =
                If(Query.Contains("guiuser"), Wiki.Family.GlobalUsers(Query("guiuser").ToString), User.GlobalUser)

            If guiNode.HasAttribute("home") Then globalUser.PrimaryUser =
                App.Wikis(guiNode.Attribute("home")).Users(globalUser.Name)
            If guiNode.HasAttribute("id") Then globalUser.Id = CInt(guiNode.Attribute("id"))
            If guiNode.HasAttribute("registration") Then globalUser.Created = CDate(guiNode.Attribute("registration"))

            For Each node As XmlNode In guiNode.ChildNodes
                Select Case node.Name
                    Case "groups"
                        Dim groups As New List(Of GlobalGroup)

                        For Each g As XmlNode In node.ChildNodes
                            AssertApi(g.Name, "g")
                            globalUser.GlobalGroups.Add(globalUser.Family.GlobalGroups(g.Value))
                        Next g

                        globalUser.GlobalGroups = groups

                    Case "rights"
                        Dim rights As New List(Of String)

                        For Each r As XmlNode In node.ChildNodes
                            AssertApi(r.Name, "r")
                            rights.Add(r.Value)
                        Next r

                        globalUser.Rights = rights

                    Case "merged"
                        globalUser.Users.Clear()
                        globalUser.Wikis.Clear()

                        For Each account As XmlNode In node.ChildNodes
                            AssertApi(account.Name, "account")

                            Dim wiki As Wiki = App.Wikis.FromInternalCode(account.Attribute("wiki"))

                            'MediaWiki keeps accounts on the list of unified accounts
                            'even when a wiki is deleted; we don't want these
                            If wiki Is Nothing Then Continue For

                            Dim user As User = App.Wikis.FromInternalCode(account.Attribute("wiki")).Users(globalUser.Name)

                            globalUser.Users.Add(user)
                            globalUser.Wikis.Add(user.Wiki)

                            user.Contributions = CInt(account.Attribute("editcount"))
                            user.UnificationDate = account.Attribute("timestamp").ToDate
                            user.UnificationMethod = account.Attribute("method")

                            If user.UnificationMethod = "primary" Then globalUser.PrimaryUser = user
                        Next account

                    Case "unattached"
                        '''

                    Case Else : Log.Write(Msg("error-apiunrecognized", "globaluserinfo", node.Name))
                End Select
            Next node
        End Sub

        Private Sub ProcessImageInfo(ByVal page As Page, ByVal mediaNode As XmlNode)
            Dim media As File = Wiki.Files(page)

            For Each ii As XmlNode In mediaNode.ChildNodes
                Dim mediaRev As New FileRevision(media, ii.Attribute("timestamp").ToDate)
                If media.LastRevision Is Nothing Then media.LastRevision = mediaRev

                mediaRev.Comment = ii.Attribute("comment")
                mediaRev.Depth = CInt(ii.Attribute("bitdepth"))
                mediaRev.Hash = ii.Attribute("sha1")
                mediaRev.Height = CInt(ii.Attribute("height"))
                mediaRev.Size = CInt(ii.Attribute("size"))
                mediaRev.Type = ii.Attribute("mime")
                mediaRev.Url = New Uri(ii.Attribute("url"))
                mediaRev.User = Wiki.Users(ii.Attribute("user"))
                mediaRev.Width = CInt(ii.Attribute("width"))

                For Each metadataNode As XmlNode In ii.ChildNodes
                    AssertApi(metadataNode.Name, "metadata")

                    For Each node As XmlNode In metadataNode.ChildNodes
                        If node.HasAttribute("name") AndAlso node.HasAttribute("value") _
                            Then mediaRev.Metadata.Merge(node.Attribute("name"), node.Attribute("value").Trim)
                    Next node
                Next metadataNode

                mediaRev.Process()
                media.Revisions.Add(mediaRev)
            Next ii

            media.FirstRevision = media.Revisions(media.Revisions.Count - 1)
            media.DetailsKnown = True
        End Sub

        Private Sub ProcessLogItem(ByVal node As XmlNode)
            Dim action As String = Nothing, type As String = Nothing
            Dim newItem As LogItem = Nothing

            'API is inconsistent here
            If node.HasAttribute("action") Then action = node.Attribute("action")
            If node.HasAttribute("logaction") Then action = node.Attribute("logaction")

            If node.HasAttribute("type") Then type = node.Attribute("type")
            If node.HasAttribute("logtype") Then type = node.Attribute("logtype")

            Dim id As Integer = CInt(node.Attribute("logid"))
            Dim rcid As Integer = If(node.HasAttribute("rcid"), CInt(node.Attribute("rcid")), 0)
            Dim isNew As Boolean = Not Wiki.Logs.All.ContainsKey(id)

            Select Case type & "/" & action
                'Account blocking
                Case "block/block", "block/reblock"
                    Dim detail As XmlNode = node.FirstChild

                    newItem = New Block(
                        action:=action,
                        anonOnly:=node.FirstChild.Attribute("flags").Contains("anononly"),
                        autoBlock:=Not node.FirstChild.Attribute("flags").Contains("noautoblock"),
                        automatic:=MessageMatch(Wiki.Message("autoblocker"), node.Attribute("comment")).Success,
                        blockCreation:=node.FirstChild.Attribute("flags").Contains("nocreate"),
                        blockEmail:=node.FirstChild.Attribute("flags").Contains("noemail"),
                        blockTalk:=node.FirstChild.Attribute("flags").Contains("notalk"),
                        Comment:=node.Attribute("comment"),
                        duration:=node.FirstChild.Attribute("duration"),
                        expires:=node.FirstChild.Attribute("expiry").ToDate,
                        id:=id,
                        rcid:=rcid,
                        target:=Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title")).Owner.Name,
                        time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Account unblocking
                Case "block/unblock"
                    newItem = New Block(
                        action:=action,
                        anonOnly:=False,
                        autoBlock:=False,
                        automatic:=False,
                        blockCreation:=False,
                        blockEmail:=False,
                        blockTalk:=False,
                        Comment:=node.Attribute("comment"),
                        duration:=Nothing,
                        expires:=Date.MinValue,
                        id:=id,
                        rcid:=rcid,
                        Target:=Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title")).Owner.Name,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Page deletion
                Case "delete/delete", "delete/restore"
                    newItem = New Deletion(
                        action:=action,
                        Comment:=node.Attribute("comment"),
                        id:=id,
                        rcid:=rcid,
                        Page:=Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title")),
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Log item hiding
                Case "delete/event"
                    newItem = New VisibilityChange(
                        id:=id,
                        item:=Nothing,
                        rcid:=rcid,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Revision hiding
                Case "delete/revision"
                    newItem = New VisibilityChange(
                        id:=id,
                        item:=Nothing,
                        rcid:=rcid,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Page importing
                Case "import/upload", "import/interwiki"

                    'Page history merging
                Case "merge/merge"
                    '...not used on Wikimedia?

                    'Page moving
                Case "move/move", "move/move_redir"
                    newItem = New Move(
                        Comment:=node.Attribute("comment"),
                        Source:=node.Attribute("title"),
                        Destination:=node.FirstChild.Attribute("new_title"),
                        id:=id,
                        rcid:=rcid,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Page protection
                Case "protect/protect", "protect/modify"
                    Dim level As String = If(node.FirstChild IsNot Nothing,
                        node.FirstChild.FirstChild.Value, node.Attribute("comment").FromFirst("[", True))

                    newItem = New Protection(
                        action:=action,
                        Cascade:=(level IsNot Nothing AndAlso level.EndsWithI("[cascading]")),
                        Comment:=node.Attribute("comment"),
                        Hidden:=node.HasAttribute("actionhidden"),
                        id:=id,
                        rcid:=rcid,
                        Levels:=If(level, "[edit:sysop; move:sysop] (indefinite)"),
                        Page:=If(node.HasAttribute("title"), Wiki.Pages(CInt(node.Attribute("ns")),
                            node.Attribute("title")), Nothing),
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Page unprotection
                Case "protect/unprotect"
                    newItem = New Protection(
                        action:=action,
                        Cascade:=False,
                        Comment:=node.Attribute("comment"),
                        Hidden:=node.HasAttribute("actionhidden"),
                        id:=id,
                        rcid:=rcid,
                        Levels:=Nothing,
                        Page:=If(node.HasAttribute("title"), Wiki.Pages(CInt(node.Attribute("ns")),
                            node.Attribute("title")), Nothing),
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Protection settings moved following a page move
                Case "protect/move_prot"
                    'Since we handle page moves simply by changing the page object's title field,
                    'the protection settings don't need moving anywhere

                    'Account creation
                Case "newusers/create", "newusers/create2"
                    newItem = New UserCreation(
                        Auto:=False,
                        id:=id,
                        rcid:=rcid,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    Wiki.Users.NewUsers.Add(Wiki.Users(node.Attribute("title")))

                    'Account renaming
                Case "renameuser/renameuser"
                    newItem = New UserRename(
                        Comment:=node.Attribute("comment"),
                        id:=id,
                        rcid:=rcid,
                        Source:=node.Attribute("title"),
                        Destination:=node.FirstChild.Value,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Account group changes
                Case "rights/rights"
                    newItem = New RightsChange(
                        Comment:=node.Attribute("comment"),
                        id:=id,
                        rcid:=rcid,
                        Rights:=node.FirstChild.Attribute("new").ToList.Trim,
                        TargetUser:=Wiki.Pages(node.Attribute("title")).Owner,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Revision or page patrolling
                Case "patrol/patrol"
                    Dim rev As Revision = Wiki.Revisions(CInt(node.FirstChild.Attribute("cur")))

                    If CInt(node.FirstChild.Attribute("prev")) = 0 Then rev.Prev = Revision.Null _
                        Else rev.Prev = Wiki.Revisions(CInt(node.FirstChild.Attribute("prev")))

                    newItem = New Review(
                        Auto:=(CInt(node.FirstChild.Attribute("auto")) = 1),
                        Comment:=node.Attribute("comment"),
                        id:=id,
                        rcid:=rcid,
                        Revision:=rev,
                        Time:=node.Attribute("timestamp").ToDate,
                        type:=If(rev.Prev Is Revision.Null, "newpage-patrol", "patrol"),
                        User:=Wiki.Users(node.Attribute("user")))

                    'Flagged revisions configuration
                Case "stable/config"

                    'Censorship
                Case "suppress/block",
                     "suppress/delete",
                     "suppress/event",
                     "suppress/file",
                     "suppress/reblock",
                     "suppress/revision"
                    'Not accessible to most of us, so don't expect anything to be done with it here

                    'File upload
                Case "upload/revert",
                     "upload/overwrite",
                     "upload/upload"

                    newItem = New Upload(
                        action:=action,
                        Comment:=node.Attribute("comment"),
                        File:=node.Attribute("title"),
                        id:=id,
                        rcid:=rcid,
                        Time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Global blocking
                Case "gblblock/gblock2",
                     "gblblock/gunblock",
                     "gblblock/modify"

                    Dim logAction As String
                    Dim params As String = If(node.FirstChild.FirstChild IsNot Nothing, node.FirstChild.FirstChild.Value, Nothing)

                    Select Case action
                        Case "gblock2" : logAction = "block"
                        Case "gunblock" : logAction = "unblock"
                        Case "modify" : logAction = "modify"
                        Case Else : logAction = action
                    End Select

                    newItem = New GlobalBlock(
                        action:=logAction,
                        anonOnly:=If(params Is Nothing, False, params.Contains("anonymous only")),
                        expires:=If(params Is Nothing, Date.MinValue, params.FromFirst("expires ").ToDate),
                        Family:=Wiki.Family,
                        id:=id,
                        rcid:=CInt(node.Attribute("rcid")),
                        Comment:=node.Attribute("comment"),
                        target:=Wiki.Pages(node.Attribute("title")).Name,
                        time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Global blocking whitelist
                Case "gblblock/dwhitelist",
                     "gblblock/whitelist"
                    'ignore

                    'Global group rights change
                Case "gblrights/groupperms",
                     "gblrights/groupprms2",
                     "gblrights/groupprms3",
                     "gblrights/usergroups",
                     "gblrights/setchange",
                     "gblrights/setrename",
                     "gblrights/setnewtype",
                     "gblrights/newset"
                    'ignore

                    'Global account status change
                Case "globalauth/setstatus"

                    Dim globalUserLock As New GlobalUserStatusChange(
                        Wiki:=Wiki,
                        Comment:=node.Attribute("comment"),
                        id:=id,
                        rcid:=rcid,
                        target:=Wiki.Family.GlobalUsers(Wiki.Pages(node.Attribute("title")).Name),
                        time:=node.Attribute("timestamp").ToDate,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Flagged revisions reviewing
                Case "review/approve",
                     "review/approve-a",
                     "review/approve-ia",
                     "review/approve2",
                     "review/approve2-i",
                     "review/unapprove",
                     "review/unapprove2"

                    Dim rev As Revision = Wiki.Revisions(CInt(node.FirstChild.FirstChild.Value))

                    If CInt(node.FirstChild.NextSibling.FirstChild.Value) = 0 Then rev.Prev = Revision.Null _
                        Else rev.Prev = Wiki.Revisions(CInt(node.FirstChild.NextSibling.FirstChild.Value))

                    rev.Page.Id = CInt(node.Attribute("pageid"))

                    newItem = New Review(
                        Auto:=(action.EndsWithI("a")),
                        Comment:=node.Attribute("comment"),
                        Revision:=rev,
                        id:=id,
                        rcid:=rcid,
                        Time:=node.Attribute("timestamp").ToDate,
                        type:="flaggedrevs-" & action,
                        User:=Wiki.Users(node.Attribute("user")))

                    'Something unknown
                Case Else : Log.Debug(Msg("error-apiunrecognized", "loginfo", type & "/" & action))
            End Select

            If Wiki.Logs.All.ContainsKey(id) Then
                Items.Add(Wiki.Logs(id))
                If isNew Then NewItems.Add(Wiki.Logs(id))
            End If
        End Sub

        Private Sub ProcessPages(ByVal pagesNode As XmlNode)
            For Each node As XmlNode In pagesNode.ChildNodes
                AssertApi(node.Name, "page")
                If node.HasAttribute("invalid") Then Continue For

                If IsSimple Then
                    Strings.Add(node.Attribute("title"))
                Else
                    Dim page As Page = Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title"))
                    If page Is Nothing Then Continue For

                    If Query.Contains("prop") AndAlso Query("prop").ToString.Contains("categories") _
                        Then page.CategoriesKnown = True
                    page.Exists = Not node.HasAttribute("missing")
                    If node.HasAttribute("edittoken") Then Session.EditToken = node.Attribute("edittoken")

                    If page.Exists Then
                        Items.Add(page)
                        page.Id = CInt(node.Attribute("pageid"))

                        If node.HasAttribute("lastrevid") AndAlso page.LastRev Is Nothing _
                            Then page.LastRev = Wiki.Revisions(CInt(node.Attribute("lastrevid")))
                        If node.HasAttribute("length") AndAlso page.LastRev IsNot Nothing _
                            Then page.LastRev.Bytes = CInt(node.Attribute("length"))

                        If Query.Contains("prop") AndAlso Query("prop").ToString.Contains("info") Then
                            page.IsRedirect = node.HasAttribute("redirect")
                            If page.LastRev IsNot Nothing Then page.LastRev.IsRedirect = page.IsRedirect

                            page.IsWatchedBy(User) = node.HasAttribute("watched")
                        End If

                        If node.HasAttribute("imagerepository") Then
                            Dim Media As File = Wiki.Files(page)
                            Media.Exists = (node.Attribute("imagerepository").Length > 0)
                            Media.IsShared = (node.Attribute("imagerepository") = "shared")
                        End If
                    End If

                    For Each pageNode As XmlNode In node.ChildNodes
                        Select Case pageNode.Name
                            Case "revisions" : ProcessRevisions(pageNode, page)

                            Case "categories"
                                For Each CategoryNode As XmlNode In pageNode.ChildNodes
                                    page.Categories.Merge(Wiki.Categories(CategoryNode.Attribute("title")))
                                Next CategoryNode

                                page.CategoriesKnown = True

                            Case "categoryinfo"
                                Dim category As Category = Wiki.Categories(page)

                                category.Count = CInt(pageNode.Attribute("size"))
                                category.SubcatCount = CInt(pageNode.Attribute("subcats"))
                                category.IsHidden = pageNode.HasAttribute("hidden")

                            Case "duplicatefiles"
                                Wiki.Files(page).Duplicates.Clear()

                                For Each df As XmlNode In pageNode.ChildNodes
                                    Wiki.Files(page).Duplicates.Add(Wiki.Files(df.Attribute("name")))
                                Next df

                            Case "extlinks"
                                For Each el As XmlNode In pageNode.ChildNodes
                                    page.ExternalLinks.Merge(el.InnerText)
                                Next el

                                page.ExternalLinksKnown = True

                            Case "flagged"
                                page.IsReviewable = True

                            Case "globalusage"
                                For Each gu As XmlNode In pageNode.ChildNodes
                                    Dim media As File = Wiki.Family.CentralWiki.Files(gu.Attribute("title"))
                                    Dim usingPage As Page = App.Wikis(gu.Attribute("wiki").Remove(".org")).Pages(gu.Attribute("title"))

                                    media.GlobalUses.Merge(usingPage)
                                    Items.Add(media.Page)
                                Next gu

                            Case "images"
                                For Each im As XmlNode In pageNode.ChildNodes
                                    page.Media.Merge(Wiki.Files(im.Attribute("title")))
                                Next im

                                page.MediaKnown = True

                            Case "imageinfo"
                                ProcessImageInfo(page, pageNode)

                            Case "langlinks"
                                For Each ll As XmlNode In pageNode.ChildNodes
                                    page.LangLinks.Merge(ll.Attribute("lang"))
                                Next ll

                                page.LangLinksKnown = True

                            Case "links"
                                For Each pl As XmlNode In pageNode.ChildNodes
                                    Dim LinkPage As Page = Wiki.Pages(pl.Attribute("title"))
                                    If LinkPage IsNot Nothing Then page.Links.Merge(LinkPage)
                                Next pl

                                page.LinksKnown = True

                            Case "protection"

                            Case "templates"
                                For Each tl As XmlNode In pageNode.ChildNodes
                                    page.Transcludes.Merge(Wiki.Pages(tl.Attribute("title")))
                                Next tl

                                page.TranscludesKnown = True

                            Case Else : Log.Write(Msg("error-apiunrecognized", "pageinfo", pageNode.Name))
                        End Select
                    Next pageNode

                    If page.Exists AndAlso Processing Then page.Process()
                End If
            Next node
        End Sub

        Private Sub ProcessParamInfo(ByVal infoNode As XmlNode)
            For Each node As XmlNode In infoNode.ChildNodes
                Select Case node.Name
                    Case "modules", "querymodules"
                        For Each moduleNode As XmlNode In node.ChildNodes
                            If moduleNode.Name <> "module" Then Continue For

                            Dim apiModule As ApiModule = Wiki.ApiModules(moduleNode.Attribute("name"))

                            apiModule.IsImplemented = Not moduleNode.HasAttribute("missing")
                            apiModule.IsDisabled = {"ApiDisabled", "ApiQueryDisabled"}.Contains(moduleNode.Attribute("classname"))
                        Next moduleNode

                    Case "mainmodule" 'Assume this is always enabled, wouldn't even get this far if it wasn't
                    Case "pagesetmodule"

                    Case Else : Log.Write(Msg("error-apiunrecognized", "paraminfo", node.Name))
                End Select
            Next node
        End Sub

        Private Sub ProcessParse(ByVal parseNode As XmlNode)
            Dim rev As Revision = Nothing
            Dim last As Boolean

            If parseNode.HasAttribute("revid") Then rev = Wiki.Revisions(CInt(parseNode.Attribute("revid")))

            If Query.Contains("page") AndAlso Not Query.Contains("oldid") AndAlso parseNode.HasAttribute("revid") Then
                rev.Page = Wiki.Pages(Query("page").ToString)
                rev.Page.LastRev = rev
                last = True
            End If

            If rev Is Nothing Then Return

            For Each node As XmlNode In parseNode.ChildNodes
                Select Case node.Name
                    Case "langlinks"
                        If last Then
                            For Each ll As XmlNode In node.ChildNodes
                                rev.Page.LangLinks.Merge(ll.Attribute("lang"))
                            Next ll

                            rev.Page.LangLinksKnown = True
                        End If

                    Case "categories"
                        If last Then
                            For Each cl As XmlNode In node.ChildNodes
                                rev.Page.Categories.Merge(Wiki.Categories(cl.FirstChild.Value))
                            Next cl

                            rev.Page.CategoriesKnown = True
                        End If

                    Case "links"
                        If last Then
                            For Each pl As XmlNode In node.ChildNodes
                                Dim Page As Page = Wiki.Pages(CInt(pl.Attribute("ns")), pl.FirstChild.Value)
                                Page.Exists = pl.HasAttribute("exists")
                                rev.Page.Links.Merge(Page)
                            Next pl

                            rev.Page.LinksKnown = True
                        End If

                    Case "templates"
                        If last Then
                            For Each tl As XmlNode In node.ChildNodes
                                Dim Template As Page = Wiki.Pages(tl.FirstChild.Value)
                                Template.Exists = tl.HasAttribute("exists")
                                rev.Page.Transcludes.Merge(Template)
                            Next tl

                            rev.Page.TranscludesKnown = True
                        End If

                    Case "images"
                        If last Then
                            For Each img As XmlNode In node.ChildNodes
                                Dim Media As File = Wiki.Files(img.FirstChild.Value)
                                rev.Page.Media.Merge(Media)
                            Next img

                            rev.Page.MediaKnown = True
                        End If

                    Case "externallinks"
                        If last Then
                            For Each el As XmlNode In node.ChildNodes
                                rev.Page.ExternalLinks.Merge(el.FirstChild.Value)
                            Next el

                            rev.Page.ExternalLinksKnown = True
                        End If

                    Case "sections"
                        If last Then
                            rev.Page.Sections = New List(Of String)

                            For Each s As XmlNode In node.ChildNodes
                                rev.Page.Sections.Add(s.Attribute("line"))
                            Next s

                            rev.Page.SectionsKnown = True
                        End If

                    Case "text"
                        rev.Html = node.FirstChild.Value

                    Case Else : Log.Write(Msg("error-apiunrecognized", "parse", node.Name))
                End Select
            Next node

            If Processing Then rev.ProcessHtml()
        End Sub

        Private Sub ProcessRecentChanges(ByVal rcNode As XmlNode)
            For Each node As XmlNode In rcNode.ChildNodes
                AssertApi(node.Name, "rc")

                If Not node.HasAttribute("type") Then Continue For
                Dim type As String = node.Attribute("type")

                Select Case type
                    Case "edit", "new"
                        Dim id As Integer = CInt(node.Attribute("revid"))

                        If Not Wiki.Revisions.All.ContainsKey(id) Then
                            Dim rev As Revision = Wiki.Revisions(id)

                            rev.IsBot = node.HasAttribute("bot")
                            rev.IsMinor = node.HasAttribute("minor")
                            rev.IsRedirect = node.HasAttribute("redirect")

                            If node.HasAttribute("title") Then rev.Page = _
                                Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title"))

                            If node.HasAttribute("pageid") Then rev.Page.Id = CInt(node.Attribute("pageid"))
                            If node.HasAttribute("rcid") Then rev.Rcid = CInt(node.Attribute("rcid"))
                            If node.HasAttribute("newlen") Then rev.Bytes = CInt(node.Attribute("newlen"))
                            If node.HasAttribute("comment") Then rev.Summary = node.Attribute("comment")
                            If node.HasAttribute("timestamp") Then rev.Time = node.Attribute("timestamp").ToDate
                            If node.HasAttribute("user") Then rev.User = Wiki.Users(node.Attribute("user"))

                            If User.Rights.Contains("patrol") _
                                Then rev.IsReviewed = node.HasAttribute("patrolled")

                            If node.Attribute("type") = "edit" Then
                                If node.HasAttribute("old_revid") _
                                    Then rev.Prev = Wiki.Revisions(CInt(node.Attribute("old_revid")))
                                If node.HasAttribute("oldlen") AndAlso rev.Prev IsNot Nothing _
                                    Then rev.Prev.Bytes = CInt(node.Attribute("oldlen"))
                            Else
                                rev.Prev = Revision.Null
                            End If

                            If rev.Next Is Nothing AndAlso LastRev IsNot Nothing Then
                                LastRev.Prev = rev
                                rev.Next = LastRev
                                LastRev = rev
                            End If

                            rev.DetailsKnown = True
                            rev.Exists = TS.True

                            If Processing Then
                                rev.Page.Process()
                                rev.User.Process()
                                rev.Process()
                                rev.ProcessNew()
                            End If

                            NewItems.Add(rev)
                        End If

                        Items.Add(Wiki.Revisions(id))

                    Case "log" : ProcessLogItem(node)

                    Case Else : Log.Write(Msg("error-apiunrecognized", "rc", type))
                End Select
            Next node
        End Sub

        Private Sub ProcessRevisions(ByVal revisionsNode As XmlNode, ByVal Page As Page)
            Dim nextRev As Revision = Nothing

            For Each node As XmlNode In revisionsNode.ChildNodes
                AssertApi(node.Name, "rev")

                If Not node.HasAttribute("revid") Then Continue For
                Dim rev As Revision = Wiki.Revisions(CInt(node.Attribute("revid")))
                Items.Add(rev)

                rev.Page = Page
                rev.IsBot = node.HasAttribute("bot")
                rev.IsMinor = node.HasAttribute("minor")

                If node.HasAttribute("size") Then rev.Bytes = CInt(node.Attribute("size"))
                If node.HasAttribute("comment") Then rev.Summary = node.Attribute("comment") Else rev.Summary = ""
                If node.HasAttribute("timestamp") Then rev.Time = node.Attribute("timestamp").ToDate
                If node.HasAttribute("user") Then rev.User = Wiki.Users(node.Attribute("user"))
                If node.HasAttribute("commenthidden") Then rev.SummaryHidden = True
                If node.HasAttribute("texthidden") Then rev.TextHidden = True
                If node.HasAttribute("userhidden") Then rev.UserHidden = True

                If Query("rvprop").ToString.Contains("content") Then rev.Text = If(node.InnerText, "")

                If Not Query.Contains("excludeuser") AndAlso Not Query.Contains("revids") Then
                    Dim dir As String = If(Query.Contains("rvdir"), Query("rvdir").ToString, "older")

                    Select Case dir
                        Case "older" : If rev.Page.LastRev Is Nothing Then rev.Page.LastRev = rev
                        Case "newer" : If rev.Page.FirstRev Is Nothing Then rev.Page.FirstRev = rev
                    End Select

                    If nextRev Is Nothing Then
                        If rev.Page.LastRev Is Nothing Then rev.Page.LastRev = rev
                    ElseIf nextRev IsNot rev Then
                        rev.Next = nextRev
                        rev.Next.Prev = rev
                    End If

                    If node.NextSibling Is Nothing AndAlso Query.Contains("rvlimit") _
                        AndAlso CInt(Query("rvlimit")) > revisionsNode.ChildNodes.Count Then

                        rev.Page.FirstRev = rev
                        rev.Prev = Revision.Null
                    End If
                End If

                If node.Value IsNot Nothing Then rev.Text = node.Value

                nextRev = rev

                rev.DetailsKnown = True
                rev.Exists = TS.True

                If Processing Then
                    rev.Page.Process()
                    rev.Process()
                End If

                For Each revNode As XmlNode In node.ChildNodes
                    Select Case revNode.Name
                        Case "#text"
                        Case "diff"
                            If Not revNode.HasAttribute("notcached") Then
                                If revNode.Attribute("from") = "" Then
                                    rev.Prev = Revision.Null
                                    Wiki.Diffs(rev).CacheState = CacheState.NewPage
                                Else
                                    Dim diff As Diff = Wiki.Diffs( _
                                        Wiki.Revisions(CInt(revNode.Attribute("from"))), _
                                        Wiki.Revisions(CInt(revNode.Attribute("to"))))

                                    If diff.CacheState <> CacheState.Cached Then diff.Html = revNode.FirstChild.Value
                                End If
                            End If

                        Case Else : Log.Debug(Msg("error-apiunrecognized", "revisions", revNode.Name))
                    End Select
                Next revNode
            Next node

            Page.OnHistoryChanged()
        End Sub

        Private Sub ProcessSiteInfo(ByVal node As XmlNode)
            Wiki.Engine = "MediaWiki"
            Wiki.Config.PlatformName = "php"

            If node.HasAttribute("articlepath") Then Wiki.ShortUrl =
                New Uri(Wiki.Url.ToString.ToLast("/") & node.Attribute("articlepath").Remove("$1"))
            If node.HasAttribute("case") Then Wiki.Config.FirstLetterCaseSensitive =
                (Not node.Attribute("case") = "first-letter")
            If node.HasAttribute("dbtype") Then Wiki.Config.Database = node.Attribute("dbtype")
            If node.HasAttribute("dbversion") Then Wiki.Config.DatabaseVersion = node.Attribute("dbversion")
            If node.HasAttribute("generator") Then Wiki.Config.EngineVersion =
                node.Attribute("generator").Remove("MediaWiki ")
            If node.HasAttribute("mainpage") Then Wiki.MainPage = Wiki.Pages(node.Attribute("mainpage"))
            If node.HasAttribute("phpversion") Then Wiki.Config.PlatformVersion = node.Attribute("phpversion")
            If node.HasAttribute("rev") Then Wiki.Config.EngineRevision = CInt(node.Attribute("rev"))
            Wiki.Config.ReadOnly = node.HasAttribute("readonly")
            If node.HasAttribute("readonlyreason") Then Wiki.Config.ReadOnlyReason = node.Attribute("readonlyreason")
            If Not String.IsNullOrEmpty(node.Attribute("rights")) Then Wiki.License = node.Attribute("rights")

            If node.HasAttribute("sitename") AndAlso Wiki.IsCustom Then
                Wiki.Name = node.Attribute("sitename")

                'Handle two wikis having the same sitename
                For Each otherWiki As Wiki In App.Wikis.All
                    If otherWiki IsNot Wiki AndAlso otherWiki.Name = Wiki.Name _
                        Then Wiki.Name &= " (" & Wiki.Url.Host & ")"
                Next otherWiki
            End If

            If node.HasAttribute("time") Then Wiki.Config.ServerTimeOffset = Date.UtcNow - CDate(node.Attribute("time"))
        End Sub

        Private Sub ProcessSiteMatrix(ByVal sitematrixNode As XmlNode)
            For Each node As XmlNode In sitematrixNode.ChildNodes
                Select Case node.Name
                    Case "specials"
                        For Each special As XmlNode In node.ChildNodes
                            AssertApi(special.Name, "special")

                            Dim wiki As Wiki = App.Wikis(special.Attribute("code"))

                            wiki.Channel = "#" & special.Attribute("url").FromFirst("http://").ToFirst(".org")
                            wiki.Family = App.Families.Wikimedia
                            wiki.Type = "special"

                            wiki.Name = UcFirst(wiki.Code)
                            wiki.FileUrl = New Uri(InternalConfig.WikimediaFilePath & "wikipedia/" & wiki.Code & "/")
                            wiki.SecureUrl = New Uri(InternalConfig.WikimediaSecurePath & "wikipedia/" & wiki.Code & "/w/")
                            wiki.Url = New Uri(special.Attribute("url") & "/w/")

                            If special.HasAttribute("private") Then
                                wiki.IsPublicEditable = False
                                wiki.IsPublicReadable = False
                            End If

                            If special.HasAttribute("fishbowl") Then wiki.IsPublicEditable = False

                            'Generate friendly names for some Wikimedia wikis
                            If wiki.Code.Contains("-") AndAlso wiki.Code.EndsWithI("labswikimedia") Then
                                wiki.Name = "Wikimedia Labs (" & wiki.Code.ToFirst("-") & ")"
                            ElseIf wiki.Code.EndsWithI("wikimedia") Then
                                wiki.Name = "Wikimedia (" & wiki.Code.ToFirst("wikimedia") & ")"
                                wiki.Type = "wikimedia"
                            End If
                        Next special

                    Case "language"
                        'sitematrix incorrectly lists "nomcom" as a language
                        If node.Attribute("code") = "nomcom" Then Continue For

                        Dim language As Language = App.Languages(node.Attribute("code"))
                        language.Name = node.Attribute("name")

                        For Each outerSite As XmlNode In node.ChildNodes
                            'For some reason, the list of sites is wrapped in a superfluous <site> tag
                            AssertApi(outerSite.Name, "site")

                            For Each site As XmlNode In outerSite.ChildNodes
                                AssertApi(site.Name, "site")

                                Dim code As String = site.Attribute("code")
                                Dim type As String = If(code = "wiki", "wikipedia", code)

                                Dim wiki As Wiki = App.Wikis(node.Attribute("code") & "." & type)
                                wiki.Channel = "#" & site.Attribute("url").FromFirst("http://").ToFirst(".org")
                                wiki.Family = App.Families.Wikimedia
                                wiki.Language = language
                                wiki.Type = type
                                wiki.Name = wiki.Code
                                wiki.FileUrl = New Uri(InternalConfig.WikimediaFilePath & type & "/" & language.Code & "/")
                                wiki.SecureUrl = New Uri(InternalConfig.WikimediaSecurePath & type & "/" & language.Code & "/w/")
                                wiki.Url = New Uri(site.Attribute("url") & "/w/")
                            Next site
                        Next outerSite

                    Case Else : Log.Write(Msg("api-unrecognized", "sitematrix", node.Name))
                End Select
            Next node
        End Sub

        Private Sub ProcessThreads(ByVal threadsNode As XmlNode)
            For Each node As XmlNode In threadsNode.ChildNodes
                Dim thread As Comment = Wiki.Threads(CInt(node.Attribute("id")))

                If node.HasAttribute("ancestor") Then thread.Parent = Wiki.Threads(CInt(node.Attribute("ancestor")))
                If node.HasAttribute("created") Then thread.Created = CDate(node.Attribute("created"))
                If node.HasAttribute("modified") Then thread.Modified = CDate(node.Attribute("modified"))
                If node.HasAttribute("pagetitle") Then thread.Page = Wiki.Pages(node.Attribute("pagetitle"))
                If node.HasAttribute("parent") Then thread.Parent = Wiki.Threads(CInt(node.Attribute("parent")))
                If node.HasAttribute("rootid") Then thread.ContentPageID = CInt(node.Attribute("rootid"))
                If node.HasAttribute("subject") Then thread.Title = node.Attribute("subject")
                If node.HasAttribute("summaryid") Then thread.SummaryPageID =
                    If(node.Attribute("summaryid") = "", 0, CInt(node.Attribute("summaryid")))
                If node.HasAttribute("type") Then thread.Type = CInt(node.Attribute("type"))

                'Author information is nested for some reason
                For Each authorNode As XmlNode In node.ChildNodes
                    AssertApi(authorNode.Name, "author")

                    If authorNode.HasAttribute("name") Then
                        Dim user As User = Wiki.Users(authorNode.Attribute("name"))

                        If authorNode.HasAttribute("id") AndAlso authorNode.Attribute("id") <> "0" _
                            Then user.Id = CInt(authorNode.Attribute("id"))
                        thread.Author = user
                    End If
                Next authorNode
            Next node
        End Sub

        Private Sub ProcessUsers(ByVal usersNode As XmlNode)
            For Each userNode As XmlNode In usersNode.ChildNodes
                AssertApi(userNode.Name, "user")

                Dim user As User = Wiki.Users(userNode.Attribute("name"))
                Items.Add(user)

                If userNode.HasAttribute("userrightstoken") _
                    Then Session.RightsTokens.Merge(user, userNode.Attribute("userrightstoken"))
                If userNode.HasAttribute("registration") AndAlso userNode.Attribute("registration").Length > 0 _
                    Then user.Created = userNode.Attribute("registration").ToDate
                If userNode.HasAttribute("editcount") Then user.Contributions = CInt(userNode.Attribute("editcount"))
                user.IsEmailable = userNode.HasAttribute("emailable")

                For Each node As XmlNode In userNode.ChildNodes
                    Select Case node.Name
                        Case "groups"
                            user.Groups.Clear()

                            For Each g As XmlNode In node.FirstChild.ChildNodes
                                AssertApi(g.Name, "g")
                                user.Groups.Merge(Wiki.UserGroups(g.Value))
                            Next g

                        Case Else : Log.Write(Msg("error-apiunrecognized", "user", node.Name))
                    End Select
                Next node

                If Processing Then user.Process()
            Next userNode
        End Sub

        Private Sub ProcessUserContribs(ByVal contribsNode As XmlNode)
            Dim nextRev As Revision = Nothing

            For Each node As XmlNode In contribsNode.ChildNodes
                AssertApi(node.Name, "item")

                Dim rev As Revision = Wiki.Revisions(CInt(node.Attribute("revid")))
                Items.Add(rev)

                rev.IsBot = node.HasAttribute("bot")
                rev.IsMinor = node.HasAttribute("minor")

                If node.HasAttribute("title") Then rev.Page =
                    Wiki.Pages(CInt(node.Attribute("ns")), node.Attribute("title"))

                If node.HasAttribute("pageid") Then rev.Page.Id = CInt(node.Attribute("pageid"))
                If node.HasAttribute("comment") Then rev.Summary = node.Attribute("comment")
                If node.HasAttribute("timestamp") Then rev.Time = node.Attribute("timestamp").ToDate
                If node.HasAttribute("user") Then rev.User = Wiki.Users(node.Attribute("user"))

                If node.HasAttribute("top") AndAlso rev.Page.LastRev Is Nothing Then rev.Page.LastRev = rev
                If node.HasAttribute("new") AndAlso rev.Page.FirstRev Is Nothing Then rev.Page.FirstRev = rev

                If nextRev Is Nothing Then
                    rev.User.LastEdit = rev
                ElseIf nextRev.Id > rev.Id Then
                    rev.NextByUser = nextRev
                    rev.NextByUser.PrevByUser = rev
                End If

                If node.NextSibling Is Nothing AndAlso Query.Contains("uclimit") _
                    AndAlso Query("uclimit").ToString <> "max" _
                    AndAlso CInt(Query("uclimit")) > contribsNode.ChildNodes.Count Then

                    rev.User.FirstEdit = rev
                    rev.PrevByUser = Revision.Null
                End If

                nextRev = rev

                rev.DetailsKnown = True
                rev.Exists = TS.True

                If Processing Then
                    rev.Page.Process()
                    rev.Process()
                End If
            Next node

            If nextRev IsNot Nothing Then nextRev.User.OnContribsChanged()
        End Sub

        Private Sub ProcessUserInfo(ByVal infoNode As XmlNode)
            If infoNode.HasAttribute("editcount") Then User.Contributions = CInt(infoNode.Attribute("editcount"))
            If infoNode.HasAttribute("emailauthenticated") _
                Then User.Config.EmailAuthenticated = CDate(infoNode.Attribute("emailauthenticated"))
            If infoNode.HasAttribute("email") Then User.Preferences.EmailAddress = infoNode.Attribute("email")
            If infoNode.HasAttribute("id") AndAlso infoNode.Attribute("id") <> "0" _
                Then User.Id = CInt(infoNode.Attribute("id"))

            'If we thought we were logged in but we're getting "anon", then apparently we weren't
            If Not User.IsAnonymous Then Session.IsActive = Not infoNode.HasAttribute("anon")

            For Each node As XmlNode In infoNode.ChildNodes
                Select Case node.Name
                    Case "changeablegroups"
                        User.GroupChanges.Reset()

                        For Each subNode As XmlNode In node.ChildNodes
                            For Each g As XmlNode In subNode.ChildNodes
                                AssertApi(g.Name, "g")
                                Dim group As UserGroup = Wiki.UserGroups(g.FirstChild.Value)

                                Select Case subNode.Name
                                    Case "add" : User.GroupChanges(group).CanAdd = True
                                    Case "add-self" : User.GroupChanges(group).CanAddSelf = True
                                    Case "remove" : User.GroupChanges(group).CanRemove = True
                                    Case "remove-self" : User.GroupChanges(group).CanRemoveSelf = True
                                End Select
                            Next g
                        Next subNode

                    Case "groups"
                        User.Groups.Clear()

                        For Each g As XmlNode In node.ChildNodes
                            AssertApi(g.Name, "g")
                            User.Groups.Merge(Wiki.UserGroups(g.FirstChild.Value))
                        Next g

                    Case "options"
                        Dim prefs As New Dictionary(Of String, String)
                        Dim prefKeys As New List(Of String)

                        For Each attr As XmlAttribute In node.Attributes
                            prefs.Merge(attr.Name, attr.Value)
                            prefKeys.Add(attr.Name)
                        Next attr

                        User.Preferences.LoadFromMwFormat(prefs)
                        prefKeys.Sort()
                        Wiki.Preferences = prefKeys

                    Case "ratelimits"
                        User.RateLimits.Clear()

                        For Each rateNode As XmlNode In node.ChildNodes
                            For Each groupNode As XmlNode In rateNode.ChildNodes
                                User.RateLimits.Add(New RateLimit( _
                                    Action:=rateNode.Name, _
                                    Group:=groupNode.Name, _
                                    Hits:=CInt(groupNode.Attribute("hits")), _
                                    Seconds:=CInt(groupNode.Attribute("seconds"))))
                            Next groupNode
                        Next rateNode

                    Case "rights"
                        User.Rights.Clear()

                        For Each r As XmlNode In node.ChildNodes
                            AssertApi(r.Name, "r")
                            User.Rights.Merge(r.FirstChild.Value)
                        Next r

                    Case Else : Log.Write(Msg("error-apiunrecognized", "userinfo", node.Name))
                End Select
            Next node
        End Sub

        Private Shared Sub AssertApi(ByVal nodeValue As String, ByVal expectedValue As String)
            If nodeValue <> expectedValue Then Log.Write(Msg("error-apiformat", nodeValue, expectedValue))
        End Sub

    End Class

End Namespace