Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Huggle

    Friend NotInheritable Class LogClass : Implements IDisposable

        Private _Items As New List(Of LogMessage)
        Private Writer As StreamWriter

        Private Delegate Sub LogDelegate(ByVal message As LogMessage)

        Friend Event UpdateProcess As SimpleEventHandler(Of Process)
        Friend Event Written As SimpleEventHandler(Of LogMessage)

        Friend ReadOnly Property Items() As List(Of LogMessage)
            Get
                Return _Items
            End Get
        End Property

        Private Shared ReadOnly Property Path() As String
            Get
                Return PathCombine(Environment.CurrentDirectory, "log.txt")
            End Get
        End Property

        Friend Sub Initialize()
            _Items.Clear()

            If IO.File.Exists(Path) Then
                Try
                    IO.File.Delete(Path)

                Catch ex As IOException
                    Debug("Could not delete log file: " & ex.Message)
                Catch ex As UnauthorizedAccessException
                    Debug("Could not delete log file: " & ex.Message)
                End Try
            End If
        End Sub

        Friend Sub AttachProcess(ByVal process As Process)
            CallOnMainThread(AddressOf _AttachProcess, process)
        End Sub

        Friend Sub Debug(ByVal message As String)
            CallOnMainThread(AddressOf Write, New LogMessage(message, True))
        End Sub

        Friend Sub Write(ByVal message As String)
            CallOnMainThread(AddressOf Write, New LogMessage(message, False))
        End Sub

        Private Sub _AttachProcess(ByVal actionObj As Object)
            AddHandler CType(actionObj, Process).Progress, AddressOf OnUpdateProcess
        End Sub

        Private Sub HandleFileLogError(ByVal ex As Exception)
            'Turn off logging to file so we don't try to log our logging to file error to file
            Config.Local.LogToFile = False
            Write(Msg("log-error", ex.Message))
        End Sub

        Private Sub OnUpdateProcess(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            If e.Value.IsComplete Then RemoveHandler e.Value.Progress, AddressOf OnUpdateProcess
            RaiseEvent UpdateProcess(Me, New EventArgs(Of Process)(e.Value))
        End Sub

        Private Sub Write(ByVal messageObject As Object)
            Dim message As LogMessage = CType(messageObject, LogMessage)
            Items.Add(message)

            If Config.Local.LogToFile AndAlso Path IsNot Nothing Then
                Try
                    If Writer Is Nothing Then Writer = New StreamWriter(Path, True, Text.Encoding.UTF8)

                    Writer.WriteLine(message.Time.ToShortDateString & " " & message.Time.ToLongTimeString &
                        "." & message.Time.Millisecond.ToStringI.PadLeft(3, "0"c) & " - " & message.Message)
                    Writer.Flush()

                Catch ex As SystemException
                    HandleFileLogError(ex)
                End Try
            End If

            RaiseEvent Written(Me, New EventArgs(Of LogMessage)(message))
        End Sub

        Friend Sub Dispose() Implements IDisposable.Dispose
            If Writer IsNot Nothing Then
                Writer.Flush()
                Writer.Close()
                Writer.Dispose()
                Writer = Nothing
            End If
        End Sub

    End Class

    Friend Class LogMessage

        Private _IsDebug As Boolean
        Private _Message As String
        Private _Time As Date

        Friend Sub New(ByVal message As String, ByVal isDebug As Boolean)
            _Message = message
            _Time = Date.Now
            _IsDebug = isDebug
        End Sub

        Friend ReadOnly Property IsDebug() As Boolean
            Get
                Return _IsDebug
            End Get
        End Property

        Friend ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Friend ReadOnly Property Time() As Date
            Get
                Return _Time
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Time.ToLongTimeString & " " & Message
        End Function

    End Class

End Namespace