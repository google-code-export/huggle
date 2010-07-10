Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class InterLangLink

        'Represents an interlanguage link

        Private _Page As Page, _Selection As Selection, _Source As Wiki

        Public Sub New(ByVal Source As Wiki, ByVal Page As Page, ByVal Selection As Selection)
            _Page = Page
            _Selection = Selection
        End Sub

        Public ReadOnly Property Code() As String
            Get
                Return Source.InterwikiFor(Destination)
            End Get
        End Property

        Public ReadOnly Property Destination() As Wiki
            Get
                Return _Page.Wiki
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return Code & ":" & Page.Title
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public ReadOnly Property Source() As Wiki
            Get
                Return _Source
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Public Class InterLangLinkCollection

        Private Document As Document
        Private Items As List(Of InterLangLink)

        Public Sub New(ByVal Document As Document)
            Me.Document = Document

            For Each match As Match In Regex.Matches(Document.ParseableText, Parsing.BaseInterlangPattern(Document.Wiki))
                Dim page As Page = Document.Wiki.Pages.FromString(match.Groups(1).Value)

                If page IsNot Nothing AndAlso page.Wiki IsNot Document.Wiki Then
                    Items.Add(New InterLangLink(Document.Wiki, page, New Selection(match.Index, match.Length)))
                End If
            Next match
        End Sub

        Public ReadOnly Property All() As IList(Of InterLangLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Item As InterLangLink) As Boolean
            Get
                Return (Items.Contains(Item))
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Page As Page) As Boolean
            Get
                For Each Item As InterLangLink In Items
                    If Item.Page Is Page Then Return True
                Next Item

                Return False
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Wiki As Wiki) As InterLangLink
            Get
                For Each interwiki As InterLangLink In Items
                    If interwiki.Destination Is Wiki Then Return interwiki
                Next interwiki

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As InterLangLink
            Get
                If Items.Count > Index Then Return Items(Index) Else Return Nothing
            End Get
        End Property

        Public Sub Add(ByVal Page As Page)
            'Don't add if already present
            If Contains(Page) Then Return

            If Items.Count = 0 Then
                'Insert at end of page
                Document.Text &= LF & Document.InterlanguageLink(Page)
            Else
                'Insert after existing links
                Document.Text.Insert(Items(Items.Count - 1).Selection.End, LF & Document.InterlanguageLink(Page))
            End If
        End Sub

        Public Sub Remove(ByVal Index As Integer)
            Dim sel As Selection = All(Index).Selection
            Document.Text = Document.Text.Remove(sel.Start, If(Document.Text(sel.End + 1) = LF, sel.Length + 1, sel.Length))
        End Sub

    End Class



End Namespace