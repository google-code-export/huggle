Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Huggle

    Public NotInheritable Class Log

        Private Shared _Items As New List(Of LogMessage)

        Private Delegate Sub LogDelegate(ByVal message As LogMessage)

        Public Shared Event UpdateProcess(ByVal process As Process)
        Public Shared Event Written(ByVal message As LogMessage)

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property Items() As List(Of LogMessage)
            Get
                Return _Items
            End Get
        End Property

        Private Shared ReadOnly Property Path() As String
            Get
                Return Environment.CurrentDirectory & Slash & "log.txt"
            End Get
        End Property

        Public Shared Sub Reset()
            _Items.Clear()

            If File.Exists(Path) Then
                Try
                    File.Delete(Path)

                Catch ex As IOException
                    Debug("Could not delete log file: " & ex.Message)
                Catch ex As UnauthorizedAccessException
                    Debug("Could not delete log file: " & ex.Message)
                End Try
            End If
        End Sub

        Public Shared Sub Debug(ByVal message As String)
            Dim lmsg As New LogMessage(message, True)

            If Threading.SynchronizationContext.Current Is App.Context _
                Then Write(lmsg) Else App.Post(AddressOf Write, lmsg)
        End Sub

        Public Shared Sub Write(ByVal message As String)
            Dim lmsg As New LogMessage(message, False)

            If Threading.SynchronizationContext.Current Is App.Context _
                Then Write(lmsg) Else App.Post(AddressOf Write, lmsg)
        End Sub

        Public Shared Sub AttachProcess(ByVal process As Process)
            App.Post(AddressOf _AttachProcess, process)
        End Sub

        Private Shared Sub _AttachProcess(ByVal actionObj As Object)
            AddHandler CType(actionObj, Process).Progress, AddressOf OnUpdateProcess
        End Sub

        Private Shared Sub OnUpdateProcess(ByVal process As Process)
            If process.IsComplete Then RemoveHandler process.Progress, AddressOf OnUpdateProcess
            RaiseEvent UpdateProcess(process)
        End Sub

        Private Shared Sub Write(ByVal messageObject As Object)
            Dim message As LogMessage = CType(messageObject, LogMessage)
            Items.Add(message)

            If Config.Local.LogToFile AndAlso Path IsNot Nothing Then
                Try
                    File.AppendAllText(Path, _
                        CRLF & message.Time.ToShortDateString & " " & message.Time.ToLongTimeString & " - " & message.Message)

                Catch ex As IOException
                    HandleFileLogError(ex)
                Catch ex As UnauthorizedAccessException
                    HandleFileLogError(ex)
                End Try
            End If

            RaiseEvent Written(message)
        End Sub

        Private Shared Sub HandleFileLogError(ByVal ex As Exception)
            'Turn off logging to file so we don't try to log our logging to file error to file
            Config.Local.LogToFile = False
            Write(Msg("log-error", ex.Message))
        End Sub

    End Class

    Public Class LogMessage

        Private _IsDebug As Boolean
        Private _Message As String
        Private _Time As Date

        Public Sub New(ByVal message As String, ByVal isDebug As Boolean)
            _Message = message
            _Time = Date.Now
            _IsDebug = isDebug
        End Sub

        Public ReadOnly Property IsDebug() As Boolean
            Get
                Return _IsDebug
            End Get
        End Property

        Public ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Public ReadOnly Property Time() As Date
            Get
                Return _Time
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Time.ToLongTimeString & " " & Message
        End Function

    End Class

End Namespace