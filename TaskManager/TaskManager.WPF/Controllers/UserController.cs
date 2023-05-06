using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Core.Services.UserService;
using TaskManager.Core.ViewModels.User;
using TaskManager.WPF.DataContexts;
using static TaskManager.WPF.Windows.MessageBoxes.MessageBoxes;

namespace TaskManager.WPF.Controllers
{
    public class UserController
    {
        private readonly MainWindowContext context;
        private readonly IUserService userService;

        public UserController(
            IUserService _userService,
            MainWindowContext _context)
        {
            userService = _userService;
            context = _context;
        }

        public async Task LoginAsync()
        {
            LoginWindow loginWindow = new LoginWindow();

            if (loginWindow.ShowDialog() == false)
            {
                return;
            }

            string username = loginWindow.Username.Text.Trim();
            string password = loginWindow.Password.Password.Trim();

            if (string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
            {
                ShowInvalidInput(Messages.Login_Input_Error_Msg);

                return;
            }

            try
            {
                context.user = await userService.GetUserAsync(username, password);

                if (context.user == null)
                {
                    ShowInvalidInput(Messages.Login_Data_Error_Msg);
                }
                else
                {
                    context.HasUser = true;
                    context.ShowLoginBtn = false;

                    ShowGreeting(
                        string.Format(Messages.Login_Welcome_Text_Msg,
                            context.user.FirstName,
                            context.user.LastName),
                        Messages.Login_Welcome_Title_Msg);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
    }
}
