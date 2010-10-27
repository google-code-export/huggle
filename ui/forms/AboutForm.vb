Imports Huggle

Namespace Huggle.UI

    Public Class AboutForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
            Logo.Image = Resources.HuggleLogo
        End Sub

        Private Sub PictureBox1_Click() Handles MediaWiki.Click
            OpenWebBrowser(Config.Internal.MediaWikiUrl)
        End Sub

        Private Sub ManualLink_LinkClicked() Handles ManualLink.LinkClicked
            OpenWebBrowser(Config.Internal.ManualUrl)
        End Sub

        Private Shared Sub FeedbackLink_LinkClicked() Handles FeedbackLink.LinkClicked
            OpenWebBrowser(Config.Internal.FeedbackUrl)
        End Sub

        Private Sub SourceLink_LinkClicked() Handles SourceLink.LinkClicked
            OpenWebBrowser(Config.Internal.SourceUrl)
        End Sub

    End Class

End Namespace