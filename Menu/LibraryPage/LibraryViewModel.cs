using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.SharedResources;

namespace Menu.LibraryPage
{
    public class LibraryViewModel: ViewModelBase
    {
        public Resources Current
        {
            get { return ResourcesProvider.Current; }
        }
    }
}
