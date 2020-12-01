using LibraryReader.Books;
using Menu.MainAppPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Menu.Helpers
{
    public static class LibraryByBlocks
    {
        public static int commonCount = 0;
        public static double sum = 0;
        public static int i = 0;
        public static int bookCount = 0;
        public static Dictionary<int, List<Book>> resultDict;

        static LibraryByBlocks()
        {
            
            resultDict = new Dictionary<int, List<Book>>();
        }
    }
}
