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

namespace Menu.LibraryPage
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Library()
        {
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
                ScrollBar.LineUpCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            if (e.Delta < 0)
            {
                ScrollBar.LineDownCommand.Execute(null, e.OriginalSource as IInputElement);
            }
            e.Handled = true;
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

        private void SearchInLibrary(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            ResourcesProvider.Current.CurrentDictionary = new Dictionary<string, List<Book>>();
            string localKey = "";

            if (textBox.Text != "")
            {
                foreach (var book in ResourcesProvider.Current.ListBooks)
                {
                    if (CompareStrings(book.Author, textBox.Text, StringComparison.OrdinalIgnoreCase) || CompareStrings(book.Title, textBox.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        if (ResourcesProvider.Current.CurrentDictionary.ContainsKey(localKey))
                        {
                            ResourcesProvider.Current.CurrentDictionary[localKey].Add(book);
                        }
                        else
                        {
                            ResourcesProvider.Current.CurrentDictionary.Add(localKey, new List<Book>());
                            ResourcesProvider.Current.CurrentDictionary[localKey].Add(book);
                        }
                    }
                }
                foreach (var list in ResourcesProvider.Current.CurrentDictionary.Values)
                {
                    foreach (var book in list)
                    {
                        if (!CompareStrings(book.Author, textBox.Text, StringComparison.OrdinalIgnoreCase) && !CompareStrings(book.Title, textBox.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            ResourcesProvider.Current.CurrentDictionary[localKey].Remove(book);
                        }
                    }
                }
            }
            else
            {
                if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Author")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByAuthor;
                }
                else
                if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Title")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByTitle;
                }
                else
                if (ResourcesProvider.Current.LastSortingFeature == "Sorted By Date")
                {
                    ResourcesProvider.Current.CurrentDictionary = ResourcesProvider.Current.SortedByDate;
                }
            }
        }

        public bool CompareStrings(string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
