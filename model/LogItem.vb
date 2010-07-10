Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Represents an entry in the MediaWiki log

    <Diagnostics.DebuggerDisplay("{Label}")> _
    Public MustInherit Class LogItem : Inherits QueueItem

        Private _Action As String
        Private _Comment As String
        Private _Id As Integer
        Private _IsHidden As Boolean
        Private _Rcid As Integer
        Private _Time As Date
        Private _User As User
        Private _Wiki As Wiki

        Public MustOverride ReadOnly Property Target() As String

        Protected Sub New(ByVal wiki As Wiki, ByVal id As Integer, ByVal rcid As Integer)
            _Id = id
            _Rcid = rcid
            _Wiki = wiki
            If id > 0 Then wiki.Logs.All.Merge(id, Me)
        End Sub

        Public Overrides ReadOnly Property Icon() As Image
            Get
                Return Resources.blob_log
            End Get
        End Property

        Public Overrides ReadOnly Property Key() As Integer
            Get
                Return Time.GetHashCode
            End Get
        End Property

        Public Overrides ReadOnly Property Label() As String
            Get
                Return Msg("queue-action", Action, User, Target)
            End Get
        End Property

        Public Property Action() As String
            Get
                Return _Action
            End Get
            Protected Set(ByVal value As String)
                _Action = value
            End Set
        End Property

        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Protected Set(ByVal value As String)
                _Comment = value
            End Set
        End Property

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public Property IsHidden() As Boolean
            Get
                Return _IsHidden
            End Get
            Protected Set(ByVal value As Boolean)
                _IsHidden = value
            End Set
        End Property

        Public ReadOnly Property Rcid() As Integer
            Get
                Return _Rcid
            End Get
        End Property

        Public Property Time() As Date
            Get
                Return _Time
            End Get
            Protected Set(ByVal value As Date)
                _Time = value
            End Set
        End Property

        Public Property User() As User
            Get
                Return _User
            End Get
            Protected Set(ByVal value As User)
                _User = value
                _User.Logs.Merge(Me)
            End Set
        End Property

        Public NotOverridable Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

    End Class

    Public Class LogsCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, LogItem)

        Public Sub New(ByVal Wiki As Wiki)
            Me.Wiki = Wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Integer, LogItem)
            Get
                Return _All
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Id As Integer) As LogItem
            Get
                If All.ContainsKey(Id) Then Return All(Id) Else Return Nothing
            End Get
        End Property

    End Class

End Namespace
