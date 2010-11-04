Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Huggle.UI

    Public Class ReviewForm : Inherits HuggleForm

        Private _Levels As New Dictionary(Of ReviewFlag, Integer)
        Private _Revision As Revision

        Private Session As Session

        Public Sub New(ByVal session As Session, ByVal revision As Revision)
            InitializeComponent()
            Me.Session = session
            _Revision = revision
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
            For Each control As Control In LevelPanel.Controls
                Dim combobox As ComboBox = CType(control, ComboBox)
                Levels(Revision.Wiki.Config.ReviewFlags(combobox.Name)) = combobox.SelectedIndex
            Next control
        End Sub

    End Class

End Namespace