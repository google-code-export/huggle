Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Id}")>
    Friend Class Comment

        'Represents a comment created by MediaWiki's LiquidThreads extension
        'The extension calls it a "thread", it's what a bulletin board or forum would call a "post"
        'because in those contexts a "thread" is something completely different, which is a bit confusing

        Private ReadOnly _Id As Integer
        Private ReadOnly _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal id As Integer)
            _Id = id
            _Wiki = wiki
        End Sub

        Friend Property Author() As User

        Friend Property ContentPageID() As Integer = -1

        Friend Property Created() As Date

        Friend ReadOnly Property HasSummary() As Boolean
            Get
                Return (_SummaryPageID > 0)
            End Get
        End Property

        Friend ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Friend Property Modified() As Date

        Friend Property Page() As Page

        Friend Property Parent() As Comment

        Friend Property Prev() As Comment

        Friend Property SummaryPageID() As Integer = -1

        Friend Property Title() As String

        Friend Property Type() As Integer

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Friend Class CommentCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, Comment)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As Dictionary(Of Integer, Comment)
            Get
                Return _All
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal id As Integer) As Comment
            Get
                If Not All.ContainsKey(id) Then All.Add(id, New Comment(Wiki, id))
                Return All(id)
            End Get
        End Property

    End Class

End Namespace
