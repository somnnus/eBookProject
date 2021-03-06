﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryReader.Books
{
    [Serializable]
    public class Book
    {
        public Book() { }

        public List<Bookmark> bookmarks = new List<Bookmark>();

        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string FullPath { get; set; }
        public DateTime Date { get; set; }
        public string CoverPath { get; set; }

        public double Zoom { get; set; }
        public int LastPage { get; set; }

        protected static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public virtual string ReturnContent()
        {
            return null;
        }

        public void AddBookmark(Bookmark mark)
        {
            bookmarks.Add(mark);
        }
    }
}
