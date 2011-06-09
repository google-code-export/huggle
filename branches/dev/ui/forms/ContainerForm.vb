Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class ContainerForm : Inherits HuggleForm

        Public Sub New(ByVal control As Control)
            ThrowNull(control, "control")

            Height = control.Height
            Width = control.Width
            control.Dock = DockStyle.Fill
            Controls.Add(control)
        End Sub

    End Class

End Namespace