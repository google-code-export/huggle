Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class ErrorForm : Inherits HuggleForm

        Private Message As String
        Private ShowRetry As Boolean

        Friend Sub New(ByVal message As String, ByVal showRetry As Boolean)
            InitializeComponent()
            If message Is Nothing Then Throw New ArgumentNullException("message")

            Dim maxLength As Integer = 80
            Dim result As String = ""
            Dim startPos As Integer
            Dim endPos As Integer
            Dim lineLength As Integer = maxLength
            Dim line As String

            While startPos < message.Length
                endPos = message.IndexOfI(CRLF, startPos)

                If endPos - startPos < maxLength Then
                    If endPos = -1 Then
                        line = message.Substring(startPos, message.Length - startPos)
                        result &= line
                        Exit While
                    Else
                        line = message.Substring(startPos, endPos - startPos)
                        startPos = endPos + 1
                        result &= line
                    End If
                Else
                    While lineLength + startPos <= endPos
                        While message.Substring(startPos + lineLength - 1, 1) <> " "c
                            lineLength -= 1
                        End While

                        line = message.Substring(startPos, lineLength)
                        result &= line & CRLF
                        startPos += lineLength
                        If lineLength >= maxLength Then startPos += 1
                        lineLength = maxLength
                    End While

                    line = message.Substring(startPos, endPos - startPos)
                    result &= line
                    startPos = endPos + 1
                End If
            End While

            Me.Message = result
            Me.ShowRetry = showRetry
        End Sub

        Private Sub _KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            If e.Control AndAlso e.KeyCode = Keys.C Then Copy_Click()
        End Sub

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Windows.Forms.Application.ProductName
                Image.Image = Resources.error_icon
                If App.Languages.Current IsNot Nothing Then App.Languages.Current.Localize(Me)
                MessageBox.Text = Message

                'Select mode
                Retry.Visible = ShowRetry
                OK.Text = If(ShowRetry, Msg("a-cancel"), Msg("a-ok"))

                'Resize to accommodate message
                Width = Math.Max(280, MessageBox.Width + 80)
                Height = Math.Max(120, MessageBox.Height + 85)

                'Center on screen
                Left = Screen.FromControl(Me).Bounds.Width \ 2 - Width \ 2
                Top = Screen.FromControl(Me).Bounds.Height \ 2 - Height \ 2

            Catch ex As SystemException
                'Error showing the error form. Better give up...
                Log.Write("Error showing error form: " & Result.FromException(ex).LogMessage)
                Close()
            End Try
        End Sub

        Private Sub Copy_Click() Handles Copy.Click
            Clipboard.SetText(Message)
        End Sub

        Private Sub OK_Click() Handles OK.Click
            DialogResult = If(Retry.Visible, DialogResult.Cancel, DialogResult.OK)
            Close()
        End Sub

        Private Sub Retry_Click() Handles Retry.Click
            DialogResult = DialogResult.Retry
            Close()
        End Sub

    End Class

End Namespace