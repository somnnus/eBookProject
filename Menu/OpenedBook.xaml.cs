﻿using System;
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

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для OpenedBook.xaml
    /// </summary>
    public partial class OpenedBook : Window
    {
        Book currentBook;
        Paragraph paragraphHigh = new Paragraph();
        public OpenedBook(Book current)
        {
            InitializeComponent();
            currentBook = current;
            DisplayBook();
            GoToPage(new object(), new RoutedEventArgs());

        }

        public void DisplayBook()
        {
            flowDocument.Document = null;
            Paragraph paragraph = new Paragraph();
            string text = currentBook.ReturnContent();
            paragraphHigh.Inlines.Add(text);
            
            FlowDocument document = new FlowDocument(paragraphHigh);

            document.FontSize = 16;
            //  document.ColumnRuleWidth = border.ActualWidth/2;
            document.ColumnWidth = 250;//3 свойства для изменения колонок! 2- одна колонка, 3 - две колонки, 4 - три колонки
            document.ColumnGap = 20;


            flowDocument.Document = document;
            
        }
        private void GoToPage(object sender,RoutedEventArgs routedEventArgs)
        {
            flowDocument.GoToPage(10);
        }
    
        private void SliderChange(object sender,RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
           
            FlowDocument document = new FlowDocument(paragraphHigh);
            document.FontSize = 16;
            document.ColumnWidth = e.NewValue;
            document.ColumnGap = 20;
            flowDocument.Document = document;

        }
        private void OpenMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            var menuWindow = new MainWindow(CommonResources.mainWindowViewModel);
            menuWindow.Show();
            this.Close();
        }
    }
}
