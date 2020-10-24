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
using System.IO;
using System.Windows.Threading;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NavigationService service;
        List<Book> books = new List<Book>();

        Dictionary<int, List<Book>> dictKeys = new Dictionary<int, List<Book>>();

        int currentPage = 0;

        int blocksCount;

        public MainWindow()
        {
            InitializeComponent();
            
            blocksCount = 6;
            dictKeys = ArrayHelperExtensions.Split(AddBooks(books), dictKeys, blocksCount);

            if (currentPage == 0)
            {
                listBoxBooks.ItemsSource = dictKeys[0];
            }
            //Loaded += OpenLibraryPage;
            service = NavigationService.GetNavigationService(this);
        }

        //void OpenLibraryPage(object sender, RoutedEventArgs e)
        //{

        //    IsLoaded = true;

        //    nav = NavigationService.GetNavigationService(this);
        //    nav.Navigate(new Uri("LibraryPage.xaml", UriKind.RelativeOrAbsolute));

        //}

        private void LeftPageClick(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                listBoxBooks.ItemsSource = dictKeys[currentPage];
            }
        }

        private void RightPageClick(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) < dictKeys.Count)
            {
                currentPage++;
                listBoxBooks.ItemsSource = dictKeys[currentPage];
            }
        }

        private void OpenLibraryPage(object sender, RoutedEventArgs e)
        {
            Page libPage;

            try
            {
                libPage = (Page)Application.LoadComponent(new Uri("LibraryPage.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                // note error and abort
                return;
            }


            libPage.Loaded += (sender1,e1) => service.Navigate(libPage);
            //Application.Current.Dispatcher.BeginInvoke(Loaded, DispatcherPriority.Normal);
            

            //while (libPage.IsLoaded)
            //{
            //    NavigationService service = NavigationService.GetNavigationService(this);
            //    service.Navigate(libPage);
            //}


            //NavigationService service = NavigationService.GetNavigationService(menu);
            //service.Navigate(new Uri("LibraryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private List<Book> AddBooks(List<Book> books)
        {
            books.Add(new Book()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Тургенев",
                bookName = "Отцы и дети",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Куприн",
                bookName = "Гранатовый браслет",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пастернак",
                bookName = "Доктор Живаго",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Капитанская дочка",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Набоков",
                bookName = "Лолита",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Маяковский",
                bookName = "Если звёзды зажигаются...",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Булгаков",
                bookName = "Мастер и Маргарита",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Война и мир",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Лермонтов",
                bookName = "Герой нашего времени",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            return books;
        }
    }
}
