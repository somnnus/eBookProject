using LibraryReader.Books;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Menu.Helpers
{
    public class DictionaryItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dict = values[0] as Dictionary<int, List<Book>>;
            if (values.Length == 2 && values != null && dict.Count>0)
            {
                var key = values[1] as int?;
                //return key != null && dict != null ? dict[key.Value] : null;
                return key != null ? dict[key.Value] : null;
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
