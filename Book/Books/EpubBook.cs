using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eBdb.EpubReader;

namespace LibraryForReader.Books
{
    [Serializable]
    public class EpubBook: Books
    {
        Epub epubBook;

        public EpubBook(string path)
        {
            epubBook = new Epub(path);
            Title = epubBook.Title[0];
            Author = epubBook.Creator[0];
        }

        public string GetContentAsHtml()
        {
            return epubBook.GetContentAsHtml();
        }
        public string GetContentAsString(string content)
        {
            Match m = Regex.Match(content, @"<body[^>]*>.+</body>", Utils.REO_csi);
            return m.Success ? Utils.ClearText(m.Value) : "";
        }
    }
}
