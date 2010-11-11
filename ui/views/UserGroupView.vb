Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Friend Class UserGroupView : Inherits Viewer

        Private _SelectedGroup As UserGroup

        Public Event ViewRight As SimpleEventHandler(Of String)

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
            InitializeComponent()
        End Sub

        Public Property SelectedGroup As UserGroup
            Get
                Return _SelectedGroup
            End Get
            Set(ByVal value As UserGroup)
                If Wiki.UserGroups.All.Contains(value) Then GroupList.SelectedValue = value.Description
            End Set
        End Property

        Public Property SelectedRight As String
            Get
                Return RightsList.SelectedValue
            End Get
            Set(ByVal value As String)
                If SelectedGroup IsNot Nothing AndAlso SelectedGroup.Rights.Contains(value) _
                    Then RightsList.SelectedValue = value
            End Set
        End Property

        Private Sub _Load() Handles Me.Load
            Dim rows As New List(Of String())

            For Each group As UserGroup In Wiki.UserGroups.All
                Dim groupCountString As String

                If group.IsImplicit Then groupCountString = Msg("view-usergroup-implicit") _
                    Else groupCountString = If(group.Count < 0, Msg("a-unknown"), group.Count.ToStringForUser)

                rows.Add({group.Description, groupCountString})
            Next group

            GroupList.SortMethods(1) = SortMethod.Integer
            GroupList.SetItems(rows)
            If GroupList.VirtualListSize > 0 Then GroupList.SelectedIndices.Add(0)

            GroupCount.Text = Msg("a-count", rows.Count)
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

            Dim rows As New List(Of String())

            For Each right As String In SelectedGroup.Rights
                rows.Add({right})
            Next right

            RightsList.SetItems(rows)
            RightsCount.Text = Msg("a-count", rows.Count)
        End Sub

        Private Sub RightsList_DoubleClick() Handles RightsList.DoubleClick
            If RightsList.SelectedValue IsNot Nothing _
                Then RaiseEvent ViewRight(Me, New EventArgs(Of String)(RightsList.SelectedValue))
        End Sub

    End Class

End Namespace