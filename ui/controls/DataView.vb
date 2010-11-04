Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class DataView

        Private _value As Object

        Private Sub _Load() Handles Me.Load
            Value = ""
        End Sub

        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal newValue As Object)
                _value = newValue

                UnsetAll()
                ViewChart.Enabled = (Data.AsChart(newValue) IsNot Nothing)
                ViewHtml.Enabled = (Data.AsHtml(newValue) IsNot Nothing)
                ViewImage.Enabled = (Data.AsImage(newValue) IsNot Nothing)
                ViewTable.Enabled = (Data.AsTable(newValue) IsNot Nothing OrElse Data.AsList(newValue) IsNot Nothing)
                ViewText.Enabled = (Data.AsString(newValue) IsNot Nothing)
                ViewWebPage.Enabled = (Data.AsHtml(newValue) IsNot Nothing)
                ViewWikiText.Enabled = (Data.AsWikitext(newValue) IsNot Nothing)

                If TypeOf newValue Is Result Then ViewResult() Else ViewText_Click()
            End Set
        End Property

        Public Sub UnsetAll()
            For Each item As ToolStripItem In ToolBar.Items
                If TypeOf item Is ToolStripButton Then DirectCast(item, ToolStripButton).Checked = False
            Next item

            ListData.Clear()
            TextData.Clear()

            ListData.Hide()
            ResultData.Hide()
            TextData.Hide()
            WebData.Hide()
        End Sub

        Private Sub ListData_Resize() Handles ListData.Resize
            If ListData.Columns.Count = 1 Then ListData.Columns(0).Width = ListData.Width - 24
        End Sub

        Private Sub ViewResult()
            UnsetAll()
            ResultData.Text = CType(Value, Result).ErrorMessage
            ResultData.Show()
        End Sub

        Private Sub ViewText_Click() Handles ViewText.Click
            UnsetAll()
            ViewText.Checked = True
            TextData.Show()
            TextData.Text = Data.AsString(Value)
        End Sub

        Private Sub ViewWikiText_Click() Handles ViewWikiText.Click
            UnsetAll()
            ViewWikiText.Checked = True
            TextData.Show()
            TextData.Text = Data.AsWikitext(Value).ToString
        End Sub

        Private Sub ViewHtml_Click() Handles ViewHtml.Click
            UnsetAll()
            ViewHtml.Checked = True
            TextData.Show()
            TextData.Text = Data.AsHtml(Value).ToString
        End Sub

        Private Sub ViewWebPage_Click() Handles ViewWebPage.Click
            UnsetAll()
            ViewWebPage.Checked = True
            WebData.Show()
            WebData.DocumentText = Resources.BasicHtmlPage.Replace("{0}", Data.AsHtml(Value).ToString)
        End Sub

        Private Sub ViewTable_Click() Handles ViewTable.Click
            UnsetAll()
            ViewTable.Checked = True

            ListData.BeginUpdate()
            ListData.Clear()

            If Data.AsTable(Value) IsNot Nothing Then
                ListData.HeaderStyle = ColumnHeaderStyle.Nonclickable
                Dim table As Scripting.ScriptTable = Data.AsTable(Value)

                For Each column As String In table.Columns
                    ListData.Columns.Add(column)
                Next column

                For Each row As Scripting.ScriptTableRow In table.Rows
                    If row.Items.Count > 0 Then
                        Dim item As New ListViewItem(Data.AsSubitem(row.Items(0)))

                        For i As Integer = 1 To row.Items.Count - 1
                            item.SubItems.Add(Data.AsSubitem(row.Items(i)))
                        Next i

                        ListData.Items.Add(item)
                    End If
                Next row
            Else
                ListData.HeaderStyle = ColumnHeaderStyle.None
                ListData.Columns.Add("Value")

                For Each item As String In Data.AsList(Value)
                    ListData.Items.Add(New ListViewItem(item))
                Next item
            End If

            ListData_Resize()
            ListData.Show()
            ListData.EndUpdate()
        End Sub

        Private Sub ViewImage_Click() Handles ViewImage.Click
            UnsetAll()
            ViewImage.Checked = True
        End Sub

        Private Sub ViewChart_Click() Handles ViewChart.Click
            UnsetAll()
            ViewChart.Checked = True
        End Sub

        Private Sub WebData_Navigating(ByVal sender As Object, ByVal e As WebBrowserNavigatingEventArgs) Handles WebData.Navigating
            If Not e.Url.ToString = "about:blank" Then
                OpenWebBrowser(e.Url)
                e.Cancel = True
            End If
        End Sub

    End Class

End Namespace