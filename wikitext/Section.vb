Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Title}")> _
    Public Class Section

        'Represents a wikitext document section

        Private Document As Document
        Private _Title As String, _Level As Integer, _Selection As Selection

        Public Sub New(ByVal Document As Document, ByVal Title As String, ByVal Level As Integer, ByVal Selection As Selection)
            Me.Document = Document
            _Title = Title
            _Level = Level
            _Selection = Selection
        End Sub

        Public ReadOnly Property Level() As Integer
            Get
                Return _Level
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public Property Text() As String
            Get
                Return Document.Text.Substring(Selection.Start, Selection.Length)
            End Get
            Set(ByVal value As String)
                Document.Text = Document.Text.Remove(Selection.Start, Selection.Length)
                Document.Text = Document.Text.Insert(Selection.Start, value)
                Selection.Length = value.Length
            End Set
        End Property

        Public ReadOnly Property Title() As String
            Get
                Return _Title
            End Get
        End Property

        Public Sub Append(ByVal Text As String)
            Document.Text = Document.Text.Insert(Selection.Start + Selection.Length, Text)
        End Sub

        Public Sub Prepend(ByVal Text As String)
            Document.Text = Document.Text.Insert(Selection.Start + Me.Text.IndexOf(LF) + 1, Text)
        End Sub

    End Class

    Public Class SectionCollection

        'Contains methods for working with sections

        Private Document As Document
        Private Items As New List(Of Section)

        Public Sub New(ByVal Document As Document)
            Me.Document = Document

            Dim Matches As MatchCollection = Parsing.SectionPattern.Matches(Document.Text)

            'Text before any actual sections is "section 0"
            Items.Add(New Section(Document, Nothing, 0, _
                New Selection(0, If(Matches.Count = 0, Document.Length, Matches(0).Index))))

            For i As Integer = 0 To Matches.Count - 2
                Items.Add(New Section(Document, Matches(i).Groups(1).Value, Matches(i).Groups(2).Value.Length - 1, _
                    New Selection(Matches(i).Index, Matches(i + 1).Index - Matches(i).Index)))
            Next i

            'Treat last section as extending to the end of the page
            'even though in articles it often semantically doesn't (categories, interwikis etc.)
            If Matches.Count > 0 Then
                Dim LastMatch As Match = Matches(Matches.Count - 1)
                Items.Add(New Section(Document, LastMatch.Groups(1).Value, LastMatch.Groups(2).Value.Length - 1, _
                    New Selection(LastMatch.Index, Document.Length - LastMatch.Index)))
            End If
        End Sub

        Public ReadOnly Property All() As IList(Of Section)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Title As String) As Boolean
            Get
                Return (Item(Title) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Title As String) As Section
            Get
                For Each section As Section In All
                    If section.Title = Title Then Return section
                Next section

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Section
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
            Document.Text = Document.Text.Remove(All(Index).Selection.Start, All(Index).Selection.Length)
        End Sub

    End Class

End Namespace