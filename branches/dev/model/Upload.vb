﻿Imports System.Collections.Generic

Namespace Huggle

    'Represents a media upload

    Public Class Upload : Inherits LogItem

        Private _File As String

        Public Sub New(ByVal time As Date, ByVal action As String, ByVal user As User, _
            ByVal file As String, ByVal comment As String, ByVal id As Integer, ByVal rcid As Integer)

            MyBase.New(user.Wiki, Id, rcid)
            Me.Action = action
            Me.Comment = Comment
            Me.Time = time
            Me.User = user

            _File = file
        End Sub

        Public ReadOnly Property File() As String
            Get
                Return _File
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _File
            End Get
        End Property

    End Class

End Namespace
