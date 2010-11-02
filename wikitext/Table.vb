Imports Huggle.Scripting
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    Public Class Table

        Private _Columns As New List(Of TableColumn)
        Private _Rows As New List(Of TableRow)
        Private _Selection As Selection

        Public Sub New()
        End Sub

        Public Sub New(ByVal wikitext As String, ByVal selection As String)
        End Sub

        Public Property Caption() As String

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

        Public Function ToHtml() As String
            Dim result As New StringBuilder


            result.Append("<table")
            If Not String.IsNullOrEmpty(Style) Then result.Append(" " & Style)

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

        Public Sub New(Optional ByVal style As String = Nothing)
            _Style = style
        End Sub

        Public ReadOnly Property Cells() As List(Of TableCell)
            Get
                Return _Cells
            End Get
        End Property

        Public Property IsHeader() As Boolean

        Public Property Style() As String

    End Class

    Public Class TableColumn

        Private _Cells As List(Of TableCell)
        Private _IsHeader As Boolean

        Public Sub New(Optional ByVal style As String = Nothing)
            _Style = style
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

        Public Property Style() As String

    End Class

    Public Class TableCell

        Public Sub New(ByVal content As String, Optional ByVal style As String = Nothing)
            _Content = content
            _Style = style
        End Sub

        Public Property Content() As String

        Public Property Style() As String

    End Class

    Public Class TableCollection

        Private Document As Document
        Private Tables As List(Of Table)

        Public Sub New(ByVal document As Document)
            Me.Document = document

            Dim matches As MatchCollection = Parsing.SectionPattern.Matches(document.Text)


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

        Default Public ReadOnly Property Item(ByVal index As Integer) As Table
            Get
                If All.Count > index Then Return All(index) Else Return Nothing
            End Get
        End Property

        Public Sub Append(ByVal Title As String, ByVal text As String, Optional ByVal level As Integer = 1)
            Insert(Title, text, -1, level)
        End Sub

        Public Sub Insert(ByVal title As String, ByVal text As String,
            ByVal index As Integer, Optional ByVal level As Integer = 1)

            Dim headerMarkup As String = (New StringBuilder).Append("=", 0, level + 1).ToString
            Dim sectionString As String = headerMarkup & " " & title & " " & headerMarkup & LF & LF & text

            If index = -1 OrElse index >= All.Count Then
                text &= LF & LF & sectionString
            Else
                text = text.Insert(All(index).Selection.Start, sectionString & LF & LF)
            End If
        End Sub

        Public Sub Remove(ByVal index As Integer)
            Document.Text = Document.Text.Remove(All(index).Selection)
        End Sub

    End Class

End Namespace
