Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class Prompt : Inherits HuggleForm

        Private WithEvents Title As New Label
        Private WithEvents ButtonPanel As New FlowLayoutPanel
        Private WithEvents Subtitle As New Label
        Private SelectedButton As Integer

        Private Sub New()
            Me.ButtonPanel.SuspendLayout()
            Me.SuspendLayout()

            Me.ButtonPanel.Anchor = CType(AnchorStyles.Top Or AnchorStyles.Bottom Or _
                AnchorStyles.Left Or AnchorStyles.Right, AnchorStyles)
            Me.ButtonPanel.Controls.Add(Me.Title)
            Me.ButtonPanel.Controls.Add(Me.Subtitle)
            Me.ButtonPanel.FlowDirection = FlowDirection.TopDown
            Me.ButtonPanel.Location = New Point(5, 5)
            Me.ButtonPanel.Size = New Size(335, 45)
            Me.ButtonPanel.TabIndex = 0
            Me.ButtonPanel.WrapContents = False

            Me.Title.AutoSize = True
            Me.Title.Font = New Font("Segoe UI", 11.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            Me.Title.ForeColor = Color.DarkBlue
            Me.Title.Location = New Point(3, 0)
            Me.Title.Margin = New Padding(3, 0, 3, 4)
            Me.Title.Size = New Size(38, 20)
            Me.Title.TabIndex = 1
            Me.Title.Text = "Title"

            Me.Subtitle.AutoSize = True
            Me.Subtitle.Location = New Point(3, 24)
            Me.Subtitle.Margin = New Padding(3, 0, 3, 4)
            Me.Subtitle.Size = New Size(42, 13)
            Me.Subtitle.TabIndex = 2
            Me.Subtitle.Text = "Subtitle"

            Me.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = AutoScaleMode.Font
            Me.BackColor = SystemColors.Control
            Me.ClientSize = New Size(344, 54)
            Me.Controls.Add(Me.ButtonPanel)
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = SizeGripStyle.Hide
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.ButtonPanel.ResumeLayout(False)
            Me.ButtonPanel.PerformLayout()
            Me.ResumeLayout(False)
        End Sub

        Private Sub _KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
            Dim Number As Integer = e.KeyCode - Keys.D0

            If e.KeyCode = Keys.Escape OrElse Number = 0 Then
                Close()
            ElseIf Number > 0 AndAlso Number <= 9 Then
                SelectedButton = Number
                Close()
            End If
        End Sub

        Private Sub _Shown() Handles Me.Shown
            ButtonPanel.Controls(2).Focus()
            Location = New Drawing.Point(Location.X, Screen.FromControl(Me).Bounds.Height - Height - 60)
        End Sub

        Private Sub AddButton(ByVal text As String)
            'Dim NewButton As New TaskDialogButton
            'NewButton.Width = ButtonPanel.Width
            'NewButton.Label = text
            'ButtonPanel.Controls.Add(NewButton)
            'AddHandler NewButton.Click, AddressOf ButtonClicked
            'Height += NewButton.Height
        End Sub

        Private Sub ButtonClicked(ByVal sender As Object, ByVal e As EventArgs)

            For i As Integer = 0 To ButtonPanel.Controls.Count - 1
                If ButtonPanel.Controls(i) Is sender Then SelectedButton = i - 1
            Next i

            Close()
        End Sub

        Public Overloads Shared Function Show(
            ByVal title As String, ByVal largeText As String, ByVal smallText As String,
            ByVal defaultButton As Integer, ByVal ParamArray buttons As String()) As Integer

            Using prompt As New Prompt
                prompt.Text = Title
                Dim InitialTitleHeight As Integer = prompt.Title.Height

                prompt.Title.Text = largeText
                prompt.Height += (prompt.Title.Height - InitialTitleHeight)
                prompt.Subtitle.Text = smallText
                If String.IsNullOrEmpty(smallText) Then prompt.Subtitle.Text = Nothing
                prompt.SelectedButton = defaultButton

                For Each button As String In buttons
                    If button IsNot Nothing Then prompt.AddButton(button)
                Next button

                prompt.ShowDialog()
                Return prompt.SelectedButton
            End Using
        End Function

    End Class

End Namespace