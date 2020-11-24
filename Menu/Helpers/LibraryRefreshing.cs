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
                int blocksCount = 4;
                ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
                ResourcesProvider.Current.BooksByPages = ArrayHelperExtensions.SplitByBlocks(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.BooksByPages, blocksCount);
            }
            else
            {
                ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
            }
        }

        public static void SortByAuthor()
        {
            ResourcesProvider.Current.ListBooks.Sort(new AuthorComparer());
            ResourcesProvider.Current.SortedByAuthor = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByAuthor = ArrayHelperExtensions.SplitByAuthor(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByAuthor);
        }

        public static void SortByTitle()
        {
            ResourcesProvider.Current.ListBooks.Sort(new TitleComparer());
            ResourcesProvider.Current.SortedByTitle = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByTitle = ArrayHelperExtensions.SplitByBookName(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByTitle);
        }

        public static void SortByDate()
        {
            ResourcesProvider.Current.ListBooks.Sort(new DateComparer());
            ResourcesProvider.Current.SortedByDate = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByDate = ArrayHelperExtensions.SplitByDate(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByDate);
        }
    }
}
