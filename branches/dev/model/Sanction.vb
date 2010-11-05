Imports System
Imports System.Collections.Generic
Imports System.Drawing

Namespace Huggle

    'Represents warnings, template messages, block notifications and such
    'This is a bad name for the class, but I can't think of anything better to concisely describe the concept

    Friend Class Sanction : Inherits LogItem

        Private _Regarding As Revision
        Private _Type As SanctionType
        Private _TargetUser As User

        Friend Sub New(ByVal time As Date, ByVal user As User, ByVal issuer As User, _
            ByVal type As SanctionType, ByVal regarding As Revision)

            MyBase.New(user.Wiki, 0, 0)
            Me.Action = "sanction"
            Me.Comment = type.Name
            Me.Time = time
            Me.User = issuer

            _Regarding = Regarding
            _Type = type
            _TargetUser = user
        End Sub

        Friend Overrides ReadOnly Property Icon() As Image
            Get
                If Type.Name = "warning" Then
                    Select Case Type.Level
                        Case 0, 1 : Return Icons.Warned1
                        Case 2 : Return Icons.Warned2
                        Case 3 : Return Icons.Warned3
                        Case 4 : Return Icons.Warned4
                    End Select
                End If

                If Type.Name = "report" Then Return Icons.Report
                If Type.Name = "blocknote" Then Return Icons.BlockNote
                Return Icons.None
            End Get
        End Property

        Friend ReadOnly Property IsCurrent() As Boolean
            Get
                Return (Time.Add(Wiki.Config.WarningAge) > Wiki.ServerTime)
            End Get
        End Property

        Friend ReadOnly Property IsFinal() As Boolean
            Get
                Return (Type.Level >= Wiki.Config.WarningLevels)
            End Get
        End Property

        Friend ReadOnly Property IsWarning() As Boolean
            Get
                Return (Type.Name = "warning")
            End Get
        End Property

        Friend ReadOnly Property Level() As Integer
            Get
                Return _Type.Level
            End Get
        End Property

        Friend ReadOnly Property Regarding() As Revision
            Get
                Return _Regarding
            End Get
        End Property

        Friend Overrides ReadOnly Property Target() As String
            Get
                Return _TargetUser.Name
            End Get
        End Property

        Friend ReadOnly Property TargetUser() As User
            Get
                Return _TargetUser
            End Get
        End Property

        Friend ReadOnly Property Type() As SanctionType
            Get
                Return _Type
            End Get
        End Property

    End Class

    Friend Structure SanctionType

        Private _Level As Integer
        Private _Name As String
        Private _Subtype As String

        Friend Sub New(ByVal name As String, ByVal subtype As String, ByVal level As Integer)
            _Level = level
            _Subtype = subtype
            _Name = name
        End Sub

        Friend ReadOnly Property Level() As Integer
            Get
                Return _Level
            End Get
        End Property

        Friend ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Friend ReadOnly Property Subtype() As String
            Get
                Return _Subtype
            End Get
        End Property

        Public Shared Operator =(ByVal x As SanctionType, ByVal y As SanctionType) As Boolean
            Return (x.Level = y.Level AndAlso x.Subtype = y.Subtype AndAlso x.Name = y.Name)
        End Operator

        Public Shared Operator <>(ByVal x As SanctionType, ByVal y As SanctionType) As Boolean
            Return Not (x = y)
        End Operator

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TypeOf obj Is SanctionType AndAlso CType(obj, SanctionType) = Me)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Level.GetHashCode Xor Name.GetHashCode Xor Subtype.GetHashCode
        End Function

    End Structure

End Namespace