using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.MainAppPage;
using Menu.LibraryPage;
using System.Windows;

namespace Menu
{
    public class MainWindowViewModel: ViewModelBase
    {
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }
        private ViewModelBase _currentViewModel;

        public RelayCommand MainCommand { get; private set; }

        public RelayCommand LibCommand { get; private set; }

        private readonly MainViewModel mainViewModel;
        private readonly LibraryViewModel libraryViewModel;

        public MainWindowViewModel(MainViewModel firstViewModel,
                    LibraryViewModel secondViewModel)
        {
            mainViewModel = firstViewModel;
            libraryViewModel = secondViewModel;
            MainCommand = new RelayCommand(ShowFirstView);
            LibCommand = new RelayCommand(ShowSecondView);
        }

        private void ShowFirstView()
        {
            CurrentViewModel = mainViewModel;
        }

        private void ShowSecondView()
        {
            CurrentViewModel = libraryViewModel;
        }
    }
}
