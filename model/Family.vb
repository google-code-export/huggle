﻿Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")> _
    Public Class Family

        Private ReadOnly _Code As String

        Private _Blocks As New Dictionary(Of Integer, GlobalBlock)
        Private _Config As FamilyConfig
        Private _GlobalGroups As GlobalGroupCollection
        Private _GlobalUsers As GlobalUserCollection
        Private _Wikis As FamilyWikiCollection

        Public Sub New(ByVal code As String)
            _Code = code
            _Name = code
        End Sub

        Public Property ActiveGlobalUser() As GlobalUser

        Public ReadOnly Property Blocks() As Dictionary(Of Integer, GlobalBlock)
            Get
                Return _Blocks
            End Get
        End Property

        Public Property CentralWiki() As Wiki

        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Public ReadOnly Property Config() As FamilyConfig
            Get
                If _Config Is Nothing Then _Config = New FamilyConfig(Me)

                Return _Config
            End Get
        End Property

        Public Property Feed() As Feed

        Public Property FileWiki() As Wiki

        Public ReadOnly Property GlobalGroups() As GlobalGroupCollection
            Get
                If _GlobalGroups Is Nothing Then _GlobalGroups = New GlobalGroupCollection(Me)
                Return _GlobalGroups
            End Get
        End Property

        Public Property GlobalTitleBlacklist As TitleBlacklist

        Public ReadOnly Property GlobalUsers() As GlobalUserCollection
            Get
                If _GlobalUsers Is Nothing Then _GlobalUsers = New GlobalUserCollection(Me)
                Return _GlobalUsers
            End Get
        End Property

        Public Property Name() As String

        Public ReadOnly Property Wikis() As FamilyWikiCollection
            Get

                If _Wikis Is Nothing Then _Wikis = New FamilyWikiCollection(Me)
                Return _Wikis
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Public Class FamilyCollection

        Private ReadOnly _All As New Dictionary(Of String, Family)
        Private ReadOnly _Wikimedia As Family

        Public Sub New()
            _Wikimedia = Item("wikimedia")
        End Sub

        Public ReadOnly Property All() As IList(Of Family)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal code As String) As Family
            Get
                If Not _All.ContainsKey(code) Then _All.Add(code, New Family(code))
                Return _All(code)
            End Get
        End Property

        Public ReadOnly Property Wikimedia As Family
            Get
                Return _Wikimedia
            End Get
        End Property

    End Class

    Public Class FamilyWikiCollection

        Private _Default As Wiki
        Private Family As Family

        Public Sub New(ByVal family As Family)
            _Default = App.Wikis(family.Code & "-default")
            _Default.Family = family
            _Default.IsDefault = True
        End Sub

        Public ReadOnly Property All() As IList(Of Wiki)
            Get
                Dim result As New List(Of Wiki)

                For Each wiki As Wiki In App.Wikis.All
                    If wiki.Family Is Family Then result.Add(wiki)
                Next wiki

                Return result.AsReadOnly
            End Get
        End Property

        Public ReadOnly Property [Default]() As Wiki
            Get
                Return _Default
            End Get
        End Property

    End Class

End Namespace