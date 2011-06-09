Imports System.Collections.Generic

Namespace Huggle.Queries

    'Retrieves user info

    Class UserDetailQuery : Inherits Query

        Private _Target As User

        Public Sub New(ByVal session As Session, ByVal target As User)
            MyBase.New(session, Msg("userdetail-desc", target))

            ThrowNull(target, "user")
            _Target = target
        End Sub

        Public ReadOnly Property Target As User
            Get
                Return _Target
            End Get
        End Property

        Public Overrides Sub Start()
            OnStarted()

            Dim query As New QueryString(
                "action", "query",
                "titles", User.Talkpage,
                "list", "logevents|users",
                "prop", "categories|info|revisions|templates",
                "cllimit", "max",
                "lelimit", "max",
                "leprop", "ids|title|type|user|timestamp|comment|details",
                "letitle", User.Userpage,
                "rvprop", "ids|flags|timestamp|user|size|comment|content",
                "tllimit", "max",
                "usprop", "blockinfo|groups|editcount|registration",
                "ususers", User)

            Dim req As New ApiRequest(Session, Description, query)
            req.Start()
            If req.IsErrored Then OnFail(req.Result) : Return

            OnSuccess()
        End Sub

    End Class

End Namespace
