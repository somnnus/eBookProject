using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Book> books = new List<Book>();

        public MainWindow()
        {
            InitializeComponent();

            listBoxBooks.ItemsSource = AddBooks(books);
        }

        private List<Book> AddBooks(List<Book> books)
        {
            books.Add(new Book()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            books.Add(new Book()
            {
                author = "Достоевский",
                bookName = "Братья Карамазовы",
                imagePath = "images/SCAN_20140123_185338818.jpg"
            });
            books.Add(new Book()
            {
                author = "Толстой",
                bookName = "Анна Каренина",
                imagePath = "images/SCAN_20140123_185430521.jpg"
            });
            books.Add(new Book()
            {
                author = "Пушкин",
                bookName = "Евгений Онегин",
                imagePath = "images/Scan_20170628_174511.jpg"
            });
            return books;
        }

        //private Point myMousePlacementPoint;

        //private void OnListViewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.MiddleButton == MouseButtonState.Pressed)
        //    {
        //        myMousePlacementPoint = this.PointToScreen(Mouse.GetPosition(this));
        //    }
        //}

        //private void OnListViewMouseMove(object sender, MouseEventArgs e)
        //{
        //    ScrollViewer scrollViewer = GetScrollViewer(listBoxBooks) as ScrollViewer;

        //    if (e.MiddleButton == MouseButtonState.Pressed)
        //    {
        //        var currentPoint = this.PointToScreen(Mouse.GetPosition(this));

        //        if (currentPoint.Y < myMousePlacementPoint.Y)
        //        {
        //            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 3);
        //        }
        //        else if (currentPoint.Y > myMousePlacementPoint.Y)
        //        {
        //            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 3);
        //        }

        //        if (currentPoint.X < myMousePlacementPoint.X)
        //        {
        //            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 3);
        //        }
        //        else if (currentPoint.X > myMousePlacementPoint.X)
        //        {
        //            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 3);
        //        }
        //    }
        //}

        //public static DependencyObject GetScrollViewer(DependencyObject o)
        //{
        //    // Return the DependencyObject if it is a ScrollViewer
        //    if (o is ScrollViewer)
        //    { return o; }

        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(o, i);

        //        var result = GetScrollViewer(child);
        //        if (result == null)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            return result;
        //        }
        //    }
        //    return null;
        //}
    }
}
