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


        //СДЕЛАТЬ ОТДЕЛЬНЫЙ СЛОВАРЬ ДЛЯ ПОСТРАНИЧНОГО ВЫВОДА, например, booksByPages
        public static Dictionary<string, List<Book>> dictionaryBooks;
        public static List<Book> listBooks;

        static CommonResources()
        {
            dictionaryBooks = new Dictionary<string, List<Book>>();
            listBooks = new List<Book>();
        }
    }
}
