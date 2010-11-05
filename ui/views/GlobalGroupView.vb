Imports Huggle.Actions
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class GlobalGroupView : Inherits Viewer

        Private Family As Family

        Friend Sub New(ByVal session As Session)
            MyBase.New(session)
            Family = session.Wiki.Family
            InitializeComponent()
        End Sub

        Private Sub _Load() Handles Me.Load
            Display()
        End Sub

        Private Sub Display()
            GroupList.BeginUpdate()
            GroupList.Items.Clear()

            For Each group As GlobalGroup In Family.GlobalGroups.All
                GroupList.AddRow(group.DisplayName)
            Next group

            GroupList.SortBy(0)
            GroupList.EndUpdate()

            Count.Text = Msg("a-count", GroupList.Items.Count)
        End Sub

        Private Sub GroupList_SelectedIndexChanged() Handles GroupList.SelectedIndexChanged
            SubSplitter.Visible = (GroupList.SelectedItems.Count > 0)
            If GroupList.SelectedItems.Count = 0 Then Return

            Dim selectedGroup As GlobalGroup = Family.GlobalGroups(GroupList.Items(GroupList.SelectedIndices(0)).Text)
            GroupName.Text = selectedGroup.DisplayName

            RightsList.BeginUpdate()
            RightsList.Items.Clear()

            If selectedGroup.Rights IsNot Nothing Then
                For Each right As String In selectedGroup.Rights
                    RightsList.AddRow(right)
                Next right
            End If

            RightsList.SortBy(0)
            RightsList.EndUpdate()
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

                WikiList.BeginUpdate()
                WikiList.Items.Clear()

                If selectedGroup.Wikis IsNot Nothing Then
                    For Each wiki As Wiki In selectedGroup.Wikis
                        WikiList.AddRow(wiki.Name)
                    Next wiki
                End If

                WikiList.SortBy(0)
                WikiList.EndUpdate()
                WikiCount.Text = Msg("a-count", WikiList.Items.Count)
            End If
        End Sub

    End Class

End Namespace
