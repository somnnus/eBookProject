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

namespace Book
{
    [Serializable]
    class FB2Book: Books
    {
        public FB2Book(string path)
        {
            

            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            XDocument doc = XDocument.Load(path);
            var body = doc.Root.XPathSelectElements("fb:body", namespaceManager).ToList();
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            body[0].WriteTo(tx);
            string str = sw.ToString();

        }
    }
}
