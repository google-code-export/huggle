Imports System

Namespace Huggle.UI

    Friend Class AccountSelectForm : Inherits HuggleForm

        Private _User As User

        Private Requester As String
        Private Wiki As Wiki

        Public Sub New(ByVal requester As String, ByVal wiki As Wiki)
            ThrowNull(wiki, "wiki")
            Me.Requester = requester
            Me.Wiki = wiki

            InitializeComponent()
        End Sub

        Public ReadOnly Property User() As User
            Get
                Return _User
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)
            Request.Text = Msg("secondarylogin-request", Requester)
            Anonymous.Visible = Config.Global.AnonymousLogin

            If Wiki.Family IsNot Nothing AndAlso Wiki.Family.ActiveGlobalUser IsNot Nothing Then
                Unified.Visible = False
                RememberUnified.Visible = False
            Else
                Unified.Text = Msg("secondarylogin-unified", Wiki.Family.ActiveGlobalUser.Name)
                Unified.Visible = True
                RememberUnified.Visible = True
            End If
        End Sub

        Private Sub OK_Click() Handles OK.Click
            If Unified.Checked Then
                '_User = Wiki.Family.ActiveGlobalUser.AccountOn(Wiki)
                'If RememberUnified.Checked Then Config.Local.AutoUnifiedAccount = User
            End If

            If Anonymous.Checked Then _User = Wiki.Users.Anonymous

            If OtherLogin.Checked Then
                _User = Wiki.Users(OtherLogin.Text)
                User.Password = Scramble(User.FullName, Password.Text, Hash(User))
            End If
        End Sub

    End Class

End Namespace