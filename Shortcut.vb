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

        Public Sub New(ByVal command As String, ByVal key As Keys, _
            ByVal control As Boolean, ByVal alt As Boolean, ByVal shift As Boolean)

            _Command = command
            _Key = key
            _Control = control
            _Alt = alt
            _Shift = shift
            _All.Merge(command, Me)
        End Sub

        Public Sub New(ByVal command As String, ByVal shortcutString As String)
            _Command = command

            If Not String.IsNullOrEmpty(shortcutString) Then
                shortcutString = shortcutString.ToLower
                _Control = (shortcutString.Contains("ctrl"))
                _Alt = (shortcutString.Contains("alt"))
                _Shift = (shortcutString.Contains("shift"))

                If shortcutString.Contains(" ") Then shortcutString = shortcutString.FromLast(" ")
                If shortcutString.Contains("-") AndAlso Not shortcutString.EndsWith("-") _
                    Then shortcutString = shortcutString.FromLast("-")
                If shortcutString.Contains("+") AndAlso Not shortcutString.EndsWith("+") _
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

        Public ReadOnly Property Alt() As Boolean
            Get
                Return _Alt
            End Get
        End Property

        Public ReadOnly Property Command() As String
            Get
                Return _Command
            End Get
        End Property

        Public ReadOnly Property Control() As Boolean
            Get
                Return _Control
            End Get
        End Property

        Public ReadOnly Property Key() As Keys
            Get
                Return _Key
            End Get
        End Property

        Public ReadOnly Property Shift() As Boolean
            Get
                Return _Shift
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim Shortcut As Shortcut = TryCast(obj, Shortcut)

            If Shortcut Is Nothing Then Return False Else Return (Shortcut.Key = Key AndAlso _
                Shortcut.Alt = Alt AndAlso Shortcut.Control = Control AndAlso Shortcut.Shift = Shift)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return _Command.GetHashCode Xor _Shift.GetHashCode Xor _Alt.GetHashCode Xor _Control.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Dim Name As String = Key.ToString

            For i As Integer = 0 To KeyNames.Length - 2 Step 2
                If Name = KeyNames(i) Then
                    Name = KeyNames(i + 1)
                    Exit For
                End If
            Next i

            If Shift Then Name = "Shift + " & Name
            If Alt Then Name = "Alt + " & Name
            If Control Then Name = "Ctrl + " & Name

            Return Name
        End Function

        Public Shared ReadOnly Property All() As Dictionary(Of String, Shortcut)
            Get
                Return _All
            End Get
        End Property

        Public Shared Function GetCommand(ByVal Key As Keys, ByVal Shift As Boolean, ByVal Alt As Boolean, ByVal Control As Boolean) As String
            For Each Item As Shortcut In All.Values
                If Item.Key = Key AndAlso Item.Shift = Shift _
                    AndAlso Item.Alt = Alt AndAlso Item.Control = Control Then Return Item.Command
            Next Item

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