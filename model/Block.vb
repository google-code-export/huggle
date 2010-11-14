Imports System
Imports System.Drawing
Imports System.Text.RegularExpressions

Namespace Huggle

    'Represents a change in block settings

    Friend Class Block : Inherits LogItem

        Private _Duration As String
        Private _IsRangeblock As Boolean
        Private _TargetRange As String

        Public Sub New(ByVal id As Integer, ByVal wiki As Wiki)
            MyBase.New(id, wiki)
        End Sub

        Public Overrides ReadOnly Property Description As String
            Get
                Return Msg("log-" & Action, Id, Target, User.Name, Duration)
            End Get
        End Property

        Public Property Duration() As String
            Get
                If _Duration IsNot Nothing Then Return _Duration
                If Expires = Date.MaxValue Then Return Msg("block-indefinite")
                If Time = Date.MinValue OrElse Expires = Date.MinValue Then Return Nothing
                Return FullFuzzyAge(Time, Expires)
            End Get
            Set(ByVal value As String)
                _Duration = value
            End Set
        End Property

        Public Property Expires() As Date

        Public Property IsAccountCreationBlocked As Boolean

        Public Property IsAnonymousOnly As Boolean

        Public Property IsAutoblockEnabled As Boolean

        Public Property IsAutomatic As Boolean

        Public Overrides ReadOnly Property Icon() As Image
            Get
                Return Resources.blob_log_block
            End Get
        End Property

        Public Property IsEmailBlocked As Boolean

        Public ReadOnly Property IsRangeblock() As Boolean
            Get
                Return _IsRangeblock
            End Get
        End Property

        Public Property IsTalkBlocked As Boolean

        Public Overrides ReadOnly Property Target() As String
            Get
                If IsRangeblock Then Return TargetRange Else Return TargetUser.Name
            End Get
        End Property

        Public ReadOnly Property TargetRange As String
            Get
                Return _TargetRange
            End Get
        End Property

        Protected Overrides Sub OnSetPage()
            If RangePattern.IsMatch(Page.Title) Then
                _IsRangeblock = True
                _TargetRange = Target
            Else
                TargetUser = Page.Owner
                If TargetUser IsNot Nothing Then TargetUser.Blocks.Add(Me)
            End If
        End Sub

    End Class

End Namespace