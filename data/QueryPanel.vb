Imports Huggle
Imports Huggle.Actions
Imports Huggle.Scripting

Public Class QueryPanel

    Private _Wiki As Wiki

    Private Evaluator As Evaluator

    Public Sub New(ByVal wiki As Wiki)
        InitializeComponent()
        Me.Wiki = wiki
    End Sub

    Public Property Wiki() As Wiki
        Get
            Return _Wiki
        End Get
        Set(ByVal value As Wiki)
            _Wiki = value
        End Set
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
            Evaluator = New Evaluator(App.Sessions(Wiki), "query", Query.Text)
            Indicator.Show()
            Progress.Show()
            Indicator.Start()
            AddHandler Evaluator.Progress, AddressOf Evaluator_Progress
            App.Start(AddressOf Evaluator.Start, AddressOf Evaluator_Done)

            Result.UnsetAll()
        End If
    End Sub

    Private Sub Evaluator_Progress(ByVal action As Process)
        Progress.Text = action.Message
    End Sub

    Private Sub Evaluator_Done()
        Indicator.Stop()
        Indicator.Hide()
        Progress.Hide()
        Run.Text = Msg("eval-start")
        If Evaluator.Result.IsError Then Result.Value = Evaluator.Result Else Result.Value = Evaluator.Value
    End Sub

End Class
