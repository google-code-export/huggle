Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Language

        'Represents a language

        Private _Code As String
        Private Shared _MessageGroups As New Dictionary(Of String, String)

        Public Sub New(ByVal code As String)
            _Code = code
            _Name = code
        End Sub

        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Public Property IsIgnored() As Boolean

        Public Property IsLocalized() As Boolean
            
        Public Property Messages() As Dictionary(Of String, String)

        Public Property Name() As String

        Public Function GetConfig() As MessageConfig
            Return New MessageConfig(Me)
        End Function

        Public Sub Localize(ByVal control As Control)
            Dim prefix As String = control.Name.ToLowerI
            If control.Name.EndsWithI("View") Then prefix = "view-" & control.Name.Remove("View").ToLowerI
            If control.Name.EndsWithI("Form") Then prefix = "form-" & control.Name.Remove("Form").ToLowerI
            Localize(control, prefix, Nothing)
        End Sub

        'Localize text of controls where needed; recurse through all child controls
        Private Sub Localize(ByVal control As Control, ByVal prefix As String, Optional ByVal tip As ToolTip = Nothing)
            If TypeOf control Is Form AndAlso Messages.ContainsKey(prefix & "-title") _
                Then control.Text = Msg(prefix & "-title")

            For Each child As Control In control.Controls
                If TypeOf child Is Label OrElse TypeOf child Is CheckBox OrElse TypeOf child Is RadioButton OrElse _
                    TypeOf child Is Button OrElse TypeOf child Is GroupBox Then

                    Dim itemMsg As String = child.Name.Replace("Label", "").Replace("Button", "").ToLowerI
                    Dim prefixedMsg As String = prefix & "-" & itemMsg

                    If child.Text <> "" Then
                        If Messages.ContainsKey(prefixedMsg) Then
                            child.Text = Msg(prefixedMsg)
                        ElseIf Messages.ContainsKey("a-" & itemMsg) Then
                            child.Text = Msg("" & itemMsg)
                        End If
                    End If

                    'Tooltips
                    If TypeOf child Is Button AndAlso tip IsNot Nothing _
                        AndAlso Messages.ContainsKey(prefixedMsg & "-tip") Then

                        Dim ToolTip As String = Msg(prefixedMsg & "-tip")
                        If Shortcut.All.ContainsKey(prefixedMsg) _
                            Then ToolTip &= " [" & Shortcut.All(prefixedMsg).ToString & "]"
                        tip.SetToolTip(child, ToolTip)
                    End If

                ElseIf TypeOf child Is ToolStrip Then
                    For Each ToolItem As ToolStripItem In DirectCast(child, ToolStrip).Items
                        LocalizeToolStripItem(ToolItem, prefix)
                    Next ToolItem
                End If

                Localize(child, prefix, tip)
            Next child
        End Sub

        Private Sub LocalizeToolStripItem(ByVal item As ToolStripItem, ByVal prefix As String)
            Dim ItemMsg As String = item.Name.Replace("Label", "").Replace("Button", "").ToLowerI
            Dim PrefixedMsg As String = prefix & "-" & ItemMsg

            If Messages.ContainsKey(PrefixedMsg) Then
                item.Text = Msg(PrefixedMsg)
            ElseIf Messages.ContainsKey("a-" & ItemMsg) Then
                item.Text = Msg("" & ItemMsg)
            End If

            item.ToolTipText = item.Text
        End Sub

        'Returns a message string in this language
        Public Function Message(ByVal name As String, ByVal ParamArray params As Object()) As String
            If Messages.ContainsKey(name) Then
                Return Messages(name).FormatForUser(params)

            ElseIf App.Languages.Default IsNot Nothing AndAlso App.Languages.Default.Messages.ContainsKey(name) Then
                'Localized message does not exist, use default language
                Return App.Languages.Default.Messages(name).FormatForUser(params)

            Else
                'Message does not exist in either form, output message name instead
                Return "[" & name & "]"
            End If
        End Function

        Public Overrides Function ToString() As String
            If _Name = _Code Then Return "(" & _Code & ")" Else Return _Name
        End Function

        Public Shared Property MessageGroups() As Dictionary(Of String, String)
            Get
                Return _MessageGroups
            End Get
            Set(ByVal value As Dictionary(Of String, String))
                _MessageGroups = value
            End Set
        End Property

    End Class

    Public Class LanguageCollection

        Private _All As New Dictionary(Of String, Language)
        Private _Current As Language
        Private _Default As Language

        Public ReadOnly Property All() As IList(Of Language)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public Property Current() As Language
            Get
                Return _Current
            End Get
            Set(ByVal value As Language)
                _Current = value
            End Set
        End Property

        Public Property [Default]() As Language
            Get
                Return _Default
            End Get
            Set(ByVal value As Language)
                _Default = value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal code As String) As Language
            Get
                If Not _All.ContainsKey(code) Then _All.Add(code, New Language(code))
                Return _All(code)
            End Get
        End Property

    End Class

End Namespace