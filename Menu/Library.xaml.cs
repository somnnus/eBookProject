﻿using System;
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

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : UserControl
    {
        public Dictionary<string, List<Books>> dictBooks { get; set; }
        public List<Books> listBooks { get; set; }
        public string lastSortedFeature { get; set; }

        public Library(Dictionary<string, List<Books>> dict, List<Books> list)
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
            dictBooks = new Dictionary<string, List<Books>>();
            dictBooks = ArrayHelperExtensions.SplitByAuthor(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new NameComparer());
            lastSortedFeature = "Sorted By Book Name";
            dictBooks = new Dictionary<string, List<Books>>();
            dictBooks = ArrayHelperExtensions.SplitByBookName(listBooks, dictBooks);
            RefreshDict();
        }

        private void SortByDate(object sender, RoutedEventArgs e)
        {
            listBooks.Sort(new DateComparer());
            lastSortedFeature = "Sorted By Date";
            dictBooks = new Dictionary<string, List<Books>>();
            dictBooks = ArrayHelperExtensions.SplitByDate(listBooks, dictBooks);
            RefreshDict();
        }

        private void RefreshDict()
        {
            dataGridLib.ItemsSource = dictBooks;
            CollectionViewSource.GetDefaultView(dataGridLib.ItemsSource).Refresh();
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

    class AuthorComparer : IComparer<Books>
    {
        public int Compare(Books book1, Books book2)
        {
            return book1.author.CompareTo(book2.author);
        }
    }

    class NameComparer : IComparer<Books>
    {
        public int Compare(Books book1, Books book2)
        {
            return book1.bookName.CompareTo(book2.bookName);
        }
    }

    class DateComparer : IComparer<Books>
    {
        public int Compare(Books book1, Books book2)
        {
            return book1.date.CompareTo(book2.date);
        }
    }
}
