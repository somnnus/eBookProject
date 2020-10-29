using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    [Serializable]
    class Bookmark
    {
        public string NumberPage { get; set; }
        public string FrontSize { get; set; }
        public string ColumnWidth { get; set; }
    }
}
