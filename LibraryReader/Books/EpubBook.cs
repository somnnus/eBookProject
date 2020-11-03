using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eBdb.EpubReader;

namespace LibraryReader.Books
{
    [Serializable]
    public class EpubBook : Book
    {
        Epub epubBook;

        public EpubBook()
        {

        }
        public EpubBook(string path)
        {
            epubBook = new Epub(path);
            FullPath = path;
            Title = epubBook.Title[0];
            Author = epubBook.Creator[0];
            FullPath = path;
            FontSize = 16;
            
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

        static string HtmlToPlainText(string html)
        {
            string buf;
            string block = "address|article|aside|blockquote|canvas|dd|div|dl|dt|" +
              "fieldset|figcaption|figure|footer|form|h\\d|header|hr|li|main|nav|" +
              "noscript|ol|output|p|pre|section|table|tfoot|ul|video";

            string patNestedBlock = $"(\\s*?</?({block})[^>]*?>)+\\s*";
            buf = Regex.Replace(html, patNestedBlock, "\n", RegexOptions.IgnoreCase);

            // Replace br tag to newline.
            buf = Regex.Replace(buf, @"<(br)[^>]*>", "\n", RegexOptions.IgnoreCase);

            // (Optional) remove styles and scripts.
            buf = Regex.Replace(buf, @"<(script|style)[^>]*?>.*?</\1>", "", RegexOptions.Singleline);

            // Remove all tags.
            buf = Regex.Replace(buf, @"<[^>]*(>|$)", "", RegexOptions.Multiline);

            // Replace HTML entities.
            buf = System.Net.WebUtility.HtmlDecode(buf);
            return buf;
        }
    }
}
