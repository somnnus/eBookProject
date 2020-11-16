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
using Menu.SharedResources;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
        static string coverPath = fullPath + "\\" + "Covers";

        public MainWindow(MainWindowViewModel mainWindowVM)
        {
            InitializeComponent();

            ResourcesProvider.Current.MainWindowVM = mainWindowVM;
            DataContext = mainWindowVM;

            CreateHiddenDirectory();
            CheckSerializization();

            FillLibrary(); //обработка постраничного вывода
            
        }

        public void FillLibrary()
        {
            if (ResourcesProvider.Current.ListBooks.Count != 0)
            {
                int blocksCount = 6;
                ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
                ResourcesProvider.Current.BooksByPages = ArrayHelperExtensions.SplitByBlocks(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.BooksByPages, blocksCount);
            }
        }

        public void AddBook(object sender, RoutedEventArgs e)
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
                            ResourcesProvider.Current.ListBooks.Insert(0, currentBook);
                            RefreshPages();

                            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось открыть книгу");
                        }                                             
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удалось открыть книгу");
                        Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
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
                            ResourcesProvider.Current.ListBooks.Insert(0, currentBook);
                            RefreshPages();

                            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось открыть книгу");
                        }
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удалось открыть книгу");
                        File.Delete(newFullFileName);
                        Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                    }
                }              
            }
            else
            {
                MessageBox.Show("Эта книга уже есть в библиотеке!");
            }
        }

        private void RefreshPages() //динамическое отображение
        {
            FillLibrary();

            //динамическая сортировка
            SortByAuthor();
            SortByTitle();
            SortByDate();

            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Author")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByAuthor;
            }
            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Title")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByTitle;
            }
            else
            if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Date")
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
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
                ResourcesProvider.Current.ListBooks =  Serialization.DeserializationLibrary(fileNameSerialize);

                //динамическая сортировка
                SortByAuthor();
                SortByTitle();
                SortByDate();
            }
        }

        private void OpenBook(object sender, RoutedEventArgs e)
        {
            var book = new Book();
            var openedBook = new OpenedBook(book);
            openedBook.Show();
            this.Close();
        }

        private void OpenLastBook(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveBook(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            this.Close();
        }

        private void SortByAuthor()
        {
            ResourcesProvider.Current.ListBooks.Sort(new AuthorComparer());
            ResourcesProvider.Current.SortedByAuthor = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByAuthor = ArrayHelperExtensions.SplitByAuthor(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByAuthor);
        }

        private void SortByTitle()
        {
            ResourcesProvider.Current.ListBooks.Sort(new NameComparer());
            ResourcesProvider.Current.SortedByTitle = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByTitle = ArrayHelperExtensions.SplitByBookName(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByTitle);
        }

        private void SortByDate()
        {
            ResourcesProvider.Current.ListBooks.Sort(new DateComparer());
            ResourcesProvider.Current.SortedByDate = new Dictionary<string, List<Book>>();
            ResourcesProvider.Current.SortedByDate = ArrayHelperExtensions.SplitByDate(ResourcesProvider.Current.ListBooks, ResourcesProvider.Current.SortedByDate);
        }

        //private void RefreshDict()
        //{
        //    dataGridLib.ItemsSource = ResourcesProvider.Current.SortedBooks;
        //    //CollectionViewSource.GetDefaultView(lastSortingFeature).Refresh();
        //}

        class AuthorComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Author.CompareTo(book2.Author);
            }
        }

        class NameComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Title.CompareTo(book2.Title);
            }
        }

        class DateComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                if (book1.Date.CompareTo(book2.Date) == 1)
                    return -1;
                else
                if (book1.Date.CompareTo(book2.Date) == -1)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
