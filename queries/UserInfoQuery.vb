Imports System.Collections.Generic

Namespace Huggle.Queries

    'Retrieves user info

    Class UserInfoQuery : Inherits Query

        Private Users As List(Of User)

        Public Sub New(ByVal session As Session, ByVal users As List(Of User))
            MyBase.New(session, Msg("userinfo-desc"))

            Me.Users = Users
        End Sub

        Public Overrides Sub Start()
            Dim names As New List(Of String)

            For Each user As User In Users
                names.Add(user.Name)
            Next user

            Dim query As New QueryString(
                "action", "query",
                "list", "users",
                "usprop", "blockinfo|groups|editcount|registration",
                "ususers", names.Join("|"))

            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsErrored Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
