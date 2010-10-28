Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle

    Public Class InternalConfig

        'Settings here are hardcoded and not user-editable; they are kept in a common place for convenience

        Public Property DownloadUrl As New Uri("http://code.google.com/p/huggle")
        Public Property FeedbackUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Feedback")

        Public Property GlobalExtensions As New List(Of String) _
            ({"Central Auth", "Global Usage", "MergeAccount", "CentralNotice", "GlobalBlocking"})

        Public Property ManualUrl As New Uri("http://en.wikipedia.org/wiki/Wikipedia:Huggle/Manual")
        Public Property MediaWikiUrl As New Uri("http://www.mediawiki.org/")
        Public Property PrivilegedRights As New List(Of String) _
            ({"delete, undelete, protect, block, userrights, suppressrevision"})

        Public Property SourceUrl As New Uri("http://huggle.googlecode.com/")
        Public Property TranslationUrl As New Uri("http://meta.wikimedia.org/wiki/Huggle")
        Public Property UserAgent As String = "Huggle/" & Windows.Forms.Application.ProductVersion
        Public Property WikimediaFilePath As String = "http://upload.wikimedia.org/"
        Public Property WikimediaSecurePath As String = "https://secure.wikimedia.org/"
        Public Property WikimediaClosedWikisPath As New Uri("http://noc.wikimedia.org/conf/closed.dblist")

        Public ReadOnly Property WikiMessages() As String()
            Get
                Static result As String()
                If result Is Nothing Then result = Resources.messages.Split(CRLF)
                Return result
            End Get
        End Property

    End Class

End Namespace
