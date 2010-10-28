Imports Huggle.Actions
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class AssessForm : Inherits HuggleForm

        Private _Page As Page

        Private Session As Session

        Public Sub New(ByVal session As Session)
            Me.Session = session
            InitializeComponent()
        End Sub

        Public ReadOnly Property Page As Page
            Get
                Return _Page
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Icon = Resources.Icon
        End Sub

        Private Sub OK_Click() Handles OK.Click
            Dim ratings As New List(Of String)({Rating1.Text, Rating2.Text, Rating3.Text, Rating4.Text})

            Dim action As New Assess(Session, Page, ratings)
            App.UserWaitForProcess(action)

            DialogResult = DialogResult.OK
            Close()
        End Sub

        Private Sub Cancel_Click() Handles Cancel.Click
            DialogResult = DialogResult.Cancel
            Close()
        End Sub

        Private Sub Page_TextChanged() Handles PageInput.TextChanged
            _Page = Session.Wiki.Pages.FromString(PageInput.Text)
            OK.Enabled = (Page IsNot Nothing)
        End Sub

    End Class

End Namespace