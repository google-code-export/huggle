Imports System
Imports System.Collections.Generic
Imports System.Diagnostics

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{LogMessage}")>
    Friend Class Result

        Private _Inner As Result
        Private _Message As String = "Unknown error"

        Private Shared ReadOnly _Success As New Result("No error")

        Friend Sub New(ByVal messages As String(), Optional ByVal errorCode As String = Nothing)
            If messages.Length = 0 Then Return
            _Message = messages(0)
            _Code = errorCode

            Dim current As Result = Me

            For i As Integer = 1 To messages.Length - 1
                If Not String.IsNullOrEmpty(messages(i)) Then
                    current._Inner = New Result(messages(i))
                    current = current.Inner
                End If
            Next i
        End Sub

        Friend Sub New(ByVal message As String, Optional ByVal errorCode As String = Nothing)
            Me.New({message}, errorCode)
        End Sub

        Friend Property Code() As String

        Friend Function LogMessage() As String
            Return GenerateMessage(False)
        End Function

        Friend Function ErrorMessage() As String
            Return GenerateMessage(True)
        End Function

        Private Function GenerateMessage(ByVal linebreaks As Boolean) As String
            If Not IsError Then Return "No error"

            Dim result As String = If(linebreaks, Message, Message.Replace(CRLF, " "))
            Dim item As Result = Inner

            While item IsNot Nothing AndAlso item.IsError
                If linebreaks Then result &= ":" & CRLF & item.Message _
                    Else result &= ": " & item.Message.Replace(CRLF, " ")
                item = item.Inner
            End While

            If Not result.EndsWithI(".") Then result &= "."
            Return result
        End Function

        Friend ReadOnly Property Inner() As Result
            Get
                Return _Inner
            End Get
        End Property

        Friend ReadOnly Property IsError() As Boolean
            Get
                Return Not (Success Is Me)
            End Get
        End Property

        Friend ReadOnly Property Message() As String
            Get
                Return _Message
            End Get
        End Property

        Friend Function Append(ByVal message As String) As Result
            Return Append(New Result(message))
        End Function

        Friend Function Append(ByVal result As Result) As Result
            Dim r As Result = Me

            While r.Inner IsNot Nothing
                r = r.Inner
            End While

            r._Inner = result
            Return Me
        End Function

        Friend Function Wrap(ByVal message As String) As Result
            Dim msg As String = _Message
            Dim r As Result = Me

            While r.Inner IsNot Nothing
                Dim temp As String = r.Inner.Message
                r.Inner._Message = msg
                msg = temp
                r = r.Inner
            End While

            r._Inner = New Result(msg)
            _Message = message
            Return Me
        End Function

        Public Overrides Function ToString() As String
            Return LogMessage()
        End Function

        Friend Shared ReadOnly Property Success As Result
            Get
                Return _Success
            End Get
        End Property

        Friend Shared Function FromException(ByVal ex As Exception, Optional ByVal inner As Boolean = False) As Result
            Dim message As String = ""

            If TypeOf ex Is ApplicationException Then
                message = ex.Message
            Else
                'Alternative hardcoded error messages in case an unhandled exception
                'occurs before we've loaded the message files
                If Not inner Then
                    If App.Languages.Current IsNot Nothing AndAlso _
                        App.Languages.Current.Messages.ContainsKey("error-exception") _
                        Then message = Msg("error-exception") Else message = "An unexpected error occured."
                End If

                If ex.StackTrace IsNot Nothing Then
                    If Not inner Then
                        If App.Languages.Current IsNot Nothing AndAlso _
                            App.Languages.Current.Messages.ContainsKey("error-info") _
                            Then message &= CRLF & CRLF & Msg("error-info") _
                            Else message &= CRLF & CRLF & "If reporting this error, please include the following information:"
                    End If

                    message &= CRLF & CRLF & ex.GetType.Name.Remove("Exception") &
                        CRLF & FormatStackTrace(New StackTrace(ex, True))
                End If
            End If

            Dim result As New Result(message)

            If ex.InnerException IsNot Nothing Then result._Inner = result.FromException(ex.InnerException, True)

            Return result
        End Function

        Private Shared Function FormatStackTrace(ByVal trace As StackTrace) As String
            Dim resultLines As New List(Of String)

            For Each frame As StackFrame In trace.GetFrames
                Dim type As Type = frame.GetMethod.ReflectedType
                Dim method As String = frame.GetMethod.Name

                'Ignore framework methods
                If method.Contains("$__") OrElse type.FullName.StartsWithI("System") Then Continue For

                Dim msg As String = " - {0}.{1}".FormatI(type.Name, method.Replace(".ctor", "New"))
                If frame.GetFileLineNumber > 0 Then msg &= ":" & CStr(frame.GetFileLineNumber)
                resultLines.Add(msg)
            Next frame

            Return resultLines.Join(CRLF)
        End Function

    End Class

End Namespace