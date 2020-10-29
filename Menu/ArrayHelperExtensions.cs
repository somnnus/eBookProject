using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public static class ArrayHelperExtensions
    {
        public static Dictionary<int, List<Book>> Split<Book>(List<Book> array, Dictionary<int, List<Book>> dictKeys, int size)
        {
            for (var i = 0; i < (float)array.Count/ size; i++)
            {
                foreach (var item in array.Skip(i * size).Take(size))
                {
                    if (dictKeys.ContainsKey(i))
                    {
                        dictKeys[i].Add(item);
                    }
                    else
                    {
                        dictKeys.Add(i, new List<Book>());
                        dictKeys[i].Add(item);
                    }
                }
            }
            return dictKeys;
        }
    }
}
