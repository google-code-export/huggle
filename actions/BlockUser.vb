Imports Huggle.Net
Imports System.Collections.Generic

Namespace Huggle.Queries

    'Handles blocking a user

    Friend Class BlockUser : Inherits Query

        Private _Target As User

        Public Sub New(ByVal session As Session, ByVal target As User)
            MyBase.New(session, Msg("block-desc", target))
            _Target = target
        End Sub

        Public Property AnonOnly() As Boolean

        Public Property AutoBlock() As Boolean

        Public Property BlockCreation() As Boolean

        Public Property BlockEmail() As Boolean

        Public Property BlockTalk() As Boolean

        Public Property Expiry() As String

        Public Property Summary() As String

        Public ReadOnly Property Target() As User
            Get
                Return _Target
            End Get
        End Property

        Public Property Watch() As WatchAction

        Public Overrides Sub Start()
            'Check user permissions
            If User.IsBlocked Then OnFail(Msg("error-blocked")) : Return
            If User.Wiki IsNot Target.Wiki OrElse Not User.HasRight("block") _
                OrElse Not User.Can("block") Then OnFail(Msg("error-account"))

            'Disallow self blocks
            If User Is Target Then OnFail(Msg("block-selfblock")) : Return

            CreateThread(AddressOf GetDetails, AddressOf GetConfirmation)
        End Sub

        Private Sub GetDetails()
            'Fetch details if necessary
            If Not User.ExtendedInfoKnown Then
                'Dim query As New UserDetailQuery(User, User)
                'query.Start()
                'If query.Result.IsError Then OnFail(query.Result.Message) : Return
            End If
        End Sub

        Private Sub GetConfirmation()
            Dim confirmMessages As New List(Of String)

            'Confirm overwriting existing block
            If Target.IsBlocked AndAlso User.Config.ConfirmBlocked Then confirmMessages.Add(Msg("block-blocked"))

            'Confirm block of ignored user
            If Target.IsIgnored AndAlso User.Config.ConfirmBlockIgnored Then confirmMessages.Add(Msg("block-ignored"))

            'Confirm block of privileged user
            If Target.IsPrivileged AndAlso User.Config.ConfirmBlockPrivileged Then confirmMessages.Add(Msg("block-privileged"))

            'Confirm block of scary user
            If Config.Global.ScaryPattern.IsMatch(Target.Name) _
                AndAlso User.Config.ConfirmBlockScary Then confirmMessages.Add(Msg("block-scary"))

            If confirmMessages.Count > 0 Then
                'Show prompt to user
                Select Case App.ShowPrompt(Msg("block-action"), MakeConfirmation(confirmMessages),
                    Nothing, 1, Msg("block-continue"), Msg("cancel"))

                    Case 1 : Exit Select
                    Case 2 : OnFail(Msg("error-cancelled")) : Return
                End Select
            End If

            If Summary Is Nothing Then Summary = User.Wiki.Config.BlockSummary

            BlockCreation = True
            BlockEmail = False
            BlockTalk = False

            If User.IsAnonymous Then
                AnonOnly = True
                AutoBlock = False
                Expiry = User.Wiki.Config.BlockTimeAnon
            Else
                AnonOnly = False
                AutoBlock = (Not User.IsBot)
                Expiry = User.Wiki.Config.BlockTime
            End If

            If Interactive AndAlso User.Config.ConfirmBlock Then
                'TODO: Show block form
            End If

            CreateThread(AddressOf DoBlock)
        End Sub

        Private Sub DoBlock()
            OnStarted()

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            If Summary Is Nothing Then Summary = Wiki.Config.BlockSummary

            'Create query string
            Dim query As New QueryString(
                "action", "block",
                "user", User,
                "reason", Summary,
                "expiry", Expiry,
                "token", Session.Tokens("block"))

            If AnonOnly Then query.Add("anononly")
            If AutoBlock Then query.Add("autoblock")
            If BlockCreation Then query.Add("nocreate")
            If BlockEmail Then query.Add("noemail")
            If Not BlockTalk Then query.Add("allowusertalk")

            'Block user
            Dim request As New ApiRequest(Session, Description, query)
            request.Start()
            If request.IsFailed Then OnFail(request.Result) : Return

            'Watch user's userpage
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, User.Userpage, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
