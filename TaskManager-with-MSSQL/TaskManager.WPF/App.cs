using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace TaskManager.WPF
{
    public class App : Application
    {
        readonly MainWindow mainWindow;

        public App(IServiceProvider services)
        {
            mainWindow = services.GetRequiredService<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
