﻿Imports System
Imports System.Security.Cryptography
Imports System.Text

Namespace Huggle.Actions

    Public Class MediaQuery : Inherits Query

        Private _Media As Media
        Private _ThumbSize As Integer

        Public Sub New(ByVal session As Session, ByVal media As Media)
            MyBase.New(session, Msg("media-desc", media))
            _Media = media
        End Sub

        Public Sub New(ByVal session As Session, ByVal media As Media, ByVal thumbSize As Integer)
            MyBase.New(session, Msg("mediathumb-desc", media, thumbSize))
            _Media = media
            _ThumbSize = thumbSize
        End Sub

        Public ReadOnly Property Media() As Media
            Get
                Return _Media
            End Get
        End Property

        Public ReadOnly Property ThumbSize() As Integer
            Get
                Return _ThumbSize
            End Get
        End Property

        Public Overrides Sub Start()
            OnProgress(Msg("media-progress", Media))
            OnStarted()

            'Upload location is determined by MD5 hash of file name
            Dim hash As String = BitConverter.ToString(MD5.Create.ComputeHash _
                (Encoding.UTF8.GetBytes(Media.Name))).Remove("-").ToLower
            Dim url As Uri

            If ThumbSize > 0 Then url = New Uri(Wiki.FileUrl.ToString & "thumb/" & hash(0) & "/" & _
                hash(0) & hash(1) & "/" & Media.Name & "/" & Media.ThumbName(ThumbSize)) _
                Else url = New Uri(Wiki.FileUrl.ToString & hash(0) & "/" & hash(0) & hash(1) & "/" & Media.Name)

            Dim req As New FileRequest(Session, url)
            req.Start()
            If req.IsFailed Then OnFail(req.Result)

            If req.File IsNot Nothing Then
                If ThumbSize = 0 Then Media.Content = req.File Else Media.Thumb(ThumbSize) = req.File
            End If

            OnSuccess()
        End Sub

    End Class

End Namespace