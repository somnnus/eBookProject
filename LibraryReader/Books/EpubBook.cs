using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eBdb.EpubReader;
using EpubSharp;

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
            try
            {
                epubBook = new Epub(path);
                FullPath = path;
                Title = epubBook.Title[0];
                Author = epubBook.Creator[0];
                FontSize = 16;
                Date = DateTime.Now;
                CoverPath = GetCoverPath();
            }
            catch(Exception ex)
            {
                throw new Exception("Не удалось открыть книгу");
            }
            
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
        private string GetCoverPath()
        {
            Random rnd = new Random();
            EpubSharp.EpubBook book = EpubReader.Read(FullPath);
            if (book.CoverImage != null)
            {
                var cover = book.CoverImage;
                Image image = ByteArrayToImage(cover);
                string coverName = string.Format("{0} {1}.jpg", Title,Convert.ToString(rnd.Next(50)));
                string coverPath = AppDomain.CurrentDomain.BaseDirectory + "Library\\Covers\\"+coverName;
                image.Save(coverPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return coverPath;
            }
            else return null;
                      
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
