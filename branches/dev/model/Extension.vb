Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Friend Class Extension

        'Represents a MediaWiki extension

        Private ReadOnly _Name As String
        Private ReadOnly _Wiki As Wiki

        Friend Const AbuseFilter As String = "Abuse Filter"
        Friend Const Gadgets As String = "Gadgets"
        Friend Const GlobalBlocking As String = "GlobalBlocking"
        Friend Const Moderation As String = "Flagged Revisions"
        Friend Const OpenSearch As String = "OpenSearchXml"
        Friend Const SiteMatrix As String = "SiteMatrix"
        Friend Const SpamList As String = "SpamBlacklist"
        Friend Const TitleList As String = "Title Blacklist"
        Friend Const UnifiedLogin As String = "Central Auth"

        Friend Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
            _Wiki = wiki
            Type = "other"
        End Sub

        Friend Property Author() As String

        Friend Property Description() As String

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend Property Type() As String

        Friend Property Url() As Uri

        Friend Property Version() As String

        Friend ReadOnly Property Wiki() As Wiki
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

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All() As IList(Of Extension)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Friend ReadOnly Property Contains(ByVal name As String) As Boolean
            Get
                Return _All.ContainsKey(name)
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal name As String) As Extension
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New Extension(Wiki, name))
                Return _All(name)
            End Get
        End Property

        Friend Sub Clear()
            _All.Clear()
        End Sub

    End Class

End Namespace
