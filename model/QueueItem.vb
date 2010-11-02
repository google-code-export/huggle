﻿Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Abstract class for objects that can be added to queues,
    'this includes pages, revisions, users and log entries.

    Public MustInherit Class QueueItem

        Protected Sub New()
        End Sub

        Public Overridable ReadOnly Property FilterVars() As Dictionary(Of String, Object)
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable ReadOnly Property Icon() As Image
            Get
                Return Nothing
            End Get
        End Property

        Public MustOverride ReadOnly Property Key() As Integer

        Public Overridable ReadOnly Property Label() As String
            Get
                Return String.Empty
            End Get
        End Property

        Public Overridable ReadOnly Property LabelBackColor() As Color
            Get
                Return Color.White
            End Get
        End Property

        Public Overridable ReadOnly Property LabelForeColor() As Color
            Get
                Return Color.Black
            End Get
        End Property

        Public Overridable ReadOnly Property LabelStyle() As FontStyle
            Get
                Return 0
            End Get
        End Property

        Public MustOverride ReadOnly Property Wiki() As Wiki

    End Class

End Namespace
