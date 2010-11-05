Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class UserGroupView : Inherits Viewer

        Private _SelectedGroup As UserGroup

        Friend Event ViewRight As SimpleEventHandler(Of String)

        Friend Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Friend Property SelectedGroup As UserGroup
            Get
                Return _SelectedGroup
            End Get
            Set(ByVal value As UserGroup)
                If Wiki.UserGroups.All.Contains(value) Then GroupList.SelectedValue = value.Description
            End Set
        End Property

        Friend Property SelectedRight As String
            Get
                Return RightsList.SelectedValue
            End Get
            Set(ByVal value As String)
                If SelectedGroup IsNot Nothing AndAlso SelectedGroup.Rights.Contains(value) _
                    Then RightsList.SelectedValue = value
            End Set
        End Property

        Private Sub _Load() Handles Me.Load
            GroupList.BeginUpdate()
            GroupList.Items.Clear()

            For Each group As UserGroup In Wiki.UserGroups.All
                Dim groupCountString As String

                If group.IsImplicit Then groupCountString = Msg("view-usergroup-implicit") _
                    Else groupCountString = If(group.Count < 0, Msg("a-unknown"), group.Count.ToStringForUser)

                GroupList.AddRow(group.Description, groupCountString)
            Next group

            GroupList.SortMethods(1) = SortMethod.Integer
            GroupList.SortBy(0)
            GroupList.SelectedIndices.Add(0)
            GroupList.EndUpdate()

            GroupCount.Text = Msg("a-count", GroupList.Items.Count)
        End Sub

        Private Sub GroupList_SelectedIndexChanged() Handles GroupList.SelectedIndexChanged
            _SelectedGroup = Nothing

            For Each group As UserGroup In Wiki.UserGroups.All
                If group.Description = GroupList.SelectedValue Then
                    _SelectedGroup = group
                    Exit For
                End If
            Next group

            If SelectedGroup Is Nothing Then Return

            GroupName.Text = SelectedGroup.Description

            RightsList.BeginUpdate()
            RightsList.Items.Clear()

            For Each right As String In SelectedGroup.Rights
                RightsList.AddRow(right)
            Next right

            RightsCount.Text = Msg("a-count", RightsList.Items.Count)
            RightsList.EndUpdate()
        End Sub

        Private Sub RightsList_DoubleClick() Handles RightsList.DoubleClick
            If RightsList.SelectedValue IsNot Nothing _
                Then RaiseEvent ViewRight(Me, New EventArgs(Of String)(RightsList.SelectedValue))
        End Sub

    End Class

End Namespace