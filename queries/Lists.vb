Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    Namespace Lists

        Public Class AllPagesQuery : Inherits ListQuery

            Sub New(ByVal session As Session, ByVal space As Space)
                MyBase.New(session, "list", "allpages", "ap", _
                    New QueryString("apnamespace", space.Number), Msg("listdesc-allpages", space.Name))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If CBool(value) Then Query.Add("apfilterredir", "redirects") _
                        Else Query.Add("apfilterredir", "nonredirects")

                    Case "langlinks" : If CBool(value) Then Query.Add("apfilterlanglinks", "withlanglinks") _
                        Else Query.Add("apfilterlanglinks", "withoutlanglinks")
                End Select
            End Sub

        End Class

        Public Class BacklinksQuery : Inherits ListQuery

            'Get pages that link to another page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "list", "backlinks", "bl", _
                    New QueryString("blfilterredir", "nonredirects", "bltitle", page), Msg("listdesc-backlinks", page.Title))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If CBool(value) Then Query.Add("blfilterredir", "redirects") _
                        Else Query.Add("blfilterredir", "nonredirects")
                End Select
            End Sub

        End Class

        Public Class CategoryQuery : Inherits ListQuery

            'Get the contents of a category

            Sub New(ByVal session As Session, ByVal category As Category)
                MyBase.New(session, "list", "categorymembers", "cm", _
                    New QueryString("cmtitle", category.Page.Title), Msg("listdesc-category", category.Name))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "from"
                        Query.Remove("cmfrom")
                        Query.Add("cmstartsortkey", value)
                    Case "namespace"
                        Query.Add("cmnamespace", value)
                    Case "start"
                        Query.Add("cmcontinue", value & "|")
                End Select
            End Sub

        End Class

        Public Class ContribsQuery : Inherits ListQuery

            'Get user's contributions

            Sub New(ByVal session As Session, ByVal target As User)
                MyBase.New(session, "revisions", "usercontribs", "uc", _
                    New QueryString("ucuser", target), Msg("listdesc-contribs", target.Name))

                ExpectDuplicates = True
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "minor"
                        If Boolean.TryParse(value, Nothing) Then _
                            If CBool(value) Then Query.Add("ucshow", "minor") Else Query.Add("ucshow", "!minor")
                End Select
            End Sub

        End Class

        Public Class DeletedContribsQuery : Inherits ListQuery

            'Get a user's deleted contributions

            Sub New(ByVal session As Session, ByVal target As User)
                MyBase.New(session, "revisions", "deletedrevs", "dr", _
                    New QueryString("druser", target), Msg("listdesc-deletedcontribs", target.Name))
            End Sub

        End Class

        Public Class DeletedHistoryQuery : Inherits ListQuery

            'Get deleted history for page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "revisions", "deletedrevs", "dr", _
                    New QueryString("titles", page), Msg("listdesc-deletedhistory", page.Title))
            End Sub

        End Class

        Public Class ExternalLinkUsageQuery : Inherits ListQuery

            'Get pages that use an external link

            Sub New(ByVal session As Session, ByVal link As String)
                MyBase.New(session, "list", "exturlusage", "eu", _
                    New QueryString("euquery", link), Msg("listdesc-externallinkusage", link))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "protocol"
                        If value IsNot Nothing Then Query.Add("euprotocol", value)
                End Select
            End Sub

        End Class

        Public Class FileQuery : Inherits ListQuery

            'Read list from a file

            Private Filename As String

            Public Sub New(ByVal session As Session, ByVal filename As String)
                MyBase.New(session, "", "", "", Nothing, Msg("listdesc-file"))
                Me.Filename = filename
            End Sub

            Public Overrides Sub Start()
                Dim data As String()

                Try
                    data = IO.File.ReadAllLines(Filename)

                Catch ex As SystemException
                    OnFail(New Result(ex.Message, "ioerror")) : Return
                End Try

                Items.Clear()

                For Each title As String In IO.File.ReadAllLines(Filename)
                    If title.StartsWithI("*[[") OrElse title.StartsWithI("* [[") Then title = title.Substring(1)
                    title = Wiki.Pages.SanitizeTitle(title)
                    If title IsNot Nothing Then Items.Add(Wiki.Pages(title))
                Next title

                OnSuccess()
            End Sub

            Public Overrides Sub DoOne()
                Start()
            End Sub

        End Class

        Public Class GlobalBlocksQuery : Inherits ListQuery

            'Get global blocks

            Sub New(ByVal session As Session, ByVal target As User)
                MyBase.New(session, "list", "globalblocks", "bg", _
                    New QueryString("bgdir", "older", "bgprop", "id|address|by|timestamp|expiry|reason|range"), _
                    Msg("listdesc-globalblocks", target.Name))
            End Sub
        End Class

        Public Class GlobalMediaUsageQuery : Inherits ListQuery

            'Get global media usage

            Sub New(ByVal session As Session, ByVal media As File)
                MyBase.New(session, "prop", "globalusage", "gu", Nothing, Msg("listdesc-globalmediausage", media.Name))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "filterlocal" : Query.Add("gufilterlocal", CBool(value))
                End Select
            End Sub

        End Class

        Public Class HistoryQuery : Inherits ListQuery

            'Get history of a page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "revisions", "revisions", "rv", _
                    New QueryString("rvprop", "ids|flags|timestamp|user|size|comment", "titles", page), _
                    Msg("listdesc-history", page.Title))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "direction" : Query.Add("rvdir", value)
                    Case "excludeuser" : Query.Add("rvexcludeuser", value)
                    Case "user" : Query.Add("rvuser", value)
                End Select
            End Sub

        End Class

        Public Class LinksQuery : Inherits ListQuery

            'Get links on a page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "prop", "links", "pl", _
                    New QueryString("titles", page), Msg("listdesc-links", page.Title))
            End Sub

        End Class

        Public Class LogsQuery : Inherits ListQuery

            Sub New(ByVal session As Session)
                MyBase.New(session, "list", "logevents", "le", Nothing, Msg("listdesc-logs"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "logtype"
                        If value IsNot Nothing Then
                            Query.Add("letype", value)
                            Description &= Msg("listdesc-logtype", value)
                        End If

                    Case "page"
                        Dim page As Page = Wiki.Pages.FromString(value)

                        If page IsNot Nothing Then
                            Query.Add("letitle", page)
                            Description &= Msg("listdesc-logpage", page.Title)
                        End If

                    Case "user"
                        Dim target As User = Wiki.Users.FromString(value)

                        If target IsNot Nothing Then
                            Query.Add("leuser", target)
                            Description &= Msg("listdesc-loguser", target.Name)
                        End If
                End Select
            End Sub

        End Class

        Public Class ManualQuery : Inherits ListQuery

            'Request that just echoes an input list of titles

            Private Titles As List(Of String)

            Public Sub New(ByVal session As Session, ByVal titles As List(Of String))
                MyBase.New(session, "", "", "", Nothing, Msg("listdesc-manual"))
                Me.Titles = titles
            End Sub

            Public Overrides Sub Start()
                For Each title As String In Titles
                    title = Wiki.Pages.SanitizeTitle(title)
                    If title IsNot Nothing Then Items.Add(Wiki.Pages(title))
                Next title

                OnSuccess()
            End Sub

            Public Overrides Sub DoOne()
                Start()
            End Sub

        End Class

        Public Class MediaQuery : Inherits ListQuery

            'Get files on a page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "prop", "images", "im", _
                    New QueryString("titles", page), Msg("listdesc-media", page.Title))
            End Sub

        End Class

        Public Class MediaUsageQuery : Inherits ListQuery

            'Get pages that include a file

            Sub New(ByVal session As Session, ByVal media As File)
                MyBase.New(session, "list", "imageusage", "iu", _
                    New QueryString("iutitle", media), Msg("listdesc-mediausage", media.Name))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If value.ToBoolean Then Query.Add("iufilterredir", "redirects") _
                        Else Query.Add("iuifilterredir", "nonredirects")
                End Select
            End Sub

        End Class

        Public Class PrefixQuery : Inherits ListQuery

            Sub New(ByVal session As Session, ByVal space As Space, ByVal prefix As String)
                MyBase.New(session, "list", "allpages", "ap", _
                    New QueryString("apfilterredir", "nonredirects", "apprefix", prefix, "apnamespace", space.Number), _
                    Msg("listdesc-prefix", prefix, space))
            End Sub

        End Class

        Public Class ProtectedPagesRequest : Inherits ListQuery

            Sub New(ByVal session As Session, ByVal space As Space)
                MyBase.New(session, "list", "allpages", "ap", _
                    New QueryString("apprtype", "edit|move", "apfilterredir", "nonredirects", "apnamespace", space.Number), _
                    Msg("listdesc-protectedpages", space))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "cascade" : If CBool(value) Then Query.Add("apprfiltercascade", "cascading") _
                        Else Query.Add("apprfiltercascade", "noncascading")
                    Case "level" : Query.Add("apprlevel", value)
                    Case "protectiontype" : Query.Add("apprtype", value)
                    Case "redirects" : If CBool(value) Then Query.Add("apfilterredir", "redirects") _
                        Else Query.Add("apfilterredir", "nonredirects")
                End Select
            End Sub

        End Class

        Public Class ProtectedTitlesRequest : Inherits ListQuery

            Sub New(ByVal session As Session)
                MyBase.New(session, "list", "protectedtitles", "pt", _
                    New QueryString("ptprop", "timestamp|user|comment|expiry|level"), Msg("listdesc-protectedtitles"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "level" : Query.Add("ptlevel", value)
                End Select
            End Sub

        End Class

        Public Class RandomQuery : Inherits ListQuery

            'Get random pages

            Sub New(ByVal session As Session)
                MyBase.New(session, "list", "random", "rn", Nothing, Msg("listdesc-random"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If CBool(value) Then Query.Add("rnredirect", "redirects")
                End Select
            End Sub

        End Class

        Public Class RedirectsQuery : Inherits ListQuery

            'Get redirects to a page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "list", "backlinks", "bl", _
                    New QueryString("bltitle", page, "blfilterredir", "redirects"), Msg("listdesc-redirects", page))
            End Sub

        End Class

        Public Class SearchQuery : Inherits ListQuery

            'Get search results

            Sub New(ByVal session As Session, ByVal search As String)
                MyBase.New(session, "list", "search", "sr", New QueryString("srsearch", search, "srwhat", "text", _
                    "srinfo", "totalhits|suggestion", "srprop", "size|wordcount|timestamp|snippet"), Msg("listdesc-search"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If CBool(value) Then Query.Add("srredirects", "")
                    Case "searchtype" : Query.Add("srwhat", value)
                End Select
            End Sub

        End Class

        Public Class SubcatsQuery : Inherits ListQuery

            Sub New(ByVal session As Session, ByVal category As Category)
                MyBase.New(session, "list", "categorymembers", "cm", _
                    New QueryString("cmtitle", category, "cmnamespace", 14), Msg("listdesc-subcats", category))
            End Sub

        End Class

        Class TemplatesQuery : Inherits ListQuery

            'Get templates on a page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "prop", "templates", "tl", _
                    New QueryString("titles", page), Msg("listdesc-templates", page))
            End Sub

        End Class

        Public Class TransclusionsQuery : Inherits ListQuery

            'Get pages that transclude another page

            Sub New(ByVal session As Session, ByVal page As Page)
                MyBase.New(session, "list", "embeddedin", "ei", _
                    New QueryString("eititle", page), Msg("listdesc-transclusions"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "redirects" : If CBool(value) Then Query.Add("eifilterredir", "redirects") _
                        Else Query.Add("eifilterredir", "nonredirects")
                End Select
            End Sub

        End Class

        Public Class UnreviewedQuery : Inherits ListQuery

            'Get pages with unreviewed revisions

            Sub New(ByVal session As Session)
                MyBase.New(session, "list", "oldreviewedpages", "or", Nothing, Msg("listdesc-unreviewed"))
            End Sub

        End Class

        Public Class UserGroupQuery : Inherits ListQuery

            'Get members of a user group

            Sub New(ByVal session As Session, ByVal group As String)
                MyBase.New(session, "list", "allusers", "au", New QueryString("auprop", "blockinfo|editcount|registration", _
                    "augroup", group), Msg("listdesc-usergroup", group))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "edits" : Query.Add("auwitheditsonly", "")
                End Select
            End Sub

        End Class

        Public Class UsersQuery : Inherits ListQuery

            'Get all users

            Sub New(ByVal session As Session)
                MyBase.New(session, "list", "allusers", "au", Nothing, Msg("listdesc-allusers"))
            End Sub

            Protected Overrides Sub SetOption(ByVal name As String, ByVal value As String)
                Select Case name
                    Case "edits" : Query.Add("auwitheditsonly", "")
                End Select
            End Sub

        End Class

        Public Class WatchlistQuery : Inherits ListQuery

            'Get contents of user's watchlist

            Sub New(ByVal session As Session, ByVal User As User, Optional ByVal Token As String = Nothing)
                MyBase.New(session, "list", "watchlistraw", "wr", _
                    New QueryString("wlowner", User.Name, "wltoken", Token), Msg("listdesc-watchlist", User))
            End Sub

        End Class

    End Namespace

End Namespace