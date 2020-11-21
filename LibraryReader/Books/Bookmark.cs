using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace LibraryReader.Books
{
    [Serializable]
    public class Bookmark
    {
        public int NumberPage { get; set; }
        public double FrontSize { get; set; }
        public double ColumnWidth { get; set; }
       // public Paragraph Paragraph { get; set; }
    }
}
