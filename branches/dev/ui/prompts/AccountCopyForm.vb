Imports System

Namespace Huggle.UI

    Friend Class AccountCopyForm : Inherits HuggleForm

        Private Session As Session

        Public Sub New(ByVal session As Session)
            ThrowNull(session, "session")
            Me.Session = session

            For Each user As User In session.Wiki.Users.Used
                If user IsNot session.User Then Source.Items.Add(user)
            Next user

            InitializeComponent()
        End Sub

        Public ReadOnly Property Result() As User
            Get
                If UseDefault.Checked Then Return Nothing
                Return CType(Source.SelectedItem, User)
            End Get
        End Property

    End Class

End Namespace