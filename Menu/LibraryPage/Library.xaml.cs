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

namespace Menu.LibraryPage
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl, INotifyPropertyChanged
    {
        private double maxWidth;

        public Library()
        {
            InitializeComponent();
            //dataGridLib.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //dataGridLib.Arrange(new Rect(0, 0, dataGridLib.DesiredSize.Width, dataGridLib.DesiredSize.Height));
            DataContext = ResourcesProvider.Current;
            ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
            ResourcesProvider.Current.LastSortingFeature = "Sorted By Date";
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
        
        //private void DoPreviewingMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (e.Delta > 0)
        //    {
        //        int i = 0;
        //        while (i < 3)
        //        {
        //            scroll.LineUp();
        //            i++;
        //        }
        //    }
        //    if (e.Delta < 0)
        //    {
        //        int i = 0;
        //        while (i < 3)
        //        {
        //            scroll.LineDown();
        //            i++;
        //        }
        //    }
        //    e.Handled = true;
        //}

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

        private void SearchInLibrary(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            LibrarySearching.Search(textBox);
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
    }
}
