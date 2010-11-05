Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Command}: {ToString()}")> _
    Class Shortcut

        'Represents a keyboard shortcut

        Private Shared ReadOnly _All As New Dictionary(Of String, Shortcut)

        Private _Command As String, _Key As Keys, _Control, _Alt, _Shift As Boolean

        Private Shared ReadOnly KeyNames As String() = { _
            "Oem1", ";", _
            "Oem2", "/", _
            "Oem4", "[", _
            "Oem5", "\", _
            "Oem6", "]", _
            "Oem7", "#", _
            "Oem8", "`", _
            "OemOpenBrackets", "[", _
            "OemCloseBrackets", "]", _
            "Oemplus", "=", _
            "OemMinus", "-", _
            "Oemcomma", ",", _
            "OemPeriod", ".", _
            "OemQuestion", "/" _
            }

        Friend Sub New(ByVal command As String, ByVal key As Keys, _
            ByVal control As Boolean, ByVal alt As Boolean, ByVal shift As Boolean)

            _Command = command
            _Key = key
            _Control = control
            _Alt = alt
            _Shift = shift
            _All.Merge(command, Me)
        End Sub

        Friend Sub New(ByVal command As String, ByVal shortcutString As String)
            _Command = command

            If Not String.IsNullOrEmpty(shortcutString) Then
                shortcutString = shortcutString.ToLowerI
                _Control = (shortcutString.Contains("ctrl"))
                _Alt = (shortcutString.Contains("alt"))
                _Shift = (shortcutString.Contains("shift"))

                If shortcutString.Contains(" ") Then shortcutString = shortcutString.FromLast(" ")
                If shortcutString.Contains("-") AndAlso Not shortcutString.EndsWithI("-") _
                    Then shortcutString = shortcutString.FromLast("-")
                If shortcutString.Contains("+") AndAlso Not shortcutString.EndsWithI("+") _
                    Then shortcutString = shortcutString.FromLast("+")

                For i As Integer = 0 To KeyNames.Length - 2 Step 2
                    If shortcutString = KeyNames(i + 1) Then
                        shortcutString = KeyNames(i)
                        Exit For
                    End If
                Next i

                _Key.TryParse(shortcutString)
                _All.Merge(_Command, Me)
            End If
        End Sub

        Friend ReadOnly Property Alt() As Boolean
            Get
                Return _Alt
            End Get
        End Property

        Friend ReadOnly Property Command() As String
            Get
                Return _Command
            End Get
        End Property

        Friend ReadOnly Property Control() As Boolean
            Get
                Return _Control
            End Get
        End Property

        Friend ReadOnly Property Key() As Keys
            Get
                Return _Key
            End Get
        End Property

        Friend ReadOnly Property Shift() As Boolean
            Get
                Return _Shift
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim shortcut As Shortcut = TryCast(obj, Shortcut)

            If shortcut Is Nothing Then Return False Else Return (shortcut.Key = Key AndAlso _
                shortcut.Alt = Alt AndAlso shortcut.Control = Control AndAlso shortcut.Shift = Shift)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _Command.GetHashCode Xor _Shift.GetHashCode Xor _Alt.GetHashCode Xor _Control.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Dim name As String = Key.ToString

            For i As Integer = 0 To KeyNames.Length - 2 Step 2
                If name = KeyNames(i) Then
                    name = KeyNames(i + 1)
                    Exit For
                End If
            Next i

            If Shift Then name = "Shift + " & name
            If Alt Then name = "Alt + " & name
            If Control Then name = "Ctrl + " & name

            Return name
        End Function

        Friend Shared ReadOnly Property All() As Dictionary(Of String, Shortcut)
            Get
                Return _All
            End Get
        End Property

        Friend Shared Function GetCommand(ByVal key As Keys,
            ByVal shift As Boolean, ByVal alt As Boolean, ByVal control As Boolean) As String

            For Each shortcut As Shortcut In All.Values
                If shortcut.Key = key AndAlso shortcut.Shift = shift _
                    AndAlso shortcut.Alt = alt AndAlso shortcut.Control = control Then Return shortcut.Command
            Next shortcut

            Return Nothing
        End Function

        Public Shared Operator =(ByVal a As Shortcut, ByVal b As Shortcut) As Boolean
            Return (a.Alt = b.Alt AndAlso a.Control = b.Control AndAlso a.Shift = b.Shift AndAlso a.Key = b.Key)
        End Operator

        Public Shared Operator <>(ByVal a As Shortcut, ByVal b As Shortcut) As Boolean
            Return Not (a = b)
        End Operator

    End Class

End Namespace