Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class FirstRunForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Windows.Forms.Application.ProductName

                For Each language As Language In App.Languages.All
                    If Not language.IsHidden AndAlso language.IsLocalized Then LanguageSelector.Items.Add(language)
                Next language

                LanguageSelector.SelectedItem = App.Languages.Default

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Private Sub DetectProxy_CheckedChanged() Handles Proxy.CheckedChanged
            Config.Local.DetectProxySettings = Proxy.Checked
        End Sub

        Private Sub LanguageSelector_SelectedValueChanged() Handles LanguageSelector.SelectedValueChanged
            If LanguageSelector.SelectedItem IsNot Nothing Then App.Languages.Current = CType(LanguageSelector.SelectedItem, Language)
            App.Languages.Current.Localize(Me)
        End Sub

        Private Sub TranslateLabel_LinkClicked() Handles TranslateLabel.LinkClicked
            OpenWebBrowser(Config.Internal.TranslationUrl)
        End Sub

    End Class

End Namespace