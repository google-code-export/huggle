﻿Imports System
Imports System.Collections.Generic

Namespace Huggle.Wikitext

    <Diagnostics.DebuggerDisplay("{Page}")>
    Friend Class Link

        Private Document As Document
        Private _Page As Page
        Private _Selection As Selection
        Private _Text As String

        Public Sub New(ByVal document As Document, ByVal page As Page,
            ByVal selection As Selection, ByVal text As String)

            Me.Document = document
            _Page = page
            _Selection = selection
            _Text = text
        End Sub

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Selection() As Selection
            Get
                Return _Selection
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "[[" & Parsing.LinkName(Page) & If(Text Is Nothing, "", Text) & "]]"
        End Function

    End Class

    Friend Class LinkCollection

        Private _All As New List(Of Link)

        Private Document As Document

        Public Sub New(ByVal document As Document)
            Me.Document = document
        End Sub

        Public ReadOnly Property All As List(Of Link)
            Get
                Return _All
            End Get
        End Property

    End Class

End Namespace
