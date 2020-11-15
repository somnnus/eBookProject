using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public static class CommonResources
    {
        public static MainWindowViewModel mainWindowViewModel;
        
        public static Dictionary<string, List<Book>> booksByPages;
        public static Dictionary<string, List<Book>> dictionaryBooks;
        public static List<Book> listBooks;

        static CommonResources()
        {
            booksByPages = new Dictionary<string, List<Book>>();
            dictionaryBooks = new Dictionary<string, List<Book>>();
            listBooks = new List<Book>();
        }
    }
}
