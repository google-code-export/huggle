'Imports System.Collections.Generic

'Namespace Huggle.Queries

'    Class RevisionInfoQuery : Inherits OldQuery

'        Private Revisions As List(Of Revision)

'        Friend Sub New(ByVal Account As User, ByVal ParamArray Revisions() As Revision)
'            MyBase.New(Account)
'            Me.Revisions = Revisions.ToList
'        End Sub

'        Protected Overrides Function Process() As Result
'            Dim Revs As String = ""

'            For Each Item As Revision In Revisions
'                Revs &= Item.Id.ToString & "|"
'            Next Item

'            Revs = Revs.Substring(0, Revs.Length - 1)

'            Dim Request As New ApiRequest(Session, Description, New QueryString( _
'                "action", "query", _
'                "prop", "info|revisions", _
'                "revids", Revs, _
'                "rvprop", "ids|flags|timestamp|user|size|comment"))

'            Request.Start()
'            If Request.Result.IsError Then Return Result.FailWith(Msg("revisionsinfo-fail"))

'            Return Result.Success
'        End Function

'    End Class

'End Namespace
