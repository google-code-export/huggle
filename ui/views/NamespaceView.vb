Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class NamespaceView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            List.BeginUpdate()
            List.Items.Clear()

            For Each space As Space In Wiki.Spaces.All
                Dim props As New List(Of String)
                If space.IsSpecial Then props.Add(Msg("view-namespace-special"))
                If space.IsContent Then props.Add(Msg("view-namespace-content"))
                If space.IsCustom Then props.Add(Msg("view-namespace-custom"))
                If space.IsEditRestricted Then props.Add(Msg("view-namespace-restricted"))
                If space.IsMoveRestricted Then props.Add(Msg("view-namespace-moverestricted"))
                If Not space.IsMovable Then props.Add(Msg("view-namespace-unmovable"))
                If space.IsTalkSpace Then props.Add(Msg("view-namespace-discussion"))
                If space.HasSubpages Then props.Add(Msg("view-namespace-subpages"))
                List.AddRow(space.Number.ToStringForUser, space.Name, props.Join(", "))
            Next space

            List.SortMethods.Add(0, SortMethod.Integer)
            List.SortBy(0)
            List.EndUpdate()

            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

    End Class

End Namespace