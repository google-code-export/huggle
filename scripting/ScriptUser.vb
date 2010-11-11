Imports System
Imports System.Collections
Imports System.Collections.Generic

Namespace Huggle.Scripting

    Partial Class Evaluator

        Private Function UserFunc(ByVal Context As Object, ByVal Func As Token, ByVal Arg As Token(), _
            ByVal Named As Dictionary(Of String, Token)) As Object

            Dim Data As New FuncData
            Data.Arg = Arg
            Data.Context = Context
            Data.Func = Func
            Data.Named = Named

            'Select Case Func.String

            '    Case "alert"
            '        Windows.Forms.MessageBox.Show(MakeString(Arg(0)), _
            '            CStr(Identifiers("queryname")), Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

            '        Return True

            '    Case "prompt"
            '        If InfoNeeded.Count > 0 Then Return Undefined
            '        Immediate = True

            '        Dim Buttons As New List(Of String)

            '        For i As Integer = 2 To Arg.Length - 1
            '            Buttons.Add(Arg(i).String)
            '        Next i

            '        Return Prompt.Show(CStr(Identifiers("queryname")), Arg(0).String, Arg(1).String, _
            '            Buttons.Count, Buttons.ToArray)

            '    Case "input"
            '        If InfoNeeded.Count > 0 Then Return Undefined
            '        Immediate = True

            '        Return InputBox.Show(CStr(Identifiers("queryname")), Arg(0).String, Arg(1).String, Arg(2).String)

            '    Case "view"
            '        If InfoNeeded.Count > 0 Then Return Undefined
            '        Immediate = True

            '        If Arg(0).ValueType = "Page" OrElse Arg(0).ValueType = "Revision" Then Return DoUIAction(Data)
            '        Throw New ParserException("Don't know how to view item of type '" & Arg(0).ValueType & "'")

            '    Case "closetab"
            '        If InfoNeeded.Count > 0 Then Return Undefined
            '        Immediate = True
            '        Return DoUIAction(Data)

            'End Select

            Return Nothing
        End Function

        Private Function DoUIAction(ByVal funcData As FuncData) As Object
            'Post(Of FuncData)(AddressOf EvalUIAction, FuncData)
            Return True
        End Function

        Private Sub EvalUIAction(ByVal data As FuncData)

            'Evaluation happens in its own thread; this method is the exception and executes on the UI thread,
            'to handle things that need to manipulate the UI

            'Select Case Data.Func.String
            '    Case "closetab"
            '        If Not (TypeOf MF.TabStrip.SelectedTab Is QueryTab) Then MF.TabStrip.RemoveAt(MF.TabStrip.SelectedIndex)

            '    Case "view"
            '        If Data.Arg(0).ValueType = "Page" Then MF.ViewPage(Data.Arg(0).Page)
            '        If Data.Arg(0).ValueType = "Revision" Then MF.ViewRevision(Data.Arg(0).Revision)
            'End Select
        End Sub

        Structure FuncData
            Public Context As Object
            Public Func As Token
            Public Arg As Token()
            Public Named As Dictionary(Of String, Token)
        End Structure

    End Class

End Namespace
