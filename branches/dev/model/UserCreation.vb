Imports System

Namespace Huggle

    'Represents a user account creation

    Public Class UserCreation : Inherits LogItem

        Private _Auto As Boolean

        Public Sub New(ByVal auto As Boolean, ByVal time As Date, ByVal user As User, _
            ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, id, rcid)
            Me.Action = "create"
            Me.Time = time
            Me.User = user

            _Auto = auto
        End Sub

        Public ReadOnly Property Auto() As Boolean
            Get
                Return _Auto
            End Get
        End Property

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_newuser
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return User.Name
            End Get
        End Property

    End Class

End Namespace