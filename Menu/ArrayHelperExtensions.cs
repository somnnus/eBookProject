using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public static class ArrayHelperExtensions
    {
        public static Dictionary<int, List<Book>> SplitByBlocks(List<Book> array, Dictionary<int, List<Book>> dictKeys, int size)
        {
            for (var i = 0; i < (float)array.Count / size; i++)
            {
                foreach (var item in array.Skip(i * size).Take(size))
                {
                    var num = i;
                    if (dictKeys.ContainsKey(num))
                    {
                        dictKeys[num].Add(item);
                    }
                    else
                    {
                        dictKeys.Add(num, new List<Book>());
                        dictKeys[num].Add(item);
                    }
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Book>> SplitByAuthor(List<Book> array, Dictionary<string, List<Book>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var firstLetter = array[i].Author[0].ToString();
                if (dictKeys.ContainsKey(firstLetter))
                {
                    dictKeys[firstLetter].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(firstLetter, new List<Book>());
                    dictKeys[firstLetter].Add(array[i]);
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Book>> SplitByBookName(List<Book> array, Dictionary<string, List<Book>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var firstLetter = array[i].Title[0].ToString();
                if (dictKeys.ContainsKey(firstLetter))
                {
                    dictKeys[firstLetter].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(firstLetter, new List<Book>());
                    dictKeys[firstLetter].Add(array[i]);
                }
            }
            return dictKeys;
        }

        public static Dictionary<string, List<Book>> SplitByDate(List<Book> array, Dictionary<string, List<Book>> dictKeys)
        {
            for (var i = 0; i < array.Count; i++)
            {
                var date = array[i].Date.ToShortDateString();
                if (dictKeys.ContainsKey(date))
                {
                    dictKeys[date].Add(array[i]);
                }
                else
                {
                    dictKeys.Add(date, new List<Book>());
                    dictKeys[date].Add(array[i]);
                }
            }
            return dictKeys;
        }
    }
}
