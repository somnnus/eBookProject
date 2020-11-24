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
using Menu.SharedResources;
using Menu.Helpers;
using LibraryReader;

namespace Menu.PageForRemoving
{
    /// <summary>
    /// Логика взаимодействия для BooksRemoving.xaml
    /// </summary>
    public partial class BooksRemoving : UserControl, INotifyPropertyChanged
    {
        private List<Book> booksForDeleting;
        static string fullPath = AppDomain.CurrentDomain.BaseDirectory + "Library";
        private double maxWidth;

        public BooksRemoving()
        {
            booksForDeleting = new List<Book>();

            DataContext = ResourcesProvider.Current;
            InitializeComponent();
            if (ResourcesProvider.Current.ListBooks.Count != 0)
            {
                ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
                ResourcesProvider.Current.LastSortingFeature = "Sorted By Date";
            }
            else
                MessageBox.Show("Library is empty!");
        }

        public double MaximumWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var selectedFeature = (TextBlock)comboBox.SelectedItem;
            if (ResourcesProvider.Current.ListBooks.Count != 0)
            {
                if (selectedFeature.Text == "Sorted By Author")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByAuthor;
                    ResourcesProvider.Current.LastSortingFeature = "Sorted By Author";
                }
                else
                if (selectedFeature.Text == "Sorted By Title")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByTitle;
                    ResourcesProvider.Current.LastSortingFeature = "Sorted By Title";
                }
                else
                if (selectedFeature.Text == "Sorted By Date")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
                    ResourcesProvider.Current.LastSortingFeature = "Sorted By Date";
                }
            }
            else
                MessageBox.Show("Library is empty!");
        }

        private void DoPreviewingMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                int i = 0;
                while (i < 3)
                {
                    scroll.LineUp();
                    i++;
                }
            }
            if (e.Delta < 0)
            {
                int i = 0;
                while (i < 3)
                {
                    scroll.LineDown();
                    i++;
                }
            }
            e.Handled = true;
        }

        private void SearchInLibrary(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            LibrarySearching.Search(textBox);
        }

        private void CleanLibrary(object sender, RoutedEventArgs e)
        {
            List<Book> delete = new List<Book>();
            foreach (var book in booksForDeleting)
            {
                delete.Add(book);     
                ResourcesProvider.Current.ListBooks.Remove(book);

            }
            ResourcesProvider.Current.deleteBook = delete;
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            Serialization.SerializationBookDelete(delete, fullPath);
            
            LibraryRefreshing.Refresh();
        }


        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                StackPanel stackPanel = (StackPanel)sender;
                Book currentBook = (Book)stackPanel.DataContext;
                var currentCheckBox = (CheckBox)stackPanel.Children[0];

                if (currentCheckBox.IsChecked == false)
                {
                    currentCheckBox.IsChecked = true;
                    booksForDeleting.Add(currentBook);
                }
                else
                if (currentCheckBox.IsChecked == true)
                {
                    currentCheckBox.IsChecked = false;
                    booksForDeleting.Remove(currentBook);
                }
            }
        }

        private void ChooseBookToDelete(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)((CheckBox)sender).Parent;
            Book currentBook = (Book)stackPanel.DataContext;
            var currentCheckBox = (CheckBox)stackPanel.Children[0];

            if (currentCheckBox.IsChecked == true)
            {
                currentCheckBox.IsChecked = true;
                booksForDeleting.Add(currentBook);
            }
            else
            if (currentCheckBox.IsChecked == false)
            {
                currentCheckBox.IsChecked = false;
                booksForDeleting.Remove(currentBook);
            }
        }
        
        private void WrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var currentWidth = ((TextBlock)sender).ActualWidth;
            if (currentWidth > MaximumWidth)
            {
                MaximumWidth = currentWidth;
            }
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var currentWidth = ((Image)sender).ActualWidth;
            if (currentWidth > MaximumWidth)
            {
                MaximumWidth = currentWidth;
            }
        }
        //private void RefreshDict()
        //{
        //    dataGridLib.ItemsSource = ResourcesProvider.Current.SortedBooks;
        //    //CollectionViewSource.GetDefaultView(lastSortingFeature).Refresh();
        //}
    }
}
