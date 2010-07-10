Imports System.Collections.Generic

Namespace Huggle

    'Handles translations of Huggle interface messages

    Class Translation

        Private _OldValue, _NewValue As String
        Private Shared _All As New Dictionary(Of Language, Dictionary(Of String, Translation))

        Private Sub New(ByVal OldValue As String, ByVal NewValue As String)
            _OldValue = OldValue
            _NewValue = NewValue
        End Sub

        Public ReadOnly Property OldValue() As String
            Get
                Return _OldValue
            End Get
        End Property

        Public ReadOnly Property NewValue() As String
            Get
                Return _NewValue
            End Get
        End Property

        Public Shared Sub Add(ByVal Language As Language, ByVal Message As String, ByVal NewValue As String)

            Dim OldValue As String = Nothing

            If Language.Messages.ContainsKey(Message) Then
                OldValue = Language.Messages(Message)
                If NewValue Is Nothing _
                    Then Language.Messages.Remove(Message) Else Language.Messages(Message) = NewValue
            Else
                Language.Messages.Add(Message, NewValue)
            End If

            If Not All.ContainsKey(Language) _
                Then All.Merge(Language, New Dictionary(Of String, Translation))

            If All(Language).ContainsKey(Message) Then
                If NewValue Is Nothing Then
                    All(Language).Remove(Message)
                ElseIf All(Language)(Message).OldValue = All(Language)(Message).NewValue Then
                    All(Language).Remove(Message)
                Else
                    All(Language)(Message)._NewValue = NewValue
                End If
            Else
                All(Language).Add(Message, New Translation(OldValue, NewValue))
            End If
        End Sub

        Public Shared Sub Undo()
            For Each Item As KeyValuePair(Of Language, Dictionary(Of String, Translation)) In All
                Dim Language As Language = Item.Key

                For Each Subitem As KeyValuePair(Of String, Translation) In Item.Value
                    Dim Message As String = Subitem.Key
                    Dim Translation As Translation = Subitem.Value

                    If Language.Messages.ContainsKey(Message) Then
                        If Translation.OldValue Is Nothing Then
                            Language.Messages.Remove(Message)
                        Else
                            Language.Messages(Message) = Translation.OldValue
                        End If
                    ElseIf Translation.OldValue IsNot Nothing Then
                        Language.Messages.Add(Message, Translation.OldValue)
                    End If
                Next Subitem
            Next Item

            ResetState()
        End Sub

        Public Shared ReadOnly Property All() As Dictionary(Of Language, Dictionary(Of String, Translation))
            Get
                Return _All
            End Get
        End Property

        Public Shared Sub ResetState()
            All.Clear()
        End Sub

    End Class

End Namespace