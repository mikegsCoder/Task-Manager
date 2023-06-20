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
                //errors.Add("First Name should be between 3 and 15 characters long.");
                errors.Add(string.Format(Messages.First_Name_Error_Msg,
                    ValidationConstants.User_FirstName_MinLength,
                    ValidationConstants.User_FirstName_MaxLength));
            }

            if (string.IsNullOrEmpty(lastName)
                || lastName.Length < 3
                || lastName.Length > 15)
            {
                //errors.Add("Last Name should be between 3 and 15 characters long.");
                errors.Add(string.Format(Messages.Last_Name_Error_Msg,
                    ValidationConstants.User_LastName_MinLength,
                    ValidationConstants.User_LastName_MaxLength));
            }

            if (string.IsNullOrEmpty(username)
                || username.Length < 8
                || username.Length > 15)
            {
                //errors.Add("Username should be between 8 and 15 characters long.");
                errors.Add(string.Format(Messages.Username_Error_Msg,
                    ValidationConstants.User_Username_MinLength,
                    ValidationConstants.User_Username_MaxLength));
            }

            if (string.IsNullOrEmpty(password)
                || password.Length < 6
                || password.Length > 10)
            {
                //errors.Add("Password should be between 6 and 10 characters long.");
                errors.Add(string.Format(Messages.Password_Error_Msg,
                    ValidationConstants.User_Password_MinLength,
                    ValidationConstants.User_Password_MaxLength));
            }

            if (string.IsNullOrEmpty(repass) || repass != password)
            {
                //errors.Add("Passwords don't match.");
                errors.Add(Messages.Repass_Error_Msg);
            }

            if (errors.Count() > 0)
            {
                //ShowInvalidInput("Invalid Input!" + Environment.NewLine + string.Join(Environment.NewLine, errors));
                ShowInvalidInput(Messages.Input_Error_Msg
                    + Environment.NewLine
                    + string.Join(Environment.NewLine, errors));

                return;
            }

            try
            {
                if (!await userService.IsUsernameAvailableAsync(username))
                {
                    //ShowError("Username is not available.");
                    ShowError(Messages.Username_NotAvailable_Error_Msg);

                    return;
                }

                userService.CreateAsync(username, password, firstName, lastName);

                //ShowSuccess("Successful registration.");
                ShowSuccess(Messages.Registration_Success_Msg);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
    }
}
