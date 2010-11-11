Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing

Namespace System.Windows.Forms

    Friend Class EnhancedComboBox : Inherits ComboBox

        Public Sub ResizeDropDown()
            Dim newWidth As Integer = DropDownWidth
            Dim graphics As Graphics = CreateGraphics()

            For Each item As Object In Items
                newWidth = Math.Max(newWidth, TextRenderer.MeasureText(item.ToString, Font).Width + 20)
            Next item

            DropDownWidth = newWidth
        End Sub

    End Class

End Namespace
