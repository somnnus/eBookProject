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

        Book currentBook;
        private int columnWidth;
        private int fontSize;
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
            Paragraph paragraph = new Paragraph();
            string text = currentBook.ReturnContent();
            //paragraphHigh.Inlines.Add(text);
            
           // FlowDocument document = new FlowDocument(paragraphHigh);

               //document.FontSize = 16;
              //  document.ColumnRuleWidth = border.ActualWidth/2;
             // document.ColumnWidth = 250;//3 свойства для изменения колонок! 2- одна колонка, 3 - две колонки, 4 - три колонки
            // columnWidth = 250;
           // document.ColumnGap = 20;
            

              //flowDocument.Document = document;
             // FlowDocument doc =new FlowDocument();   
            //  doc.LineHeight = 1.5;
           

            var s = Serialization.SplitPage(text, 3000);
            
            foreach (var paragrapg in s)
            {
               Paragraph p = new Paragraph();
               p.Margin = new Thickness(0,0,0,0);
              
                 p.Inlines.Add(paragrapg);
                 doc.Blocks.Add(p);
                
              
            }
            
            doc.ColumnWidth = currentBook.ColumnWidth;
            doc.FontSize = currentBook.FontSize;
            flowDocument.Document = doc;


            
        }
        
        private void CreateBookmark(object sender,RoutedEventArgs routedEventArgs)
        {
            Bookmark mark = new Bookmark();
          //  mark.NumberPage = flowDocument.MasterPageNumber;
            mark.FrontSize = flowDocument.FontSize;
            mark.ColumnWidth = columnWidth;
            //  currentBook.AddBookmark(mark);

            page = flowDocument.MasterPageNumber;

            
            //  string text = paragraphHigh.ContentEnd

            //System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Paragraph));
            //using (var writer = new StreamWriter(@"C:\Users\1394954\Desktop\Menu\Menu\Menu\bin\Debug\Library\mark.xml"))
            //{
            //    serializer.Serialize(writer, paragraphHigh);
            //}
        
          //  Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);

        }
        private void OpenBookmark(object sender, RoutedEventArgs routedEventArgs)
        {
            if (currentBook.bookmarks.Count!=0)
            {
                
               // flowDocument.BringIntoView();
                //paragraphHigh.BringIntoView();
            }
            // flowDocument.GoToPage(page);
            flowDocument.GoToPage(50);
            flowDocument.Find();
           

        }

            
        private void OpenMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            
            Serialization.SerializationInformationAboutBook(ResourcesProvider.Current.ListBooks, fullPath);
            var menuWindow = new MainWindow(ResourcesProvider.Current.MainWindowVM);
            menuWindow.Show();
            Close();
        }
    }
}
