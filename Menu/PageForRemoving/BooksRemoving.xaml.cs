﻿using LibraryReader.Books;
using Menu.SharedResources;
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
using Menu.Comparers;
using Menu.Helpers;
using System.IO;
using LibraryReader;

namespace Menu.PageForRemoving
{
    /// <summary>
    /// Логика взаимодействия для BooksRemoving.xaml
    /// </summary>
    public partial class BooksRemoving : UserControl
    {
        private List<Book> booksForDeleting;
        static string fullPath = AppDomain.CurrentDomain.BaseDirectory + "Library";

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
        
        private void SearchInLibrary(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            LibrarySearching.Search(textBox);
        }

        private void CleanLibrary(object sender, RoutedEventArgs e)
        {
            foreach (var book in booksForDeleting)
            {
              //  File.Delete(book.FullPath);              
                ResourcesProvider.Current.ListBooks.Remove(book);                       
            }
           // Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            
            LibraryRefreshing.Refresh();
            //Написать вызов к SortingLibrary
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

        //private void RefreshDict()
        //{
        //    dataGridLib.ItemsSource = ResourcesProvider.Current.SortedBooks;
        //    //CollectionViewSource.GetDefaultView(lastSortingFeature).Refresh();
        //}
    }
}
