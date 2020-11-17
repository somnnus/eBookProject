using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Castle.MicroKernel.Registration;
using Menu.MainAppPage;
using Menu.LibraryPage;
using Menu.SettingsPage;

namespace Menu
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WindsorContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            container = new WindsorContainer();
            container.Register(Component.For<MainViewModel>());
            container.Register(Component.For<LibraryViewModel>());
            container.Register(Component.For<SettingsViewModel>());
            container.Register(Component.For<MainWindowViewModel>());
            container.Register(Component.For<MainWindow>());

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.ShowDialog();
        }
    }
}
