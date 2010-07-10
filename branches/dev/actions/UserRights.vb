Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Change user rights

    Public Class UserRights : Inherits Query

        Private _Comment As String
        Private _Target As User

        Public Sub New(ByVal session As Session, ByVal target As User, ByVal comment As String)
            MyBase.New(session, Msg("userrights-desc", target.Name))

            If target Is Nothing Then Throw New ArgumentNullException("target")

            _Comment = comment
            _Target = target
        End Sub

        Public Property AddGroups As List(Of UserGroup)

        Public ReadOnly Property Comment As String
            Get
                Return _Comment
            End Get
        End Property

        Public Property RemoveGroups As List(Of UserGroup)

        Public ReadOnly Property Target() As User
            Get
                Return _Target
            End Get
        End Property

        Public Property Watch As WatchAction

        Public Overrides Sub Start()
            OnProgress(Msg("userrights-progress", User))

            'Get token
            If Not Session.RightsTokens.ContainsKey(Target) Then
                Dim tokenReq As New ApiRequest(Session, Description, New QueryString( _
                    "action", "query", _
                    "list", "users", _
                    "ususers", Target.Name, _
                    "ustoken", "userrights"))

                If tokenReq.IsFailed Then OnFail(tokenReq.Result) : Return
                If Not Session.RightsTokens.ContainsKey(User) Then OnFail(Msg("userrights-notoken", Target)) : Return
            End If

            'Create query string
            Dim query As New QueryString( _
                "action", "userrights", _
                "reason", Comment, _
                "token", Session.RightsTokens(Target))

            If AddGroups IsNot Nothing Then query.Add("add", AddGroups.Join("|"))
            If RemoveGroups IsNot Nothing Then query.Add("remove", RemoveGroups.Join("|"))

            'Change user's rights
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Watch user's userpage
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, Target.Userpage, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
