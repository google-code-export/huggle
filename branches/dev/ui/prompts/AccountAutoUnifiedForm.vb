﻿Imports Huggle
Imports System
Imports System.Windows.Forms

Public Class AccountAutoUnifiedForm

    Private Requester As String
    Private User As User

    Public Sub New(ByVal user As User, ByVal requester As String)
        InitializeComponent()
        Me.Requester = requester
        Me.User = user
    End Sub

    Private Sub _FormClosing() Handles Me.FormClosing
        If DialogResult = DialogResult.OK Then
            User.GlobalUser.Config.AutoUnifiedLogin = AutoUnifiedLogin.Checked
            User.GlobalUser.Config.SaveLocal()
        End If
    End Sub

    Private Sub _Load() Handles Me.Load
        Try
            Icon = Resources.Icon
            Request.Text = Request.Text.FormatWith(Requester, User.FullName, User.GlobalUser.FullName)

        Catch ex As SystemException
            App.ShowError(Result.FromException(ex))
            DialogResult = DialogResult.Abort
            Close()
        End Try
    End Sub

End Class