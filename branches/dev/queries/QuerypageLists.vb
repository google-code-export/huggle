Namespace Huggle.Queries.Lists

    Friend MustInherit Class QuerypageQuery : Inherits ListQuery

        Sub New(ByVal session As Session, ByVal queryName As String)
            MyBase.New(session, "list", "querypage", "qp",
                New QueryString("qppage", queryName), Msg("listdesc-" & queryName.ToLower))
        End Sub

    End Class


    Friend Class AncientPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Ancientpages")
        End Sub

    End Class


    Friend Class BrokenRedirectsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "BrokenRedirects")
        End Sub

    End Class


    Friend Class DeadendPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "DeadendPages")
        End Sub

    End Class


    Friend Class DisambiguationsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Disambiguations")
        End Sub

    End Class


    Friend Class DoubleRedirectsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "DoubleRedirects")
        End Sub

    End Class


    Friend Class FewestRevisionsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Fewestrevisions")
        End Sub

    End Class


    Friend Class ListRedirectsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Listredirects")
        End Sub

    End Class


    Friend Class LonelyPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Lonelypages")
        End Sub

    End Class


    Friend Class LongPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Longpages")
        End Sub

    End Class


    Friend Class MostCategoriesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostcategories")
        End Sub

    End Class


    Friend Class MostFilesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostimages")
        End Sub

    End Class


    Friend Class MostLinkedCategoriesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostlinkedcategories")
        End Sub

    End Class


    Friend Class MostLinkedPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostlinked")
        End Sub

    End Class


    Friend Class MostLinkedTemplatesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostlinkedtemplates")
        End Sub

    End Class


    Friend Class MostRevisionsQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Mostrevisions")
        End Sub

    End Class


    Friend Class PopularPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Popularpages")
        End Sub

    End Class


    Friend Class ShortPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Shortpages")
        End Sub

    End Class


    Friend Class UncategorizedCategoriesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Uncategorizedcategories")
        End Sub

    End Class


    Friend Class UncategorizedFilesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Uncategorizedimages")
        End Sub

    End Class


    Friend Class UncategorizedPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Uncategorizedpages")
        End Sub

    End Class


    Friend Class UncategorizedTemplatesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Uncategorizedtemplates")
        End Sub

    End Class


    Friend Class UnusedCategoriesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Unusedcategories")
        End Sub

    End Class


    Friend Class UnusedFilesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Unusedimages")
        End Sub

    End Class


    Friend Class UnusedTemplatesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Unusedtemplates")
        End Sub

    End Class


    Friend Class UnwatchedPages : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Unwatchedpages")
        End Sub

    End Class


    Friend Class WantedCategoriesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Wantedcategories")
        End Sub

    End Class


    Friend Class WantedFilesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Wantedfiles")
        End Sub

    End Class


    Friend Class WantedPagesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Wantedpages")
        End Sub

    End Class


    Friend Class WantedTemplatesQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Wantedtemplates")
        End Sub

    End Class


    Friend Class WithoutInterwikiQuery : Inherits QuerypageQuery

        Sub New(ByVal session As Session)
            MyBase.New(session, "Withoutinterwiki")
        End Sub

    End Class

End Namespace
