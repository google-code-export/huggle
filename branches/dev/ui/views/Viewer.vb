Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class Viewer : Inherits UserControl

        Friend Sub New()
        End Sub

        Friend Sub New(ByVal session As Session)
            Me.Session = session
            Dock = DockStyle.Fill
        End Sub

        Friend Property Session As Session

        Friend ReadOnly Property User As User
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User
            End Get
        End Property

        Friend ReadOnly Property Wiki As Wiki
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.Wiki
            End Get
        End Property

    End Class

End Namespace