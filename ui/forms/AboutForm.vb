Namespace Huggle.UI

    Public Class AboutForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
            Logo.Image = Resources.HuggleLogo
        End Sub

        Private Sub PictureBox1_Click() Handles MediaWiki.Click
            OpenWebBrowser(InternalConfig.MediaWikiUrl)
        End Sub

        Private Sub ManualLink_LinkClicked() Handles ManualLink.LinkClicked
            OpenWebBrowser(InternalConfig.ManualUrl)
        End Sub

        Private Shared Sub FeedbackLink_LinkClicked() Handles FeedbackLink.LinkClicked
            OpenWebBrowser(InternalConfig.FeedbackUrl)
        End Sub

        Private Sub SourceLink_LinkClicked() Handles SourceLink.LinkClicked
            OpenWebBrowser(InternalConfig.SourceUrl)
        End Sub

    End Class

End Namespace