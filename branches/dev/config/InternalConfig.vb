Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle

    Public Class InternalConfig

        'Settings here are hardcoded and not user-editable
        'They are kept in a common place for convenience

        Public Shared Property CloudUrl As New Uri("http://api.openkeyval.org/")
        Public Shared Property DownloadUrl As New Uri("http://code.google.com/p/huggle")
        Public Shared Property FeedbackUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Feedback")

        Public Shared Property GlobalExtensions As New List(Of String) _
            ({"Central Auth", "Global Usage", "MergeAccount", "CentralNotice", "GlobalBlocking"})

        Public Shared Property ManualUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Manual")
        Public Shared Property MediaWikiUrl As New Uri("http://www.mediawiki.org/")
        Public Shared Property PrivilegedRights As New List(Of String) _
            ({"delete, undelete, protect, block, userrights, suppressrevision"})

        Public Shared Property SourceUrl As New Uri("http://huggle.googlecode.com/")
        Public Shared Property TranslationUrl As New Uri("http://meta.wikimedia.org/wiki/Huggle")
        Public Shared Property UseCloud As Boolean = False
        Public Shared Property UserAgent As String = "Huggle/" & Windows.Forms.Application.ProductVersion
        Public Shared Property WikimediaFilePath As String = "http://upload.wikimedia.org/"
        Public Shared Property WikimediaSecurePath As String = "https://secure.wikimedia.org/"
        Public Shared Property WikimediaClosedWikisUrl As New Uri("http://noc.wikimedia.org/conf/closed.dblist")
        Public Shared Property WikimediaGlobalGroupsUrl As New Uri("http://toolserver.org/~pathoschild/globalgroups/")

        Public Shared Property MessageGroups As List(Of String)
        Public Shared Property WikiMessages As List(Of String)

        Public Shared Sub Initialize()
            MessageGroups = New List(Of String)
            WikiMessages = New List(Of String)

            For Each item As String In Resources.messages.Split(CRLF).Trim
                If item.Contains("*") Then MessageGroups.Merge(item) Else WikiMessages.Merge(item)
            Next item
        End Sub

    End Class

End Namespace
