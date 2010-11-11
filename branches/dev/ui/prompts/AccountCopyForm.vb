Imports System

Namespace Huggle.UI

    Friend Class AccountCopyForm : Inherits HuggleForm

        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            If session Is Nothing Then Throw New ArgumentNullException("session")
            Me.Session = session

            For Each user As User In session.Wiki.Users.All
                If user IsNot session.User AndAlso user.IsUsed Then Source.Items.Add(user)
            Next user
        End Sub

        Public ReadOnly Property Result() As User
            Get
                If UseDefault.Checked Then Return Nothing
                Return CType(Source.SelectedItem, User)
            End Get
        End Property

    End Class

End Namespace