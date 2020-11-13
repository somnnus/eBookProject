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

        public delegate void ValuePassDelegate();
        public event ValuePassDelegate ValuePassEvent;

        public delegate void ExitDelegate();
        public event ExitDelegate ExitInTheWindow;

        //public delegate void AddBookDelegate();
        //public event AddBookDelegate AddBook;

        MainWindowViewModel mainWindowViewModel;

        static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
        static string coverPath = fullPath + "\\" + "Covers";
            

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            this.mainWindowViewModel = mainWindowViewModel;
            DataContext = mainWindowViewModel;

            CreateHiddenDirectory();
            CheckSerializization();

            //ValuePassEvent = new ValuePassDelegate(method1);
            //mainScreen.del = ValuePassEvent;

            //ExitInTheWindow = new ExitDelegate(method2);
            //mainScreen.del2 = ExitInTheWindow;

            //FillWithBooks();

            //AddBook = new AddBookDelegate(method3);
            //mainScreen.delAddBook = AddBook;
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
                //mainScreen.dictBooks = new Dictionary<string, List<Book>>();
                //mainScreen.dictBooks = ArrayHelperExtensions.SplitByBlocks(books, mainScreen.dictBooks, blocksCount);

                //dictionaryBooks = mainScreen.dictBooks;

                //mainScreen.listBoxBooks.ItemsSource = mainScreen.dictBooks["0"];
            }
        }

        public void method1()
        {
            //contentMain.Content = new Library(dictionaryBooks, books);
        }

        public void method2()
        {
            Serialization.SerializationInformationAboutBook(books, fullPath);
            Close();
        }

        private void AddBook(object s, RoutedEventArgs eventArgs)
        {
            method3();
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
                if (fileName.Contains(".epub"))
                {
                    try
                    {
                        Book currentBook = null;                      
                        currentBook = new EpubBook(path,newFullFileName);
                        if (currentBook != null)
                        {                            
                            books.Add(currentBook);
                            FillWithBooks();                           
                            Serialization.SerializationInformationAboutBook(books, fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось открыть книгу");
                        }                                             
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось открыть книгу");
                        Serialization.SerializationInformationAboutBook(books, fullPath);
                    }

                    
                }
                else if (fileName.Contains(".fb2"))
                {
                    try
                    {
                        Book currentBook = null;
                        currentBook = new FB2Book(path,newFullFileName);
                        if (currentBook != null)
                        {
                            books.Add(currentBook);
                            FillWithBooks();
                            Serialization.SerializationInformationAboutBook(books, fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось открыть книгу");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось открыть книгу");
                        File.Delete(newFullFileName);
                        Serialization.SerializationInformationAboutBook(books, fullPath);
                    }

                }              
            }
            else
            {
                MessageBox.Show("Эта книга уже есть в библиотеке!");
            }
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

        private void OpenBook(object sender, RoutedEventArgs eventArgs)
        {
            var openedBook = new OpenedBook(mainWindowViewModel);
            openedBook.Show();
            this.Close();
        }
    }
}
