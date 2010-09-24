Imports Huggle
Imports System.Web.HttpUtility

Public Class GadgetView : Inherits Viewer

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        InitializeComponent()
    End Sub

    Private Current As Gadget

    Private Sub _Load() Handles Me.Load
        App.Languages.Current.Localize(Me)

        List.BeginUpdate()
        List.Items.Clear()

        For Each gadget As Gadget In Wiki.Gadgets.All
            List.AddRow(gadget.Name, If(gadget.TypeDesc, gadget.Type), _
                WikiStripSummary(HtmlDecode(gadget.Description)))
        Next gadget

        List.EndUpdate()
        List.SortBy(0)
        Count.Text = Msg("a-count", List.Items.Count)
    End Sub

    Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
        If List.SelectedItems.Count = 0 Then
            Current = Nothing
        Else
            Current = Wiki.Gadgets.FromName(List.SelectedItems(0).Text)

            GadgetName.Text = Current.Name
            GadgetType.Text = Msg("wikiprop-gadget-type", If(Current.TypeDesc, Current.Type))
            GadgetDescription.Text = Current.Description

            GadgetImage.Visible = True
            GadgetProps.Visible = True
        End If
    End Sub

    Private Sub GadgetName_LinkClicked() Handles GadgetName.LinkClicked
        If Current.Pages.Count > 0 Then OpenWebBrowser(Current.Pages(0).Url)
    End Sub

End Class
