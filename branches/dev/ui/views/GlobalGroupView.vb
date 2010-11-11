Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class GlobalGroupView : Inherits Viewer

        Private Family As Family

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            Family = session.Wiki.Family
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Display()
        End Sub

        Private Sub Display()
            Dim rows As New List(Of String())

            For Each group As GlobalGroup In Family.GlobalGroups.All
                rows.Add({group.DisplayName})
            Next group

            GroupList.SetItems(rows)

            Count.Text = Msg("a-count", GroupList.Items.Count)
        End Sub

        Private Sub GroupList_SelectedIndexChanged() Handles GroupList.SelectedIndexChanged
            SubSplitter.Visible = (GroupList.SelectedItems.Count > 0)
            If GroupList.SelectedItems.Count = 0 Then Return

            Dim selectedGroup As GlobalGroup = Family.GlobalGroups(GroupList.Items(GroupList.SelectedIndices(0)).Text)
            GroupName.Text = selectedGroup.DisplayName

            Dim rightRows As New List(Of String())

            If selectedGroup.Rights IsNot Nothing Then
                For Each right As String In selectedGroup.Rights
                    rightRows.Add({right})
                Next right
            End If

            RightsList.SetItems(rightRows)
            RightsCount.Text = Msg("a-count", RightsList.Items.Count)

            If selectedGroup.Applicability = GlobalGroupApplicability.All Then
                SubSplitter.Panel2Collapsed = True
                WikiCount.Visible = False
                Applicability.Text = Msg("view-globalgroup-all")
            Else
                SubSplitter.Panel2Collapsed = False
                WikiCount.Visible = True
                If selectedGroup.Applicability = GlobalGroupApplicability.Exclusive _
                    Then Applicability.Text = Msg("view-globalgroup-excludes") _
                    Else Applicability.Text = Msg("view-globalgroup-includes")

                Dim wikiRows As New List(Of String())

                If selectedGroup.Wikis IsNot Nothing Then
                    For Each wiki As Wiki In selectedGroup.Wikis
                        wikiRows.Add({wiki.Name})
                    Next wiki
                End If

                WikiList.SetItems(wikiRows)
                WikiCount.Text = Msg("a-count", WikiList.Items.Count)
            End If
        End Sub

    End Class

End Namespace
