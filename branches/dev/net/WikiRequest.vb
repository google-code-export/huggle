Imports System
Imports System.Text

Namespace Huggle

    Friend MustInherit Class WikiRequest : Inherits Request

        Private _Response As String
        Private ReadOnly _Session As Session

        Public Sub New(ByVal session As Session)
            If session Is Nothing Then Throw New ArgumentNullException("session")
            _Session = session
        End Sub

        Public Shadows ReadOnly Property Response As String
            Get
                Return _Response
            End Get
        End Property

        Public ReadOnly Property Session As Session
            Get
                Return _Session
            End Get
        End Property

        Public ReadOnly Property User() As User
            Get
                Return Session.User
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return Session.User.Wiki
            End Get
        End Property

        Public Overrides Sub Start()
            MyBase.Start()
            If IsCancelled Then Result = New Result(Msg("error-cancelled"))

            _Response = Encoding.UTF8.GetString(MyBase.Response.ToArray)
            MyBase.Response.Dispose()

            If Result.IsError Then
                'When the URL is at fault, make it clear which wiki the URL is for
                Select Case Result.Code
                    Case "badurl" : Result = New Result(Msg("error-wikibadurl", Wiki))
                    Case "httperror" : Result.Append(Msg("error-wikinotfound", Wiki, Wiki.Url))
                End Select

                Return
            End If

            'Check page title for error page
            'API requests are checked too in case one returns an HTML error page (can happen)

            Dim title As String = Response.FromFirst("<title>").ToFirst("</title>")

            If title IsNot Nothing Then
                If title = "Wikimedia Error" Then
                    Result = New Result(Msg("error-internal", "tsod"), "wikierror")

                ElseIf title = "Database error" Then
                    'Match the equivalent API error
                    Result = New Result(Msg("error-internal", "DB query error"), "internal-dbqueryerror")

                ElseIf title.Contains("has a problem") _
                    AndAlso Response.Contains("Try waiting a few minutes and reloading") Then

                    Result = New Result(Msg("error-internal", "wikiproblem"), "wikierror")
                End If
            End If
        End Sub

    End Class

End Namespace