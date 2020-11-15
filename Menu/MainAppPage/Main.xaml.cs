﻿using LibraryReader.Books;
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

namespace Menu.MainAppPage
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public int currentPage = 0;

        public Main()
        {
            InitializeComponent();
            
            listBoxBooks.ItemsSource = CommonResources.dictionaryBooks["0"];
        }

        private void LeftPageClick(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                listBoxBooks.ItemsSource = CommonResources.dictionaryBooks[currentPage.ToString()];
            }
        }

        private void RightPageClick(object sender, RoutedEventArgs e)
        {
            if ((currentPage + 1) < CommonResources.dictionaryBooks.Count)
            {
                currentPage++;
                listBoxBooks.ItemsSource = CommonResources.dictionaryBooks[currentPage.ToString()];
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 1)
            {
                StackPanel stackPanel = (StackPanel)sender;
                Book current = (Book)stackPanel.DataContext;
                var openedBook = new OpenedBook();
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
}
