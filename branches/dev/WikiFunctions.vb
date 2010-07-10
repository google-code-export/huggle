Imports Huggle.Scripting
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Namespace Huggle

    Public Module WikiFunctions

        Public Function WikiStripMarkup(ByVal text As String) As String
            If text Is Nothing Then Return Nothing

            'Remove CSS-hidden text
            Dim hiddenPattern As New Regex( _
                "<([a-z]+) [^/>]*?style *= *[""']display: *none[""'][^/>]*?/?>", RegexOptions.Compiled)

            For Each item As Match In hiddenPattern.Matches(text)
                If item.Value.EndsWith("/>") Then
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
            While text.Contains("[[") AndAlso text.IndexOf("[[") < text.IndexOf("]]")
                Dim title As String = text.FromFirst("[[")

                If title.Contains("|") AndAlso (Not title.Contains("]]") _
                    OrElse title.IndexOf("|") < title.IndexOf("]]")) _
                    Then If title.StartsWith("File:") OrElse title.StartsWith("Image:") _
                    Then title = title.ToFirst("|") Else title = title.FromFirst("|")

                title = title.ToFirst("]]")
                text = text.ToFirst("[[") & title & text.FromFirst("]]")
            End While

            'Strip HTML
            While text.Contains("<") AndAlso text.IndexOf("<") < text.IndexOf(">")
                text = text.ToFirst("<") & text.FromFirst(">")
            End While

            'Strip references
            While text.Contains("{{cite") AndAlso text.IndexOf("}}", text.IndexOf("{{cite")) > -1
                text = text.ToFirst("{{cite") & text.FromFirst("{{cite").FromFirst("}}")
            End While

            Return text
        End Function

        Public Function WikiStripSummary(ByVal summary As String) As String
            If summary Is Nothing Then Return Nothing

            While summary.IndexOf("[[") > -1 AndAlso summary.IndexOf("[[") < summary.IndexOf("]]")
                Dim title As String = summary.FromFirst("[[")

                If title.Contains("|") AndAlso (Not title.Contains("]]") _
                    OrElse title.IndexOf("|") < title.IndexOf("]]")) Then title = title.FromFirst("|")

                title = title.ToFirst("]]")
                summary = summary.ToFirst("[[") & title & summary.FromFirst("]]")
            End While

            Return summary
        End Function

        Public Function WikiSummaryToHtml(ByVal text As String) As String
            'Text = Regex.Replace(Text, "\[\[([^\]\|]+)\]\]", _
            '    "<a href='{0}$1' title='$1'>$1</a>".FormatWith(Wiki.Current.ShortPath))
            'Text = Regex.Replace(Text, "\[\[([^\]\|]+)\|([^\]\|]+)\]\]", _
            '    "<a href='{0}$1' title='$1'>$2</a>".FormatWith(Wiki.Current.ShortPath))
            Return text
        End Function

        Public Function WikiSummaryHtmlToWikitext(ByVal text As String) As String
            'Serves as a VERY basic "reverse" HTML-to-wikitext parser, mostly for screen-scraping purposes
            text = Regex.Replace(text, "<a href=""/[^""]+"" title=""([^""]+)""[^>]*>([^<]+)</a>", "[[${1}|${2}]]")
            text = text.Replace("</p><p>", CRLF)
            text = HtmlDecode(StripHtml(text))
            Return text
        End Function

        Public Function FromWikiTimestamp(ByVal ts As String) As Date
            Return New Date(CInt(ts.Substring(0, 4)), CInt(ts.Substring(4, 2)), CInt(ts.Substring(6, 2)), _
                CInt(ts.Substring(8, 2)), CInt(ts.Substring(10, 2)), CInt(ts.Substring(12, 2)), DateTimeKind.Utc)
        End Function

        Public Function WikiTimestamp(ByVal time As Date) As String
            If time = Date.MaxValue Then Return "never"
            time = time.ToUniversalTime

            Return time.Year.ToString & time.Month.ToString.PadLeft(2, "0"c) & time.Day.ToString.PadLeft(2, "0"c) & _
                time.Hour.ToString.PadLeft(2, "0"c) & time.Minute.ToString.PadLeft(2, "0"c) & _
                time.Second.ToString.PadLeft(2, "0"c)
        End Function

        Public Function WikiUrlEncode(ByVal Text As String) As String
            Return UrlEncode(Text.Replace(" ", "_"))
        End Function

        'Determines whether a string corresponds to a MediaWiki
        'message and extracts the values of any parameters
        Public Function MessageMatch(ByVal Message As String, ByVal Text As String) As Match
            Static NoMatch As Match = Regex.Match("", ".", RegexOptions.Compiled)

            If Message Is Nothing Then Return NoMatch
            Dim Pattern As String = EscapeMwMessage(Message)
            Dim i As Integer = 1

            While Pattern.Contains("$" & CStr(i))
                Pattern = Pattern.Replace("$" & CStr(i), "(?<m" & CStr(i) & ">.+?)")
                i += 1
            End While

            Return Regex.Match(Text, Pattern)
        End Function

        Public Function FormatMwMessage(ByVal message As String, ByVal ParamArray Params() As Object) As String
            For i As Integer = 0 To Params.Length - 1
                message = message.Replace("$" & CStr(i + 1), Params(i).ToString)
            Next i

            Return message
        End Function

        Public Function MwMessagePattern(ByVal message As String, ByVal ParamArray paramPatterns() As String) As String
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

        Public Function EscapeMwMessage(ByVal message As String) As String
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

        Public Function EscapeWikitext(ByVal text As String) As String
            Return "<nowiki>" & text & "</nowiki>"
        End Function

        Public Function WikitextOrderedList(ByVal list As ArrayList) As Wikistring
            Dim lines As New List(Of Wikistring)
            WikitextList(list, 1, lines, "#"c)
            Return New Wikistring(lines.Join(CRLF))
        End Function

        Public Function WikitextUnorderedList(ByVal list As ArrayList) As Wikistring
            Dim lines As New List(Of Wikistring)
            WikitextList(list, 1, lines, "*"c)
            Return New Wikistring(lines.Join(CRLF))
        End Function

        Private Sub WikitextList(ByVal list As ArrayList, ByVal level As Integer, _
            ByRef result As List(Of Wikistring), ByVal indent As Char)

            For Each item As Object In list
                If TypeOf item Is ArrayList Then
                    WikitextList(CType(item, ArrayList), level + 1, result, indent)
                Else
                    result.Add(New Wikistring(New String(indent, level) & Data.AsWikitext(item).ToString))
                End If
            Next item
        End Sub

    End Module

    Public Class Wikistring

        Private _Value As String

        Public Sub New(ByVal value As String)
            _Value = value
        End Sub

        Public ReadOnly Property Value() As String
            Get
                Return _Value
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return _Value
        End Function

    End Class
End Namespace