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
using LibraryReader.Books;
using System.IO;
using System.Windows.Threading;
using Microsoft.Win32;
using eBdb.EpubReader;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, List<Book>> dictionaryBooks;
        public List<Book> books = new List<Book>();

        public Dictionary<string, List<Books>> dictBooks; //общие ресурсы
        public List<Books> listBooks;

        public delegate void ValuePassDelegate();
        public event ValuePassDelegate ValuePassEvent;

        public delegate void ExitDelegate();
        public event ExitDelegate ExitInTheWindow;

        public delegate void AddBookDelegate();
        public event AddBookDelegate AddBook;

        public delegate void OpenBookDelegate();
        public event OpenBookDelegate OpenBook;

        Epub epub;

        static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
        static string coverPath = fullPath + "\\" + "Covers";
            

        public MainWindow()
        {
            InitializeComponent();
            listBooks = new List<Books>();

            CreateHiddenDirectory();
            CheckSerializization();

            ValuePassEvent = new ValuePassDelegate(method1);
            mainScreen.del = ValuePassEvent;

            ExitInTheWindow = new ExitDelegate(method2);
            mainScreen.del2 = ExitInTheWindow;

            FillWithBooks();

            AddBook = new AddBookDelegate(method3);
            mainScreen.delAddBook = AddBook;

            OpenBook = new OpenBookDelegate(method4);
            mainScreen.delOpenBook = OpenBook;
        }

        //private void FillLibrary()
        //{
        //    AddBooks(listBooks);
        //    int blocksCount = 6;
        //    //mainScreen.dictBooks = new Dictionary<string, List<Books>>();
        //    //mainScreen.dictBooks = ArrayHelperExtensions.SplitByBlocks(listBooks, mainScreen.dictBooks, blocksCount);

        //    //dictBooks = mainScreen.dictBooks;

        //    mainScreen.listBoxBooks.ItemsSource = mainScreen.dictBooks["0"];
        //}

        public void FillWithBooks()
        {
            if (books.Count != 0)
            {
                int blocksCount = 6;
                mainScreen.dictBooks = new Dictionary<string, List<Book>>();
                mainScreen.dictBooks = ArrayHelperExtensions.SplitByBlocks(books, mainScreen.dictBooks, blocksCount);

                dictionaryBooks = mainScreen.dictBooks;

                mainScreen.listBoxBooks.ItemsSource = mainScreen.dictBooks["0"];
            }
        }

        public void method1()
        {
            contentMain.Content = new Library(dictionaryBooks, books);
        }

        public void method2()
        {
            Serialization.SerializationInformationAboutBook(books, fullPath);
            Close();
        }

        public void method3()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Книги (*.epub, *.fb2)|*.epub;*.fb2";

            string path = "";
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
            }

            string newFullFileName = fullPath + "\\" + System.IO.Path.GetFileName(path);
            string fileName = System.IO.Path.GetFileName(newFullFileName);

            if (!File.Exists(newFullFileName))
            {
                File.Copy(path, newFullFileName);

               // Book currentBook = null;

                if (fileName.Contains(".epub"))
                {
                    try
                    {
                        Book currentBook = null;
                        epub = new Epub(newFullFileName);
                        currentBook = new EpubBook(newFullFileName);
                        books.Add(currentBook);
                        FillWithBooks();
                        Serialization.SerializationInformationAboutBook(books, fullPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удается открыть книгу");
                        File.Delete(newFullFileName);
                        Serialization.SerializationInformationAboutBook(books, fullPath);
                    }

                    
                }
                else if (fileName.Contains(".fb2"))
                {
                   
                        Book currentBook = null;
                        currentBook = new FB2Book(newFullFileName);
                        books.Add(currentBook);
                        FillWithBooks();
                        Serialization.SerializationInformationAboutBook(books, fullPath);
                    

                }
                
            }
        }

        public void method4()
        {
            contentMain.Content = new OpenedPage();
        }

        private void CreateHiddenDirectory()
        {           
            if (!Directory.Exists(fullPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(fullPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            if (!Directory.Exists(coverPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(coverPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private void CheckSerializization()
        {
            string fileNameSerialize = fullPath + "\\" + "library.xml";
            
            if (File.Exists(fileNameSerialize))
            {
              books =  Serialization.DeserializationLibrary(fileNameSerialize);
            }

        }

        //private List<Books> AddBooks(List<Books> books)
        //{
        //    books.Add(new Books()
        //    {
        //        author = "Достоевский",
        //        bookName = "Братья Карамазовы",
        //        imagePath = "images/SCAN_20140123_185338818.jpg",
        //        date = new DateTime(2000, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Толстой",
        //        bookName = "Анна Каренина",
        //        imagePath = "images/SCAN_20140123_185430521.jpg",
        //        date = new DateTime(2001, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Пушкин",
        //        bookName = "Евгений Онегин",
        //        imagePath = "images/Scan_20170628_174511.jpg",
        //        date = new DateTime(2000, 10, 2)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Тургенев",
        //        bookName = "Отцы и дети",
        //        imagePath = "images/SCAN_20140123_185338818.jpg",
        //        date = new DateTime(2001, 11, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Куприн",
        //        bookName = "Гранатовый браслет",
        //        imagePath = "images/SCAN_20140123_185430521.jpg",
        //        date = new DateTime(2000, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Пастернак",
        //        bookName = "Доктор Живаго",
        //        imagePath = "images/Scan_20170628_174511.jpg",
        //        date = new DateTime(2003, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Пушкин",
        //        bookName = "Капитанская дочка",
        //        imagePath = "images/SCAN_20140123_185338818.jpg",
        //        date = new DateTime(2004, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Набоков",
        //        bookName = "Лолита",
        //        imagePath = "images/SCAN_20140123_185430521.jpg",
        //        date = new DateTime(2004, 9, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Маяковский",
        //        bookName = "Если звёзды зажигаются...",
        //        imagePath = "images/Scan_20170628_174511.jpg",
        //        date = new DateTime(2004, 8, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Булгаков",
        //        bookName = "Мастер и Маргарита",
        //        imagePath = "images/SCAN_20140123_185338818.jpg",
        //        date = new DateTime(2005, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Толстой",
        //        bookName = "Война и мир",
        //        imagePath = "images/SCAN_20140123_185430521.jpg",
        //        date = new DateTime(2006, 10, 1)
        //    });
        //    books.Add(new Books()
        //    {
        //        author = "Лермонтов",
        //        bookName = "Герой нашего времени",
        //        imagePath = "images/Scan_20170628_174511.jpg",
        //        date = new DateTime(2020, 10, 1)
        //    });
        //    return books;
        //}
    }
}
