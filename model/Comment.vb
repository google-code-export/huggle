Imports System.Collections.Generic

Namespace Huggle

    Public Class Comment

        'Represents a comment created by MediaWiki's LiquidThreads extension
        'The extension calls it a "thread", it's what a bulletin board or forum would call a "post"
        'because in those contexts a "thread" is something completely different, which is a bit confusing

        Private ReadOnly _Id As Integer
        Private ReadOnly _Wiki As Wiki

        Public Sub New(ByVal Wiki As Wiki, ByVal Id As Integer)
            _Id = Id
            _Wiki = Wiki
        End Sub

        Public Property Author() As User

        Public Property ContentPageID() As Integer = -1

        Public Property Created() As Date

        Public ReadOnly Property HasSummary() As Boolean
            Get
                Return (_SummaryPageID > 0)
            End Get
        End Property

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public Property Modified() As Date

        Public Property Page() As Page

        Public Property Parent() As Comment

        Public Property Prev() As Comment

        Public Property SummaryPageID() As Integer = -1

        Public Property Title() As String

        Public Property Type() As Integer

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Public Class CommentCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, Comment)

        Public Sub New(ByVal Wiki As Wiki)
            Me.Wiki = Wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Integer, Comment)
            Get
                Return _All
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Id As Integer) As Comment
            Get
                If Not All.ContainsKey(Id) Then All.Add(Id, New Comment(Wiki, Id))
                Return All(Id)
            End Get
        End Property

    End Class

End Namespace
