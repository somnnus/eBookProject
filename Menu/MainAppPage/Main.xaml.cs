using LibraryReader.Books;
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

namespace Menu.MainAppPage
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Dictionary<string, List<Book>> dictBooks;

        public Delegate del;
        public Delegate del2;
        public Delegate delAddBook;
        public Delegate delOpenBook;

        public void method1()
        {
            del.DynamicInvoke();
        }

        public void method2()
        {
            del2.DynamicInvoke();
        }

        public void method3()
        {
            delAddBook.DynamicInvoke();
        }

        public void method4()
        {
            delOpenBook.DynamicInvoke();
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
                listBoxBooks.ItemsSource = dictBooks[currentPage.ToString()];
            }
        }

        private void RightPageClick(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) < dictBooks.Count)
            {
                currentPage++;
                listBoxBooks.ItemsSource = dictBooks[currentPage.ToString()];
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

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            method3();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                method4();
            }
        }
    }
}
