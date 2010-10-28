Namespace Huggle.UI

    Public Class AccountCopyForm : Inherits HuggleForm

        Private User As User

        Public Sub New(ByVal user As User)
            InitializeComponent()
            Me.User = user

            For Each item As User In user.Wiki.Users.All
                If item IsNot user Then Source.Items.Add(item)
            Next item
        End Sub

        Public ReadOnly Property Result() As User
            Get
                If UseDefault.Checked Then Return Nothing
                Return CType(Source.SelectedItem, User)
            End Get
        End Property

    End Class

End Namespace