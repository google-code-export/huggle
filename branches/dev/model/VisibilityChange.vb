Namespace Huggle

    Friend Class VisibilityChange : Inherits LogItem

        Private _item As QueueItem

        Friend Sub New(ByVal user As User, ByVal item As QueueItem, ByVal id As Integer, ByVal rcid As Integer)
            MyBase.New(user.Wiki, id, rcid)

            _item = item
        End Sub

        Friend ReadOnly Property Item() As QueueItem
            Get
                Return _Item
            End Get
        End Property

        Friend Overrides ReadOnly Property Target() As String
            Get
                Return item.Label
            End Get
        End Property

    End Class

End Namespace
