using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Infrastructure.Data;

namespace TaskManager.WPF
{
    public class App : Application
    {
        readonly MainWindow mainWindow;

        public App(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
