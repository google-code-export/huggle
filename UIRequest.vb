Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports System.Xml

Namespace Huggle

    'Represents a request made through MediaWiki's UI (index.php)
    'Used only when there is no adequate API equivalent

    Class UIRequest : Inherits Request

        Private _Query As QueryString
        Private _Response As String

        Public Sub New(ByVal session As Session, ByVal description As String, _
            ByVal query As QueryString, ByVal data As QueryString)

            MyBase.New(session)
            _Query = query
            _Query.Add("uselang", "en")
            _Query.Add("useskin", "myskin")

            MyBase.Description = description
            If data IsNot Nothing Then Me.Data = Encoding.UTF8.GetBytes(data.ToUrlString)
        End Sub

        Public ReadOnly Property Query() As QueryString
            Get
                Return _Query
            End Get
        End Property

        Public Shadows ReadOnly Property Response() As String
            Get
                Return _Response
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
            Url = New Uri(If(Session.IsSecure, Wiki.SecureUrl, Wiki.Url).ToString & "index.php?" & Query.ToUrlString)
            Cookies = Session.Cookies
            MyBase.Start()
            If IsCancelled Then OnFail(Msg("error-cancelled")) : Return

            If Result.IsError Then
                'When the URL is at fault, make it clear which wiki the URL is for
                Select Case Result.Code
                    Case "badurl" : Result = New Result(Msg("error-wikibadurl", Wiki))
                    Case "httperror" : Result.Append(Msg("error-wikinotfound", Wiki, Wiki.Url))
                End Select

                OnFail(Result) : Return
            End If

            _Response = Encoding.UTF8.GetString(MyBase.Response.ToArray)
            MyBase.Response.Close()
            MyBase.Response = Nothing

            Dim errorCheck As Result = CheckForWikiError(Response)
            If errorCheck.IsError Then OnFail(errorCheck) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
