'Imports System

'Namespace Huggle.Queries.Info

'    Friend Class ThreadQuery : Inherits OldQuery

'        Private Limit As Integer

'        Public Sub New(ByVal Account As User)
'            MyBase.New(Account)
'        End Sub

'        Protected Overrides Function Process() As Result

'            Dim Query As New QueryString( _
'                "action", "query", _
'                "list", "threads", _
'                "thdir", "older", _
'                "thlimit", If(Limit > 0, CStr(Limit), "max"), _
'                "thprop", "id|subject|page|parent|ancestor|created|modified|author|summaryid|rootid|type")

'            Dim Request As New ApiRequest(User, Query)
'            Request.Start()

'            Return Result.Success

'        End Function

'    End Class

'End Namespace
