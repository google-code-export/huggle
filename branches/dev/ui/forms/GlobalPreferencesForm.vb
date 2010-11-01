Imports Huggle.Actions
Imports System
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class GlobalPreferencesForm : Inherits HuggleForm

        Private PrefsValid As Boolean
        Private Session As Session
        Private Source As User

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Me.Session = session
        End Sub

        Private ReadOnly Property GlobalUser() As GlobalUser
            Get
                Return Session.User.GlobalUser
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                Account.Text = GlobalUser.FullName

                For Each user As User In GlobalUser.Users
                    CopyFrom.Items.Add(user.Wiki)
                Next user

                For Each wiki As Wiki In GlobalUser.Family.Wikis.All
                    If GlobalUser.HasAccountOn(wiki) Then CopyTo.Items.Add(wiki, True)
                Next wiki

                CopyFrom.SelectedItem = Session.Wiki

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

        Private Sub CopyFrom_SelectedIndexChanged() Handles CopyFrom.SelectedIndexChanged
            If Source IsNot Nothing AndAlso Not CopyTo.Items.Contains(Source.Wiki) _
                AndAlso (ShowAll.Checked OrElse GlobalUser.HasAccountOn(Source.Wiki)) _
                Then CopyTo.Items.Add(Source.Wiki, True)

            Source = GlobalUser.UserOn(CType(CopyFrom.SelectedItem, Wiki))
            If CopyTo.Items.Contains(Source.Wiki) Then CopyTo.Items.Remove(Source.Wiki)
            PrefsError.Visible = False

            If Source.Wiki.Preferences Is Nothing Then
                PrefsValid = False
                CopyWhat.Items.Clear()

                Dim login As New Login(App.Sessions(Source), "Global preferences")
                App.UserWaitForProcess(login)

                If login.IsFailed Then
                    PrefsError.Visible = True
                    App.ShowError(login.Result) : Return
                End If

                Dim extraConfig As New ExtraWikiConfig(Source.Wiki)
                App.UserWaitForProcess(extraConfig)

                If extraConfig.IsFailed Then
                    PrefsError.Visible = True
                    App.ShowError(extraConfig.Result) : Return
                End If
            End If

            If Source.Wiki.Preferences IsNot Nothing Then
                PrefsValid = True
                CopyWhat.Items.Clear()

                For Each pref As String In Source.Wiki.Preferences
                    CopyWhat.Items.Add(pref, False)
                Next pref
            End If
        End Sub

        Private Sub CopyToCheck_LinkClicked() Handles CopyToCheck.LinkClicked
            SetAll(CopyTo, True)
        End Sub

        Private Sub CopyToClear_LinkClicked() Handles CopyToClear.LinkClicked
            SetAll(CopyTo, False)
        End Sub

        Private Sub CheckValid() Handles CopyWhat.ItemCheck
            OK.Enabled = (PrefsValid AndAlso CopyWhat.CheckedItems.Count > 0)
        End Sub

        Private Sub CopyWhatCheck_LinkClicked() Handles CopyWhatCheck.LinkClicked
            SetAll(CopyWhat, True)
            CheckValid()
        End Sub

        Private Sub CopyWhatClear_LinkClicked() Handles CopyWhatClear.LinkClicked
            SetAll(CopyWhat, False)
            CheckValid()
        End Sub

        Private Shared Sub SetAll(ByVal control As CheckedListBox, ByVal state As Boolean)
            For i As Integer = 0 To control.Items.Count - 1
                control.SetItemChecked(i, state)
            Next i
        End Sub

        Private Sub ShowAll_CheckedChanged() Handles ShowAll.CheckedChanged
            If ShowAll.Checked Then
                For Each wiki As Wiki In GlobalUser.Family.Wikis.All
                    If Not CopyTo.Items.Contains(wiki) Then CopyTo.Items.Add(wiki, True)
                Next wiki
            Else
                For Each Wiki As Wiki In GlobalUser.Family.Wikis.All
                    If Not GlobalUser.HasAccountOn(Wiki) Then CopyTo.Items.Remove(Wiki)
                Next Wiki
            End If
        End Sub

    End Class

End Namespace