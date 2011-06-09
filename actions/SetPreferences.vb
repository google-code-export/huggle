Imports Huggle.Net
Imports System.Collections.Generic

Namespace Huggle.Queries

    Friend Class SetPreferences : Inherits Query

        Private _NewPrefs As Preferences

        Public Sub New(ByVal session As Session, ByVal newPrefs As Preferences)
            MyBase.New(session, Msg("setprefs-desc"))
            _NewPrefs = newPrefs
        End Sub

        Public ReadOnly Property NewPrefs() As Preferences
            Get
                Return _NewPrefs
            End Get
        End Property

        Public Overrides Sub Start()
            If User.IsAnonymous Then OnFail(Msg("setprefs-anon")) : Return
            OnStarted()
            OnProgress(Msg("setprefs-progress", User.FullName))

            'Get token
            If Not Session.HasTokens Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsFailed Then OnFail(tokenQuery.Result) : Return
            End If

            Dim query As New QueryString("wpEditToken", Session.Tokens("preferences"))

            For Each item As KeyValuePair(Of String, String) In NewPrefs.ToMwFormat
                query.Add("wp" & item.Key, item.Value)
            Next item

            'Currently MediaWiki API can retrieve preferences but not save them, must use UI
            Dim req As New UIRequest(Session, Description, New QueryString("title", "Special:Preferences"), query)
            req.Start()
            If req.IsFailed Then OnFail(req.Result) : Return

            If Not req.Response.Contains("<div class=""successbox"">") Then OnFail(Msg("error-unknown")) : Return
            User.Preferences = NewPrefs
            OnSuccess()
        End Sub

    End Class

End Namespace
