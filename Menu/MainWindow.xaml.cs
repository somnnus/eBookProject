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
       
        static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
        static string coverPath = fullPath + "\\" + "Covers";

        public MainWindow(MainWindowViewModel mainWindowVM)
        {
            InitializeComponent();

            CommonResources.mainWindowViewModel = mainWindowVM;
            DataContext = mainWindowVM;

            CreateHiddenDirectory();
            CheckSerializization();

            FillLibrary(); //обработка постраничного вывода
        }

        public void FillLibrary()
        {
            if (CommonResources.listBooks.Count != 0)
            {
                int blocksCount = 6;
                CommonResources.booksByPages = new Dictionary<string, List<Book>>();
                CommonResources.booksByPages = ArrayHelperExtensions.SplitByBlocks(CommonResources.listBooks, CommonResources.booksByPages, blocksCount);
            }
        }

        public void AddBook(object sender, RoutedEventArgs eventArgs)
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
                            CommonResources.listBooks.Insert(0, currentBook);
                            FillLibrary();                           
                            Serialization.SerializationInformationAboutBook(CommonResources.listBooks, fullPath);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось открыть книгу");
                        }                                             
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удалось открыть книгу");
                        Serialization.SerializationInformationAboutBook(CommonResources.listBooks, fullPath);
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
                            CommonResources.listBooks.Insert(0, currentBook);
                            FillLibrary();
                            Serialization.SerializationInformationAboutBook(CommonResources.listBooks, fullPath);
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
                        Serialization.SerializationInformationAboutBook(CommonResources.listBooks, fullPath);
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
                CommonResources.listBooks =  Serialization.DeserializationLibrary(fileNameSerialize);
            }
        }

        private void OpenLastBook(object sender, RoutedEventArgs eventArgs)
        {
            string fileNameSerialazeLastBook = fullPath + "\\" + "last.xml";
            Book current = null;
            if (File.Exists(fileNameSerialazeLastBook))
            {
                current = Serialization.DeserializationLastBook(fileNameSerialazeLastBook);
                var openedBook = new OpenedBook(current);
                openedBook.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nothing");
            }


            }
        //private void OpenBook(object sender, RoutedEventArgs eventArgs)
        //{
        //    Book current = null;
        //    var openedBook = new OpenedBook(current);
        //    openedBook.Show();
        //    this.Close();
        //}

        private void RemoveBook(object sender, RoutedEventArgs eventArgs)
        {

        }

        private void Exit(object sender, RoutedEventArgs eventArgs)
        {
            Serialization.SerializationInformationAboutBook(CommonResources.listBooks, fullPath);
            this.Close();
        }
    }
}
