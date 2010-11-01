Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Category}")> _
    Public Class CategoryLink

        Private Document As Document

        Private _Category As Category
        Private _Selection As Selection
        Private _Sortkey As String

        Public Sub New(ByVal Document As Document, ByVal Category As Category, _
            ByVal Selection As Selection, ByVal Sortkey As String)

            Me.Document = Document
            _Category = Category
            _Selection = Selection
            _Sortkey = Sortkey
        End Sub

        Public ReadOnly Property Category() As Category
            Get
                Return _Category
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public ReadOnly Property Sortkey() As String
            Get
                Return _Sortkey
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "[[" & Category.Page.Title & If(Sortkey Is Nothing, "", "|" & Sortkey) & "]]"
        End Function

    End Class

    Public Class CategoryLinkCollection

        Private Document As Document
        Private Items As New List(Of CategoryLink)

        Public Sub New(ByVal document As Document)
            Me.Document = document
            Parse()
        End Sub

        Public ReadOnly Property All() As IList(Of CategoryLink)
            Get
                Return Items.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal category As Category) As Boolean
            Get
                Return (Item(category) IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Items.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal category As Category) As CategoryLink
            Get
                For Each link As CategoryLink In Items
                    If link.Category Is category Then Return link
                Next link

                Return Nothing
            End Get
        End Property

        Public Sub Add(ByVal category As Category, Optional ByVal sortkey As String = Nothing)

            'Don't add if already present
            If Contains(category) Then Return

            If Items.Count = 0 Then
                'Insert at end of page
                Document.Text &= LF & category.ToString
            Else
                'Insert after existing categories
                Document.Text.Insert(Items(Items.Count - 1).Selection.End, LF & category.ToString)
            End If
        End Sub

        Public Sub Remove(ByVal category As Category)

        End Sub

        Public Sub Parse()
            For Each Match As Match In Regex.Matches _
                (Document.ParseableText, Parsing.BaseCatPattern(Document.Wiki), RegexOptions.Compiled)

                Items.Add(New CategoryLink(Document, Document.Wiki.Categories(Match.Groups(1).Value), _
                     New Selection(Match.Index, Match.Length), Match.Groups(2).Value))
            Next Match
        End Sub

    End Class

End Namespace
