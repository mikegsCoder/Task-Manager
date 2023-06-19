using System;
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
using TaskManager.Core.Services.UserService;
using TaskManager.Core.ViewModels.Task;
using TaskManager.WPF.Controllers;
using TaskManager.WPF.DataContexts;

namespace TaskManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string categorySelector;
        private string statusSelector;

        private TaskViewModel selectedTask;

        private readonly IUserService userService;

        public UserController userController;

        public MainWindowContext context;

        public MainWindow(
            IUserService _userService, 
            MainWindowContext _context)
        {
            userService = _userService;

            context = _context;

            userController = new UserController(userService, context);

            InitializeComponent();

            DataContext = context;
        }

        private void AllTasksBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        // By Category
        private void PersonalCategory_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void JobCategory_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void FamilyCategory_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void OtherCategory_Click(object sender, RoutedEventArgs e)
        {
           
        }

        // By Status:
        private void AwaitingStatus_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void InProgressStatus_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void FinishedStatus_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void CanceledStatus_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RemarksBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void DelBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }
        
        private void ExportJson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportXml_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LogoutMouse_Enter(object sender, RoutedEventArgs e)
        {
           
        }

        private void LogoutMouse_Leave(object sender, RoutedEventArgs e)
        {
           
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void SelectedTask(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
