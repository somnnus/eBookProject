using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using LibraryReader.Books;
using System.Windows.Forms;

namespace LibraryReader
{
    public static class Serialization
    {
        public static void SerializationInformationAboutBook(List<Book> books,string fullPath)
        {
            if (books != null)
            {
                string fileName = "library.xml";
                fullPath = fullPath + "\\" + fileName;

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                using (Stream fStream = new FileStream(fullPath,
                            FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer xmlFormat1 = new XmlSerializer(typeof(List<Book>), new Type[] { typeof(Book), typeof(string), typeof(EpubBook),typeof(FB2Book) });
                    xmlFormat1.Serialize(fStream, books);
                }
              //  MessageBox.Show("=> Saved list in XML format!");
            }
        }

        public static List<Book> DeserializationLibrary(string fullPath)
        {
            List<Book> books;

            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Book>), new Type[] { typeof(Book), typeof(string), typeof(EpubBook), typeof(FB2Book) });

            using (Stream fStream = new FileStream(fullPath,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                books = (List<Book>)xmlFormat.Deserialize(fStream);
            }

            return books;
        }

        public static void SerializationLastBook(Book book,string fullPath)
        {
            if (book != null)
            {
                string fileName = "last.xml";
                fullPath = fullPath + "\\" + fileName;

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                using (Stream fStream = new FileStream(fullPath,
                            FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer xmlFormat1 = new XmlSerializer(typeof(Book), new Type[] { typeof(Book), typeof(string), typeof(EpubBook), typeof(FB2Book) });
                    xmlFormat1.Serialize(fStream, book);
                }
                //  MessageBox.Show("=> Saved list in XML format!");
            }
        }
        public static Book DeserializationLastBook(string fullPath)
        {
            Book book;

            XmlSerializer xmlFormat = new XmlSerializer(typeof(Book), new Type[] { typeof(Book), typeof(string), typeof(EpubBook), typeof(FB2Book) });

            using (Stream fStream = new FileStream(fullPath,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                book = (Book)xmlFormat.Deserialize(fStream);
            }

            return book;
        }







    }
}
