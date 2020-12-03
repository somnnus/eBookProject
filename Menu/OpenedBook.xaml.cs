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
using System.Windows.Shapes;
using LibraryReader.Books;
using LibraryReader;
using System.Threading;
using Menu.SharedResources;
using System.IO;
using System.Windows.Markup;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для OpenedBook.xaml
    /// </summary>
    /// 
    [Serializable]
    public partial class OpenedBook : Window
    {
        string fullPath = AppDomain.CurrentDomain.BaseDirectory + "Library";

        // string bookmark;
        // int page;

        Book currentBook = null;

        bool flag = false;

        private int lastPage;

        string[] newText;

        Paragraph paragraphHigh { get; set; }
        public OpenedBook(Book current)
        {
            currentBook = current;
            InitializeComponent();
            openBookWindow.Title = currentBook.Title + " - " + currentBook.Author;
            DisplayBook();
            Serialization.SerializationLastBook(currentBook, fullPath);

        
           
           // bookmarkList.DataContext = currentBook.bookmarks;
            bookmarkList.DataContext = currentBook.bookmarks;
           


        }


        public void DisplayBook()
        {
            flowDocument.Document = null;
            string text = currentBook.ReturnContent();
            doc.FontSize = 16;
            newText = Regex.Split(text, @"\n");

            foreach (var paragrapg in newText)
            {
                string abz;
                if (paragrapg.Contains("Table"))
                {
                    abz = "   ";
                }
                else
                {
                    abz = "    " + paragrapg;
                }              
                Paragraph p = new Paragraph();
                p.Margin = new Thickness(0, 0, 0, 0);
                p.Inlines.Add(abz);

                doc.Blocks.Add(p);
            }
            doc.ColumnWidth = 650;
            if (currentBook.Zoom != -1 && currentBook.LastPage != -1)
            {
                flowDocument.Zoom = currentBook.Zoom;
                lastPage = currentBook.LastPage;
            }
            else
            {
                flowDocument.Zoom = 110;
            }

            flowDocument.Document = doc;

        }

        private void CreateBookmark(object sender, RoutedEventArgs routedEventArgs)
        {
            flag = true;

            Bookmark mark = new Bookmark();
            mark.NumberPage = flowDocument.MasterPageNumber;

            foreach (var bookmark in currentBook.bookmarks)
            {
                if (bookmark.NumberPage == mark.NumberPage)
                {
                    return;
                }
            }

            currentBook.AddBookmark(mark);
            
           
            bookmarkList.DataContext = null;
            bookmarkList.DataContext = currentBook.bookmarks;
            
            bookmarkList.Text = "Choose a bookmark";
            //bookmarkList.SelectedItem = null;
            


            flag = false;

        }
        private void DeleteBookmark(object sender, RoutedEventArgs routedEventArgs)
        {
            flag = true;

            var button = (Button)sender;
            var docPanel = (DockPanel)button.Parent;
            var numberPage =((TextBlock)docPanel.Children[0]).DataContext;
            Bookmark mark = (Bookmark)numberPage;         
            
            foreach(var bookmark in currentBook.bookmarks)
            {
                if (bookmark.NumberPage == mark.NumberPage)
                {
                    currentBook.bookmarks.Remove(bookmark);
                    break;
                }
            }

            bookmarkList.DataContext = null;
            bookmarkList.DataContext = currentBook.bookmarks;
            
            
            bookmarkList.Text = "Choose a bookmark";



            flag = false;

        }
        private void OpenBookmark(object sender, RoutedEventArgs routedEventArgs)
        {
            if (currentBook.bookmarks.Count != 0)
            {
                flowDocument.GoToPage(currentBook.bookmarks[0].NumberPage);
            }
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs routedEventArgs)
        {
         
            var comboBox = (ComboBox)sender;
            if (((Bookmark)(comboBox.SelectedItem)) != null && flag == false)
            {
                var selectedNum = ((Bookmark)(comboBox.SelectedItem)).NumberPage;
                flowDocument.GoToPage(selectedNum);               
                bookmarkList.Text = "Choose a bookmark";
            }

            

            //   bookmarkList.ItemsSource = currentBook.bookmarks;

        }

        private void FindInBook(object sender, RoutedEventArgs routedEventArgs)
        {
            flowDocument.Find();
        }

        private void ContinueReading(object sender, RoutedEventArgs routedEventArgs)
        {

            flowDocument.GoToPage(currentBook.LastPage);
        }

        private void OpenMenu()
        {            
            Close();
        }

        private void OnClosing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            currentBook.LastPage = flowDocument.MasterPageNumber;
            currentBook.Zoom = flowDocument.Zoom;

            Serialization.SerializationLastBook(currentBook, fullPath);
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            var menuWindow = new MainWindow(ResourcesProvider.Current.MainWindowVM);
                    
            menuWindow.Show();
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                OpenMenu();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
