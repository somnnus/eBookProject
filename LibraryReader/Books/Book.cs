using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryReader.Books
{
    [Serializable]
    public class Book
    {
        List<Bookmark> bookmarks = new List<Bookmark>();

        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string FullPath { get; set; }
        public DateTime Date { get; set; }

        public int FontSize { get; set; }
        public int LastPage { get; set; }

        public Book()
        {

        }

    }
}
