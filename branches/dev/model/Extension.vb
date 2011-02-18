﻿Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class Extension

        'Represents a MediaWiki extension

        Private ReadOnly _Name As String
        Private ReadOnly _Wiki As Wiki

        Public Const AbuseFilter As String = "Abuse Filter"
        Public Const Gadgets As String = "Gadgets"
        Public Const GlobalBlocking As String = "GlobalBlocking"
        Public Const Moderation As String = "Flagged Revisions"
        Public Const OpenSearch As String = "OpenSearchXml"
        Public Const SiteMatrix As String = "SiteMatrix"
        Public Const SpamList As String = "SpamBlacklist"
        Public Const TitleList As String = "Title Blacklist"
        Public Const UnifiedLogin As String = "Central Auth"

        Public Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
            _Wiki = wiki
            Type = "other"
        End Sub

        Public Property Author() As String

        Public Property Description() As String

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public Property Type() As String

        Public Property Url() As Uri

        Public Property Version() As String

        Public ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return _Name
        End Function

    End Class

    Friend Class ExtensionCollection

        Private Wiki As Wiki
        Private ReadOnly _All As New Dictionary(Of String, Extension)

        Public Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Public ReadOnly Property All() As IList(Of Extension)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal name As String) As Boolean
            Get
                Return _All.ContainsKey(name)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal name As String) As Extension
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New Extension(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Public Sub Clear()
            _All.Clear()
        End Sub

    End Class

End Namespace