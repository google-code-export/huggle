Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Represents an entry in the MediaWiki log

    <Diagnostics.DebuggerDisplay("{Description}")>
    Friend MustInherit Class LogItem : Inherits QueueItem

        Private _ID As Integer
        Private _Page As Page
        Private _User As User
        Private _Wiki As Wiki

        Protected Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            _Id = id
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

        Public Overridable ReadOnly Property Description As String
            Get
                If App.Languages.Current.Messages.ContainsKey("log-" & Action) _
                    Then Return Msg("log-" & Action, Action, Id, User, Target)

                Return Msg("log-misc", Action, Id, User, If(Target, "(" & Msg("a-unknown") & ")"))
            End Get
        End Property

        Public Overrides ReadOnly Property Label() As String
            Get
                Return Msg("queue-action", Action, User, Target)
            End Get
        End Property

        Public Property Action() As String

        Public Property ActionHidden As Boolean

        Public Property Comment() As String

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public Property IsHidden() As Boolean

        Public Property Page As Page
            Get
                Return _Page
            End Get
            Set(ByVal value As Page)
                _Page = value
                OnSetPage()
            End Set
        End Property

        Public Property Rcid() As Integer

        Public Overridable ReadOnly Property Status As String
            Get
                Return ""
            End Get
        End Property

        Public MustOverride ReadOnly Property Target() As String

        Public Property TargetUser As User

        Public Property Time() As Date

        Public Property User() As User
            Get
                Return _User
            End Get
            Set(ByVal value As User)
                _User = value
                OnSetUser()
            End Set
        End Property

        Public NotOverridable Overrides ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Protected Overridable Sub OnSetUser()
        End Sub

        Protected Overridable Sub OnSetTargetUser()
        End Sub

        Protected Overridable Sub OnSetPage()
        End Sub

        Public Overrides Function ToString() As String
            Return Description
        End Function

    End Class

    Friend Class LogsCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Integer, LogItem)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Integer, LogItem)
            Get
                Return _All
            End Get
        End Property

        Default Public ReadOnly Property FromID(ByVal id As Integer) As LogItem
            Get
                If All.ContainsKey(id) Then Return All(id) Else Return Nothing
            End Get
        End Property

    End Class

End Namespace
