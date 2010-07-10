Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Huggle.Queries

    Public MustInherit Class OldQuery

        Private _Context As Object
        Private _CurrentRequest As Request
        Private _Description As String
        Private _LastMessage As String
        Private _Result As Result
        Private _State As QueryState
        Private _User As User

        Private Shared ReadOnly _All As New List(Of OldQuery)

        Public Event Done As EventHandler(Of OldQuery, Result)
        Public Event Progress As EventHandler(Of OldQuery, QueryMessage)

        Protected Sub New(ByVal user As User)
            _All.Add(Me)
            _User = user
            State = QueryState.None
        End Sub

        Public Property Context() As Object
            Get
                Return _Context
            End Get
            Set(ByVal value As Object)
                _Context = value
            End Set
        End Property

        Public Property CurrentRequest() As Request
            Get
                Return _CurrentRequest
            End Get
            Private Set(ByVal value As Request)
                _CurrentRequest = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Protected Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public ReadOnly Property IsDone() As Boolean
            Get
                Return (State = QueryState.Done OrElse State = QueryState.Failed)
            End Get
        End Property

        Public ReadOnly Property LastMessage() As String
            Get
                Return _LastMessage
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _User.Wiki
            End Get
        End Property

        Public Property Result() As Result
            Get
                Return _Result
            End Get
            Protected Set(ByVal value As Result)
                _Result = value
            End Set
        End Property

        Public Property State() As QueryState
            Get
                Return _State
            End Get
            Private Set(ByVal value As QueryState)
                _State = value
                DoMessage("")
            End Set
        End Property

        Public Property User() As User
            Get
                Return _User
            End Get
            Set(ByVal value As User)
                _User = value
            End Set
        End Property

        Public Sub Cancel()
            State = QueryState.Cancelled
            If CurrentRequest IsNot Nothing Then CurrentRequest.Cancel()
        End Sub

        Public Function [Do]() As Result
            State = QueryState.Active
            Result = Process()
            State = QueryState.Done
            App.Invoke(AddressOf OnDone)
            Return Result
        End Function

        Public Sub Start()
            Dim Thread As New Thread(AddressOf Start)

            'Ensure any queries still waiting for web responses when
            'the application is terminated are terminated automatically
            Thread.IsBackground = True
            Thread.Start()
        End Sub

        Protected MustOverride Function Process() As Result

        Protected Sub DoMessage(ByVal Text As String)
            'App.Invoke(AddressOf OnProgress, New QueryMessage(Text, Nothing))
        End Sub

        Protected Sub DoProgress(ByVal Text As String, Optional ByVal Context As Object = Nothing)
            'App.Invoke(AddressOf OnProgress, New QueryMessage(Text, Context))
        End Sub

        Private Sub OnDone()
            RaiseEvent Done(Me, Result)
        End Sub

        Private Sub OnProgress(ByVal Message As QueryMessage)
            _LastMessage = Message.Text
            RaiseEvent Progress(Me, Message)
        End Sub

    End Class

    Public Enum QueryState As Integer
        : None : Active : Failed : Cancelled : Done
    End Enum

    Public Class QueryMessage : Inherits EventArgs

        Private _Text As String, _Context As Object

        Public Sub New(ByVal Text As String, ByVal Context As Object)
            _Context = Context
            _Text = Text
        End Sub

        Public ReadOnly Property Context() As Object
            Get
                Return _Context
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

    End Class

End Namespace