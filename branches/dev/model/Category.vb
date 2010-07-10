Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Category

        Private ReadOnly _Page As Page

        Public Sub New(ByVal Page As Page)
            _Page = Page
        End Sub

        Public Property Count() As Integer = -1

        Public Property IsHidden() As Boolean

        Public Property Items() As List(Of Page)

        Public ReadOnly Property Name() As String
            Get
                Return _Page.Name
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public Property SubcatCount() As Integer = -1

        Public Property Subcats() As List(Of Category)

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Page.Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Public Class CategoryCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Page, Category)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Page, Category)
            Get
                Return _All
            End Get
        End Property

        Public Function FromString(ByVal Name As String) As Category
            Name = Wiki.Pages.SanitizeTitle(Name)
            If Name Is Nothing Then Return Nothing
            Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.Category, Wiki.Pages(Name).Name))
        End Function

        Default Public ReadOnly Property Item(ByVal name As String) As Category
            Get
                Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.Category, name))
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal page As Page) As Category
            Get
                If page Is Nothing OrElse page.Space IsNot Wiki.Spaces.Category Then Return Nothing
                If Not All.ContainsKey(page) Then All.Add(page, New Category(page))
                Return All(page)
            End Get
        End Property

    End Class

End Namespace
