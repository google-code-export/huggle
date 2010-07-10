Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Media

        Private _DetailsKnown As Boolean
        Private _Duplicates As New List(Of Media)
        Private _Exists As Boolean
        Private _FirstRevision As MediaRevision
        Private _GlobalUses As New List(Of Page)
        Private _IsShared As Boolean
        Private _LastRevision As MediaRevision
        Private _Page As Page
        Private _Revisions As New List(Of MediaRevision)
        Private _Uses As New List(Of Page)
        Private _Wiki As Wiki

        Public Sub New(ByVal wiki As Wiki, ByVal page As Page)
            _Exists = True
            _Page = page
            _Wiki = wiki
        End Sub

        Public Property Content() As Stream
            Get
                Try
                    If File.Exists(FilePath) Then Return New MemoryStream(File.ReadAllBytes(FilePath))
                Catch ex As IOException
                    Log.Write("Failed to load file '{0}'".FormatWith(Name))
                End Try

                Return Nothing
            End Get
            Set(ByVal value As Stream)
                Try
                    WriteFile(FilePath, value)
                Catch ex As IOException
                    Log.Write("Failed to save file '{0}'".FormatWith(Name))
                End Try
            End Set
        End Property

        Public ReadOnly Property ContentKnown() As Boolean
            Get
                Return File.Exists(FilePath)
            End Get
        End Property

        Public Property DetailsKnown() As Boolean
            Get
                Return _DetailsKnown
            End Get
            Set(ByVal value As Boolean)
                _DetailsKnown = value
            End Set
        End Property

        Public ReadOnly Property Duplicates() As List(Of Media)
            Get
                Return _Duplicates
            End Get
        End Property

        Public Property Exists() As Boolean
            Get
                Return _Exists
            End Get
            Set(ByVal value As Boolean)
                _Exists = value
            End Set
        End Property

        Private ReadOnly Property FilePath() As String
            Get
                Return Config.Local.ConfigPath & "media" & Slash & GetValidFileName(Wiki.Code & "-" & Name)
            End Get
        End Property

        Public Property FirstRevision() As MediaRevision
            Get
                Return _FirstRevision
            End Get
            Set(ByVal value As MediaRevision)
                _FirstRevision = value
            End Set
        End Property

        Public ReadOnly Property GlobalUses() As List(Of Page)
            Get
                Return _GlobalUses
            End Get
        End Property

        Public Property IsShared() As Boolean
            Get
                Return _IsShared
            End Get
            Set(ByVal value As Boolean)
                _IsShared = value
            End Set
        End Property

        Public Property LastRevision() As MediaRevision
            Get
                Return _LastRevision
            End Get
            Set(ByVal value As MediaRevision)
                _LastRevision = value
            End Set
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Page.Name
            End Get
        End Property

        Public ReadOnly Property Page() As Page
            Get
                Return _Page
            End Get
        End Property

        Public ReadOnly Property Revisions() As List(Of MediaRevision)
            Get
                Return _Revisions
            End Get
        End Property

        Public Property Thumb(ByVal size As Integer) As Stream
            Get
                Try
                    If File.Exists(ThumbPath(size)) Then Return New MemoryStream(File.ReadAllBytes(ThumbPath(size)))
                Catch ex As IOException
                    Log.Write("Failed to load file '{0}'".FormatWith(Name))
                End Try

                Return Nothing
            End Get
            Set(ByVal value As Stream)
                Try
                    WriteFile(ThumbPath(size), value)
                Catch ex As IOException
                    Log.Write("Failed to save file '{0}'".FormatWith(Name))
                End Try
            End Set
        End Property

        Public ReadOnly Property ThumbKnown(ByVal size As Integer) As Boolean
            Get
                Return (File.Exists(ThumbPath(size)))
            End Get
        End Property

        Public ReadOnly Property ThumbPath(ByVal size As Integer) As String
            Get
                Return Config.Local.ConfigPath & "media" & Slash & "thumbs" & _
                    Slash & GetValidFileName(Wiki.Code & "-" & ThumbName(size))
            End Get
        End Property

        Public ReadOnly Property ThumbName(ByVal size As Integer) As String
            Get
                Return CStr(size) & "px-" & Name & If(Type = "svg", ".png", "")
            End Get
        End Property

        Public ReadOnly Property Type() As String
            Get
                If Not Name.Contains(".") Then Return Nothing
                Return Name.FromLast(".").ToLower
            End Get
        End Property

        Public ReadOnly Property Uploader() As User
            Get
                If LastRevision Is Nothing Then Return Nothing Else Return LastRevision.User
            End Get
        End Property

        Public ReadOnly Property Uses() As List(Of Page)
            Get
                Return _Uses
            End Get
        End Property

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Private Shared Sub WriteFile(ByVal fileName As String, ByVal stream As Stream)
            If Not Directory.Exists(Path.GetDirectoryName(fileName)) _
                Then Directory.CreateDirectory(Path.GetDirectoryName(fileName))

            Dim fileStream As FileStream = File.OpenWrite(fileName)
            Dim buffer(255) As Byte, size As Integer
            stream.Seek(0, SeekOrigin.Begin)

            Do
                size = stream.Read(buffer, 0, buffer.Length)
                fileStream.Write(buffer, 0, size)
            Loop While size > 0

            fileStream.Close()
        End Sub

    End Class

    Public Class MediaRevision

        Private _Comment As String
        Private _Depth As Integer
        Private _Hash As String
        Private _Height As Integer
        Private _IsRevert As Boolean
        Private _Media As Media
        Private _Metadata As New Dictionary(Of String, String)
        Private _Size As Integer
        Private _Time As Date
        Private _Type As String
        Private _Url As Uri
        Private _User As User
        Private _Width As Integer

        Public Sub New(ByVal media As Media, ByVal time As Date)
            _Media = media
            _Time = time
        End Sub

        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Set(ByVal value As String)
                _Comment = value
            End Set
        End Property

        Public Property Depth() As Integer
            Get
                Return _Depth
            End Get
            Set(ByVal value As Integer)
                _Depth = value
            End Set
        End Property

        Public Property Hash() As String
            Get
                Return _Hash
            End Get
            Set(ByVal value As String)
                _Hash = value
            End Set
        End Property

        Public Property Height() As Integer
            Get
                Return _Height
            End Get
            Set(ByVal value As Integer)
                _Height = value
            End Set
        End Property

        Public ReadOnly Property IsRevert() As Boolean
            Get
                Return _IsRevert
            End Get
        End Property

        Public ReadOnly Property Media() As Media
            Get
                Return _Media
            End Get
        End Property

        Public ReadOnly Property Metadata() As Dictionary(Of String, String)
            Get
                Return _Metadata
            End Get
        End Property

        Public Property Size() As Integer
            Get
                Return _Size
            End Get
            Set(ByVal value As Integer)
                _Size = value
            End Set
        End Property

        Public ReadOnly Property Time() As Date
            Get
                Return _Time
            End Get
        End Property

        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Public Property Url() As Uri
            Get
                Return _Url
            End Get
            Set(ByVal value As Uri)
                _Url = value
            End Set
        End Property

        Public Property User() As User
            Get
                Return _User
            End Get
            Set(ByVal value As User)
                _User = value
            End Set
        End Property

        Public Property Width() As Integer
            Get
                Return _Width
            End Get
            Set(ByVal value As Integer)
                _Width = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Media.Name & ", " & Time.ToLongDateString & " " & Time.ToLongTimeString
        End Function

        Public Sub Process()
            If Comment = Media.Wiki.Message("reverted") Then
                _IsRevert = True
            Else
                For Each Pattern As Regex In Media.Wiki.Config.RevertPatterns
                    If Pattern.IsMatch(Comment) Then
                        _IsRevert = True
                        Exit For
                    End If
                Next Pattern
            End If
        End Sub

    End Class

    Public Class MediaCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Page, Media)

        Private _Total As Integer = -1

        Public Sub New(ByVal Wiki As Wiki)
            Me.Wiki = Wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Page, Media)
            Get
                Return _All
            End Get
        End Property

        Public Property Total() As Integer
            Get
                Return _Total
            End Get
            Set(ByVal value As Integer)
                _Total = value
            End Set
        End Property

        Public Function FromString(ByVal name As String) As Media
            name = Wiki.Pages.SanitizeTitle(name)
            If name Is Nothing Then Return Nothing
            Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.File, name))
        End Function

        Default Public ReadOnly Property Item(ByVal name As String) As Media
            Get
                Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.File, name))
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal page As Page) As Media
            Get
                If page Is Nothing OrElse page.Space IsNot Wiki.Spaces.File Then Return Nothing
                If Not All.ContainsKey(page) Then All.Add(page, New Media(Wiki, page))
                Return All(page)
            End Get
        End Property

    End Class

End Namespace
