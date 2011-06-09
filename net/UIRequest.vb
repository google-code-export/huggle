Imports Huggle.Net
Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.Text
Imports System.Xml

Namespace Huggle.Queries

    'Represents a request made through MediaWiki's UI (index.php)
    'Used only when there is no adequate API equivalent

    Friend Class UIRequest : Inherits WikiRequest

        Private ReadOnly GetQuery As QueryString
        Private ReadOnly PostQuery As QueryString

        Public Sub New(ByVal session As Session, ByVal description As String,
            ByVal getQuery As QueryString, ByVal postQuery As QueryString)

            MyBase.New(session)

            Me.GetQuery = getQuery
            Me.GetQuery.Add("uselang", "en")
            Me.GetQuery.Add("useskin", "myskin")
            Me.PostQuery = postQuery
        End Sub

        Public Overrides Sub Start()
            Url = New Uri(If(Session.IsSecure, Wiki.SecureUrl, Wiki.Url).ToString & "index.php?" & GetQuery.ToUrlString)
            Cookies = Session.Cookies
            If PostQuery IsNot Nothing Then Data = Encoding.UTF8.GetBytes(PostQuery.ToUrlString)

            MyBase.Start()
            If Result.IsError Then OnFail(Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
