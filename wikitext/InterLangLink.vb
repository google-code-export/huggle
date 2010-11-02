Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Name}")>
    Public Class InterLangLink

        'Represents an interlanguage link

        Private _Page As Page
        Private _Selection As Selection
        Private _Source As Wiki

        Public Sub New(ByVal source As Wiki, ByVal page As Page, ByVal selection As Selection)
            _Page = page
            _Selection = selection
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

        Public Sub New(ByVal document As Document)
            Me.Document = document

            For Each match As Match In Regex.Matches(document.ParseableText, Parsing.BaseInterlangPattern(document.Wiki))
                Dim page As Page = document.Wiki.Pages.FromString(match.Groups(1).Value)

                If page IsNot Nothing AndAlso page.Wiki IsNot document.Wiki Then
                    Items.Add(New InterLangLink(document.Wiki, page, New Selection(match.Index, match.Length)))
                End If
            Next match
        End Sub

        Public ReadOnly Property All() As IList(Of InterLangLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal item As InterLangLink) As Boolean
            Get
                Return (Items.Contains(item))
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal page As Page) As Boolean
            Get
                For Each Item As InterLangLink In Items
                    If Item.Page Is page Then Return True
                Next Item

                Return False
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return All.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal wiki As Wiki) As InterLangLink
            Get
                For Each interwiki As InterLangLink In Items
                    If interwiki.Destination Is wiki Then Return interwiki
                Next interwiki

                Return Nothing
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As InterLangLink
            Get
                If Items.Count > index Then Return Items(index) Else Return Nothing
            End Get
        End Property

        Public Sub Add(ByVal page As Page)
            'Don't add if already present
            If Contains(page) Then Return

            If Items.Count = 0 Then
                'Insert at end of page
                Document.Text &= LF & Document.InterlanguageLink(page)
            Else
                'Insert after existing links
                Document.Text = Document.Text.Insert(Items(Items.Count - 1).Selection.End, LF & Document.InterlanguageLink(page))
            End If
        End Sub

        Public Sub Remove(ByVal index As Integer)
            Dim sel As Selection = All(index).Selection
            Document.Text = Document.Text.Remove(sel.Start, If(Document.Text(sel.End + 1) = LF, sel.Length + 1, sel.Length))
        End Sub

    End Class



End Namespace