Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class LogPanel

        Private Sub _Load() Handles Me.Load
            AddHandler Log.UpdateProcess, AddressOf Log_UpdateAction
            AddHandler Log.Written, AddressOf Log_Written

            LogList.BeginUpdate()

            For Each msg As LogMessage In New List(Of LogMessage)(Log.Items)
                Log_Written(Me, New EventArgs(Of LogMessage)(msg))
            Next msg

            LogList.EndUpdate()
        End Sub

        Private Sub _Resize() Handles Me.Resize
            If LogList.Columns.Count > 0 Then LogList.Columns(1).Width = LogList.Width - LogList.Columns(0).Width - 24
        End Sub

        Private Sub LogList_SelectedIndexChanged() Handles LogList.SelectedIndexChanged
            LogList.ContextMenuStrip = If(LogList.SelectedItems.Count = 0, Nothing, LogMenu)
        End Sub

        Private Sub Log_UpdateAction(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            'Dim action As Process = e.Value

            'For i As Integer = 0 To LogList.Items.Count - 1
            '    If LogList.Items(i).Tag Is action Then
            '        If action.IsComplete Then LogList.Items.RemoveAt(i) _
            '            Else LogList.Items(i).SubItems(0).Text = action.Message
            '        Return
            '    End If
            'Next i

            'If Not action.IsComplete Then
            '    LogList.InsertRow(0, FullDateString(Date.Now), action.Message)
            '    LogList.Items(0).UseItemStyleForSubItems = True
            '    LogList.Items(0).ForeColor = Color.Red
            '    LogList.Items(0).Tag = action
            'End If
        End Sub

        Private Sub Log_Written(ByVal sender As Object, ByVal e As EventArgs(Of LogMessage))
            'If Config.Local.DebugVisible OrElse Not e.Value.IsDebug _
            '    Then LogList.InsertRow(0, FullDateString(e.Value.Time), e.Value.Message)
        End Sub

        Private Sub LogCopy_Click() Handles LogCopy.Click
            If LogList.SelectedItems.Count > 0 Then Clipboard.SetText(LogList.SelectedItems(0).SubItems(1).Text)
        End Sub

    End Class

End Namespace