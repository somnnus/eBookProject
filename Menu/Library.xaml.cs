using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using LibraryReader.Books;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Dictionary<string, List<Book>> dictBooks { get; set; }
        public List<Book> listBooks { get; set; }
        public string lastSortedFeature { get; set; }

        public Library(Dictionary<string, List<Book>> dict, List<Book> list)
        {
            DataContext = this;
            dictBooks = dict;
            listBooks = list;

            lastSortedFeature = "";
            
            InitializeComponent();
            
        }

        private void SortByAuthor(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new AuthorComparer());
            lastSortedFeature = "Sorted By Author";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByAuthor(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new NameComparer());
            lastSortedFeature = "Sorted By Book Name";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByBookName(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByDate(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new DateComparer());
            lastSortedFeature = "Sorted By Date";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByDate(listBooks, dictBooks);
            RefreshDict();
        }

        private void RefreshDict()
        {
            dataGridLib.ItemsSource = dictBooks;
            //CollectionViewSource.GetDefaultView(dataGridLib.ItemsSource).Refresh();
        }

        private void DoPreviewingMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ScrollBar.LineUpCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            if (e.Delta < 0)
            {
                ScrollBar.LineDownCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            e.Handled = true;
        }
    }

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
            return book1.Date.CompareTo(book2.Date);
        }
    }
}
