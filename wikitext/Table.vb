Imports Huggle.Scripting
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    Public Class Table

        Private _Caption As String
        Private _Columns As New List(Of TableColumn)
        Private _Rows As New List(Of TableRow)
        Private _Selection As Selection
        Private _Style As String = ""

        Public Sub New()
        End Sub

        Public Sub New(ByVal Wikitext As String, ByVal Selection As String)
        End Sub

        Public Property Caption() As String
            Get
                Return _Caption
            End Get
            Set(ByVal value As String)
                _Caption = value
            End Set
        End Property

        Public ReadOnly Property Columns() As List(Of TableColumn)
            Get
                Return _Columns
            End Get
        End Property

        Public ReadOnly Property Rows() As List(Of TableRow)
            Get
                Return _Rows
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public Property Style() As String
            Get
                Return _Style
            End Get
            Set(ByVal value As String)
                _Style = value
            End Set
        End Property

        Public Function ToHtml() As String
            Dim result As New StringBuilder


            result.Append("<table")
            If Style.Length > 0 Then result.Append(" " & Style)

            If Rows.Count = 0 Then
                result.Append("/>" & LF)
            Else
                For Each row As TableRow In Rows
                    result.Append("<tr")
                    If row.Style.Length > 0 Then result.Append(" " & row.Style)

                    If row.Cells.Count = 0 Then
                        result.Append("/>")
                    Else
                        result.Append(">" & LF)

                        For Each cell As TableCell In row.Cells
                            result.Append("<td")
                            If cell.Style.Length > 0 Then result.Append(" " & cell.Style)

                            If cell.Content Is Nothing Then
                                result.Append("/>")
                            Else
                                result.Append(">")
                                result.Append(cell.Content)
                                result.Append("</td>" & LF)
                            End If
                        Next cell

                        result.Append("</tr>" & LF)
                    End If
                Next row

                result.Append("</table>")
            End If

            Return result.ToString
        End Function

        Public Function ToWikitext() As String
            Dim result As New StringBuilder

            result.Append("{|")
            If Style IsNot Nothing Then result.Append(" " & Style)
            result.Append(LF)

            For Each row As TableRow In Rows
                If row.IsHeader Then result.Append("!-") Else result.Append("|-")
                If row.Style.Length > 0 Then result.Append(" " & row.Style)
                result.Append(LF)

                For Each cell As TableCell In row.Cells
                    result.Append("| ")
                    If cell.Style.Length > 0 Then result.Append(cell.Style & " | ")
                    result.Append(cell.Content & LF)
                Next cell
            Next row

            result.Append("|}")
            Return result.ToString
        End Function

        Public Overrides Function ToString() As String
            Return ToWikitext()
        End Function

        Public Shared Function FromScriptTable(ByVal source As ScriptTable) As Table
            Dim result As New Table
            result.Style = "class=""wikitable"""

            Dim headerRow As New TableRow
            headerRow.IsHeader = True

            For Each column As String In source.Columns
                result.Columns.Add(New TableColumn)
                headerRow.Cells.Add(New TableCell(column))
            Next column

            For Each sourceRow As ScriptTableRow In source.Rows
                Dim resultRow As New TableRow

                For Each sourceCell As Object In sourceRow.Items
                    resultRow.Cells.Add(New TableCell(AsSubitem(sourceCell)))
                Next sourceCell

                result.Rows.Add(resultRow)
            Next sourceRow

            Return result
        End Function

    End Class

    Public Class TableRow

        Private _Cells As New List(Of TableCell)
        Private _IsHeader As Boolean
        Private _Style As String

        Public Sub New(Optional ByVal Style As String = "")
            _Style = Style
        End Sub

        Public ReadOnly Property Cells() As List(Of TableCell)
            Get
                Return _Cells
            End Get
        End Property

        Public Property IsHeader() As Boolean
            Get
                Return _IsHeader
            End Get
            Set(ByVal value As Boolean)
                _IsHeader = value
            End Set
        End Property

        Public Property Style() As String
            Get
                Return _Style
            End Get
            Set(ByVal value As String)
                _Style = value
            End Set
        End Property

    End Class

    Public Class TableColumn

        Private _Cells As List(Of TableCell)
        Private _IsHeader As Boolean
        Private _Style As String

        Public Sub New(Optional ByVal Style As String = "")
            _Style = Style
        End Sub

        Public ReadOnly Property Cells() As List(Of TableCell)
            Get
                Return _Cells
            End Get
        End Property

        Public ReadOnly Property IsHeader() As Boolean
            Get
                Return _IsHeader
            End Get
        End Property

        Public ReadOnly Property Style() As String
            Get
                Return _Style
            End Get
        End Property

    End Class

    Public Class TableCell

        Private _Content As String
        Private _Style As String

        Public Sub New(ByVal content As String, Optional ByVal style As String = "")
            _Content = content
            _Style = style
        End Sub

        Public Property Content() As String
            Get
                Return _Content
            End Get
            Set(ByVal value As String)
                _Content = value
            End Set
        End Property

        Public Property Style() As String
            Get
                Return _Style
            End Get
            Set(ByVal value As String)
                _Style = value
            End Set
        End Property

    End Class

    Public Class TableCollection

        Private Document As Document
        Private Tables As List(Of Table)

        Public Sub New(ByVal Document As Document)
            Me.Document = Document

            Dim Matches As MatchCollection = Parsing.SectionPattern.Matches(Document.Text)

            
        End Sub

        Public ReadOnly Property All() As IList(Of Table)
            Get
                Return Tables.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Table
            Get
                If All.Count > Index Then Return All(Index) Else Return Nothing
            End Get
        End Property

        Public Sub Append(ByVal Title As String, ByVal Text As String, Optional ByVal Level As Integer = 1)
            Insert(Title, Text, -1, Level)
        End Sub

        Public Sub Insert(ByVal Title As String, ByVal Text As String, _
            ByVal Index As Integer, Optional ByVal Level As Integer = 1)

            Dim HeaderMarkup As String = (New StringBuilder).Append("=", 0, Level + 1).ToString
            Dim SectionString As String = HeaderMarkup & " " & Title & " " & HeaderMarkup & LF & LF & Text

            If Index = -1 OrElse Index >= All.Count Then
                Text &= LF & LF & SectionString
            Else
                Text = Text.Insert(All(Index).Selection.Start, SectionString & LF & LF)
            End If
        End Sub

        Public Sub Remove(ByVal Index As Integer)
            Document.Text = Document.Text.Remove(All(Index).Selection)
        End Sub

    End Class

End Namespace
