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
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для OpenedBook.xaml
    /// </summary>
    public partial class OpenedBook : Window
    {
        public OpenedBook(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            InitializeComponent();
        }

        MainWindowViewModel mainWindowViewModel;

        private void OpenMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            var menuWindow = new MainWindow(mainWindowViewModel);
            menuWindow.Show();
            Close();
        }
    }
}
