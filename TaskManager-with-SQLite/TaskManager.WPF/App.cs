using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

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
