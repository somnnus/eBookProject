using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Comparers
{
    public class TitleComparer : IComparer<Book>
    {
        public int Compare(Book book1, Book book2)
        {
            return book1.Title.CompareTo(book2.Title);
        }
    }
}
