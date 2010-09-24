Imports Huggle
Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class ReviewForm

    Private _Levels As New Dictionary(Of ReviewFlag, Integer)
    Private _Revision As Revision

    Public Sub New(ByVal Revision As Revision)
        InitializeComponent()
        _Revision = Revision
    End Sub

    Public ReadOnly Property Comment() As String
        Get
            Return CommentField.Text
        End Get
    End Property

    Public ReadOnly Property Levels() As Dictionary(Of ReviewFlag, Integer)
        Get
            Return _Levels
        End Get
    End Property

    Public ReadOnly Property Revision() As Revision
        Get
            Return _Revision
        End Get
    End Property

    Private Sub _Load() Handles Me.Load
        Icon = Resources.Icon
        App.Languages.Current.Localize(Me)

        For Each flag As ReviewFlag In Revision.Wiki.Config.ReviewFlags.Values
            If flag.Levels >= 2 Then
                Dim combobox As New ComboBox
                combobox.Name = flag.Name

                For i As Integer = 0 To flag.Levels - 1
                    combobox.Items.Add(flag.LevelName(i))
                Next i

                Levels.Add(flag, flag.DefaultLevel)
                combobox.SelectedIndex = flag.DefaultLevel
                LevelPanel.Controls.Add(combobox)
            End If
        Next flag

        If Not Revision.Wiki.Config.ReviewComments Then
            CommentLabel.Visible = False
            CommentField.Visible = False
        End If

        Height -= (OK.Top - LevelPanel.Bottom - 6)
    End Sub

    Private Sub _FormClosing() Handles Me.FormClosing
        For Each item As Control In LevelPanel.Controls
            Dim combobox As ComboBox = CType(item, ComboBox)
            Levels(Revision.Wiki.Config.ReviewFlags(combobox.Name)) = combobox.SelectedIndex
        Next item
    End Sub

End Class