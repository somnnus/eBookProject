using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public static class ArrayHelperExtensions
    {
        public static Dictionary<string, List<Books>> SplitByBlocks(List<Books> array, Dictionary<string, List<Books>> dictKeys, int size)
        {
            for (var i = 0; i < (float)array.Count/ size; i++)
            {
                foreach (var item in array.Skip(i * size).Take(size))
                {
                    var num = i.ToString();
                    if (dictKeys.ContainsKey(num))
                    {
                        dictKeys[num].Add(item);
                    }
                    else
                    {
                        dictKeys.Add(num, new List<Books>());
                        dictKeys[num].Add(item);
                    }
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Books>> SplitByAuthor(List<Books> array, Dictionary<string, List<Books>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var firstLetter = array[i].author[0].ToString();
                if (dictKeys.ContainsKey(firstLetter))
                {
                    dictKeys[firstLetter].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(firstLetter, new List<Books>());
                    dictKeys[firstLetter].Add(array[i]);
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Books>> SplitByBookName(List<Books> array, Dictionary<string, List<Books>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var firstLetter = array[i].bookName[0].ToString();
                if (dictKeys.ContainsKey(firstLetter))
                {
                    dictKeys[firstLetter].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(firstLetter, new List<Books>());
                    dictKeys[firstLetter].Add(array[i]);
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Books>> SplitByDate(List<Books> array, Dictionary<string, List<Books>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var date = array[i].date.ToString();
                if (dictKeys.ContainsKey(date))
                {
                    dictKeys[date].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(date, new List<Books>());
                    dictKeys[date].Add(array[i]);
                }
            }
            return dictKeys;
        }
    }
}
