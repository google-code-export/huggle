Imports System
Imports System.Collections.Generic

Namespace Huggle

    <Diagnostics.DebuggerDisplay("{Name}")>
    Friend Class ApiModule

        Private _Name As String
        Private _Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki, ByVal name As String)
            _Name = name
            _Wiki = wiki
        End Sub

        Friend ReadOnly Property IsAvailable As Boolean
            Get
                Return (IsImplemented AndAlso Not IsDisabled)
            End Get
        End Property

        Friend Property IsDisabled As Boolean = False
        Friend Property IsImplemented As Boolean = True

        Friend ReadOnly Property Name As String
            Get
                Return _Name
            End Get
        End Property

        Friend ReadOnly Property Wiki As Wiki
            Get
                Return _Wiki
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class

    Friend Class ApiModuleCollection

        Private _All As New Dictionary(Of String, ApiModule)
        Private Wiki As Wiki

        Friend Sub New(ByVal wiki As Wiki)
            Me.Wiki = wiki
        End Sub

        Friend ReadOnly Property All As IList(Of ApiModule)
            Get
                Return _All.Values.ToList.AsReadOnly
            End Get
        End Property

        Default Friend ReadOnly Property FromString(ByVal name As String) As ApiModule
            Get
                If Not _All.ContainsKey(name) Then _All.Add(name, New ApiModule(Wiki, name))
                Return _All(name)
            End Get
        End Property

    End Class

End Namespace
