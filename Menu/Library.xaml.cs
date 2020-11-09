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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl, INotifyPropertyChanged
    {
        public Dictionary<string, List<Book>> dictBooks { get; set; }
        public List<Book> listBooks { get; set; }

        public string lastSortingFeature { get; set; }


        public Library(Dictionary<string, List<Book>> dict, List<Book> list)
        {
            DataContext = this;
            dictBooks = dict;
            listBooks = list;
            lastSortingFeature = "";

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastSortingFeature
        {
            get { return lastSortingFeature; }
            set
            {
                lastSortingFeature = value;
                NotifyPropertyChanged();
            }
        }

        protected virtual void NotifyPropertyChanged(
           [CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void SortByAuthor(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new AuthorComparer());
            LastSortingFeature = "Sorted By Author";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByAuthor(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new NameComparer());
            LastSortingFeature = "Sorted By Title";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByBookName(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByDate(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new DateComparer());
            LastSortingFeature = "Sorted By Date";
            dictBooks = new Dictionary<string, List<Book>>();
            dictBooks = ArrayHelperExtensions.SplitByDate(listBooks, dictBooks);
            RefreshDict();
        }

        private void RefreshDict()
        {
            dataGridLib.ItemsSource = dictBooks;
            //CollectionViewSource.GetDefaultView(lastSortingFeature).Refresh();
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
