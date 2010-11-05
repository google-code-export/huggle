Imports Huggle.Scripting
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle.Wikitext

    'Represents a wikitext document

    <Diagnostics.DebuggerDisplay("{Text}")>
    Friend Class Document

        Private _Page As Page
        Private _ParseableText As String
        Private _Text As String
        Private _Wiki As Wiki

        Private _CategoryLinks As CategoryLinkCollection
        Private _FileLinks As FileLinkCollection
        Private _InterLangLinks As InterLangLinkCollection
        Private _Links As LinkCollection
        Private _Sections As SectionCollection
        Private _Tables As TableCollection
        Private _Threads As CommentCollection
        Private _Transclusions As TransclusionCollection

        Friend Sub New(ByVal page As Page, Optional ByVal text As String = Nothing)
            _Page = page
            _Text = If(text, If(page.Text, ""))
            _Wiki = page.Wiki
        End Sub

        Friend Sub New(ByVal wiki As Wiki, ByVal text As String)
            _Text = text
            _Wiki = wiki
        End Sub

        Friend ReadOnly Property Bytes() As Integer
            Get
                Return Encoding.UTF8.GetBytes(Text).GetLength(0)
            End Get
        End Property

        Friend ReadOnly Property Categories() As CategoryLinkCollection
            Get
                If _CategoryLinks Is Nothing Then _CategoryLinks = New CategoryLinkCollection(Me)
                Return _CategoryLinks
            End Get
        End Property

        Friend Property DefaultSortkey() As String
            Get
                Dim Match As Match = Parsing.SortkeyPattern.Match(Text)
                If Match.Success Then Return Match.Groups(1).Value Else Return ""
            End Get
            Set(ByVal value As String)
                Dim Match As Match = Parsing.SortkeyPattern.Match(Text)

                If Match.Success Then
                    'Overwrite the existing default sort key
                    Text = Text.Remove(Match.Groups(1).Index, Match.Groups(1).Length).Insert(Match.Groups(1).Index, Text)
                Else
                    'Construct a new default sort key
                    Dim Sortkey As String = "{{" & Wiki.MagicWords("defaultsort") & value & "}}"
                    Dim CatMatch As Match = New Regex(Parsing.BaseCatPattern(Wiki), RegexOptions.Compiled).Match(Text)

                    If CatMatch.Success Then
                        'Insert immediately before category links if there are any
                        Text = Text.Insert(CatMatch.Index, Sortkey & LF)
                    Else
                        'Insert at end of page
                        Text &= LF & LF & Sortkey
                    End If
                End If
            End Set
        End Property

        Friend ReadOnly Property Files() As FileLinkCollection
            Get
                If _FileLinks Is Nothing Then _FileLinks = New FileLinkCollection(Me)
                Return _FileLinks
            End Get
        End Property

        Friend ReadOnly Property InterLangLinks() As InterLangLinkCollection
            Get
                If _InterLangLinks Is Nothing Then _InterLangLinks = New InterLangLinkCollection(Me)
                Return _InterLangLinks
            End Get
        End Property

        Friend ReadOnly Property Length() As Integer
            Get
                Return Text.Length
            End Get
        End Property

        Friend ReadOnly Property Links() As LinkCollection
            Get
                If _Links Is Nothing Then _Links = New LinkCollection(Me)
                Return _Links
            End Get
        End Property

        Friend ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Friend ReadOnly Property References() As List(Of Reference)
            Get
                Dim Result As New List(Of Reference)

                Return Result
            End Get
        End Property

        Friend ReadOnly Property Sections() As SectionCollection
            Get
                If _Sections Is Nothing Then _Sections = New SectionCollection(Me)
                Return _Sections
            End Get
        End Property

        Friend ReadOnly Property Tables() As TableCollection
            Get
                If _Tables IsNot Nothing Then _Tables = New TableCollection(Me)
                Return _Tables
            End Get
        End Property

        Friend Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value

                _ParseableText = Nothing
                _Sections = Nothing
                _Tables = Nothing
            End Set
        End Property

        Friend ReadOnly Property Threads() As CommentCollection
            Get
                If _Threads Is Nothing Then _Threads = New CommentCollection(Wiki)
                Return _Threads
            End Get
        End Property

        Friend ReadOnly Property Transclusions() As TransclusionCollection
            Get
                If _Transclusions Is Nothing Then _Transclusions = New TransclusionCollection(Me)
                Return _Transclusions
            End Get
        End Property

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Text
        End Function

        Friend ReadOnly Property ParseableText() As String
            Get
                If _ParseableText IsNot Nothing Then Return _ParseableText

                _ParseableText = Text

                Dim spos As Integer = _ParseableText.IndexOfI("<nowiki>", 0)
                Dim epos As Integer = _ParseableText.IndexOfI("</nowiki>", Math.Max(spos, 0))

                While spos > -1 AndAlso epos > spos
                    _ParseableText = _ParseableText.Remove(spos, epos - spos).Insert(spos, _
                        (New StringBuilder).Append(Convert.ToChar(0), epos - spos).ToString)
                End While

                Return _ParseableText
            End Get
        End Property

        Friend Shared Function FromObject(ByVal data As Object) As Document

            If TypeOf data Is Page Then
                Dim Page As Page = DirectCast(data, Page)

                If Page.Space Is Page.Wiki.Spaces.Template _
                    Then Return New Document(Page.Wiki, "{{tl|" & Page.Name)
                Return New Document(Page.Wiki, "[[" & Page.Title & "]]")
            End If

            If TypeOf data Is User Then
                Dim User As User = DirectCast(data, User)

                Return New Document(User.Wiki, New Transclusion(User.Wiki, "user", 1, User).Wikitext)
            End If

            If TypeOf data Is Revision Then
                Dim Rev As Revision = DirectCast(data, Revision)
                Return New Document(Rev.Wiki, "[" & Rev.Wiki.ShortUrl.ToString & "?diff=" &
                    CStr(Rev.Id) & " " & CStr(Rev.Id) & "]")
            End If

            If TypeOf data Is ArrayList Then
                Dim Result As New StringBuilder
                Dim List As ArrayList = CType(data, ArrayList)

                If List.Count > 0 AndAlso List(0).GetType.Name = "Revisions" _
                    Then Result.Append("<div class='plainlinks'>" & Environment.NewLine)

                For Each Item As Object In List
                    Result.Append("* " & FromObject(Item).Text & Environment.NewLine)
                Next Item

                If List.Count > 0 AndAlso List(0).GetType.Name = "Revision" Then Result.Append("</div>")

                Return New Document(App.Wikis.Default, Result.ToString)
            End If

            If TypeOf data Is Table Then
                Dim Table As Table = CType(data, Table)
                Dim sb As New StringBuilder

                sb.Append("{| class='plainlinks sortable wikitable'" & Environment.NewLine & "! ")

                For i As Integer = 0 To Table.Columns.Count - 2
                    sb.Append(Table.Columns(i).Cells(0).Content & " !! ")
                Next i

                sb.Append(Table.Columns(Table.Columns.Count - 1).Cells(0).Content & Environment.NewLine)

                For Each Row As TableRow In Table.Rows
                    sb.Append("|-" & Environment.NewLine & "| ")

                    For i As Integer = 0 To Row.Cells.Count - 2
                        sb.Append(FromObject(Row.Cells(i)).Text & " || ")
                    Next i

                    sb.Append(FromObject(Row.Cells(Row.Cells.Count - 1)).Text & Environment.NewLine)
                Next Row

                sb.Append("|}")
                Return New Document(App.Wikis.Default, sb.ToString)
            End If

            Return New Document(App.Wikis.Default, data.ToString)
        End Function

        Friend Function ExternalLink(ByVal url As Uri, ByVal display As String) As String
            Return "[" & url.ToString & If(display Is Nothing, "", " " & display) & "]"
        End Function

        Friend Function InternalLink(ByVal page As Page, ByVal display As String,
            ByVal ParamArray params() As String) As String

            Dim result As String = "["
            Dim title As String = page.Title.Replace(" ", "_")
            Dim paramstring As String = ""

            For i As Integer = 0 To params.Length - 1
                params(i) = params(i).Replace(" ", "_")
                If i Mod 2 = 0 Then paramstring &= "&" & params(i) Else paramstring &= "=" & params(i)
            Next i

            If page.Wiki.ShortUrl Is Nothing _
                Then result &= page.Wiki.Url.ToString & "index.php?title=" & title & paramstring _
                Else result &= page.Wiki.ShortUrl.ToString & title & "?" & paramstring.Substring(1)

            If Not String.IsNullOrEmpty(display) Then result &= " " & display
            result &= "]"

            Return result
        End Function

        Friend Function InterlanguageLink(ByVal page As Page) As String
            If page.Wiki Is Wiki OrElse Wiki.InterwikiFor(page.Wiki) Is Nothing Then Return Nothing
            Return "[[" & Wiki.InterwikiFor(page.Wiki) & ":" & page.Title & "]]"
        End Function

        Friend Function Link(ByVal page As Page, ByVal display As String) As String
            Dim result As String = "[["

            If page.Wiki IsNot Wiki Then
                If Wiki.InterwikiFor(page.Wiki) Is Nothing Then Return Nothing
                result &= Wiki.InterwikiFor(page.Wiki) & ":"
            Else
                If page.Space Is page.Wiki.Spaces.Category _
                    OrElse page.Space Is page.Wiki.Spaces.File Then result &= ":"
            End If

            result &= page.Title
            If Not String.IsNullOrEmpty(display) Then result &= "|" & display
            result &= "]]"

            Return result
        End Function

        Friend Function UserLink(ByVal user As User) As String
            If user.IsAnonymous AndAlso Wiki.Config.UserLinkAnon IsNot Nothing _
                Then Return Wiki.Config.UserLinkAnon.FormatI(user)

            If Wiki.Config.UserLink IsNot Nothing Then Return Wiki.Config.UserLink.FormatI(user)
            Return Link(user.Userpage, user.Name)
        End Function

    End Class

End Namespace
