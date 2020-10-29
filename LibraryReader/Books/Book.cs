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
        protected string Title { get; set; }
        protected string Author { get; set; }
        protected string Content { get; set; }

    }
}
