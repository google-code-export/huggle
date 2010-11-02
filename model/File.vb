Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class File

        Private _Duplicates As New List(Of File)
        Private _GlobalUses As New List(Of Page)
        Private _Page As Page
        Private _Revisions As New List(Of FileRevision)
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
                    If IO.File.Exists(FilePath) Then Return New MemoryStream(IO.File.ReadAllBytes(FilePath))
                Catch ex As IOException
                    Log.Write("Failed to load file '{0}'".FormatI(Name))
                End Try

                Return Nothing
            End Get
            Set(ByVal value As Stream)
                Try
                    WriteFile(FilePath, value)
                Catch ex As IOException
                    Log.Write("Failed to save file '{0}'".FormatI(Name))
                End Try
            End Set
        End Property

        Public ReadOnly Property ContentKnown() As Boolean
            Get
                Return IO.File.Exists(FilePath)
            End Get
        End Property

        Public Property DetailsKnown() As Boolean

        Public ReadOnly Property Duplicates() As List(Of File)
            Get
                Return _Duplicates
            End Get
        End Property

        Public Property Exists() As Boolean

        Private ReadOnly Property FilePath() As String
            Get
                Return PathCombine(Config.BaseLocation, "media", GetValidFileName(Wiki.Code & "-" & Name))
            End Get
        End Property

        Public Property FirstRevision() As FileRevision

        Public ReadOnly Property GlobalUses() As List(Of Page)
            Get
                Return _GlobalUses
            End Get
        End Property

        Public Property IsShared() As Boolean
        Public Property LastRevision() As FileRevision

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

        Public ReadOnly Property Revisions() As List(Of FileRevision)
            Get
                Return _Revisions
            End Get
        End Property

        Public Property Thumb(ByVal size As Integer) As Stream
            Get
                Try
                    If IO.File.Exists(ThumbPath(size)) Then Return New MemoryStream(IO.File.ReadAllBytes(ThumbPath(size)))
                Catch ex As IOException
                    Log.Write("Failed to load file '{0}'".FormatI(Name))
                End Try

                Return Nothing
            End Get
            Set(ByVal value As Stream)
                Try
                    WriteFile(ThumbPath(size), value)
                Catch ex As IOException
                    Log.Write("Failed to save file '{0}'".FormatI(Name))
                End Try
            End Set
        End Property

        Public ReadOnly Property ThumbKnown(ByVal size As Integer) As Boolean
            Get
                Return (IO.File.Exists(ThumbPath(size)))
            End Get
        End Property

        Public ReadOnly Property ThumbPath(ByVal size As Integer) As String
            Get
                Return PathCombine(Config.BaseLocation, "media", "thumbs",
                    GetValidFileName(Wiki.Code & "-" & ThumbName(size)))
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
                Return Name.FromLast(".").ToLowerI
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

            Using fileStream As FileStream = IO.File.OpenWrite(fileName)
                Dim buffer(255) As Byte, size As Integer
                stream.Seek(0, SeekOrigin.Begin)

                Do
                    size = stream.Read(buffer, 0, buffer.Length)
                    fileStream.Write(buffer, 0, size)
                Loop While size > 0
            End Using
        End Sub

    End Class

    Public Class FileRevision

        Private _File As File
        Private _IsRevert As Boolean
        Private _Metadata As New Dictionary(Of String, String)
        Private _Time As Date

        Public Sub New(ByVal file As File, ByVal time As Date)
            _File = file
            _Time = time
        End Sub

        Public Property Comment() As String
        Public Property Depth() As Integer
        Public Property Hash() As String
        Public Property Height() As Integer

        Public ReadOnly Property IsRevert() As Boolean
            Get
                Return _IsRevert
            End Get
        End Property

        Public ReadOnly Property Media() As File
            Get
                Return _File
            End Get
        End Property

        Public ReadOnly Property Metadata() As Dictionary(Of String, String)
            Get
                Return _Metadata
            End Get
        End Property

        Public Property Size() As Integer

        Public ReadOnly Property Time() As Date
            Get
                Return _Time
            End Get
        End Property

        Public Property Type() As String
        Public Property Url() As Uri
        Public Property User() As User
        Public Property Width() As Integer

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

    Public Class FileCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of Page, File)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As Dictionary(Of Page, File)
            Get
                Return _All
            End Get
        End Property

        Public Property Count() As Integer = -1

        Public Function FromString(ByVal name As String) As File
            name = Wiki.Pages.SanitizeTitle(name)
            If name Is Nothing Then Return Nothing
            Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.File, name))
        End Function

        Default Public ReadOnly Property Item(ByVal name As String) As File
            Get
                Return Item(Wiki.Pages.FromNsAndName(Wiki.Spaces.File, name))
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal page As Page) As File
            Get
                If page Is Nothing OrElse page.Space IsNot Wiki.Spaces.File Then Return Nothing
                If Not All.ContainsKey(page) Then All.Add(page, New File(Wiki, page))
                Return All(page)
            End Get
        End Property

    End Class

End Namespace
