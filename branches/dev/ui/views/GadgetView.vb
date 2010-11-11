Imports System.Collections.Generic
Imports System.Web.HttpUtility

Namespace Huggle.UI

    Friend Class GadgetView : Inherits Viewer

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Private Current As Gadget

        Private Sub _Load() Handles Me.Load
            App.Languages.Current.Localize(Me)

            Dim rows As New List(Of String())

            For Each gadget As Gadget In Wiki.Gadgets.All
                rows.Add({gadget.Name, If(gadget.TypeDesc, gadget.Type),
                    WikiStripSummary(HtmlDecode(gadget.Description))})
            Next gadget

            List.SetItems(rows)
            Count.Text = Msg("a-count", List.Items.Count)
        End Sub

        Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
            If Not List.HasSelectedItem Then
                Current = Nothing
            Else
                Current = Wiki.Gadgets.FromName(List.SelectedValue)

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

End Namespace