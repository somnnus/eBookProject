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
using LibraryReader;

namespace Menu.SettingsPage
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        private string fullPath = AppDomain.CurrentDomain.BaseDirectory + "Library";

        public Settings()
        {
            InitializeComponent();
        }

        private void SetWhiteTheme(object sender, RoutedEventArgs e)
        {
            List<string> setting = new List<string>();
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));            
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#EAEAEA"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#424242"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#424242"));
            //Application.Current.Resources["clBrComboBox"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            setting.Add("#FFFFFF");
            setting.Add("#EAEAEA");
            setting.Add("#424242");
            setting.Add("#000000");
            setting.Add("#424242");
            //setting.Add("#000000");
            Serialization.SerializationSetting(setting, fullPath);
            

        }
        private void SetSepiaTheme(object sender, RoutedEventArgs e)
        {
            List<string> setting = new List<string>();
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFEBCD"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#FFDEC3A7"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#442C2E"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#442C2E"));
            //Application.Current.Resources["clBrComboBox"] = (Brush)(new BrushConverter().ConvertFrom("#000000"));
            setting.Add("#FFFFEBCD");
            setting.Add("#FFDEC3A7");
            setting.Add("#442C2E");
            setting.Add("#000000");
            setting.Add("#442C2E");
            //setting.Add("#000000");
            Serialization.SerializationSetting(setting, fullPath);
        }
        private void SetDarkBlueTheme(object sender, RoutedEventArgs e)
        {
            List<string> setting = new List<string>();
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#3539A5"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#535BAB"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#E6E7F1"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#EFEFFE"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#E6E7F1"));
            //Application.Current.Resources["clBrComboBox"] = (Brush)(new BrushConverter().ConvertFrom("#EFEFFE"));
            setting.Add("#3539A5");
            setting.Add("#535BAB");
            setting.Add("#E6E7F1");
            setting.Add("#EFEFFE");
            setting.Add("#E6E7F1");
            //setting.Add("#EFEFFE");
            Serialization.SerializationSetting(setting, fullPath);
        }
        private void SetNightTheme(object sender, RoutedEventArgs e)
        {
            List<string> setting = new List<string>();
            Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom("#363740"));
            Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom("#494A56"));
            Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            //Application.Current.Resources["clBrComboBox"] = (Brush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            setting.Add("#363740");
            setting.Add("#494A56");
            setting.Add("#FFFFFF");
            setting.Add("#FFFFFF");
            setting.Add("#FFFFFF");
            //setting.Add("#FFFFFF");
            Serialization.SerializationSetting(setting, fullPath);
        }
    }
}
