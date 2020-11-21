using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.MainAppPage;
using Menu.LibraryPage;
using Menu.SettingsPage;
using Menu.PageForRemoving;
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

        public RelayCommand SettingsCommand { get; set; }

        public RelayCommand RemovingPageCommand { get; set; }

        private readonly MainViewModel mainViewModel;
        private readonly LibraryViewModel libraryViewModel;
        private readonly SettingsViewModel settingsViewModel;
        private readonly BooksRemovingViewModel removingViewModel;

        public MainWindowViewModel(MainViewModel firstViewModel,
                    LibraryViewModel secondViewModel, SettingsViewModel thirdViewModel, BooksRemovingViewModel fourthViewModel)
        {
            mainViewModel = firstViewModel;
            libraryViewModel = secondViewModel;
            settingsViewModel = thirdViewModel;
            removingViewModel = fourthViewModel;
            MainCommand = new RelayCommand(ShowFirstView);
            LibCommand = new RelayCommand(ShowSecondView);
            SettingsCommand = new RelayCommand(ShowThirdModel);
            RemovingPageCommand = new RelayCommand(ShowFourthDocument);
            ShowFirstView();
        }

        private void ShowFirstView()
        {
            CurrentViewModel = mainViewModel;
        }

        private void ShowSecondView()
        {
            CurrentViewModel = libraryViewModel;
        }

        private void ShowThirdModel()
        {
            CurrentViewModel = settingsViewModel;
        }

        private void ShowFourthDocument()
        {
            CurrentViewModel = removingViewModel;
        }

    }
}
