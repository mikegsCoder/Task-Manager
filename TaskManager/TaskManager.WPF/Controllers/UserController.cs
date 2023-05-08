using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Core.Constants;
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

        public async void RegisterAsync()
        {
            RegisterWindow registerWindow = new RegisterWindow();

            if (registerWindow.ShowDialog() == false)
            {
                return;
            }

            List<string> errors = new List<string>();

            string username = registerWindow.Username.Text.Trim();
            string password = registerWindow.Password.Password.Trim();
            string repass = registerWindow.Repass.Password.Trim();
            string firstName = registerWindow.FirstName.Text.Trim();
            string lastName = registerWindow.LastName.Text.Trim();

            if (string.IsNullOrEmpty(firstName)
                || firstName.Length < 3
                || firstName.Length > 15)
            {
                errors.Add(string.Format(Messages.First_Name_Error_Msg,
                    ValidationConstants.User_FirstName_MinLength,
                    ValidationConstants.User_FirstName_MaxLength));
            }

            if (string.IsNullOrEmpty(lastName)
                || lastName.Length < 3
                || lastName.Length > 15)
            {
                errors.Add(string.Format(Messages.Last_Name_Error_Msg,
                    ValidationConstants.User_LastName_MinLength,
                    ValidationConstants.User_LastName_MaxLength));
            }

            if (string.IsNullOrEmpty(username)
                || username.Length < 8
                || username.Length > 15)
            {
                errors.Add(string.Format(Messages.Username_Error_Msg,
                    ValidationConstants.User_Username_MinLength,
                    ValidationConstants.User_Username_MaxLength));
            }

            if (string.IsNullOrEmpty(password)
                || password.Length < 6
                || password.Length > 10)
            {
                errors.Add(string.Format(Messages.Password_Error_Msg,
                    ValidationConstants.User_Password_MinLength,
                    ValidationConstants.User_Password_MaxLength));
            }

            if (string.IsNullOrEmpty(repass) || repass != password)
            {
                errors.Add(Messages.Repass_Error_Msg);
            }

            if (errors.Count() > 0)
            {
                ShowInvalidInput(Messages.Input_Error_Msg
                    + Environment.NewLine
                    + string.Join(Environment.NewLine, errors));

                return;
            }

            try
            {
                if (!await userService.IsUsernameAvailableAsync(username))
                {
                    ShowError(Messages.Username_NotAvailable_Error_Msg);

                    return;
                }

                userService.CreateAsync(username, password, firstName, lastName);

                ShowSuccess(Messages.Registration_Success_Msg);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
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

        public void Logout()
        {
            ShowGreeting
                (Messages.Logout_Text_Msg,
                string.Format(Messages.Logout_Title_Msg, context.user.Username));

            context.user = null;
            context.tasks = null;

            context.HasUser = false;
            context.ShowLoginBtn = true;
            context.NoTasks = false;
        }
    }
}
