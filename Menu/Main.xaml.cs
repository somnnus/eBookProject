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

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Dictionary<int, List<Book>> dictBooks;

        public Delegate del;
        public Delegate del2;

        public void method1()
        {
            del.DynamicInvoke();
        }

        public void method2()
        {
            del2.DynamicInvoke();
        }
        
        int currentPage = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void LeftPageClick(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                listBoxBooks.ItemsSource = dictBooks[currentPage];
            }
        }

        private void RightPageClick(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) < dictBooks.Count)
            {
                currentPage++;
                listBoxBooks.ItemsSource = dictBooks[currentPage];
            }
        }

        public void OpenLibraryPage(object sender, RoutedEventArgs args)
        {
            method1();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            method2();
        }
    }
}
