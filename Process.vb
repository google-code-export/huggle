Imports System
Imports System.Collections.Generic

Namespace Huggle

    'Represents an asynchronous process

    Friend MustInherit Class Process

        Private _Message As String

        Private State As ProcessState

        Public MustOverride Sub Start()

        Public Event Complete As SimpleEventHandler(Of Process)
        Public Event Fail As SimpleEventHandler(Of Process)
        Public Event Progress As SimpleEventHandler(Of Process)
        Public Event Started As SimpleEventHandler(Of Process)
        Public Event Success As SimpleEventHandler(Of Process)

        Protected Sub New()
            _Result = Result.Success
        End Sub

        Public Property Interactive() As Boolean

        Public ReadOnly Property IsCancelled() As Boolean
            Get
                Return (State = ProcessState.Cancelled)
            End Get
        End Property

        Public ReadOnly Property IsComplete() As Boolean
            Get
                Return (State = ProcessState.Cancelled OrElse State = ProcessState.Errored _
                    OrElse State = ProcessState.Success)
            End Get
        End Property

        Public ReadOnly Property IsErrored() As Boolean
            Get
                Return (State = ProcessState.Errored)
            End Get
        End Property

        Public ReadOnly Property IsFailed() As Boolean
            Get
                Return (State = ProcessState.Cancelled OrElse State = ProcessState.Errored)
            End Get
        End Property

        Public ReadOnly Property IsRunning() As Boolean
            Get
                Return (State = ProcessState.Running)
            End Get
        End Property

        Public ReadOnly Property IsSuccess() As Boolean
            Get
                Return (State = ProcessState.Success)
            End Get
        End Property

        Public ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Public Property Result() As Result

        Public Sub Cancel()
            OnFail(Msg("error-cancelled"))
            State = ProcessState.Cancelled
        End Sub

        Public Sub Reset()
            OnProgress(Nothing)
            State = ProcessState.None
        End Sub

        Protected Sub FailUndefined(ByVal key As String)
            OnFail(Msg("error-undefined", key))
        End Sub

        Protected Shared Function MakeConfirmation(ByVal messages As List(Of String)) As String
            If messages.Count = 1 Then
                Return messages(0)
            Else
                Dim result As String = ""

                For Each message As String In messages
                    result &= "- " & message & CRLF
                Next message

                Return result
            End If
        End Function

        Protected Sub OnFail(ByVal newResult As Result)
            If Not IsFailed Then
                _Result = If(newResult, New Result({Msg("error-unknown")}))

                'Use the progress message as the source of the error when available
                If Message IsNot Nothing Then
                    Result.Wrap(Msg("error-process", Message.ToLowerFirstI.Remove("...")))
                    Result.Code = Result.Inner.Code
                End If

                _Message = Nothing
                State = ProcessState.Errored
                Log.Debug(Result.LogMessage)
                CallOnMainThread(AddressOf _OnFail)
            End If
        End Sub

        Protected Sub OnFail(ByVal message As String, Optional ByVal errorCode As String = Nothing)
            OnFail(New Result(message, errorCode))
        End Sub

        Protected Sub OnFail(ByVal messages() As String, Optional ByVal errorCode As String = Nothing)
            OnFail(New Result(messages, errorCode))
        End Sub

        Private Sub _OnFail()
            RaiseEvent Fail(Me, New EventArgs(Of Process)(Me))
            RaiseEvent Complete(Me, New EventArgs(Of Process)(Me))
        End Sub

        Protected Sub OnProgress(ByVal message As String)
            _Message = message & "..."
            CallOnMainThread(AddressOf _OnProgress)
        End Sub

        Private Sub _OnProgress()
            RaiseEvent Progress(Me, New EventArgs(Of Process)(Me))
        End Sub

        Protected Sub OnStarted()
            State = ProcessState.Running
            CallOnMainThread(AddressOf _OnStarted)
        End Sub

        Private Sub _OnStarted()
            RaiseEvent Started(Me, New EventArgs(Of Process)(Me))
        End Sub

        Protected Sub OnSuccess()
            State = ProcessState.Success
            CallOnMainThread(AddressOf _OnSuccess)
        End Sub

        Private Sub _OnSuccess()
            RaiseEvent Success(Me, New EventArgs(Of Process)(Me))
            RaiseEvent Complete(Me, New EventArgs(Of Process)(Me))
        End Sub

        Protected Sub SetProgressByProcess(ByVal process As Process)
            OnProgress(process.Message)
        End Sub

        Private Enum ProcessState
            : None : Running : Success : Cancelled : Errored
        End Enum

    End Class

    Friend Class GeneralProcess : Inherits Process

        Private Method As Action

        Public Sub New(ByVal message As String, ByVal method As Action)
            OnProgress(message)
            Me.Method = method
        End Sub

        Public Overrides Sub Start()
            Method.Invoke()
            OnSuccess()
        End Sub

    End Class

End Namespace
