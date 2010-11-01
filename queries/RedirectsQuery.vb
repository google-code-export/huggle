Imports System
Imports System.Collections.Generic

Namespace Huggle.Actions

    'Get redirect targets

    Public Class RedirectsQuery : Inherits Query

        Private Pages As List(Of Page)

        Public Sub New(ByVal session As Session, ByVal Pages As List(Of Page))
            MyBase.New(session, Msg("redirects-desc"))
            Me.Pages = Pages
        End Sub

        Public Overrides Sub Start()
            Dim titles As New List(Of String)

            For Each page As Page In Pages
                titles.Add(page.Title)
            Next page

            Dim request As New ApiRequest(Session, Description, New QueryString( _
                    "action", "query", _
                        "redirects", True, _
                        "titles", titles.Join("|")))

            request.Start()
            If request.IsFailed Then OnFail(request.Message) : Return
            OnSuccess()
        End Sub

    End Class

End Namespace