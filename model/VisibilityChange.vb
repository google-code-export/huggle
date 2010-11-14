Imports System.Collections.Generic

Namespace Huggle

    'Represents the change in visibility of a log item

    Friend Class LogVisibilityChange : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property LogItemID As Integer

        Public Property PrevState As VisibilityState

        Public Property State As VisibilityState

        Public Overrides ReadOnly Property Target() As String
            Get
                Return LogItemID.ToStringI
            End Get
        End Property

    End Class

    'Represents the change in visibility of a revision

    Friend Class RevisionVisibilityChange : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property PrevState As VisibilityState

        Public Property Revisions As New List(Of Revision)

        Public Property State As VisibilityState

        Public Overrides ReadOnly Property Target() As String
            Get
                Return Nothing
            End Get
        End Property

    End Class

    Friend Structure VisibilityState

        Private _Author As Boolean
        Private _Comment As Boolean
        Private _Content As Boolean

        Public Sub New(ByVal value As Integer)
            _Author = (value Mod 4 = 4)
            _Comment = (value Mod 2 = 2)
            _Content = (value Mod 1 = 1)
        End Sub

        Public ReadOnly Property Author As Boolean
            Get
                Return _Author
            End Get
        End Property

        Public ReadOnly Property Comment As Boolean
            Get
                Return _Comment
            End Get
        End Property

        Public ReadOnly Property Content As Boolean
            Get
                Return _Content
            End Get
        End Property

    End Structure

End Namespace
