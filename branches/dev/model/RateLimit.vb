Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Description}")>
    Friend Class RateLimit

        Private _Action As String
        Private _Count As Integer
        Private _Group As String
        Private _Groups As New List(Of String)
        Private _Time As TimeSpan

        Friend Shared None As New RateLimit(Nothing, Nothing, Nothing, 0, TimeSpan.Zero)

        Friend Sub New(ByVal action As String, ByVal group As String, ByVal groups As List(Of String),
            ByVal count As Integer, ByVal time As TimeSpan)

            _Action = action
            _Count = count
            _Group = group
            _Groups = groups
            _Time = time
        End Sub

        Friend ReadOnly Property Action() As String
            Get
                Return _Action
            End Get
        End Property

        Friend ReadOnly Property Description As String
            Get
                If Count = 0 Then Return Msg("a-none")
                If Action IsNot Nothing Then Return Msg("view-misc-ratelimitaction", Count, Action, FuzzyTime(Time))
                If Groups IsNot Nothing Then Return Msg("view-misc-ratelimitgrouped", Count, FuzzyTime(Time), Groups.Join(", "))
                Return Msg("view-misc-ratelimit", Count, FuzzyTime(Time))
            End Get
        End Property

        Friend ReadOnly Property Count() As Integer
            Get
                Return _Count
            End Get
        End Property

        Friend ReadOnly Property Group As String
            Get
                Return _Group
            End Get
        End Property

        Friend ReadOnly Property Groups As List(Of String)
            Get
                Return _Groups
            End Get
        End Property

        Friend ReadOnly Property Time As TimeSpan
            Get
                Return _Time
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Description
        End Function

    End Class

End Namespace