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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryReader;
using LibraryReader.Books;
using System.IO;
using System.Windows.Threading;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ValuePassDelegate();
        public event ValuePassDelegate ValuePassEvent;

        public delegate void ExitDelegate();
        public event ExitDelegate ExitInTheWindow;

        private List<Book> books = new List<Book>();
        public MainWindow()
        {
            InitializeComponent();

            CreateHiddenDirectory();

            ValuePassEvent = new ValuePassDelegate(method1);
            mainScreen.del = ValuePassEvent;

            ExitInTheWindow = new ExitDelegate(method2);
            mainScreen.del2 = ExitInTheWindow;
        }
        public void method1()
        {
            contentMain.Content = new Library();
        }

        public void method2()
        {
            Close();
        }

        private void CreateHiddenDirectory()
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;
            fullPath = fullPath+ "Library";
            if (!Directory.Exists(fullPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(fullPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

        }
    }
}
