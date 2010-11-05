﻿Imports Huggle.UI
Imports System.Windows.Forms

Namespace Huggle.Actions

    Friend Class Import : Inherits Process

        Private WithEvents Login As Login

        Friend Sub New()

        End Sub

        Friend Property Attribution() As Boolean

        Friend Property DestPage() As Page

        Friend Property DestWiki() As Wiki

        Friend Property History() As Boolean

        Friend Property SourcePage() As Page

        Friend Property SourceWiki() As Wiki

        Friend Overrides Sub Start()
            If SourcePage Is Nothing OrElse DestPage Is Nothing Then
                If Not Interactive Then OnFail(Msg("import-notspecified")) : Return

                Using form As New ImportForm
                    If form.ShowDialog = DialogResult.Cancel Then OnFail(Msg("error-cancelled")) : Return

                    Attribution = form.Attribution
                    DestPage = form.DestPage
                    DestWiki = form.DestWiki
                    History = form.History
                    SourcePage = form.SourcePage
                    SourceWiki = form.SourceWiki
                End Using
            End If

            If Not DestWiki.IsLoaded Then
                Login = New Login(DestWiki, Msg("action-import"))
                Login.Start()
                Return
            End If

            If History Then CreateThread(AddressOf DoCopy) Else CreateThread(AddressOf DoImport)
        End Sub

        Private Sub LoginAction_Done() Handles Login.Complete
            If Login.IsFailed Then OnFail(Login.Result)
            If History Then CreateThread(AddressOf DoCopy) Else CreateThread(AddressOf DoImport)
        End Sub

        Private Sub DoCopy()

        End Sub

        Private Sub DoImport()

        End Sub

    End Class

End Namespace