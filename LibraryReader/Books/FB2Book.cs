using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using eBdb.EpubReader;
using FB2Library;

namespace LibraryReader.Books
{
    [Serializable]
    public class FB2Book: Book
    {
        FB2File fB2File;
        public FB2Book()
        {

        }
        public FB2Book(string path,string newPath)
        {
            fB2File = new FB2File();
            File.Copy(path, newPath);
            FullPath = newPath;
            FontSize = 16;
            XDocument doc = XDocument.Load(path);
            fB2File.Load(doc, false);
            FullPath = path;
            Date = DateTime.Now;
           
            var s = fB2File.TitleInfo;
            var author = s.BookAuthors;
            StringBuilder name = new StringBuilder();
            foreach (var aurho in author)
            {
                name.Append(string.Format("{0} {1}", aurho.FirstName.Text, aurho.LastName.Text));
            }

            Title = s.BookTitle.Text;
            Author = Convert.ToString(name);


            CoverPath = GetCoverPath();


            //var namespaceManager = new XmlNamespaceManager(new NameTable());
            //namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            //XDocument doc1 = XDocument.Load(path);
            //var body = doc1.Root.XPathSelectElements("fb:body", namespaceManager).ToList();
            //StringWriter sw = new StringWriter();
            //XmlTextWriter tx = new XmlTextWriter(sw);
            //body[0].WriteTo(tx);
            //string str = sw.ToString();

        }
        public static string GetContentAsTitle(string Content)
        {
            Match m = Regex.Match(Content, @"<title[^>]*>.+</title>", Utils.REO_csi);
            return m.Success ? Utils.ClearText(m.Value) : "";
        }
        private string GetCoverPath()
        {
            Random rnd = new Random();
            if (fB2File.Images.Count != 0)
            {
                var images = fB2File.Images.First();
                var cover = images.Value.BinaryData;
                string coverName = string.Format("{0} {1}.jpg", Title, Convert.ToString(rnd.Next(50)));
                Image image = ByteArrayToImage(cover);
                string coverPath = AppDomain.CurrentDomain.BaseDirectory + "Library\\Covers\\" + coverName;
                image.Save(coverPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                return coverPath;
            }
            else
            {
                return AppDomain.CurrentDomain.BaseDirectory + "Library\\Covers\\defoltCover.jpg";
            }

        }

        public override string ReturnContent()
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            XDocument doc1 = XDocument.Load(FullPath);
            var body = doc1.Root.XPathSelectElements("fb:body", namespaceManager).ToList();
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            body[0].WriteTo(tx);
            string str = sw.ToString();

            return GetContentAsTitle(str);
        }
    }
}
