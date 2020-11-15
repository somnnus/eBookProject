using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Menu.SharedResources;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Menu.Helpers;

namespace Menu.MainAppPage
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl, INotifyPropertyChanged
    {
        public int currentPage = 0;

        public List<Book> GetCurrentList
        {
            get { return ResourcesProvider.Current.BooksByPages[CurrentPage]; }
        }

        public Main()
        {
            InitializeComponent();

            DataContext = ResourcesProvider.Current;
            if (ResourcesProvider.Current.BooksByPages.Count != 0)
            {
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages["0"];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
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

        private void LeftPageClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void RightPageClick(object sender, RoutedEventArgs e)
        {
            if ((CurrentPage + 1) < ResourcesProvider.Current.BooksByPages.Count)
            {
                CurrentPage++;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                StackPanel stackPanel = (StackPanel)sender;
                Book current = (Book)stackPanel.DataContext;
                var openedBook = new OpenedBook(current);
                openedBook.Show();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
        }

        //private MultiBinding createFieldMultiBinding(string fieldName)
        //{
        //    // Create the multi-binding
        //    MultiBinding mbBinding = new MultiBinding();
        //    // Create the dictionary binding
        //    Binding bDictionary = new Binding(ResourcesProvider.Current.BooksByPages);
        //    bDictionary.Source = this.DataContext;
        //    // Create the key binding
        //    Binding bKey = new Binding(fieldName);
        //    bKey.Source = this.DataContext;
        //    // Set the multi-binding converter
        //    mbBinding.Converter = new DictionaryItemConverter();
        //    // Add the bindings to the multi-binding
        //    mbBinding.Bindings.Add(bDictionary);
        //    mbBinding.Bindings.Add(bKey);

        //    return mbBinding;
        //}
    }
}
