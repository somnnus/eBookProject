using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Menu.SharedResources;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Menu.Helpers;

namespace Menu.MainAppPage
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl, INotifyPropertyChanged
    {
        //public BitmapImage CurrentImage { get; set; }

        //public List<BitmapImage> Images { get; set; }

        //private int _imageIndex = 0;

        //public void NextImageClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (_imageIndex >= Images.Count) _imageIndex = 0;
        //    CurrentImage = Images[imageIndex];
        //    NotifyPropertyChanged();  //необходимо реализовать INotifyPropertyChanged в классе модели
        //    _imageIndex++;
        //}

        private int currentPage;

        public Main()
        {
            InitializeComponent();

            DataContext = ResourcesProvider.Current;
            if (ResourcesProvider.Current.BooksByPages.Count != 0)
            {
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages["0"];
            }
        }

        public void FillMain()
        {

            //LibraryByBlocks.sum += ResourcesProvider.Current.MaxWidth;
            //LibraryByBlocks.sum += 250;

            //while (LibraryByBlocks.bookCount != ResourcesProvider.Current.ListBooks.Count)
            //{
            //    if (OuterGrid.ActualWidth * 2 >= LibraryByBlocks.sum)
            //    {
            //        if (LibraryByBlocks.resultDict.ContainsKey(LibraryByBlocks.i))
            //        {
            //            LibraryByBlocks.resultDict[LibraryByBlocks.i].Add(ResourcesProvider.Current.ListBooks[LibraryByBlocks.bookCount]);
            //        }
            //        else
            //        {
            //            LibraryByBlocks.resultDict.Add(LibraryByBlocks.i, new List<Book>());
            //            LibraryByBlocks.resultDict[LibraryByBlocks.i].Add(ResourcesProvider.Current.ListBooks[LibraryByBlocks.bookCount]);
            //        }
            //    }
            //    else
            //    {
            //        LibraryByBlocks.sum = 0;
            //        LibraryByBlocks.i++;
            //        LibraryByBlocks.resultDict.Add(LibraryByBlocks.i, new List<Book>());
            //        LibraryByBlocks.resultDict[LibraryByBlocks.i].Add(ResourcesProvider.Current.ListBooks[LibraryByBlocks.bookCount]);
            //    }

            //    LibraryByBlocks.bookCount++;
            //    LibraryByBlocks.sum += 250;
            //}
            //LibraryByBlocks.sum = 0;
            //LibraryByBlocks.i = 0;
            //LibraryByBlocks.bookCount = 0;
            //ResourcesProvider.Current.BooksByPages = new Dictionary<int, List<Book>>();
            //ResourcesProvider.Current.BooksByPages = LibraryByBlocks.resultDict;
            //LibraryByBlocks.resultDict = new Dictionary<int, List<Book>>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyPropertyChanged();
            }
        }

        protected virtual void NotifyPropertyChanged(
           [CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void LeftPageClick()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void RightPageClick()
        {
            if ((CurrentPage + 1) < ResourcesProvider.Current.BooksByPages.Count)
            {
                CurrentPage++;
                //listBoxBooks.ItemsSource = ResourcesProvider.Current.BooksByPages[currentPage.ToString()];
            }
        }

        private void Label_MouseDownLeft(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                LeftPageClick();
            }
        }

        private void Label_MouseDownRight(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                RightPageClick();
            }
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

        //private void WrapPanel_Initialized(object sender, EventArgs e)
        //{
        //    FillMain();
        //}

        //private void DockPanelLibrary_Initialized(object sender, EventArgs e)
        //{
        //    FillMain();
        //}

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid grid = (Grid)sender;
            if (ResourcesProvider.Current.MaxWidth < grid.ActualWidth)
            {
                ResourcesProvider.Current.MaxWidth = grid.ActualWidth;
            }
        }

        //private void UserControlMain_Loaded(object sender, RoutedEventArgs e)
        //{
        //    FillMain();
        //}

        //private void Grid_Initialized(object sender, EventArgs e)
        //{
        //    FillMain();
        //}

        //private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    FillMain();
        //}

        //private MultiBinding createFieldMultiBinding(string fieldName)
        //{
        //    // Create the multi-binding
        //    MultiBinding mbBinding = new MultiBinding();
        //    // Create the dictionary binding
        //    Binding bDictionary = new Binding(ResourcesProvider.Current.BooksByPages);
        //    bDictionary.Source = this.DataContext;
        //    // Create the key binding
        //    Binding bKey = new Binding(fieldName);
        //    bKey.Source = this.DataContext;
        //    // Set the multi-binding converter
        //    mbBinding.Converter = new DictionaryItemConverter();
        //    // Add the bindings to the multi-binding
        //    mbBinding.Bindings.Add(bDictionary);
        //    mbBinding.Bindings.Add(bKey);

        //    return mbBinding;
        //}
    }
}
