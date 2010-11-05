Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{ToString()}")> _
    Friend Class Space

        'Represents a MediaWiki namespace

        Private _Aliases As New List(Of String)
        Private _Number As Integer
        Private _Name As String
        Private _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal number As Integer)
            _Wiki = wiki
            _Number = number
            _IsMovable = True
            _IsSpecial = (number < 0)
            _HasSubpages = True
        End Sub

        Friend ReadOnly Property Aliases() As List(Of String)
            Get
                Return _Aliases
            End Get
        End Property

        Friend Property CanonicalName() As String
        Friend Property HasSubpages() As Boolean

        Friend ReadOnly Property IsArticleSpace() As Boolean
            Get
                Return (Number = 0)
            End Get
        End Property

        Friend Property IsContent() As Boolean

        Friend ReadOnly Property IsCustom() As Boolean
            Get
                Return (Number >= 16)
            End Get
        End Property

        Friend Property IsEditRestricted() As Boolean
        Friend Property IsMovable() As Boolean
        Friend Property IsMoveRestricted() As Boolean
        Friend Property IsSpecial() As Boolean

        Friend ReadOnly Property IsSubjectSpace() As Boolean
            Get
                Return (Number Mod 2 = 0) AndAlso Not IsSpecial
            End Get
        End Property

        Friend ReadOnly Property IsTalkSpace() As Boolean
            Get
                Return (Number Mod 2 = 1) AndAlso Not IsSpecial
            End Get
        End Property

        Friend Property Name() As String
            Get
                If _Name Is Nothing Then Return _CanonicalName Else Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Friend ReadOnly Property Number() As Integer
            Get
                Return _Number
            End Get
        End Property

        Friend ReadOnly Property Wiki() As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Friend ReadOnly Property SubjectSpace() As Space
            Get
                If IsSpecial Then Return Nothing
                If IsTalkSpace Then Return Wiki.Spaces(Number - 1) Else Return Me
            End Get
        End Property

        Friend ReadOnly Property TalkSpace() As Space
            Get
                If IsSpecial Then Return Nothing
                If IsTalkSpace Then Return Me Else Return Wiki.Spaces(Number + 1)
            End Get
        End Property

        Public Shared Operator =(ByVal a As Space, ByVal b As Space) As Boolean
            Return (a.Wiki Is b.Wiki AndAlso a.Number = b.Number)
        End Operator

        Public Shared Operator <>(ByVal a As Space, ByVal b As Space) As Boolean
            Return (a.Wiki Is b.Wiki AndAlso a.Number = b.Number)
        End Operator

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TypeOf obj Is Space AndAlso DirectCast(obj, Space) = Me)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Wiki.GetHashCode Xor Number.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            If _Name IsNot Nothing Then Return _Name Else Return _Number.ToStringI
        End Function

    End Class

    Friend Class SpaceCollection

        Private _IsDefault As Boolean
        Private Wiki As Wiki

        Private ReadOnly _All As New Dictionary(Of Integer, Space)

        'Built-in namespaces
        Friend ReadOnly Media As Space
        Friend ReadOnly Special As Space
        Friend ReadOnly Article As Space
        Friend ReadOnly Talk As Space
        Friend ReadOnly User As Space
        Friend ReadOnly UserTalk As Space
        Friend ReadOnly Project As Space
        Friend ReadOnly ProjectTalk As Space
        Friend ReadOnly File As Space
        Friend ReadOnly FileTalk As Space
        Friend ReadOnly MediaWiki As Space
        Friend ReadOnly MediaWikiTalk As Space
        Friend ReadOnly Template As Space
        Friend ReadOnly TemplateTalk As Space
        Friend ReadOnly Help As Space
        Friend ReadOnly HelpTalk As Space
        Friend ReadOnly Category As Space
        Friend ReadOnly CategoryTalk As Space

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki

            For i As Integer = -2 To 15
                _All.Add(i, New Space(Me.Wiki, i))
            Next i

            Media = Me(-2)
            Special = Me(-1)
            Article = Me(-0)
            Talk = Me(1)
            User = Me(2)
            UserTalk = Me(3)
            Project = Me(4)
            ProjectTalk = Me(5)
            File = Me(6)
            FileTalk = Me(7)
            MediaWiki = Me(8)
            MediaWikiTalk = Me(9)
            Template = Me(10)
            TemplateTalk = Me(11)
            Help = Me(12)
            HelpTalk = Me(13)
            Category = Me(14)
            CategoryTalk = Me(15)

            If wiki Is App.Wikis.Default Then
                Media.CanonicalName = "Media"
                Special.CanonicalName = "Special"
                Article.CanonicalName = ""
                Talk.CanonicalName = "Talk"
                User.CanonicalName = "User"
                UserTalk.CanonicalName = "User talk"
                Project.CanonicalName = "Project"
                ProjectTalk.CanonicalName = "Project talk"
                File.CanonicalName = "File"
                FileTalk.CanonicalName = "File talk"
                MediaWiki.CanonicalName = "MediaWiki"
                MediaWikiTalk.CanonicalName = "MediaWiki talk"
                Template.CanonicalName = "Template"
                TemplateTalk.CanonicalName = "Template talk"
                Help.CanonicalName = "Help"
                HelpTalk.CanonicalName = "Help talk"
                Category.CanonicalName = "Category"
                CategoryTalk.CanonicalName = "Category talk"
            Else
                For Each space As Space In All
                    space.CanonicalName = App.Wikis.Default.Spaces(space.Number).CanonicalName
                Next space
            End If

            'Default namespace attributes
            MediaWiki.IsEditRestricted = True
            MediaWiki.IsMoveRestricted = True
            Category.IsMovable = False
            File.IsMoveRestricted = True

            _IsDefault = True
        End Sub

        Friend ReadOnly Property All() As IEnumerable(Of Space)
            Get
                Return _All.Values
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal number As Integer) As Space
            Get
                If Not _All.ContainsKey(number) Then _All.Add(number, New Space(Wiki, number))
                Return _All(number)
            End Get
        End Property

        Default Friend ReadOnly Property Item(ByVal name As String) As Space
            Get
                If name Is Nothing Then Return Nothing

                For Each space As Space In All
                    If space.Name IsNot Nothing AndAlso space.Name.EqualsIgnoreCase(name) Then Return space
                    If space.Aliases.Contains(name) Then Return space
                Next space

                Return Nothing
            End Get
        End Property

        Friend Function FromTitle(ByVal title As String) As Space
            If title Is Nothing Then Return Nothing

            If Not title.Contains(":") Then Return Article
            Dim space As Space = Item(title.ToFirst(":"))
            If space Is Nothing Then Return Article
            Return space
        End Function

    End Class

End Namespace