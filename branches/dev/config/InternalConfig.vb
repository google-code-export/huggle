Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle

    Friend NotInheritable Class InternalConfig

        Private Sub New()
            '
        End Sub

        'Settings here are hardcoded and not user-editable
        'They are kept in a common place for convenience

        Friend Shared Property CloudUrl As New Uri("http://api.openkeyval.org/")
        Friend Shared Property DownloadUrl As New Uri("http://code.google.com/p/huggle")
        Friend Shared Property FeedbackUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Feedback")

        Friend Shared Property GlobalExtensions As New List(Of String) _
            ({"Central Auth", "Global Usage", "MergeAccount", "CentralNotice", "GlobalBlocking"})

        Friend Shared Property ManualUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Manual")
        Friend Shared Property MediaWikiUrl As New Uri("http://www.mediawiki.org/")
        Friend Shared Property PrivilegedRights As New List(Of String) _
            ({"delete, undelete, protect, block, userrights, suppressrevision"})

        Friend Shared Property SourceUrl As New Uri("http://huggle.googlecode.com/")
        Friend Shared Property TranslationUrl As New Uri("http://meta.wikimedia.org/wiki/Huggle")
        Friend Shared Property UseCloud As Boolean = False
        Friend Shared Property UserAgent As String = "Huggle/" & Windows.Forms.Application.ProductVersion
        Friend Shared Property WikimediaFilePath As String = "http://upload.wikimedia.org/"
        Friend Shared Property WikimediaSecurePath As String = "https://secure.wikimedia.org/"
        Friend Shared Property WikimediaClosedWikisUrl As New Uri("http://noc.wikimedia.org/conf/closed.dblist")
        Friend Shared Property WikimediaGlobalGroupsUrl As New Uri("http://toolserver.org/~pathoschild/globalgroups/")

        Friend Shared Property MessageGroups As List(Of String)
        Friend Shared Property WikiMessages As List(Of String)

        Friend Shared Sub Initialize()
            MessageGroups = New List(Of String)
            WikiMessages = New List(Of String)

            For Each item As String In Resources.messages.Split(CRLF).Trim
                If item.Contains("*") Then MessageGroups.Merge(item) Else WikiMessages.Merge(item)
            Next item
        End Sub

    End Class

End Namespace
