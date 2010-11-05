Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Actions

    'Retrieve page info

    Class PageDetailQuery : Inherits Query

        Private Page As Page
        Private Categories, Content, Diffs, Externals, LangLinks, Links, Media, Revisions, Templates As Boolean
        Private Revs As RevType

        Friend Sub New(ByVal session As Session, ByVal Page As Page, Optional ByVal Revs As RevType = RevType.Last, _
            Optional ByVal Categories As Boolean = False, Optional ByVal Content As Boolean = True, _
            Optional ByVal Diffs As Boolean = False, _
            Optional ByVal Externals As Boolean = False, Optional ByVal LangLinks As Boolean = False, _
            Optional ByVal Links As Boolean = False, Optional ByVal Media As Boolean = False, _
            Optional ByVal Revisions As Boolean = True, Optional ByVal Templates As Boolean = False)

            MyBase.New(session, Msg("pagedetail-desc"))
            Me.Categories = Categories
            Me.Content = Content
            Me.Diffs = Diffs
            Me.Externals = Externals
            Me.LangLinks = LangLinks
            Me.Links = Links
            Me.Media = Media
            Me.Page = Page
            Me.Revs = Revs
            Me.Revisions = Revisions
            Me.Templates = Templates
        End Sub

        Friend Overrides Sub Start()

            Dim Query As New QueryString( _
                "action", "query", _
                "titles", Page)

            Dim Prop As New List(Of String)
            Prop.Add("info")

            If Page.Space Is User.Wiki.Spaces.Category Then Prop.Add("categoryinfo")

            If Page.Space Is User.Wiki.Spaces.File Then
                Prop.Add("imageinfo")
                Query.Add("iiprop", "timestamp|user|comment|url|size|sha1|mime|metadata|archivename|bitdepth")
                Query.Add("iilimit", User.ApiLimit)
            End If

            If Categories Then
                Prop.Add("categories")
                Query.Add("cllimit", User.ApiLimit)
            End If

            If Externals Then
                Prop.Add("extlinks")
                Query.Add("ellimit", User.ApiLimit)
            End If

            If LangLinks Then
                Prop.Add("langlinks")
                Query.Add("lllimit", User.ApiLimit)
            End If

            If Links Then
                Prop.Add("links")
                Query.Add("pllimit", User.ApiLimit)
            End If

            If Media Then
                Prop.Add("images")
                Query.Add("imlimit", User.ApiLimit)
            End If

            If Templates Then
                Prop.Add("templates")
                Query.Add("tllimit", User.ApiLimit)
            End If

            If Revisions Then
                Prop.Add("revisions")

                If Diffs Then Query.Add("rvdiffto", "prev")

                If Revs = RevType.First OrElse Revs = RevType.Last _
                    Then Query.Add("rvlimit", 1) Else Query.Add("rvlimit", 10)

                If Revs = RevType.First OrElse Revs = RevType.Old _
                    Then Query.Add("rvdir", "newer") Else Query.Add("rvdir", "older")

                Query.Add("rvprop", "ids|flags|timestamp|user|size|comment" & If(Content, "|content", ""))
            End If

            Query.Add("prop", Prop.Join("|"))

            Dim Request As ApiRequest = New ApiRequest(Session, Description, Query)
            Request.Start()

            If Request.Result.IsError Then OnFail(Request.Result) : Return
            OnSuccess()
        End Sub

    End Class

    Friend Enum RevType As Integer
        : Last : First : Recent : Old
    End Enum

End Namespace
