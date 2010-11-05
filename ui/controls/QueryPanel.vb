Imports Huggle.Actions
Imports Huggle.Scripting
Imports System

Namespace Huggle.UI

    Friend Class QueryPanel

        Private _Session As Session

        Private Evaluator As Evaluator

        Friend Sub New(ByVal session As Session)
            InitializeComponent()
            _Session = session
        End Sub

        Friend ReadOnly Property Session As Session
            Get
                Return _Session
            End Get
        End Property

        Private Sub Query_TextChanged() Handles Query.TextChanged
            Run.Enabled = (Query.Text.Length > 0)
        End Sub

        Private Sub Run_Click() Handles Run.Click
            If Evaluator IsNot Nothing AndAlso Evaluator.IsRunning Then
                Evaluator.Cancel()
                Run.Text = Msg("eval-start")
            Else
                Result.Value = Nothing
                Run.Text = Msg("eval-stop")
                Evaluator = New Evaluator(Session, "query", Query.Text)
                Indicator.Show()
                Progress.Show()
                Indicator.Start()
                AddHandler Evaluator.Progress, AddressOf Evaluator_Progress
                CreateThread(AddressOf Evaluator.Start, AddressOf Evaluator_Done)

                Result.UnsetAll()
            End If
        End Sub

        Private Sub Evaluator_Progress(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            Progress.Text = e.Value.Message
        End Sub

        Private Sub Evaluator_Done()
            Indicator.Stop()
            Indicator.Hide()
            Progress.Hide()
            Run.Text = Msg("eval-start")
            If Evaluator.Result.IsError Then Result.Value = Evaluator.Result Else Result.Value = Evaluator.Value
        End Sub

    End Class

End Namespace