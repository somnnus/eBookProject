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
        Dictionary<int, List<Book>> dictBooks; //общие ресурсы
        List<Book> listBooks;

        public delegate void ValuePassDelegate();
        public event ValuePassDelegate ValuePassEvent;

        public delegate void ExitDelegate();
        public event ExitDelegate ExitInTheWindow;

        public MainWindow()
        {
            InitializeComponent();
            listBooks = new List<Book>();

            ValuePassEvent = new ValuePassDelegate(method1);
            mainScreen.del = ValuePassEvent;

            ExitInTheWindow = new ExitDelegate(method2);
            mainScreen.del2 = ExitInTheWindow;

            FillLibrary();
        }

        private void FillLibrary()
        {
            AddBooks(listBooks);
            int blocksCount = 6;
            mainScreen.dictBooks = new Dictionary<int, List<Book>>();
            mainScreen.dictBooks = ArrayHelperExtensions.Split(listBooks, mainScreen.dictBooks, blocksCount);

            dictBooks = mainScreen.dictBooks;

            mainScreen.listBoxBooks.ItemsSource = mainScreen.dictBooks[0];
        }

        public void method1()
        {
            contentMain.Content = new Library(dictBooks);
        }

        public void method2()
        {
            Close();
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
