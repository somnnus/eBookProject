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
using Menu.Helpers;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
        private static string coverPath = fullPath + "\\" + "Covers";
       

        public MainWindow(MainWindowViewModel mainWindowVM)
        {
            InitializeComponent();

            ResourcesProvider.Current.MainWindowVM = mainWindowVM;
            DataContext = mainWindowVM;

            CreateHiddenDirectory();
            CheckSerializization();
            CheckSettingsSerialization();
            CheckSerializationBookDelete();

            LibraryRefreshing.FillMain(); //обработка постраничного вывода
        }

        private void AddBook(object sender, RoutedEventArgs e)
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
                            if (FormatCheck(currentBook))
                            {
                                MessageBox.Show("The book has already been added in the library!");
                                File.Delete(currentBook.CoverPath);
                                File.Delete(currentBook.FullPath);
                            }
                            else
                            {
                                ResourcesProvider.Current.ListBooks.Insert(0, currentBook);
                                RefreshPages();
                                Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Couldn't open the book");
                        }                                             
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Couldn't open the book");
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
                            if (FormatCheck(currentBook))
                            {
                                MessageBox.Show("The book has already been added in the library!");
                                File.Delete(currentBook.CoverPath);
                                File.Delete(currentBook.FullPath);
                            }
                            else
                            {
                                ResourcesProvider.Current.ListBooks.Insert(0, currentBook);
                                RefreshPages();
                                Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Couldn't open the book");
                        }
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Couldn't open the book");
                        File.Delete(newFullFileName);
                        Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                    }
                }              
            }
            else
            {
                if (ResourcesProvider.Current.deleteBook != null)
                {
                    foreach(var book in ResourcesProvider.Current.deleteBook)
                    {
                        if (book.FullPath == newFullFileName)
                        {
                            ResourcesProvider.Current.deleteBook.Remove(book);
                            ResourcesProvider.Current.ListBooks.Insert(0, book);
                            RefreshPages();

                            Serialization.SerializationBookDelete(ResourcesProvider.Current.deleteBook, fullPath);
                            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The book has already been added in the library!");
                }
            }
        }

        private bool FormatCheck(Book current)
        {
            bool flag = false;
            foreach (var book in ResourcesProvider.Current.ListBooks)
            {
                if(book.Title== current.Title && book.Author == current.Author)
                {
                    flag = true;
                    return flag;
                }
            }
            return flag;
        }

        private void RefreshPages() //динамическое отображение
        {
            LibraryRefreshing.Refresh();
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
                LibraryRefreshing.SortByAuthor();
                LibraryRefreshing.SortByTitle();
                LibraryRefreshing.SortByDate();
            }
        }
        private Book CheckLastBookSerializization()
        {
            string fileNameSerialize = fullPath + "\\" + "last.xml";
            if (File.Exists(fileNameSerialize))
            {
                Book book = Serialization.DeserializationLastBook(fileNameSerialize);
                return book;
            }
            else
            {
                return null;
            }
        }

        private void CheckSettingsSerialization()
        {
            string fileNameSerialize = fullPath + "\\" + "setting.xml";
            List<string> setting;
            if (File.Exists(fileNameSerialize))
            {
                setting = Serialization.DeserializationSetting(fileNameSerialize);
                Application.Current.Resources["clBr"] = (Brush)(new BrushConverter().ConvertFrom(setting[0]));
                Application.Current.Resources["clBrSearch"] = (Brush)(new BrushConverter().ConvertFrom(setting[1]));
                Application.Current.Resources["clBrMenu"] = (Brush)(new BrushConverter().ConvertFrom(setting[2]));
                Application.Current.Resources["clBrText"] = (Brush)(new BrushConverter().ConvertFrom(setting[3]));
                Application.Current.Resources["clBrArrow"] = (Brush)(new BrushConverter().ConvertFrom(setting[4]));
                //Application.Current.Resources["clBrComboBox"] = (Brush)(new BrushConverter().ConvertFrom(setting[5]));
            }
        }

        private void CheckSerializationBookDelete()
        {
            string fileNameSerialize = fullPath + "\\" + "delete.xml";
            List<Book> deleteBooks = null;
            if (File.Exists(fileNameSerialize))
            {
                deleteBooks = Serialization.DeserializationBookDelete(fileNameSerialize);
                File.Delete(fileNameSerialize);
            }
            if (deleteBooks!=null)
            {
                foreach (var book in deleteBooks)
                {
                    File.Delete(book.FullPath);
                   // if (!book.CoverPath.Contains("defoltCover"))       
                    File.Delete(book.CoverPath);
                }
                ResourcesProvider.Current.deleteBook = null;
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
            bool found = false;
            Book book = CheckLastBookSerializization();
            if (book != null)
            {
                foreach(var dict in ResourcesProvider.Current.ListBooks)
                {
                    if (book.FullPath == dict.FullPath)
                    {
                        found = true;
                        var openedBook = new OpenedBook(book);
                        openedBook.Show();
                        this.Close();
                    }
                    else
                    {
                        File.Delete(fullPath + "\\last.xml");
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Book wasn't found");
                }
            }
            else
            {
                MessageBox.Show("You haven't opened the book earlier!");
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            this.Close();
        }

        //private void RefreshDict()
        //{
        //    dataGridLib.ItemsSource = ResourcesProvider.Current.SortedBooks;
        //    //CollectionViewSource.GetDefaultView(lastSortingFeature).Refresh();
        //}
    }
}
