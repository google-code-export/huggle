Imports System
Imports System.IO
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class FirstRunForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Windows.Forms.Application.ProductName

                LanguageSelector.BeginUpdate()

                For Each language As Language In App.Languages.All
                    If Not language.IsIgnored Then LanguageSelector.Items.Add(language)
                Next language

                LanguageSelector.ResizeDropDown()
                LanguageSelector.SelectedItem = App.Languages.Default
                LanguageSelector.EndUpdate()

                DoLayout()
                CenterToScreen()

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                DialogResult = DialogResult.Abort
                Close()
            End Try
        End Sub

        Private Sub ContinueButton_Click() Handles ContinueButton.Click
            If Proxy.Checked Then
                Config.Local.DetectProxySettings = DetectProxy.Checked
                Config.Local.ManualProxySettings = ManualProxy.Checked
                Config.Local.ProxyHost = ProxyHost.Text
                Config.Local.ProxyPort = CInt(ProxyPort.Value)
            Else
                Config.Local.DetectProxySettings = False
                Config.Local.ManualProxySettings = False
            End If
        End Sub

        Private Sub LanguageSelector_SelectedValueChanged() Handles LanguageSelector.SelectedValueChanged
            If LanguageSelector.SelectedItem IsNot Nothing Then App.Languages.Current = CType(LanguageSelector.SelectedItem, Language)
            App.Languages.Current.Localize(Me)

            Translate.Text = Msg("form-firstrun-translate", Msg("form-firstrun-translatelink"))
        End Sub

        Private Sub ProxyOptions_CheckedChanged() Handles DetectProxy.CheckedChanged, ManualProxy.CheckedChanged
            ProxyLayout.Enabled = ManualProxy.Checked
        End Sub

        Private Sub TranslateLabel_LinkClicked() Handles Translate.LinkClicked
            OpenWebBrowser(InternalConfig.TranslationUrl)
        End Sub

        Private Sub DoLayout() Handles Proxy.CheckedChanged
            MainLayout.SuspendLayout()

            For Each item As Control In List(Of Control)(DetectProxy, ManualProxy, ProxyLayout)
                item.Visible = Proxy.Checked
            Next item

            MainLayout.ResumeLayout()
            Height = MainLayout.Height + 70
        End Sub

    End Class

End Namespace