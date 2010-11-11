Imports Huggle.Actions
Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class WikiAddForm : Inherits HuggleForm

        Private _User As User
        Private _Wiki As Wiki

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
            App.Languages.Current.Localize(Me)
        End Sub

        Private Sub InputChanged() Handles Url.TextChanged, Username.TextChanged, Password.TextChanged
            OK.Enabled = (Url.Text.Length > 0 AndAlso Not (Username.Text.Length > 0 Xor Password.Text.Length > 0))
        End Sub

        Private Sub OK_Click() Handles OK.Click
            Dim urlText As String = Url.Text
            If Not urlText.Contains("://") Then urlText = "http://" & urlText

            Dim newUrl As Uri

            Try
                newUrl = New Uri(urlText)

            Catch ex As UriFormatException
                App.ShowError(New Result({Msg("addwiki-fail"), Msg("error-badurl")}))
                Return
            End Try

            Dim addwiki As AddWiki = If(Username.Text.Length > 0,
                New AddWiki(newUrl, Username.Text, Password.Text), New AddWiki(newUrl))

            App.UserWaitForProcess(addwiki)

            If Not addwiki.IsFailed Then
                _Wiki = addwiki.Wiki
                Config.Global.SaveLocal()
                Close()
            End If
        End Sub

        Private Sub Cancel_Click() Handles Cancel.Click
            Close()
        End Sub

    End Class

End Namespace