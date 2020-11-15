using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.SharedResources
{
    public static class ResourcesProvider //Singleton pattern
    {
        private static Resources _current = new Resources();
        public static Resources Current
        {
            get
            {
                return _current;
            }
        }
    }
}
