Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class Viewer : Inherits UserControl

        Protected Session As Session

        Friend Sub New()
        End Sub

        Public Sub New(ByVal session As Session)
            Me.Session = session
            Dock = DockStyle.Fill
        End Sub

        Public ReadOnly Property User As User
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.User
            End Get
        End Property

        Public ReadOnly Property Wiki As Wiki
            Get
                If Session Is Nothing Then Return Nothing
                Return Session.Wiki
            End Get
        End Property

    End Class

End Namespace