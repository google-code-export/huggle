Imports Huggle.Actions
Imports System.Collections.Generic

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function ActionFunc(ByVal Context As Object, ByVal Func As Token, _
            ByVal Arg As Token(), ByVal Params As Dictionary(Of String, Token)) As Token

            Select Case Func.String
                Case "edit"
                    If Arg(0).Page.Text = Arg(1).String _
                        Then Return New Token("Ignored as no change to text")

                    'Dim Result As New Edit _
                    '    (Arg(0).Page, Arg(1).String, Arg(2).String, _
                    '    If(Params.ContainsKey("section"), Params("section").String, Nothing), _
                    '    If(Params.ContainsKey("minor"), Params("minor").Bool, True), _
                    '    If(Params.ContainsKey("watch"), Params("watch").Bool, False), _
                    '    If(Params.ContainsKey("bot"), Params("bot").Bool, Account.User.Groups.Contains("bot")), _
                    '    False, _
                    '    If(Params.ContainsKey("create"), Params("create").Bool, True), _
                    '    True, _
                    '    False, _
                    '    If(Params.ContainsKey("conflict"), CType(Params("conflict").Value,  _
                    '    ConflictBehaviour), ConflictBehaviour.Ignore))

                    'Result.Start()
                    Return New Token("")
            End Select

            Return Nothing
        End Function

    End Class

End Namespace
