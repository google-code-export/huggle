﻿Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Namespace Huggle.Actions

    Public Class AddWiki : Inherits Process

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
            Me.Username = Username
            Me.Password = Password
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

            Dim testReq As New FileRequest(Nothing, Url)
            testReq.Start()
            If testReq.IsFailed Then OnFail(Msg("addwiki-connection")) : Return

            Dim response As String = Encoding.UTF8.GetString(testReq.File.ToArray)

            If Not response.Contains("wgScriptPath=""") Then OnFail(Msg("addwiki-badurl")) : Return

            Dim vars As New Dictionary(Of String, String)

            For Each var As String In response.FromFirst("<script").FromFirst(">").ToFirst("</script>").Split(LF)
                If var.Contains("=") Then vars.Add(var.ToFirst("=").Trim, var.FromFirst("=").Trim(","c, """"c))
            Next var

            Dim siteUrl As New Uri(vars("wgServer") & vars("wgScriptPath"))

            For Each wiki As Wiki In App.Wikis.All
                If wiki.Url = siteUrl Then OnFail(Msg("addwiki-alreadyadded", wiki.Name)) : Return
            Next wiki

            _Wiki = App.Wikis(siteUrl.ToString.Remove("http://").TrimEnd(CChar("/")))
            Wiki.IsCustom = True
            Wiki.Url = siteUrl

            If User Is Nothing Then
                _User = Wiki.Users.Anonymous
            Else
                _User = Wiki.Users.FromString(Username)

                If User Is Nothing Then OnFail(Msg("usernamecheck-invalid"))
                User.Password = Scramble(Password, Hash(User))

                Dim login As New Login(User.Session, "addwiki")
                login.Start()
                If login.IsFailed Then OnFail(login.Message)
            End If

            Dim apiReq As New ApiRequest(User.Session, Msg("addwiki-desc"),
                New QueryString("action", "query", "meta", "siteinfo"))

            apiReq.Start()
            If apiReq.Result.IsError Then OnFail(testReq.Result.Message) : Return

            OnSuccess()
        End Sub

        Private Sub _Fail() Handles Me.Fail
            'Don't leave a non-working entry in the wiki list
            If Wiki IsNot Nothing Then App.Wikis.Remove(Wiki)
        End Sub

    End Class

End Namespace