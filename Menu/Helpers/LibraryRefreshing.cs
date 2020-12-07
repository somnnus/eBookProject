using LibraryReader.Books;
using Menu.Comparers;
using Menu.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Helpers
{
    public static class LibraryRefreshing
    {
        public static void Refresh()
        {
            FillMain();

            SortByAuthor();
            SortByTitle();
            SortByDate();

            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Author")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByAuthor;
            }
            else
            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Title")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByTitle;
            }
            else
            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Date")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
            }
        }

        public static void FillMain()
        {
            if (ResourcesProvider.Current.ListBooks.Count != 0)
            {
                var recentlyReadBooksHelper = new Dictionary<int, Book>();
                if (ResourcesProvider.Current.ListBooks.Count == 1)
                {
                    recentlyReadBooksHelper.Add(0, ResourcesProvider.Current.ListBooks[0]);
                    //ResourcesProvider.Current.RecentlyReadBooks[0] = ResourcesProvider.Current.ListBooks[0];

                    ResourcesProvider.Current.RecentlyReadBooks = recentlyReadBooksHelper;
                    ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
                }
                else
                if (ResourcesProvider.Current.ListBooks.Count >= 2)
                {
                    recentlyReadBooksHelper.Add(0, ResourcesProvider.Current.ListBooks[0]);
                    recentlyReadBooksHelper.Add(1, ResourcesProvider.Current.ListBooks[1]);
                    ResourcesProvider.Current.RecentlyReadBooks = recentlyReadBooksHelper;

                    //ResourcesProvider.Current.RecentlyReadBooks[0] = ResourcesProvider.Current.ListBooks[0];
                    //ResourcesProvider.Current.RecentlyReadBooks[1] = ResourcesProvider.Current.ListBooks[1];

                    var restOfBooksHelper = new List<Book>();
                    foreach (var book in ResourcesProvider.Current.ListBooks)
                    {
                        restOfBooksHelper.Add(book);
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        restOfBooksHelper.RemoveAt(0);
                    }

                    int blocksCount = 4;
                    ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
                    ResourcesProvider.Current.BooksByPages = LibrarySorting.SplitByBlocks(restOfBooksHelper, ResourcesProvider.Current.BooksByPages, blocksCount);
                }
            }
            else
            {
                ResourcesProvider.Current.RecentlyReadBooks = new Dictionary<int, Book>();
                ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
            }
        }

        public static void SortByAuthor()
        {
            ResourcesProvider.Current.ListBooks.Sort(new AuthorComparer());
            ResourcesProvider.Current.SortedByAuthor = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByAuthor = LibrarySorting.SplitByAuthor(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByAuthor);
        }

        public static void SortByTitle()
        {
            ResourcesProvider.Current.ListBooks.Sort(new TitleComparer());
            ResourcesProvider.Current.SortedByTitle = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByTitle = LibrarySorting.SplitByBookName(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByTitle);
        }

        public static void SortByDate()
        {
            ResourcesProvider.Current.ListBooks.Sort(new DateComparer());
            ResourcesProvider.Current.SortedByDate = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByDate = LibrarySorting.SplitByDate(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByDate);
        }
    }
}
