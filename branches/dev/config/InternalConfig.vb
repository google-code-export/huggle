Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle

    Public Class InternalConfig

        'Settings here are hardcoded and not user-editable; they are kept in a common place for convenience

        Public Shared Property CloudKey As String = "25f3a3bb6875a21508c452345ba61d7e" 'md5("huggle")
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
        Public Shared Property UseCloud As Boolean = True
        Public Shared Property UserAgent As String = "Huggle/" & Windows.Forms.Application.ProductVersion
        Public Shared Property WikimediaFilePath As String = "http://upload.wikimedia.org/"
        Public Shared Property WikimediaSecurePath As String = "https://secure.wikimedia.org/"
        Public Shared Property WikimediaClosedWikisPath As New Uri("http://noc.wikimedia.org/conf/closed.dblist")

        Public Shared ReadOnly Property WikiMessages() As String()
            Get
                Static result As String()
                If result Is Nothing Then result = Resources.messages.Split(CRLF)
                Return result
            End Get
        End Property

    End Class

End Namespace
