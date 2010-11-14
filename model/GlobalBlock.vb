Namespace Huggle

    Friend Class GlobalBlock : Inherits LogItem

        'Represents a global block made with MediaWiki's GlobalBlocking extension

        Private _IsRangeblock As Boolean
        Private _Target As String

        Public Sub New(ByVal id As Integer, ByVal target As String, ByVal wiki As Wiki)
            MyBase.New(id, wiki)

            _IsRangeblock = RangePattern.IsMatch(target)
            _Target = target
        End Sub

        Public ReadOnly Property Duration As String
            Get
                Return FullFuzzyAge(Time, Expires)
            End Get
        End Property

        Public Property Expires() As Date

        Public ReadOnly Property Family() As Family
            Get
                Return Wiki.Family
            End Get
        End Property

        Public Property IsAnonymousOnly As Boolean

        Public ReadOnly Property IsRangeblock As Boolean
            Get
                Return _IsRangeblock
            End Get
        End Property

        Public Overrides ReadOnly Property Target() As String
            Get
                Return _Target
            End Get
        End Property

    End Class

    Friend Class GlobalBlockLocalOverride : Inherits LogItem

        Private _Target As String

        Public Sub New(ByVal id As Integer, ByVal target As String, ByVal wiki As Wiki)
            MyBase.New(id, wiki)

            _Target = target
        End Sub

        Public Property IsEnable As Boolean

        Public Overrides ReadOnly Property Target As String
            Get
                Return _Target
            End Get
        End Property

    End Class

End Namespace