using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
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
        public FB2Book(string path)
        {
            fB2File = new FB2File();
            FullPath = path;
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
            


            //var namespaceManager = new XmlNamespaceManager(new NameTable());
            //namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            //XDocument doc1 = XDocument.Load(path);
            //var body = doc1.Root.XPathSelectElements("fb:body", namespaceManager).ToList();
            //StringWriter sw = new StringWriter();
            //XmlTextWriter tx = new XmlTextWriter(sw);
            //body[0].WriteTo(tx);
            //string str = sw.ToString();

        }
    }
}
