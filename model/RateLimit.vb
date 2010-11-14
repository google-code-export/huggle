Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Description}")>
    Friend Class RateLimit

        Private _Action As String
        Private _Count As Integer
        Private _Group As String
        Private _Time As TimeSpan

        Public Shared ReadOnly None As New RateLimit(Nothing, Nothing, Nothing, 0, TimeSpan.Zero)

        Public Sub New(ByVal action As String, ByVal group As String, ByVal groups As List(Of String),
            ByVal count As Integer, ByVal time As TimeSpan)

            _Action = action
            _Count = count
            _Group = group
            _Groups = groups
            _Time = time
        End Sub

        Public ReadOnly Property Action() As String
            Get
                Return _Action
            End Get
        End Property

        Public Function Format(ByVal timeFormat As [Function](Of TimeSpan, String)) As String
            If None Is Me OrElse Count = 0 Then Return Msg("a-none")
            If Action IsNot Nothing Then Return Msg("view-misc-ratelimitaction", Count, Action, FuzzyTime(Time))
            If Groups IsNot Nothing Then Return Msg("view-misc-ratelimitgrouped", Count, FuzzyTime(Time), Groups.Join(", "))
            Return Msg("view-misc-ratelimit", Count, timeFormat(Time))
        End Function

        Public ReadOnly Property Count() As Integer
            Get
                Return _Count
            End Get
        End Property

        Public ReadOnly Property Group As String
            Get
                Return _Group
            End Get
        End Property

        Public Property Groups As List(Of String)

        Public ReadOnly Property Time As TimeSpan
            Get
                Return _Time
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Format(AddressOf FuzzyTime)
        End Function

    End Class

End Namespace