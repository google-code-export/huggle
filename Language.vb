﻿Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Language

        'Represents a language

        Private _Code As String
        Private _IsHidden As Boolean
        Private _IsLocalized As Boolean
        Private _Name As String
        Private _Messages As New Dictionary(Of String, String)

        Private Shared _MessageGroups As New Dictionary(Of String, String)

        Public Sub New(ByVal code As String)
            _Code = code
            _Name = code
        End Sub

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property

        Public Property IsHidden() As Boolean
            Get
                Return _IsHidden
            End Get
            Set(ByVal value As Boolean)
                _IsHidden = value
            End Set
        End Property

        Public Property IsLocalized() As Boolean
            Get
                Return _IsLocalized
            End Get
            Set(ByVal value As Boolean)
                _IsLocalized = value
            End Set
        End Property

        Public Property Messages() As Dictionary(Of String, String)
            Get
                Return _Messages
            End Get
            Set(ByVal value As Dictionary(Of String, String))
                _Messages = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Sub Load(ByVal text As String)
            For Each item As KeyValuePair(Of String, String) In Config.ParseConfig("messages-" & Code, Nothing, text)
                Messages.Merge(item.Key, item.Value)
            Next item
        End Sub

        Public Sub LocalizeControl(ByVal Control As Control, Optional ByVal Tip As ToolTip = Nothing)
            LocalizeControl(Control, Control.Name.Replace("Form", "").Replace("Tab", "").ToLower, Tip)
        End Sub

        'Localize text of controls where needed; recurse through all child controls
        Private Sub LocalizeControl(ByVal Control As Control, ByVal Prefix As String, ByVal Tip As ToolTip)
            For Each Item As Control In Control.Controls
                If TypeOf Item Is Label OrElse TypeOf Item Is CheckBox OrElse TypeOf Item Is RadioButton OrElse _
                    TypeOf Item Is Button OrElse TypeOf Item Is GroupBox Then

                    Dim ItemMsg As String = Item.Name.Replace("Label", "").Replace("Button", "").ToLower
                    Dim PrefixedMsg As String = Prefix & "-" & ItemMsg

                    If Item.Text <> "" Then
                        If Messages.ContainsKey(PrefixedMsg) Then
                            Item.Text = Msg(PrefixedMsg)
                        ElseIf Messages.ContainsKey("a-" & ItemMsg) Then
                            Item.Text = Msg("" & ItemMsg)
                        End If
                    End If

                    'Tooltips
                    If TypeOf Item Is Button AndAlso Tip IsNot Nothing _
                        AndAlso Messages.ContainsKey(PrefixedMsg & "-tip") Then

                        Dim ToolTip As String = Msg(PrefixedMsg & "-tip")
                        If Shortcut.All.ContainsKey(PrefixedMsg) _
                            Then ToolTip &= " [" & Shortcut.All(PrefixedMsg).ToString & "]"
                        Tip.SetToolTip(Item, ToolTip)
                    End If

                ElseIf TypeOf Item Is ToolStrip Then
                    For Each ToolItem As ToolStripItem In CType(Item, ToolStrip).Items
                        LocalizeToolStripItem(ToolItem, Prefix)
                    Next ToolItem
                End If

                LocalizeControl(Item, Prefix, Tip)
            Next Item
        End Sub

        Private Sub LocalizeToolStripItem(ByVal Item As ToolStripItem, ByVal Prefix As String)
            Dim ItemMsg As String = Item.Name.Replace("Label", "").Replace("Button", "").ToLower
            Dim PrefixedMsg As String = Prefix & "-" & ItemMsg

            If Messages.ContainsKey(PrefixedMsg) Then
                Item.Text = Msg(PrefixedMsg)
            ElseIf Messages.ContainsKey("a-" & ItemMsg) Then
                Item.Text = Msg("" & ItemMsg)
            End If

            Item.ToolTipText = Item.Text
        End Sub

        'Returns a message string in this language
        Public Function Message(ByVal Name As String, ByVal ParamArray Params As Object()) As String
            If Messages.ContainsKey(Name) Then
                Return Messages(Name).FormatWith(Params)

            ElseIf App.Languages.Default IsNot Nothing AndAlso App.Languages.Default.Messages.ContainsKey(Name) Then
                'Localized message does not exist, use default language
                Return App.Languages.Default.Messages(Name).FormatWith(Params)

            Else
                'Message does not exist in either form, output message name instead
                Return "[" & Name & "]"
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