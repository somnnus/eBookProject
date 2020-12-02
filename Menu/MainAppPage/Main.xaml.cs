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
        private int currentPage;

        public Main()
        {
            InitializeComponent();

            DataContext = ResourcesProvider.Current;
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

        private void LeftPageClick()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void RightPageClick()
        {
            if ((CurrentPage + 1) < ResourcesProvider.Current.BooksByPages.Count)
            {
                CurrentPage++;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void Label_MouseDownLeft(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                LeftPageClick();
            }
        }

        private void Label_MouseDownRight(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                RightPageClick();
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                StackPanel stackPanel = (StackPanel)sender;
                Book current = (Book)stackPanel.DataContext;
                OpenBook(current);
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid grid = (Grid)sender;
            if (ResourcesProvider.Current.MaxWidth < grid.ActualWidth)
            {
                ResourcesProvider.Current.MaxWidth = grid.ActualWidth;
            }
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            Book current = (Book)((System.Windows.Controls.ListBoxItem)(e.Source)).DataContext;
            OpenBook(current);
        }

        private void OpenBook(Book current)
        {
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
}
