Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Represents an entry in the MediaWiki log

    <Diagnostics.DebuggerDisplay("{Label}")>
    Friend MustInherit Class LogItem : Inherits QueueItem

        Private _Id As Integer
        Private _Rcid As Integer
        Private _User As User
        Private _Wiki As Wiki

        Friend MustOverride ReadOnly Property Target() As String

        Protected Sub New(ByVal wiki As Wiki, ByVal id As Integer, ByVal rcid As Integer)
            _Id = id
            _Rcid = rcid
            _Wiki = wiki
            If id > 0 Then wiki.Logs.All.Merge(id, Me)
        End Sub

        Friend Overrides ReadOnly Property Icon() As Image
            Get
                Return Resources.blob_log
            End Get
        End Property

        Friend Overrides ReadOnly Property Key() As Integer
            Get
                Return Time.GetHashCode
            End Get
        End Property

        Friend Overrides ReadOnly Property Label() As String
            Get
                Return Msg("queue-action", Action, User, Target)
            End Get
        End Property

        Friend Property Action() As String

        Friend Property Comment() As String

        Friend ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Friend Property IsHidden() As Boolean

        Friend ReadOnly Property Rcid() As Integer
            Get
                Return _Rcid
            End Get
        End Property

        Friend Property Time() As Date

        Friend Property User() As User
            Get
                Return _User
            End Get
            Set(ByVal value As User)
                _User = value
                _User.Logs.Merge(Me)
            End Set
        End Property

        Friend NotOverridable Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Friend Class LogsCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, LogItem)

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As Dictionary(Of Integer, LogItem)
            Get
                Return _All
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal id As Integer) As LogItem
            Get
                If All.ContainsKey(id) Then Return All(id) Else Return Nothing
            End Get
        End Property

    End Class

End Namespace
