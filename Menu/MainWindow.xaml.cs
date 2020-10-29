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

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ValuePassDelegate();
        public event ValuePassDelegate ValuePassEvent;

        public delegate void ExitDelegate();
        public event ExitDelegate ExitInTheWindow;

        public delegate void AddBookDelegate();
        public event AddBookDelegate AddBook;

        private List<Book> books = new List<Book>();

        static string fullPath = AppDomain.CurrentDomain.BaseDirectory+"Library";
            

        public MainWindow()
        {
            InitializeComponent();

            CreateHiddenDirectory();

            ValuePassEvent = new ValuePassDelegate(method1);
            mainScreen.del = ValuePassEvent;

            ExitInTheWindow = new ExitDelegate(method2);
            mainScreen.del2 = ExitInTheWindow;

            AddBook = new AddBookDelegate(method3);
            mainScreen.delAddBook = AddBook;
        }
        public void method1()
        {
            contentMain.Content = new Library();
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
            }
                      

            Book currentBook = new Book();
            currentBook.FullPath = newFullFileName;
            if (fileName.Contains(".epub"))
            {
                currentBook.Format = Book.FormatBook.EPUB;
            }
            else if (fileName.Contains(".fb2"))
            {
                currentBook.Format = Book.FormatBook.FB2;
            }
            books.Add(currentBook);
        }

        private void CreateHiddenDirectory()
        {           
            if (!Directory.Exists(fullPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(fullPath);
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
    }
}
