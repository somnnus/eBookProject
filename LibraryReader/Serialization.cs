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
            string fileName = "library.xml";
            fullPath = fullPath + "\\" + fileName;

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            using (Stream fStream = new FileStream(fullPath,
                        FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat1 = new XmlSerializer(typeof(List<Book>), new Type[] { typeof(Book), typeof(string) });
                xmlFormat1.Serialize(fStream, books);
            }
            MessageBox.Show("=> Saved list in XML format!");
        }

        public static List<Book> DeserializationLibrary(string fullPath)
        {
            List<Book> books;

            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Book>), new Type[] { typeof(Book), typeof(string) });

            using (Stream fStream = new FileStream(fullPath,
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                books = (List<Book>)xmlFormat.Deserialize(fStream);
            }

            return books;
        }








    }
}
