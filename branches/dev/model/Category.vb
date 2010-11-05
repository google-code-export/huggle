Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class Category

        Private ReadOnly _Page As Page

        Friend Sub New(ByVal page As Page)
            _Page = page
        End Sub

        Friend Property Count() As Integer = -1

        Friend Property IsHidden() As Boolean

        Friend Property Items() As List(Of Page)

        Friend ReadOnly Property Name() As String
            Get
                Return _Page.Name
            End Get
        End Property

        Friend ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Friend Property SubcatCount() As Integer = -1

        Friend Property Subcats() As List(Of Category)

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Page.Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Friend Class CategoryCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Page, Category)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As Dictionary(Of Page, Category)
            Get
                Return _All
            End Get
        End Property

        Friend Function FromString(ByVal name As String) As Category
            name = Wiki.Pages.SanitizeTitle(name)
            If name Is Nothing Then Return Nothing
            Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.Category, Wiki.Pages(name).Name))
        End Function

        Default Friend ReadOnly Property Item(ByVal name As String) As Category
            Get
                Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.Category, name))
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal page As Page) As Category
            Get
                If page Is Nothing OrElse page.Space IsNot Wiki.Spaces.Category Then Return Nothing
                If Not All.ContainsKey(page) Then All.Add(page, New Category(page))
                Return All(page)
            End Get
        End Property

    End Class

End Namespace
