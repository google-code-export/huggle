Imports System

Namespace Huggle

    'Represents a user account creation

    Friend Class UserCreation : Inherits LogItem

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Property IsAutomatic() As Boolean

        Public Overrides ReadOnly Property Icon() As Drawing.Image
            Get
                Return Resources.blob_log_newuser
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return TargetUser.Name
            End Get
        End Property

        Public Property UserID As Integer

        Protected Overrides Sub OnSetTargetUser()
            TargetUser.Id = UserID
        End Sub

    End Class

End Namespace