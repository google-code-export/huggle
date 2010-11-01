Imports Huggle.UI
Imports Huggle.Queries
Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Windows.Forms

Namespace Huggle.Actions

    Public Class Login : Inherits Query

        Private Anonymous As Boolean
        Private Requester As String
        Private Response As LoginResponse
        Private SendingToken As Boolean

        Private Shared ReadOnly KnownErrors As String() =
            {"emptypass", "illegal", "noname", "notexists", "throttled", "wrongpass", "wrongtoken"}

        Public Sub New(ByVal session As Session, ByVal requester As String)
            MyBase.New(session, Msg("login-desc"))
            Anonymous = session.User.IsAnonymous
            Me.Requester = requester
        End Sub

        Public Sub New(ByVal wiki As Wiki, ByVal requester As String)
            MyBase.New(App.Sessions(wiki.Users.Anonymous), Msg("login-desc"))
            Me.Requester = requester
        End Sub

        Public Overrides Sub Start()
            If Session.IsActive Then OnSuccess() : Return

            'Let global config finish preloading. If it wasn't preloading, load it.
            If Not Config.Global.IsLoaded Then Config.Global.Loader.Start()

            If Config.Global.Loader.IsRunning Then
                OnProgress(Msg("config-progress"))
                App.WaitFor(Function() Config.Global.Loader.IsComplete)
            End If

            If Config.Global.Loader.IsFailed Then OnFail(Config.Global.Loader.Result)

            'Connect to recent changes feed
            If Wiki.Family IsNot Nothing AndAlso Wiki.Family.Feed IsNot Nothing _
                AndAlso Not Wiki.Family.Feed.ConnectionAttempted Then Wiki.Family.Feed.Connect()

            If Session.User.IsAnonymous AndAlso Not Anonymous Then
                If Not Interactive Then OnFail(Msg("login-noaccount", Wiki)) : Return

                'Prompt the user to select an account to use
                Dim form As New AccountSelectForm(Requester, Wiki)
                If form.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return

                Session = App.Sessions(form.User)
            End If

            'Load cached config
            If Not Wiki.Config.IsLoaded Then Wiki.Config.LoadLocal()
            If Not User.Config.IsLoaded Then User.Config.LoadLocal()

            'Automatically select a unified account where possible
            If Not Session.User.IsAnonymous AndAlso Session.User.GlobalUser IsNot Nothing _
                AndAlso Session.User.GlobalUser.IsActive Then

                'Prompt the user for permission to use a unified account
                If Not Interactive AndAlso Not User.GlobalUser.Config.AutoUnifiedLogin Then
                    Dim form As New AccountAutoUnifiedForm(User, Requester)
                    If form.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return
                End If

                Session.Cookies.Add(Session.User.GlobalUser.Cookies)
                Session.IsActive = True

            ElseIf Not Session.User.IsAnonymous AndAlso Session.User.Password Is Nothing Then
                'Prompt for account password
                Dim form As New AccountLoginForm(User, Requester)
                If form.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return
            End If

            CreateThread(AddressOf Process, AddressOf SaveConfig)
        End Sub

        Private Sub Process()
            If Not User.IsAnonymous AndAlso Not Session.IsActive Then DoLogin()
            If IsFailed Then Return

            If User.IsUnified AndAlso Not User.GlobalUser.Config.IsLoaded Then User.GlobalUser.Config.LoadLocal()

            'Load config from wiki if necessary
            If Not Wiki.Config.IsCurrent OrElse Not User.Config.IsCurrent Then
                OnProgress(Msg("userconfig-progress", Session.User.FullName))
                Dim process As New LoadUserConfig(Session)
                process.Start()
                If process.IsFailed Then OnFail(process.Result.Inner) : Return
            End If

            'Check if wiki is in read-only mode
            If Wiki.Config.ReadOnly Then OnFail({Msg("error-readonly", Wiki), Wiki.Config.ReadOnlyReason}) : Return
        End Sub

        Private Sub DoLogin()
            OnProgress(Msg("login-progress", User.FullName))

            'Construct query
            Dim req As New ApiRequest(Session, Description, New QueryString(
                "action", "login",
                "lgname", User.Name,
                "lgpassword", Unscramble(User.FullName, User.Password, Hash(User))))

            If Response IsNot Nothing Then req.Query.Add("lgtoken", Response.Token)

            'Log in
            req.Start()
            If req.Result.IsError OrElse req.LoginResponse Is Nothing Then OnFail(req.Result) : Return
            Response = req.LoginResponse

            'Handle login errors
            Select Case Response.Result
                Case "success"
                    Session.IsActive = True

                Case "needtoken"
                    If SendingToken Then
                        'Our token was not recieved correctly
                        OnFail(Msg("login-error-tokenrejected"))

                    ElseIf Response.Token Is Nothing Then
                        'Need token, but they didn't give us one
                        OnFail(Msg("login-error-notoken"))
                    Else
                        'Try again, send the token this time
                        SendingToken = True
                        DoLogin() : Return
                    End If

                Case "throttled"
                    OnFail(Msg("login-error-throttled", FuzzyTime(Response.Wait)))

                Case Else : If KnownErrors.Contains(Response.Result) _
                    Then OnFail(Msg("login-error-" & Response.Result)) _
                    Else OnFail(Msg("login-error-unknown", Response.Result))
            End Select

            If IsFailed Then Return
            
            Log.Debug("Logged in {0}".FormatWith(User.FullName))

            'Check cookies for unified account
            If Wiki.Family IsNot Nothing Then
                For Each cookie As Cookie In Session.Cookies.GetCookies(Wiki.Url)
                    If cookie.Name.StartsWith("centralauth") Then
                        Dim globalUser As GlobalUser = Wiki.Family.GlobalUsers(User.Name)
                        User.GlobalUser = globalUser

                        If Not globalUser.Config.IsLoaded Then globalUser.Config.LoadLocal()
                        globalUser.Cookies.Add(cookie)
                        globalUser.IsActive = True
                        globalUser.Users.Merge(User)
                        globalUser.Wikis.Merge(User.Wiki)

                        Wiki.Family.ActiveGlobalUser = User.GlobalUser
                        Exit For
                    End If
                Next cookie
            End If
        End Sub

        Private Sub SaveConfig()
            If IsFailed Then Return

            If Not User.IsUsed Then
                User.IsUsed = True

                'Prompt to copy settings across from another account
                If Interactive Then
                    Dim copyable As New List(Of User)

                    For Each otherUser As User In Wiki.Users.All
                        If otherUser.IsUsed AndAlso Not otherUser.IsAnonymous AndAlso otherUser IsNot User _
                            Then copyable.Add(otherUser)
                    Next otherUser

                    If copyable.Count > 0 Then
                        Dim form As New AccountCopyForm(User)
                        form.ShowDialog()
                        If form.Result IsNot Nothing Then User.Config = form.Result.Config.Copy(User)
                    End If
                End If
            End If

            User.IsLoaded = True
            Wiki.IsLoaded = True
            Config.Local.SaveLocal()
            User.Config.SaveLocal()
            Wiki.Config.SaveLocal()
            Wiki.Rc.Enabled = True
            Wiki.Rc.ForceUpdate()
            If Wiki.Family IsNot Nothing AndAlso Wiki.Family.Feed IsNot Nothing Then Wiki.Family.Feed.AddWiki(Wiki)

            OnSuccess()
        End Sub

        Private Sub _Fail() Handles Me.Fail
            Session.IsActive = False
        End Sub

    End Class

End Namespace
