'Imports System.Collections.Generic

'Namespace Huggle.Queries

'    'Retrieves user info

'    Class UserDetailQuery : Inherits OldQuery

'        Private Target As User

'        Friend Sub New(ByVal Account As User, ByVal User As User)
'            MyBase.New(Account)
'            Me.User = User
'        End Sub

'        Protected Overrides Function Process() As Result

'            Dim Query As New QueryString( _
'                "action", "query", _
'                "titles", User.Talkpage, _
'                "list", "logevents|users", _
'                "prop", "categories|info|revisions|templates", _
'                "cllimit", 500, _
'                "lelimit", 500, _
'                "leprop", "ids|title|type|user|timestamp|comment|details", _
'                "letitle", User.Userpage, _
'                "rvprop", "ids|flags|timestamp|user|size|comment|content", _
'                "tllimit", 500, _
'                "usprop", "blockinfo|groups|editcount|registration", _
'                "ususers", User)

'            Dim request As New ApiRequest(User, Query)
'            request.Start()
'            If request.Result.IsError Then Return Result.FailWith(Msg("userinfo-failed"))

'            Return Result.Success
'        End Function

'    End Class

'End Namespace
