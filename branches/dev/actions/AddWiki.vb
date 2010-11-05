Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Actions

    Friend Class AddWiki : Inherits Process

        Private _User As User
        Private _Wiki As Wiki

        Private Password As String
        Private Url As Uri
        Private Username As String

        Private Shared ReadOnly oldUrlRegex As New Regex _
            ("< *script[^>]*>.*wgScriptPath *= *""([^""]*)""", RegexOptions.Compiled Or RegexOptions.Singleline)
        Private Shared ReadOnly newUrlRegex As New Regex _
            ("< *script[^>]*src *= *""([^""]*)/load.php", RegexOptions.Compiled Or RegexOptions.Singleline)

        Friend Sub New(ByVal url As Uri)
            Me.New(url, Nothing, Nothing)
        End Sub

        Friend Sub New(ByVal url As Uri, ByVal username As String, ByVal password As String)
            Me.Url = url
            Me.Username = username
            Me.Password = password
        End Sub

        Friend ReadOnly Property User As User
            Get
                Return _User
            End Get
        End Property

        Friend Shadows ReadOnly Property Wiki As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Friend Overrides Sub Start()
            OnProgress(Msg("addwiki-progress"))

            Dim testReq As New FileRequest(Nothing, Url)
            testReq.Start()
            If testReq.IsFailed Then OnFail(Msg("addwiki-connection")) : Return

            Dim response As String = Encoding.UTF8.GetString(testReq.File.ToArray)
            Dim oldMatch As Match = oldUrlRegex.Match(response)
            Dim newMatch As Match = newUrlRegex.Match(response)
            Dim siteUrl As Uri

            If oldMatch.Success Then
                siteUrl = New Uri(Url.Scheme & "://" & Url.Host & oldMatch.Groups(1).Value & "/")

            ElseIf newMatch.Success Then
                siteUrl = New Uri(Url.Scheme & "://" & Url.Host & newMatch.Groups(1).Value & "/")
            Else
                OnFail(Msg("addwiki-badurl")) : Return
            End If

            'Check if the wiki is already in the list
            For Each wiki As Wiki In App.Wikis.All
                If wiki.Url = siteUrl OrElse wiki.SecureUrl = siteUrl _
                    Then OnFail(Msg("addwiki-alreadyadded", wiki.Name)) : Return
            Next wiki

            _Wiki = App.Wikis(siteUrl.Host & siteUrl.AbsolutePath.TrimEnd("/"c))
            Wiki.IsCustom = True
            Wiki.Url = siteUrl

            If User Is Nothing Then
                _User = Wiki.Users.Anonymous
            Else
                _User = Wiki.Users.FromString(Username)

                If User Is Nothing Then OnFail(Msg("usernamecheck-invalid"))
                User.Password = Scramble(User.FullName, Password, Hash(User))

                Dim login As New Login(App.Sessions(User), "addwiki")
                login.Start()
                If login.IsFailed Then OnFail(login.Message)
            End If

            Dim apiReq As New ApiRequest(App.Sessions(User), Msg("addwiki-desc"),
                New QueryString("action", "query", "meta", "siteinfo"))

            apiReq.Start()
            If apiReq.Result.IsError Then OnFail(apiReq.Result.Message) : Return

            OnSuccess()
        End Sub

        Private Sub _Fail() Handles Me.Fail
            'Don't leave a non-working entry in the wiki list
            If Wiki IsNot Nothing Then App.Wikis.Remove(Wiki)
        End Sub

    End Class

End Namespace
