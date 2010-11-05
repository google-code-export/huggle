Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Title}")>
    Friend Class Section

        'Represents a wikitext document section

        Private Document As Document

        Private _Level As Integer
        Private _Selection As Selection
        Private _Title As String

        Friend Sub New(ByVal document As Document, ByVal title As String,
            ByVal level As Integer, ByVal selection As Selection)

            Me.Document = document
            _Title = title
            _Level = level
            _Selection = selection
        End Sub

        Friend ReadOnly Property Level() As Integer
            Get
                Return _Level
            End Get
        End Property

        Friend ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Friend Property Text() As String
            Get
                Return Document.Text.Substring(Selection.Start, Selection.Length)
            End Get
            Set(ByVal value As String)
                Document.Text = Document.Text.Remove(Selection.Start, Selection.Length)
                Document.Text = Document.Text.Insert(Selection.Start, value)
                _Selection = New Selection(Selection.Start, value.Length)
            End Set
        End Property

        Friend ReadOnly Property Title() As String
            Get
                Return _Title
            End Get
        End Property

        Friend Sub Append(ByVal text As String)
            Document.Text = Document.Text.Insert(Selection.Start + Selection.Length, text)
        End Sub

        Friend Sub Prepend(ByVal text As String)
            Document.Text = Document.Text.Insert(Selection.Start + Me.Text.IndexOfI(LF) + 1, text)
        End Sub

    End Class

    Friend Class SectionCollection

        'Contains methods for working with sections

        Private Document As Document
        Private Items As New List(Of Section)

        Friend Sub New(ByVal document As Document)
            Me.Document = document

            Dim matches As MatchCollection = Parsing.SectionPattern.Matches(document.Text)

            'Text before any actual sections is "section 0"
            Items.Add(New Section(document, Nothing, 0,
                New Selection(0, If(matches.Count = 0, document.Length, matches(0).Index))))

            For i As Integer = 0 To matches.Count - 2
                Items.Add(New Section(document, matches(i).Groups(1).Value, matches(i).Groups(2).Value.Length - 1,
                    New Selection(matches(i).Index, matches(i + 1).Index - matches(i).Index)))
            Next i

            'Treat last section as extending to the end of the page
            'even though in articles it often semantically doesn't (categories, interwikis etc.)
            If matches.Count > 0 Then
                Dim LastMatch As Match = matches(matches.Count - 1)
                Items.Add(New Section(document, LastMatch.Groups(1).Value, LastMatch.Groups(2).Value.Length - 1,
                    New Selection(LastMatch.Index, document.Length - LastMatch.Index)))
            End If
        End Sub

        Friend ReadOnly Property All() As IList(Of Section)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Friend ReadOnly Property Contains(ByVal title As String) As Boolean
            Get
                Return (Item(title) IsNot Nothing)
            End Get
        End Property

        Friend ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal title As String) As Section
            Get
                For Each section As Section In All
                    If section.Title = title Then Return section
                Next section

                Return Nothing
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal index As Integer) As Section
            Get
                If All.Count > index Then Return All(index) Else Return Nothing
            End Get
        End Property

        Friend Sub Append(ByVal title As String, ByVal text As String, Optional ByVal level As Integer = 1)
            Insert(title, text, -1, level)
        End Sub

        Friend Sub Insert(ByVal title As String, ByVal text As String,
            ByVal index As Integer, Optional ByVal level As Integer = 1)

            Dim HeaderMarkup As String = (New StringBuilder).Append("=", 0, level + 1).ToString
            Dim SectionString As String = HeaderMarkup & " " & title & " " & HeaderMarkup & LF & LF & text

            If index = -1 OrElse index >= All.Count Then
                text &= LF & LF & SectionString
            Else
                text = text.Insert(All(index).Selection.Start, SectionString & LF & LF)
            End If
        End Sub

        Friend Sub Remove(ByVal index As Integer)
            Document.Text = Document.Text.Remove(All(index).Selection.Start, All(index).Selection.Length)
        End Sub

    End Class

End Namespace