Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Public Class ConfigRead

        Public Shared Sub ReadSpamLists(ByVal lists As SpamListCollection, ByVal context As String, ByVal value As String)

            For Each listItem As KVP In Config.ParseConfig(context, "spam-list", value)
                Dim list As SpamList = lists.FromPage(lists.Wiki.Pages.FromString(listItem.Key))

                For Each listProp As KVP In Config.ParseConfig(context, listItem.Key, listItem.Value)
                    Select Case listProp.Key
                        Case "action" : list.Action = If(listProp.Value = "permit", SpamListAction.Permit, SpamListAction.Deny)
                        Case "custom" : list.IsCustom = listProp.Value.ToBoolean
                        Case "entries"
                            Dim entries As New List(Of SpamListEntry)

                            For Each entryItem As KVP In Config.ParseConfig("family", listProp.Key, listProp.Value)

                                Dim entry As New SpamListEntry(list, New Regex(entryItem.Key))

                                For Each entryProp As KVP In Config.ParseConfig("family", entryItem.Key, entryItem.Value)
                                    Select Case entryProp.Key
                                        Case "comment" : entry.Comment = entryProp.Value
                                        Case "last-modified-at" : entry.LastModifiedAt = entryProp.Value.ToDate
                                        Case "last-modified-by" : entry.LastModifiedBy = lists.Wiki.Users.FromString(entryProp.Value)
                                    End Select
                                Next entryProp

                                entries.Add(entry)
                            Next entryItem

                            list.Entries = entries
                    End Select
                Next listProp
            Next listItem
        End Sub

    End Class

End Namespace