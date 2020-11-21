using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Comparers
{
    public class DateComparer : IComparer<Book>
    {
        public int Compare(Book book1, Book book2)
        {
            if (book1.Date.CompareTo(book2.Date) == 1)
                return -1;
            else
            if (book1.Date.CompareTo(book2.Date) == -1)
                return 1;
            else
                return 0;
        }
    }
}
