Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing

Namespace System.Windows.Forms

    Friend Class EnhancedFlowLayoutPanel : Inherits FlowLayoutPanel

        Public Sub New()
            MyBase.New()

            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.DoubleBuffer, True)
        End Sub

    End Class

End Namespace
