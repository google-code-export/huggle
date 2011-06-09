Imports System.Collections.Generic

Namespace Huggle.Queries

    'Retrieve page info

    Friend Class PageInfoQuery : Inherits Query

        Private _Pages As List(Of Page)

        Public Sub New(ByVal session As Session)
            Me.New(session, New List(Of Page))
        End Sub

        Public Sub New(ByVal session As Session, ByVal pages As List(Of Page))
            MyBase.New(session, Msg("pageinfo-desc"))
            _Pages = pages
        End Sub

        Public Property Categories As Boolean

        Public Property Content As Boolean

        Public Property Diffs As Boolean

        Public Property Externals As Boolean

        Public Property LangLinks As Boolean

        Public Property Links As Boolean

        Public Property Media As Boolean

        Public Property OldRevision As Boolean

        Public ReadOnly Property Pages As List(Of Page)
            Get
                Return _Pages
            End Get
        End Property

        Public Property Revision As Boolean

        Public Property Transclusions As Boolean

        Public Overrides Sub Start()

            Dim query As New QueryString(
                "action", "query",
                "titles", Pages.ToStringArray.Join("|"))

            Dim Prop As New List(Of String)
            Prop.Add("info")

            For Each page As Page In Pages
                If page IsNot Nothing Then
                    If page.Space Is page.Wiki.Spaces.Category Then Prop.Merge("categoryinfo")

                    If page.Space Is page.Wiki.Spaces.File Then
                        Prop.Merge("imageinfo")
                        query.Add("iiprop", "timestamp|user|comment|url|size|sha1|mime|metadata|archivename|bitdepth")
                        query.Add("iilimit", "max")
                    End If
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
