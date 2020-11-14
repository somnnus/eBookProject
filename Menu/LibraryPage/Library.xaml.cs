using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Menu.LibraryPage
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl, INotifyPropertyChanged
    {
        public string lastSortingFeature { get; set; }

        public Library()
        {
            lastSortingFeature = "";

            InitializeComponent();
            dataGridLib.ItemsSource = CommonResources.dictionaryBooks;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastSortingFeature
        {
            get { return lastSortingFeature; }
            set
            {
                lastSortingFeature = value;
                //NotifyPropertyChanged();
            }
        }

        //protected virtual void NotifyPropertyChanged(
        //   [CallerMemberName] String propertyName = "")
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var selectedFeature = (TextBlock)comboBox.SelectedItem;
            if (selectedFeature.Text == "Sorted By Author")
            {
                SortByAuthor();
            }
            else
            if (selectedFeature.Text == "Sorted By Title")
            {
                SortByName();
            }
            else
            if (selectedFeature.Text == "Sorted By Date")
            {
                SortByDate();
            }
        }

        private void SortByAuthor()
        {
            CommonResources.listBooks.Sort(new AuthorComparer());
            LastSortingFeature = "Sorted By Author";
            CommonResources.dictionaryBooks = new Dictionary<string, List<Book>>();
            CommonResources.dictionaryBooks = ArrayHelperExtensions.SplitByAuthor(CommonResources.listBooks, CommonResources.dictionaryBooks);
            RefreshDict();
        }

        private void SortByName()
        {
            CommonResources.listBooks.Sort(new NameComparer());
            LastSortingFeature = "Sorted By Title";
            CommonResources.dictionaryBooks = new Dictionary<string, List<Book>>();
            CommonResources.dictionaryBooks = ArrayHelperExtensions.SplitByBookName(CommonResources.listBooks, CommonResources.dictionaryBooks);
            RefreshDict();
        }

        private void SortByDate()
        {
            CommonResources.listBooks.Sort(new DateComparer());
            LastSortingFeature = "Sorted By Date";
            CommonResources.dictionaryBooks = new Dictionary<string, List<Book>>();
            CommonResources.dictionaryBooks = ArrayHelperExtensions.SplitByDate(CommonResources.listBooks, CommonResources.dictionaryBooks);
            RefreshDict();
        }

        private void RefreshDict()
        {
            dataGridLib.ItemsSource = CommonResources.dictionaryBooks;
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
