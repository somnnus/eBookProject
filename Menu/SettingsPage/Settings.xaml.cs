using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu.SettingsPage
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        //SolidColorBrush colorBrush;

        public Settings()
        {
            InitializeComponent();
            settingsTextBlock.Text = "При переопределении в производном классе предоставляет точку входа обратного вызова для уведомления в случае изменения значения свойства Source в экземпляре.";
        }

        private void SetWhiteTheme(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#EAEAEA"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#424242"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#424242"));
        }
        private void SetSepiaTheme(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFEBCD"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#FFDEC3A7"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#442C2E"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#442C2E"));
        }
        private void SetDarkBlueTheme(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#3539A5"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#535BAB"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#E6E7F1"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#EFEFFE"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#E6E7F1"));
        }
        private void SetNightTheme(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#363740"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#494A56"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
        }
    }
}
