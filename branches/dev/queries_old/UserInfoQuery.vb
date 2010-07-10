'Imports System.Collections.Generic

'Namespace Huggle.Queries.Info

'    'Retrieves user info

'    Class UserInfoQuery : Inherits OldQuery

'        Private Users As List(Of User)

'        Public Sub New(ByVal Account As User, ByVal Users As List(Of User))
'            MyBase.New(Account)
'            Me.Users = Users
'        End Sub

'        Protected Overrides Function Process() As Result
'            Dim Usernames As New List(Of String)

'            For Each User As User In Users
'                Usernames.Add(User.Name)
'            Next User

'            Dim Query As New QueryString( _
'                "action", "query", _
'                "list", "users", _
'                "usprop", "blockinfo|groups|editcount|registration", _
'                "ususers", Usernames.Join("|"))

'            Dim request As New ApiRequest(User, Query)
'            request.Start()
'            If request.Result.IsError Then Return Result.FailWith(Msg("userinfo-failed"))

'            Return Result.Success
'        End Function

'    End Class

'End Namespace
