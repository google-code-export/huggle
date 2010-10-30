Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Imports KVP = System.Collections.Generic.KeyValuePair(Of String, String)

Namespace Huggle

    Partial Public Class Config

        Protected Shared Sub ReadSpamLists(ByVal lists As SpamListCollection,
            ByVal context As String, ByVal value As String)

            For Each rootItem As KVP In ParseConfig(context, "spam-list", value)
                Dim list As SpamList = lists.FromPage(lists.Wiki.Pages.FromString(rootItem.Key))

                For Each rootProp As KVP In ParseConfig(context, rootItem.Key, rootItem.Value)
                    Select Case rootProp.Key
                        Case "action"
                            list.Action = If(rootProp.Value = "permit", SpamListAction.Permit, SpamListAction.Deny)

                        Case "custom"
                            list.IsCustom = rootProp.Value.ToBoolean

                        Case "entries"
                            Dim entries As New List(Of SpamListEntry)

                            For Each item As KVP In ParseConfig("family", rootProp.Key, rootProp.Value)
                                Dim entry As New SpamListEntry(list, New Regex(item.Key))

                                For Each prop As KVP In ParseConfig("family", item.Key, item.Value)
                                    Select Case prop.Key
                                        Case "comment"
                                            entry.Comment = prop.Value

                                        Case "last-modified-at"
                                            entry.LastModifiedAt = prop.Value.ToDate

                                        Case "last-modified-by"
                                            entry.LastModifiedBy = lists.Wiki.Users.FromString(prop.Value)
                                    End Select
                                Next prop

                                entries.Add(entry)
                            Next item

                            list.Entries = entries
                    End Select
                Next rootProp
            Next rootItem
        End Sub

    End Class

End Namespace