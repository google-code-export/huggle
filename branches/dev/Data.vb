Imports Huggle.Scripting
Imports Huggle.Wikitext
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Web.HttpUtility

Namespace Huggle

    Public Module Data

        Public Function AsString(ByVal item As Object) As String
            If item Is Nothing Then Return "null"

            If TypeOf item Is Token Then
                Return Data.AsString(CType(item, Token).Value)

            ElseIf TypeOf item Is ArrayList Then
                Dim result As New List(Of String)

                For Each listItem As Object In CType(item, ArrayList)
                    result.Add(Data.AsSubitem(listItem))
                Next listItem

                Return "[" & result.Join(", ") & "]"

            ElseIf TypeOf item Is ScriptTable Then
                Dim result As New StringBuilder("[" & CRLF)

                For Each row As ScriptTableRow In CType(item, ScriptTable).Rows
                    result.Append("    " & "[")

                    Dim cells As New List(Of String)

                    For Each cell As Object In row.Items
                        cells.Add(Data.AsSubitem(cell))
                    Next cell

                    result.Append(cells.Join(",") & "]" & CRLF)
                Next row

                result.Append("]")
                Return result.ToString

            ElseIf TypeOf item Is Result Then
                Return Nothing

            ElseIf TypeOf item Is Revision Then
                Return "Revision " & CStr(CType(item, Revision).Id)
            End If

            Return item.ToString
        End Function

        Public Function AsChart(ByVal item As Object) As Image
            If item Is Nothing Then Return Nothing

            Return Nothing
        End Function

        Public Function AsHtml(ByVal item As Object) As Html
            If item Is Nothing Then Return New Html("")

            If TypeOf item Is Html Then
                Return CType(item, Html)

            ElseIf TypeOf item Is Token Then
                Return Data.AsHtml(CType(item, Token).Value)

            ElseIf TypeOf item Is Result Then
                Return Nothing

            ElseIf TypeOf item Is ArrayList Then
                Dim result As New StringBuilder("<ul>" & CRLF)

                For Each listItem As Object In CType(item, ArrayList)
                    result.Append("<li>")
                    result.Append(Data.AsHtml(listItem).ToString)
                    result.Append("</li>" & CRLF)
                Next listItem

                result.Append("</ul>")
                Return New Html(result.ToString)

            ElseIf TypeOf item Is ScriptTable Then
                Return New Html(Table.FromScriptTable(CType(item, ScriptTable)).ToHtml)

            ElseIf TypeOf item Is Revision Then
                Dim rev As Revision = CType(item, Revision)
                Return New Html("<a href=""" & rev.Wiki.Url.ToString & "?oldid=" & CStr(rev.Id) & """>Revision " & CStr(rev.Id) & "</a>")
            End If

            Return New Html(HtmlEncode(Data.AsString(item)))
        End Function

        Public Function AsImage(ByVal item As Object) As Image
            If item Is Nothing Then Return Nothing

            Return Nothing
        End Function

        Public Function AsList(ByVal item As Object) As List(Of String)
            If item Is Nothing Then Return Nothing

            If TypeOf item Is ArrayList Then
                Dim result As New List(Of String)

                For Each listItem As Object In CType(item, ArrayList)
                    result.Add(Data.AsSubitem(listItem))
                Next listItem

                Return result

            ElseIf TypeOf item Is Token Then
                Return Data.AsList(CType(item, Token).Value)
            End If

            Return Nothing
        End Function

        Public Function AsSubitem(ByVal item As Object) As String
            If item Is Nothing Then Return "null"

            If TypeOf item Is Image Then
                Return "[Image]"

            ElseIf TypeOf item Is Token Then
                Return Data.AsSubitem(CType(item, Token).Value)
            End If

            Return Data.AsString(item)
        End Function

        Public Function AsTable(ByVal item As Object) As ScriptTable
            If item Is Nothing Then Return Nothing

            If TypeOf item Is ScriptTable Then
                Return CType(item, ScriptTable)

            ElseIf TypeOf item Is Token Then
                Return Data.AsTable(CType(item, Token).Value)
            End If

            Return Nothing
        End Function

        Public Function AsWikitext(ByVal item As Object) As Wikistring
            If item Is Nothing Then Return New Wikistring("")

            If TypeOf item Is Wikistring Then
                Return CType(item, Wikistring)

            ElseIf TypeOf item Is Token Then
                Return Data.AsWikitext(CType(item, Token).Value)

            ElseIf TypeOf item Is Result Then
                Return Nothing

            ElseIf TypeOf item Is ArrayList Then
                Return WikitextUnorderedList(CType(item, ArrayList))

            ElseIf TypeOf item Is ScriptTable Then
                Return New Wikistring(Table.FromScriptTable(CType(item, ScriptTable)).ToWikitext)

            ElseIf TypeOf item Is Revision Then
                Dim rev As Revision = CType(item, Revision)

                Return New Wikistring("[" & rev.Wiki.Url.ToString & "?oldid=" & CStr(rev.Id) & " Revision " & CStr(rev.Id) & "]")
            End If

            Return New Wikistring(EscapeWikitext(item.ToString))
        End Function

    End Module

    Public Class Html

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
