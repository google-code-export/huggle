Imports System.Collections.Generic

Namespace Huggle

    Friend Class RightsChange : Inherits LogItem

        'Represents a user rights change

        Private _Rights As List(Of String)
        Private _TargetUser As User

        Public Sub New(ByVal time As Date, ByVal user As User, ByVal targetUser As User, _
            ByVal comment As String, ByVal rights As List(Of String), ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = "rights"
            Me.Comment = comment
            Me.Time = time
            Me.User = user

            _Rights = rights
            _TargetUser = user
        End Sub

        Public ReadOnly Property Rights() As List(Of String)
            Get
                Return _Rights
            End Get
        End Property

        Public ReadOnly Property TargetUser() As User
            Get
                Return _TargetUser
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _TargetUser.Name
            End Get
        End Property

    End Class

End Namespace