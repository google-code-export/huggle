Imports System.Collections.Generic

Namespace Huggle.Actions

    'Purge one or more pages

    Class Purge : Inherits Query

        Private _Pages As List(Of Page)
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal page As Page)
            MyBase.New(session, Msg("purge-desc"))
            _Pages = List(page)
        End Sub

        Public Sub New(ByVal session As Session, ByVal pages As List(Of Page))
            MyBase.New(session, Msg("purge-desc"))
            _Pages = Pages
        End Sub

        Public ReadOnly Property Pages() As List(Of Page)
            Get
                Return _Pages
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
            OnStarted()
            If Pages.Count = 0 Then OnSuccess() : Return
            If Pages.Count = 1 Then OnProgress(Msg("purge-progress", Pages(0))) _
                Else OnProgress(Msg("purge-progress-multi", Pages.Count))

            'Create query string
            Dim query As New QueryString( _
                "action", "purge", _
                "titles", Pages.ToStringArray.Join("|"))

            'Purge the page(s)
            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            'Watch the page, if there is only one
            If Pages.Count = 1 AndAlso (Watch = WatchAction.Watch OrElse Watch = WatchAction.Unwatch) Then
                Dim watchQuery As New Watch(Session, Pages(0), Watch)
                watchQuery.Start()
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace
