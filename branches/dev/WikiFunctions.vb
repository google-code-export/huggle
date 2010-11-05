Imports Huggle.Scripting
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle

    Friend Module WikiFunctions

        Friend Function ParseWikiUrl(ByVal uri As Uri) As Dictionary(Of String, String)
            Dim params As New Dictionary(Of String, String)
            Dim url As String = uri.ToString

            If url.Contains("wiki/") OrElse url.Contains("/index.php/") Then

                If url.Contains("wiki/") Then url = url.Substring(url.IndexOfI("wiki/") + 5) _
                    Else url = url.Substring(url.IndexOfI("/index.php/") + 12)

                If url.Contains("?") Then
                    Dim title As String = url.Substring(0, url.IndexOfI("?"))

                    If title.Contains("#") Then title = title.Substring(0, title.IndexOfI("#"))
                    params.Add("title", UrlDecode(title.Replace("_", " ")))
                    url = url.Substring(url.IndexOfI("?") + 1)
                Else
                    If url.Contains("#") Then url = url.Substring(0, url.IndexOfI("#"))
                    params.Add("title", UrlDecode(url.Replace("_", " ")))
                    url = ""
                End If

            ElseIf url.Contains("/index.php?") Then
                url = url.Substring(url.IndexOfI("/index.php?") + 12)

            ElseIf url.Contains("/wiki.phtml?") Then
                url = url.Substring(url.IndexOfI("/wiki.phtml?") + 13)
            End If

            For Each param As String In url.Split("&"c)
                If param.Contains("=") Then params.Merge(param.Substring(0, param.IndexOfI("=")),
                    UrlDecode(param.Substring(param.IndexOfI("=") + 1)))
            Next param

            Return params
        End Function

        Friend Function WikiStripMarkup(ByVal text As String) As String
            If text Is Nothing Then Return Nothing

            'Remove CSS-hidden text
            Dim hiddenPattern As New Regex( _
                "<([a-z]+) [^/>]*?style *= *[""']display: *none[""'][^/>]*?/?>", RegexOptions.Compiled)

            For Each item As Match In hiddenPattern.Matches(text)
                If item.Value.EndsWithI("/>") Then
                    text = text.Remove(">")
                Else
                    Dim hiddenText As String = item.Value
                    Dim remaining As String = text.FromFirst(item.Value)
                    Dim nestingDepth As Integer = 1
                    Dim hpos As Integer = remaining.IndexOfPattern("[<>]")

                    While hpos > -1 AndAlso nestingDepth > 0
                        If remaining(hpos) = "<"c AndAlso remaining(hpos + 1) <> "/" Then
                            nestingDepth += 2
                        ElseIf remaining(hpos) = ">" Then
                            nestingDepth -= 1
                        End If

                        hiddenText &= remaining.Substring(0, hpos + 1)
                        remaining = remaining.Substring(hpos + 1)
                        hpos = remaining.IndexOfPattern("[<>]")
                    End While

                    text = text.Remove(hiddenText)
                End If
            Next item

            'Strip wikilinks
            While text.Contains("[[") AndAlso text.IndexOfI("[[") < text.IndexOfI("]]")
                Dim title As String = text.FromFirst("[[")

                If title.Contains("|") AndAlso (Not title.Contains("]]") _
                    OrElse title.IndexOfI("|") < title.IndexOfI("]]")) _
                    Then If title.StartsWithI("File:") OrElse title.StartsWithI("Image:") _
                    Then title = title.ToFirst("|") Else title = title.FromFirst("|")

                title = title.ToFirst("]]")
                text = text.ToFirst("[[") & title & text.FromFirst("]]")
            End While

            'Strip HTML
            While text.Contains("<") AndAlso text.IndexOfI("<") < text.IndexOfI(">")
                text = text.ToFirst("<") & text.FromFirst(">")
            End While

            'Strip references
            While text.Contains("{{cite") AndAlso text.IndexOfI("}}", text.IndexOfI("{{cite")) > -1
                text = text.ToFirst("{{cite") & text.FromFirst("{{cite").FromFirst("}}")
            End While

            Return text
        End Function

        Friend Function WikiStripSummary(ByVal summary As String) As String
            If summary Is Nothing Then Return Nothing

            While summary.IndexOfI("[[") > -1 AndAlso summary.IndexOfI("[[") < summary.IndexOfI("]]")
                Dim title As String = summary.FromFirst("[[")

                If title.Contains("|") AndAlso (Not title.Contains("]]") _
                    OrElse title.IndexOfI("|") < title.IndexOfI("]]")) Then title = title.FromFirst("|")

                title = title.ToFirst("]]")
                summary = summary.ToFirst("[[") & title & summary.FromFirst("]]")
            End While

            Return summary
        End Function

        Friend Function WikiSummaryToHtml(ByVal text As String) As String
            'Text = Regex.Replace(Text, "\[\[([^\]\|]+)\]\]", _
            '    "<a href='{0}$1' title='$1'>$1</a>".FormatWith(Wiki.Current.ShortPath))
            'Text = Regex.Replace(Text, "\[\[([^\]\|]+)\|([^\]\|]+)\]\]", _
            '    "<a href='{0}$1' title='$1'>$2</a>".FormatWith(Wiki.Current.ShortPath))
            Return text
        End Function

        Friend Function WikiSummaryHtmlToWikitext(ByVal text As String) As String
            'Serves as a VERY basic "reverse" HTML-to-wikitext parser, mostly for screen-scraping purposes
            text = Regex.Replace(text, "<a href=""/[^""]+"" title=""([^""]+)""[^>]*>([^<]+)</a>", "[[${1}|${2}]]")
            text = text.Replace("</p><p>", CRLF)
            text = HtmlDecode(StripHtml(text))
            Return text
        End Function

        'Converts a timestamp from MediaWiki's internal format
        Friend Function FromWikiTimestamp(ByVal ts As String) As Date
            Return New Date(CInt(ts.Substring(0, 4)), CInt(ts.Substring(4, 2)), CInt(ts.Substring(6, 2)),
                CInt(ts.Substring(8, 2)), CInt(ts.Substring(10, 2)), CInt(ts.Substring(12, 2)), DateTimeKind.Utc)
        End Function

        'Returns a timestamp in MediaWiki's internal format
        Friend Function WikiTimestamp(ByVal time As Date) As String
            If time = Date.MaxValue Then Return "never"
            time = time.ToUniversalTime

            Return time.Year.ToStringI & time.Month.ToStringI.PadLeft(2, "0"c) & time.Day.ToStringI.PadLeft(2, "0"c) &
                time.Hour.ToStringI.PadLeft(2, "0"c) & time.Minute.ToStringI.PadLeft(2, "0"c) &
                time.Second.ToStringI.PadLeft(2, "0"c)
        End Function

        Friend Function WikiUrlEncode(ByVal text As String) As String
            Return UrlEncode(text.Replace(" ", "_"))
        End Function

        'Determines whether a string corresponds to a MediaWiki
        'message and extracts the values of any parameters
        Friend Function MessageMatch(ByVal message As String, ByVal text As String) As Match
            Static noMatch As Match = Regex.Match("", ".", RegexOptions.Compiled)

            If message Is Nothing Then Return noMatch
            Dim pattern As String = EscapeMwMessage(message)
            Dim i As Integer = 1

            While pattern.Contains("$" & CStr(i))
                pattern = pattern.Replace("$" & CStr(i), "(?<m" & CStr(i) & ">.+?)")
                i += 1
            End While

            Return Regex.Match(text, pattern)
        End Function

        Friend Function FormatMwMessage(ByVal message As String, ByVal ParamArray Params() As Object) As String
            For i As Integer = 0 To Params.Length - 1
                message = message.Replace("$" & CStr(i + 1), Params(i).ToString)
            Next i

            Return message
        End Function

        Friend Function MwMessagePattern(ByVal message As String, ByVal ParamArray paramPatterns() As String) As String
            'Hacky matching of PLURAL: function because some messages use it
            Dim result As String = Regex.Replace(message, "\{\{PLURAL:(.*)\|(.*)\}\}", "($1\|$2)")

            Dim i As Integer = 1

            'Escape the placeholders
            While result.Contains("$" & CStr(i))
                result = result.Replace("$" & CStr(i), Convert.ToChar(i))
                i += 1
            End While

            'Escape the message
            result = Regex.Escape(result)

            'Fill in the placeholders
            While i > 1
                i -= 1
                result = result.Replace(Convert.ToChar(i), paramPatterns(i - 1))
            End While

            Return result
        End Function

        Friend Function EscapeMwMessage(ByVal message As String) As String
            Dim i As Integer = 1

            While message.Contains("$" & CStr(i))
                message = message.Replace("$" & CStr(i), Convert.ToChar(i))
                i += 1
            End While

            message = Regex.Escape(message)

            While i > 0
                i -= 1
                message = message.Replace(Convert.ToChar(i), "$" & CStr(i))
            End While

            Return message
        End Function

        Friend Function EscapeWikitext(ByVal text As String) As String
            Return "<nowiki>" & text & "</nowiki>"
        End Function

        Friend Function WikitextOrderedList(ByVal list As ArrayList) As Wikistring
            Dim lines As New List(Of Wikistring)
            WikitextList(list, 1, lines, "#"c)
            Return New Wikistring(lines.Join(CRLF))
        End Function

        Friend Function WikitextUnorderedList(ByVal list As ArrayList) As Wikistring
            Dim lines As New List(Of Wikistring)
            WikitextList(list, 1, lines, "*"c)
            Return New Wikistring(lines.Join(CRLF))
        End Function

        Private Sub WikitextList(ByVal list As ArrayList, ByVal level As Integer,
            ByRef result As List(Of Wikistring), ByVal indent As Char)

            For Each item As Object In list
                If TypeOf item Is ArrayList Then
                    WikitextList(DirectCast(item, ArrayList), level + 1, result, indent)
                Else
                    result.Add(New Wikistring(New String(indent, level) & Data.AsWikitext(item).ToString))
                End If
            Next item
        End Sub

    End Module

    Friend Class Wikistring

        Private _Value As String

        Friend Sub New(ByVal value As String)
            _Value = value
        End Sub

        Friend ReadOnly Property Value() As String
            Get
                Return _Value
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return _Value
        End Function

    End Class

    Friend Class Confirmation

        Private _Id As String
        Private _Image As Image

        Friend Sub New(ByVal id As String, ByVal image As Image)
            _Id = id
            _Image = image
        End Sub

        Friend Property Answer As String

        Friend ReadOnly Property Id As String
            Get
                Return _Id
            End Get
        End Property

        Friend ReadOnly Property Image As Image
            Get
                Return _Image
            End Get
        End Property

    End Class


End Namespace