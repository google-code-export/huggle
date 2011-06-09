Imports Huggle
Imports Huggle.Queries
Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class WaitForm : Inherits HuggleForm

        Private _Cancelled As Boolean

        Private Message As String

        Public Sub New(ByVal message As String)
            ThrowNull(message, "message")
            Me.Message = message

            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
            Text = Msg("wait-title")
            App.Languages.Current.Localize(Me)
            SetMessage(Message)
            Indicator.Start()
        End Sub

        Private Sub _FormClosing() Handles Me.FormClosing
            Indicator.Stop()
        End Sub

        Public ReadOnly Property Cancelled() As Boolean
            Get
                Return _Cancelled
            End Get
        End Property

        Public Sub CloseByProcess(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            Done()
        End Sub

        Public Sub UpdateByProcess(ByVal sender As Object, ByVal e As EventArgs(Of Process))
            SetMessage(e.Value.Message)
        End Sub

        Public Sub SetMessage(ByVal message As String)
            If Not IsAvailable Then Return

            Indicator.Text = message

            'Resize to accommodate message
            Width = Math.Max(280, Indicator.Width + 32)
            Height = Math.Max(100, Indicator.Height + 80)

            CenterToScreen()
        End Sub

        Private Sub Cancel_Click() Handles Cancel.Click
            _Cancelled = True
            Close()
        End Sub

        Public Function Done() As Object
            _Cancelled = False
            Close()
            Return Nothing
        End Function

    End Class

End Namespace