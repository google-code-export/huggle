Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    'Retrieve page info

    Class PageDetailQuery : Inherits Query

        Private _Page As Page

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("pagedetail-desc", page))

            ThrowNull(page, "page")
            _Page = page
        End Sub

        Public Property Categories As Boolean

        Public Property Content As Boolean = True

        Public Property Diffs As Boolean

        Public Property Externals As Boolean

        Public Property LangLinks As Boolean

        Public Property Links As Boolean

        Public Property Media As Boolean

        Public ReadOnly Property Page As Page
            Get
                Return _Page
            End Get
        End Property

        Public Property Revisions As Boolean = True

        Public Property Revs As RevType

        Public Property Templates As Boolean

        Public Overrides Sub Start()

            Dim query As New QueryString(
                "action", "query",
                "titles", Page)

            Dim prop As New List(Of String)
            prop.Add("info")

            If Page.Space Is User.Wiki.Spaces.Category Then prop.Add("categoryinfo")

            If Page.Space Is User.Wiki.Spaces.File Then
                prop.Add("imageinfo")
                query.Add("iiprop", "timestamp|user|comment|url|size|sha1|mime|metadata|archivename|bitdepth")
                query.Add("iilimit", User.ApiLimit)
            End If

            If Categories Then
                prop.Add("categories")
                query.Add("cllimit", User.ApiLimit)
            End If

            If Externals Then
                prop.Add("extlinks")
                query.Add("ellimit", User.ApiLimit)
            End If

            If LangLinks Then
                prop.Add("langlinks")
                query.Add("lllimit", User.ApiLimit)
            End If

            If Links Then
                prop.Add("links")
                query.Add("pllimit", User.ApiLimit)
            End If

            If Media Then
                prop.Add("images")
                query.Add("imlimit", User.ApiLimit)
            End If

            If Templates Then
                prop.Add("templates")
                query.Add("tllimit", User.ApiLimit)
            End If

            If Revisions Then
                prop.Add("revisions")

                If Diffs Then query.Add("rvdiffto", "prev")

                If Revs = RevType.First OrElse Revs = RevType.Last _
                    Then query.Add("rvlimit", 1) Else query.Add("rvlimit", 10)

                If Revs = RevType.First OrElse Revs = RevType.Old _
                    Then query.Add("rvdir", "newer") Else query.Add("rvdir", "older")

                query.Add("rvprop", "ids|flags|timestamp|user|size|comment" & If(Content, "|content", ""))
            End If

            query.Add("prop", prop.Join("|"))

            Dim req As ApiRequest = New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsErrored Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

    Friend Enum RevType As Integer
        : Last : First : Recent : Old
    End Enum

End Namespace
