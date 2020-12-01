using LibraryReader.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Menu.SharedResources
{
    public class Resources : INotifyPropertyChanged
    {
        private MainWindowViewModel mainWindowViewModel;

        private string lastSortingFeature;
        private Dictionary<string, List<Book>> currentDictionary;
        private Dictionary<int, List<Book>> booksByPages;
        private Dictionary<string, List<Book>> sortedByAuthor;
        private Dictionary<string, List<Book>> sortedByTitle;
        private Dictionary<string, List<Book>> sortedByDate;
        private Dictionary<int, Book> recentlyReadBooks;
        private List<Book> listBooks;
        private double maxWidth = 0;

        public List<Book> deleteBook;

        public Resources()
        {
            booksByPages = new Dictionary<int, List<Book>>();
            sortedByAuthor = new Dictionary<string, List<Book>>();
            sortedByTitle = new Dictionary<string, List<Book>>();
            sortedByDate = new Dictionary<string, List<Book>>();
            listBooks = new List<Book>();
            recentlyReadBooks = new Dictionary<int, Book>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel MainWindowVM //не свойство зависимости
        {
            get { return mainWindowViewModel; }
            set
            {
                mainWindowViewModel = value;
            }
        }

        public string LastSortingFeature
        {
            get { return lastSortingFeature; }
            set
            {
                lastSortingFeature = value;
                NotifyPropertyChanged();
            }
        }

        public double MaxWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<string, List<Book>> CurrentDictionary
        {
            get { return currentDictionary; }
            set
            {
                currentDictionary = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<int, List<Book>> BooksByPages
        {
            get { return booksByPages; }
            set
            {
                booksByPages = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<string, List<Book>> SortedByAuthor
        {
            get { return sortedByAuthor; }
            set
            {
                sortedByAuthor = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<string, List<Book>> SortedByTitle
        {
            get { return sortedByTitle; }
            set
            {
                sortedByTitle = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<string, List<Book>> SortedByDate
        {
            get { return sortedByDate; }
            set
            {
                sortedByDate = value;
                NotifyPropertyChanged();
            }
        }

        public List<Book> ListBooks
        {
            get { return listBooks; }
            set
            {
                listBooks = value;
                NotifyPropertyChanged();
            }
        }

        public Dictionary<int, Book> RecentlyReadBooks
        {
            get { return recentlyReadBooks; }
            set
            {
                recentlyReadBooks = value;
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
    }
}
