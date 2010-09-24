Imports Huggle

Public Class AccountSelectForm

    Private _User As User

    Private Wiki As Wiki

    Public Sub New(ByVal requester As String, ByVal wiki As Wiki)
        InitializeComponent()
        Me.Wiki = wiki

        App.Languages.Current.Localize(Me)
        Request.Text = Msg("secondarylogin-request", requester)
        Anonymous.Visible = Config.Global.AnonymousLogin

        If wiki.Family IsNot Nothing AndAlso wiki.Family.ActiveGlobalUser IsNot Nothing Then
            Unified.Visible = False
            RememberUnified.Visible = False
        Else
            Unified.Text = Msg("secondarylogin-unified", wiki.Family.ActiveGlobalUser.Name)
            Unified.Visible = True
            RememberUnified.Visible = True
        End If
    End Sub

    Public ReadOnly Property User() As User
        Get
            Return _User
        End Get
    End Property

    Private Sub OK_Click() Handles OK.Click
        If Unified.Checked Then
            '_User = Wiki.Family.ActiveGlobalUser.AccountOn(Wiki)
            'If RememberUnified.Checked Then Config.Local.AutoUnifiedAccount = User
        End If

        If Anonymous.Checked Then _User = Wiki.Users.Anonymous

        If OtherLogin.Checked Then
            _User = Wiki.Users(OtherLogin.Text)
            User.Password = Scramble(Password.Text, Hash(User))
        End If
    End Sub

End Class