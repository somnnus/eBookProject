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

        string bookmark;
        int page;

        Book currentBook=null;

        private double columnWidth;
        private int lastPage;

        IEnumerable<string> s;

        Paragraph paragraphHigh { get; set; }
        public OpenedBook(Book current)
        {

            InitializeComponent();
            currentBook = current;
            DisplayBook();
            Serialization.SerializationLastBook(currentBook, fullPath);
            
                  
            
        }
        
       
        public void DisplayBook()
        {
            flowDocument.Document = null;
            string text = currentBook.ReturnContent(); 
            doc.FontSize = 16;
             s = Serialization.SplitPage(text, 3000);
            
            foreach (var paragrapg in s)
            {
                Paragraph p = new Paragraph();
                p.Margin = new Thickness(0, 0, 0, 0);

                p.Inlines.Add(paragrapg);
                doc.Blocks.Add(p);
                              
            }
            if (currentBook.Zoom != -1 && currentBook.LastPage != -1 && currentBook.ColumnWidth != -1)
            {
                flowDocument.Zoom = currentBook.Zoom;
                lastPage = currentBook.LastPage;
                doc.ColumnWidth = currentBook.ColumnWidth;
                slider.SelectionEnd = currentBook.ColumnWidth;
                columnWidth = currentBook.ColumnWidth;

            }
            else
            {
                slider.SelectionEnd = 650;
                doc.ColumnWidth = 650;
                columnWidth = 650;
                
            }                     
            flowDocument.Document = doc;
            


            
        }
        
        private void CreateBookmark(object sender,RoutedEventArgs routedEventArgs)
        {
            Bookmark mark = new Bookmark();
          //  mark.NumberPage = flowDocument.MasterPageNumber;
            mark.ColumnWidth = columnWidth;
            //  currentBook.AddBookmark(mark);

            page = flowDocument.MasterPageNumber;

            

        }
        private void OpenBookmark(object sender, RoutedEventArgs routedEventArgs)
        {
            //if (currentBook.bookmarks.Count!=0)
            //{
                
            //   // flowDocument.BringIntoView();
            //    //paragraphHigh.BringIntoView();
            //}
            // flowDocument.GoToPage(page);
            flowDocument.Zoom = 150;
            flowDocument.GoToPage(50);                      
        }

        private void FindInBook(object sender, RoutedEventArgs routedEventArgs)
        {
            flowDocument.Find();
        }

        private void SliderChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
            if (currentBook != null)
            {
                foreach (var paragrapg in s)
                {
                    Paragraph p = new Paragraph();
                    p.Margin = new Thickness(0, 0, 0, 0);

                    p.Inlines.Add(paragrapg);
                    doc.Blocks.Add(p);


                }

                doc.ColumnWidth = e.NewValue;
                flowDocument.Document = doc;
            }

        }

        private void ContinueReading(object sender, RoutedEventArgs routedEventArgs)
        {

            flowDocument.GoToPage(currentBook.LastPage);
        }

        private void OpenMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            currentBook.LastPage = flowDocument.MasterPageNumber;
            currentBook.ColumnWidth = columnWidth;
            currentBook.Zoom = flowDocument.Zoom;

            Serialization.SerializationLastBook(currentBook, fullPath);
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            var menuWindow = new MainWindow(ResourcesProvider.Current.MainWindowVM);
            menuWindow.Show();
            Close();
        }
    }
}
