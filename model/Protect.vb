Imports System.Collections.Generic

Namespace Huggle.Actions

    'Proptect a page

    Public Class Protect : Inherits Query

        Private _Cascade As Boolean
        Private _Levels As New Dictionary(Of String, ProtectionPart)
        Private _Page As Page
        Private _Summary As String
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal Page As Page, ByVal Summary As String)
            MyBase.New(session, Msg("protect-desc", Page))

            _Page = Page
            _Summary = Summary
        End Sub

        Public Property Cascade() As Boolean
            Get
                Return _Cascade
            End Get
            Set(ByVal value As Boolean)
                _Cascade = value
            End Set
        End Property

        Public ReadOnly Property Levels() As Dictionary(Of String, ProtectionPart)
            Get
                Return _Levels
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Summary() As String
            Get
                Return _Summary
            End Get
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
            OnProgress(Msg("protect-progress", Page))
            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim level As New List(Of String)
            Dim expiry As New List(Of String)

            For Each part As KeyValuePair(Of String, ProtectionPart) In Levels
                level.Add(part.Key & "=" & part.Value.Level)
                expiry.Add(WikiTimestamp(part.Value.Expires))
            Next part

            Dim query As New QueryString( _
                "action", "protect", _
                "title", Page, _
                "reason", Summary, _
                "protections", level.Join("|"), _
                "expiry", expiry.Join("|"), _
                "token", Session.EditToken)

            If Cascade Then query.Add("cascade")

            'Protect the page
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Watch the page
            If Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch Then
                Dim watchQuery As New Watch(Session, Page, Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
