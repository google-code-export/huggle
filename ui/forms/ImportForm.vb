Namespace Huggle.UI

    Friend Class ImportForm : Inherits HuggleForm

        Private Sub _Load() Handles Me.Load
            SourceWikiInput.Items.AddRange(App.Wikis.All.ToArray)
            DestWikiInput.Items.AddRange(App.Wikis.All.ToArray)
        End Sub

        Public ReadOnly Property Attribution() As Boolean
            Get
                Return AttributeLink.Checked
            End Get
        End Property

        Public ReadOnly Property DestPage() As Page
            Get
                If DestWiki Is Nothing Then Return Nothing Else Return DestWiki.Pages.FromString(DestPageInput.Text)
            End Get
        End Property

        Public ReadOnly Property DestWiki() As Wiki
            Get
                Return CType(DestWikiInput.SelectedItem, Wiki)
            End Get
        End Property

        Public ReadOnly Property History() As Boolean
            Get
                Return AllContent.Checked
            End Get
        End Property

        Public ReadOnly Property SourcePage() As Page
            Get
                If SourceWiki Is Nothing Then Return Nothing Else Return SourceWiki.Pages.FromString(SourcePageInput.Text)
            End Get
        End Property

        Public ReadOnly Property SourceWiki() As Wiki
            Get
                Return CType(SourceWikiInput.SelectedItem, Wiki)
            End Get
        End Property

    End Class

End Namespace