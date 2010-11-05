Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Retrieve page info

    Class PageInfoQuery : Inherits Query

        Private _Pages As List(Of Page)

        Private Categories, Diffs, Externals, LangLinks, Links As Boolean
        Private Media, OldRevision, Revision, Transclusions As Boolean

        Friend Sub New(ByVal session As Session)
            MyBase.New(session, Msg("pageinfo-desc"))
            _Pages = New List(Of Page)
        End Sub

        Friend Property Content As Boolean

        Friend ReadOnly Property Pages As List(Of Page)
            Get
                Return _Pages
            End Get
        End Property

        Friend Sub New(ByVal session As Session, ByVal Pages As List(Of Page), _
            Optional ByVal Categories As Boolean = False, Optional ByVal Content As Boolean = False, _
            Optional ByVal Diffs As Boolean = False, Optional ByVal Externals As Boolean = False, _
            Optional ByVal LangLinks As Boolean = False, Optional ByVal Links As Boolean = False, _
            Optional ByVal Media As Boolean = False, Optional ByVal Revision As Boolean = False, _
            Optional ByVal Transclusions As Boolean = False)

            MyBase.New(session, Msg("pageinfo-desc", Pages(0)))
            Me.Categories = Categories
            Me.Diffs = Diffs
            Me.Externals = Externals
            Me.LangLinks = LangLinks
            Me.Links = Links
            Me.Media = Media
            _Pages = Pages
            Me.Transclusions = Transclusions
            Me.OldRevision = OldRevision
            Me.Revision = Revision
            Me.Content = Content
        End Sub

        Friend Overrides Sub Start()

            Dim query As New QueryString(
                "action", "query",
                "titles", Pages.ToStringArray.Join("|"))

            Dim Prop As New List(Of String)
            Prop.Add("info")

            For Each page As Page In Pages
                If page.Space Is page.Wiki.Spaces.Category Then Prop.Merge("categoryinfo")

                If page.Space Is page.Wiki.Spaces.File Then
                    Prop.Merge("imageinfo")
                    query.Add("iiprop", "timestamp|user|comment|url|size|sha1|mime|metadata|archivename|bitdepth")
                    query.Add("iilimit", "max")
                End If
            Next page

            If Categories Then
                Prop.Add("categories")
                query.Add("cllimit", "max")
            End If

            If Externals Then
                Prop.Add("extlinks")
                query.Add("ellimit", "max")
            End If

            If LangLinks Then
                Prop.Add("langlinks")
                query.Add("lllimit", "max")
            End If

            If Links Then
                Prop.Add("links")
                query.Add("pllimit", "max")
            End If

            If Media Then
                Prop.Add("images")
                query.Add("imlimit", "max")
            End If

            If Transclusions Then
                Prop.Add("templates")
                query.Add("tllimit", "max")
            End If

            If Revision OrElse Content Then
                Prop.Add("revisions")
                If Diffs Then query.Add("rvdiffto", "prev")
                query.Add("rvprop", "ids|flags|timestamp|user|size|comment" & If(Content, "|content", ""))
            End If

            query.Add("prop", Prop.Join("|"))

            Dim request As ApiRequest = New ApiRequest(Session, Description, query)
            request.Start()

            If request.Result.IsError Then OnFail(request.Result) : Return
            OnSuccess()
        End Sub

    End Class

End Namespace
