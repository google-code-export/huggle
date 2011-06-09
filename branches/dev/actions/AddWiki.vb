Imports Huggle.Net
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle.Queries

    Friend Class AddWiki : Inherits Process

        Private _User As User
        Private _Wiki As Wiki

        Private Password As String
        Private Url As Uri
        Private Username As String

        Public Sub New(ByVal url As Uri)
            Me.New(url, Nothing, Nothing)
        End Sub

        Public Sub New(ByVal url As Uri, ByVal username As String, ByVal password As String)
            Me.Url = url
            Me.Username = username
            Me.Password = password
        End Sub

        Public ReadOnly Property User As User
            Get
                Return _User
            End Get
        End Property

        Public Shadows ReadOnly Property Wiki As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("addwiki-progress"))

            'Test connection to the wiki
            Dim req As New TextRequest(Url)
            req.Start()
            If req.IsFailed Then OnFail(Msg("addwiki-connection")) : Return

            'No single method of determining wiki URL works for all MediaWiki installations
            'These two should work in most cases
            Static urlPattern1 As New Regex("< *script[^>]*>.*wgScriptPath *= *""([^""]*)"".*wgServer *= *""([^""]*)""",
                RegexOptions.Compiled Or RegexOptions.Singleline)
            Static urlPattern2 As New Regex("href=""([^>]+)/api\.php\?action=rsd"" +/>",
                RegexOptions.Compiled Or RegexOptions.Singleline)

            Dim match1 As Match = urlPattern1.Match(req.Response)
            Dim match2 As Match = urlPattern2.Match(req.Response)
            Dim siteUrl As Uri

            If match1.Success Then
                siteUrl = New Uri(match1.Groups(1).Value & match1.Groups(2).Value & "/")

            ElseIf match2.Success Then
                siteUrl = New Uri(match2.Groups(1).Value & "/")

            Else
                'Maybe the entered URL redirects elsewhere via a meta-refresh tag
                'Find the URL redirected to and try again
                Static metaRefreshPattern As New Regex("<meta +http-equiv=""refresh"" +content=""\d+;url=([^""]+)"">",
                    RegexOptions.Compiled Or RegexOptions.Singleline)

                Dim metaRefreshMatch As Match = metaRefreshPattern.Match(req.Response)

                If metaRefreshMatch.Success Then
                    Url = New Uri(metaRefreshMatch.Groups(1).Value)
                    Start()
                    Return
                Else
                    OnFail(Msg("addwiki-badurl")) : Return
                End If
            End If

            'Check if the wiki is already in the list
            For Each wiki As Wiki In App.Wikis.All
                If wiki.Url = siteUrl OrElse wiki.SecureUrl = siteUrl Then
                    If wiki.IsHidden OrElse Not wiki.IsPublicReadable OrElse Not wiki.IsPublicEditable _
                        Then OnFail(Msg("addwiki-privatewmwiki", wiki.Name)) _
                        Else OnFail(Msg("addwiki-alreadyadded", wiki.Name))
                    Return
                End If
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
            If apiReq.Result.IsError Then OnFail(apiReq.Result) : Return

            OnSuccess()
        End Sub

        Private Sub _Fail() Handles Me.Fail
            'Don't leave a non-working entry in the wiki list
            If Wiki IsNot Nothing Then App.Wikis.Remove(Wiki)
        End Sub

    End Class

End Namespace
