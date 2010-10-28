Imports Huggle.UI
Imports System.Windows.Forms

Namespace Huggle.Actions

    Public Class Import : Inherits Process

        Private _Attribution As Boolean
        Private _DestPage As Page
        Private _DestWiki As Wiki
        Private _History As Boolean
        Private _SourcePage As Page
        Private _SourceWiki As Wiki

        Private WithEvents Login As Login

        Public Sub New()
        End Sub

        Public Property Attribution() As Boolean
            Get
                Return _Attribution
            End Get
            Set(ByVal value As Boolean)
                _Attribution = value
            End Set
        End Property

        Public Property DestPage() As Page
            Get
                Return _DestPage
            End Get
            Set(ByVal value As Page)
                _DestPage = value
            End Set
        End Property

        Public Property DestWiki() As Wiki
            Get
                Return _DestWiki
            End Get
            Set(ByVal value As Wiki)
                _DestWiki = value
            End Set
        End Property

        Public Property History() As Boolean
            Get
                Return _History
            End Get
            Set(ByVal value As Boolean)
                _History = value
            End Set
        End Property

        Public Property SourcePage() As Page
            Get
                Return _SourcePage
            End Get
            Set(ByVal value As Page)
                _SourcePage = value
            End Set
        End Property

        Public Property SourceWiki() As Wiki
            Get
                Return _SourceWiki
            End Get
            Set(ByVal value As Wiki)
                _SourceWiki = value
            End Set
        End Property

        Public Overrides Sub Start()
            If SourcePage Is Nothing OrElse DestPage Is Nothing Then
                If Not Interactive Then OnFail(Msg("import-notspecified")) : Return

                Dim form As New ImportForm
                If form.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return

                Attribution = form.Attribution
                DestPage = form.DestPage
                DestWiki = form.DestWiki
                History = form.History
                SourcePage = form.SourcePage
                SourceWiki = form.SourceWiki
            End If

            If Not DestWiki.IsLoaded Then
                Login = New Login(DestWiki, Msg("action-import"))
                Login.Start()
                Return
            End If

            If History Then CreateThread(AddressOf DoCopy) Else CreateThread(AddressOf DoImport)
        End Sub

        Private Sub LoginAction_Done() Handles Login.Complete
            If Login.IsFailed Then OnFail(Login.Result)
            If History Then CreateThread(AddressOf DoCopy) Else CreateThread(AddressOf DoImport)
        End Sub

        Private Sub DoCopy()

        End Sub

        Private Sub DoImport()

        End Sub

    End Class

End Namespace