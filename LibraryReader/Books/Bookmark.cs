﻿using System;
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

        public override string ToString()
        {
            return "Choose a bookmark";
        }
    }
}
