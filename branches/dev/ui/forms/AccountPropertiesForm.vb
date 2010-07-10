﻿Imports Huggle

Public Class AccountPropertiesForm

    Private _User As User

    Public Sub New(ByVal user As User)
        InitializeComponent()
        _User = user
    End Sub

    Public ReadOnly Property User() As User
        Get
            Return _User
        End Get
    End Property

    Private Sub _Load() Handles Me.Load

        'Finish loading extra config
        If Not User.Wiki.Config.ExtraConfigLoaded Then
            App.UserWaitForProcess(User.Wiki.Config.ExtraLoader)
            If User.Wiki.Config.ExtraLoader.IsFailed Then App.ShowError(User.Wiki.Config.ExtraLoader.Result) : Close() : Return
        End If

        Icon = Resources.Icon
        Text = Msg("accountprop-title", User.Name)
    End Sub

    Private Function FuzzyContribCount(ByVal contribs As Integer) As String
        Select Case contribs
            Case 0 : Return "none"
            Case Is < 10 : Return "few"
            Case Is < 100 : Return "some"
            Case Else : Return "many"
        End Select
    End Function

    Private Sub LoadData()
        AccountContributions.Text = Msg("accountprop-contribs", FuzzyContribCount(User.Contributions))
        AccountCreated.Text = Msg("accountprop-created", If(User.Created = Date.MinValue, Msg("a-unknown"), FullDateString(User.Created)))

        If User.Preferences.EmailAddress <> "" Then
            AccountEmail.Text = Msg("accountprop-email", User.Preferences.EmailAddress)
            AccountEmailAuthenticated.Visible = True
            AccountEmailAuthenticated.Text = Msg("accountprop-emailauth", _
                If(User.Config.EmailAuthenticated > Date.MinValue, FullDateString(User.Config.EmailAuthenticated), Msg("a-unknown")))
        Else
            AccountEmail.Text = Msg("accountprop-email", Msg("a-notspecified"))
        End If

        AccountGender.Text = Msg("accountprop-gender", If(User.Preferences.Gender = "unknown", Msg("a-notspecified"), User.Preferences.Gender))
        AccountGlobal.Text = Msg("accountprop-global", If(User.IsUnified, Msg("a-yes"), Msg("a-no")))
        AccountID.Text = Msg("accountprop-id", CStr(User.Id))
        AccountName.Text = User.Name
        AccountWiki.Text = Msg("accountprop-wiki", User.Wiki.Name)
    End Sub

    Private Sub SetPreferences_LinkClicked() Handles SetPreferences.LinkClicked
        Dim form As New AccountPreferencesForm(User)
        form.ShowDialog()
    End Sub

    Private Sub ChangeGroups_LinkClicked() Handles ChangeGroups.LinkClicked
        Dim form As New UserRightsForm(User.Session, User)
        form.ShowDialog()
    End Sub

End Class