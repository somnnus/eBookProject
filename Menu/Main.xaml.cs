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

        List<Books> books = new List<Books>();

        Dictionary<int, List<Books>> dictKeys = new Dictionary<int, List<Books>>();

        int currentPage = 0;

        int blocksCount;

        public Main()
        {
            InitializeComponent();

            blocksCount = 6;
            dictKeys = ArrayHelperExtensions.Split(AddBooks(books), dictKeys, blocksCount);

            if (currentPage == 0)
            {
                listBoxBooks.ItemsSource = dictKeys[0];
            }
        }

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

        public void OpenLibraryPage(object sender, RoutedEventArgs args)
        {
            method1();
        }

        private List<Books> AddBooks(List<Books> books)
        {
            books.Add(new Books()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Books()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Books()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Books()
            {
                author = "Тургенев",
                bookName = "Отцы и дети",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Books()
            {
                author = "Куприн",
                bookName = "Гранатовый браслет",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Books()
            {
                author = "Пастернак",
                bookName = "Доктор Живаго",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Books()
            {
                author = "Пушкин",
                bookName = "Капитанская дочка",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Books()
            {
                author = "Набоков",
                bookName = "Лолита",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Books()
            {
                author = "Маяковский",
                bookName = "Если звёзды зажигаются...",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Books()
            {
                author = "Булгаков",
                bookName = "Мастер и Маргарита",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Books()
            {
                author = "Толстой",
                bookName = "Война и мир",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Books()
            {
                author = "Лермонтов",
                bookName = "Герой нашего времени",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            return books;
        }     

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            method2();
        }
    }
}
