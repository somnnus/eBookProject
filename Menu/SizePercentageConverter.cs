using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LibraryReader.Books;
using Menu.Helpers;
using Menu.MainAppPage;
using Menu.SharedResources;

namespace Menu
{
    public class SizePercentageConverter : IValueConverter
    {
        public SizePercentageConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return 0.7 * System.Convert.ToDouble(value);

            string[] split = parameter.ToString().Split('.');
            double parameterDouble = System.Convert.ToDouble(split[0]) + System.Convert.ToDouble(split[1]) / (Math.Pow(10, split[1].Length));

            double refreshedValue = System.Convert.ToDouble(value) * parameterDouble;

            return refreshedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Don't need to implement this
            return null;
        }
    }
}
