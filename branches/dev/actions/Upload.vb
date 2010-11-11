Imports System.IO

Namespace Huggle.Actions

    Friend Class Upload : Inherits Query

        Private _File As MemoryStream
        Private _Name As String
        Private _Summary As String
        Private _Watch As WatchAction

        Public Sub New(ByVal session As Session, ByVal file As MemoryStream, ByVal name As String, ByVal summary As String)
            MyBase.New(session, Msg("upload-desc", name))

            _File = file
            _Name = name
            _Summary = summary
        End Sub

        Public ReadOnly Property File() As MemoryStream
            Get
                Return _File
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
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
            OnProgress(Msg("upload-progress", Name))
            OnStarted()

            'Get token
            If Session.EditToken Is Nothing Then
                Dim tokenQuery As New TokenQuery(Session)
                tokenQuery.Start()
                If tokenQuery.IsErrored Then OnFail(tokenQuery.Result) : Return
            End If

            'Create query string
            Dim query As New QueryString( _
                "action", "upload", _
                "comment", Summary, _
                "file", File, _
                "token", Session.EditToken, _
                "watchlist", Watch)

            'Upload file
            Dim req As New ApiRequest(Session, Description, query)
            req.Filename = Name
            req.Start()

            If req.Result.IsError Then OnFail(req.Result.Message) : Return
            OnSuccess()
        End Sub

    End Class

End Namespace
