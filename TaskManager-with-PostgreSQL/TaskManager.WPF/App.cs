using System.Windows;

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
