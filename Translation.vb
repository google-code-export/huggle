Imports System.Collections.Generic

Namespace Huggle

    'Handles translations of Huggle interface messages

    Friend Class Translation

        Private _OldValue As String
        Private _NewValue As String
        Private Shared _All As New Dictionary(Of Language, Dictionary(Of String, Translation))

        Private Sub New(ByVal oldValue As String, ByVal newValue As String)
            _OldValue = oldValue
            _NewValue = newValue
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

        Public Shared Sub Add(ByVal language As Language, ByVal message As String, ByVal newValue As String)

            Dim oldValue As String = Nothing

            If language.Messages.ContainsKey(message) Then
                oldValue = language.Messages(message)
                If newValue Is Nothing _
                    Then language.Messages.Remove(message) Else language.Messages(message) = newValue
            Else
                language.Messages.Add(message, newValue)
            End If

            If Not All.ContainsKey(language) _
                Then All.Merge(language, New Dictionary(Of String, Translation))

            If All(language).ContainsKey(message) Then
                If newValue Is Nothing Then
                    All(language).Remove(message)
                ElseIf All(language)(message).OldValue = All(language)(message).NewValue Then
                    All(language).Remove(message)
                Else
                    All(language)(message)._NewValue = newValue
                End If
            Else
                All(language).Add(message, New Translation(oldValue, newValue))
            End If
        End Sub

        Public Shared Sub Undo()
            For Each item As KeyValuePair(Of Language, Dictionary(Of String, Translation)) In All
                Dim language As Language = item.Key

                For Each subitem As KeyValuePair(Of String, Translation) In item.Value
                    Dim message As String = subitem.Key
                    Dim translation As Translation = subitem.Value

                    If language.Messages.ContainsKey(message) Then
                        If translation.OldValue Is Nothing Then
                            language.Messages.Remove(message)
                        Else
                            language.Messages(message) = translation.OldValue
                        End If
                    ElseIf translation.OldValue IsNot Nothing Then
                        language.Messages.Add(message, translation.OldValue)
                    End If
                Next subitem
            Next item

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