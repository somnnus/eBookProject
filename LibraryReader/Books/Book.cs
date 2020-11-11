using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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
        public string CoverPath {get;set;}

        public int FontSize { get; set; }
        public int LastPage { get; set; }

        public Book()
        {

        }
        protected static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        
    }
}
