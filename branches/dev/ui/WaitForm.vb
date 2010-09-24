Imports Huggle
Imports Huggle.Actions
Imports System
Imports System.Windows.Forms

Public Class WaitForm

    Private _Cancelled As Boolean

    Private Available As Boolean
    Private Message As String

    Public Sub New(ByVal message As String)
        InitializeComponent()
        Me.Message = message
    End Sub

    Private Sub _HandleCreated() Handles Me.HandleCreated
        Available = True
    End Sub

    Private Sub _HandleDestroyed() Handles Me.HandleDestroyed
        Available = False
    End Sub

    Private Sub _Load() Handles Me.Load
        Icon = Resources.Icon
        Text = Msg("wait-title")
        App.Languages.Current.Localize(Me)
        If Message IsNot Nothing Then SetMessage(Message)
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

    Public Sub CloseByProcess(ByVal process As Process)
        Done()
    End Sub

    Public Sub UpdateByProcess(ByVal process As Process)
        SetMessage(process.Message)
    End Sub

    Public Sub SetMessage(ByVal message As String)
        If Not Available Then Return

        Label.Text = message

        'Resize to accommodate message
        Width = Math.Max(280, Label.Width + 60)
        Height = Math.Max(100, Label.Height + 80)

        'Center on screen
        Left = Screen.FromControl(Me).Bounds.Width \ 2 - Width \ 2
        Top = Screen.FromControl(Me).Bounds.Height \ 2 - Height \ 2
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