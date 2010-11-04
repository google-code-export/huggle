Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class FamilyPropertiesForm : Inherits HuggleForm

        Private LoadedViews As New List(Of Viewer)
        Private Session As Session

        Public Sub New(ByVal session As Session)
            InitializeComponent()
            Size = New Size(720, 480)
            Me.Session = session
        End Sub

        Private ReadOnly Property Family As Family
            Get
                Return Session.Wiki.Family
            End Get
        End Property

        Private Sub _Load() Handles Me.Load
            Try
                Icon = Resources.Icon
                Text = Msg("view-family-title", Family.Name)
                App.Languages.Current.Localize(Me)

                'Core MediaWiki views
                Views.Items.AddRange({
                    Msg("view-familygeneral-title"),
                    Msg("view-globalgroup-title")})

                Views.SelectedIndex = 0

            Catch ex As SystemException
                App.ShowError(Result.FromException(ex))
                Close()
            End Try
        End Sub

        Private Sub Views_SelectedIndexChanged() Handles Views.SelectedIndexChanged
            Select Case Views.SelectedItem.ToString
                Case Msg("view-familygeneral-title")
                    If Not LoadedViews.ContainsInstance(Of GeneralFamilyView)() Then LoadedViews.Add(New GeneralFamilyView(Session))
                    ViewInstance(Of GeneralFamilyView)()

                Case Msg("view-globalgroup-title")
                    If Not LoadedViews.ContainsInstance(Of GlobalGroupView)() Then LoadedViews.Add(New GlobalGroupView(Session))
                    ViewInstance(Of GlobalGroupView)()
            End Select
        End Sub

        Private Sub ViewInstance(Of T As Viewer)()
            ViewContainer.Controls.Clear()
            ViewContainer.Controls.Add(LoadedViews.FirstInstance(Of T))
        End Sub

        Private Sub Views_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles Views.DrawItem
            e.DrawBackground()
            e.DrawFocusRectangle()

            TextRenderer.DrawText(e.Graphics, Views.Items(e.Index).ToString,
                Views.Font, e.Bounds, e.ForeColor, e.BackColor, TextFormatFlags.VerticalCenter)
        End Sub

    End Class

End Namespace