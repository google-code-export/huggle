Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class Viewer : Inherits UserControl

        Public Sub New()
        End Sub

        Public Sub New(ByVal session As Session)
            Me.Session = session
            Dock = DockStyle.Fill
        End Sub

        Private Sub _Load() Handles Me.Load
            If Not DesignMode Then App.Languages.Current.Localize(Me)
        End Sub

        Public Property Session As Session

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