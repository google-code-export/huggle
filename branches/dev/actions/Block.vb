Imports Huggle.Queries
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Handles blocking a user

    Public Class Block : Inherits Query

        Private _AnonOnly As Boolean
        Private _AutoBlock As Boolean
        Private _BlockEmail As Boolean
        Private _BlockCreation As Boolean
        Private _BlockTalk As Boolean
        Private _Expiry As String
        Private _Summary As String
        Private _Target As User
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal target As User, Optional ByVal summary As String = Nothing)
            MyBase.New(session, Msg("block-desc", target))
            _Summary = summary
            _Target = target
        End Sub

        Public Property AnonOnly() As Boolean
            Get
                Return _AnonOnly
            End Get
            Set(ByVal value As Boolean)
                _AnonOnly = value
            End Set
        End Property

        Public Property AutoBlock() As Boolean
            Get
                Return _AutoBlock
            End Get
            Set(ByVal value As Boolean)
                _AutoBlock = value
            End Set
        End Property

        Public Property BlockCreation() As Boolean
            Get
                Return _BlockCreation
            End Get
            Set(ByVal value As Boolean)
                _BlockCreation = value
            End Set
        End Property

        Public Property BlockEmail() As Boolean
            Get
                Return _BlockEmail
            End Get
            Set(ByVal value As Boolean)
                _BlockEmail = value
            End Set
        End Property

        Public Property BlockTalk() As Boolean
            Get
                Return _BlockTalk
            End Get
            Set(ByVal value As Boolean)
                _BlockTalk = value
            End Set
        End Property

        Public Property Expiry() As String
            Get
                Return _Expiry
            End Get
            Set(ByVal value As String)
                _Expiry = value
            End Set
        End Property

        Public Property Summary() As String
            Get
                Return _Summary
            End Get
            Set(ByVal value As String)
                _Summary = value
            End Set
        End Property

        Public Property Target() As User
            Get
                Return _Target
            End Get
            Set(ByVal value As User)
                _Target = value
            End Set
        End Property

        Public Property Watch() As WatchAction
            Get
                Return _Watch
            End Get
            Set(ByVal value As WatchAction)
                _Watch = value
            End Set
        End Property

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
                Select Case Prompt.Show(Msg("block-action"), MakeConfirmation(confirmMessages), _
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
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            If Summary Is Nothing Then Summary = Wiki.Config.BlockSummary

            'Create query string
            Dim query As New QueryString( _
                "action", "block", _
                "user", User, _
                "reason", Summary, _
                "expiry", Expiry, _
                "token", Session.EditToken)

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
