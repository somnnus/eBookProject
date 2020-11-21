using LibraryReader.Books;
using Menu.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Menu.Helpers
{
    public static class LibrarySearching
    {
        public static void Search(TextBox textBox)
        {
            ResourcesProvider.Current.CurrentDictionary = new Dictionary<string, List<Book>>();
            string localKey = "";

            if (textBox.Text != "")
            {
                foreach (var book in ResourcesProvider.Current.ListBooks)
                {
                    if (CompareStrings(book.Author, textBox.Text, StringComparison.OrdinalIgnoreCase) || CompareStrings(book.Title, textBox.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ResourcesProvider.Current.CurrentDictionary.ContainsKey(localKey))
                        {
                            ResourcesProvider.Current.CurrentDictionary[localKey].Add(book);
                        }
                        else
                        {
                            ResourcesProvider.Current.CurrentDictionary.Add(localKey, new List<Book>());
                            ResourcesProvider.Current.CurrentDictionary[localKey].Add(book);
                        }
                    }
                }
                foreach (var list in ResourcesProvider.Current.CurrentDictionary.Values)
                {
                    foreach (var book in list)
                    {
                        if (!CompareStrings(book.Author, textBox.Text, StringComparison.OrdinalIgnoreCase) && !CompareStrings(book.Title, textBox.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            ResourcesProvider.Current.CurrentDictionary[localKey].Remove(book);
                        }
                    }
                }
            }
            else
            {
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
        }

        private static bool CompareStrings(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
