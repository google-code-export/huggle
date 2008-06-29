Class AddTemplateForm

    Public ReadOnly Property DisplayText() As String
        Get
            Return DisplayTextBox.Text
        End Get
    End Property

    Public ReadOnly Property Template() As String
        Get
            Return TemplateBox.Text
        End Get
    End Property

    Private Sub AddTemplateForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) _
        Handles MyBase.FormClosing

        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub AddTemplateForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ItemTextChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles DisplayTextBox.TextChanged, TemplateBox.TextChanged

        OK.Enabled = (DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "")
    End Sub

    Private Sub Template_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles TemplateBox.KeyDown
        If e.KeyCode = Keys.Enter AndAlso DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

End Class