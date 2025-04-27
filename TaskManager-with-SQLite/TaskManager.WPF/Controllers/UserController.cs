using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Services.User;
using TaskManager.WPF.DataContexts;
using static TaskManager.WPF.MessageBoxes.MessageBoxes;

namespace TaskManager.WPF.Controllers
{
    public class UserController
    {
        private readonly MainWindowContext context;
        private readonly IUserService userService;

        public UserController(IServiceProvider services)
        {
            userService = services.GetRequiredService<IUserService>();
            context = services.GetRequiredService<MainWindowContext>();
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
                errors.Add("First Name should be between 3 and 15 characters long.");
            }

            if (string.IsNullOrEmpty(lastName)
                || lastName.Length < 3
                || lastName.Length > 15)
            {
                errors.Add("Last Name should be between 3 and 15 characters long.");
            }

            if (string.IsNullOrEmpty(username)
                || username.Length < 8
                || username.Length > 15)
            {
                errors.Add("Username should be between 8 and 15 characters long.");
            }

            if (string.IsNullOrEmpty(password)
                || password.Length < 6
                || password.Length > 10)
            {
                errors.Add("Password should be between 6 and 10 characters long.");
            }

            if (string.IsNullOrEmpty(repass) || repass != password)
            {
                errors.Add("Passwords don't match.");
            }

            if (errors.Count() > 0)
            {
                ShowInvalidInput("Invalid Input!" + Environment.NewLine + string.Join(Environment.NewLine, errors));

                return;
            }

            try
            {
                if (!await userService.IsUsernameAvailableAsync(username))
                {
                    ShowError("Username is not available.");

                    return;
                }

                userService.CreateAsync(username, password, firstName, lastName);

                ShowSuccess("Successful registration.");
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
                ShowInvalidInput("Both fields are required.");

                return;
            }

            try
            {
                context.user = await userService.GetUserAsync(username, password);

                if (context.user == null)
                {
                    ShowInvalidInput("Invalid Username or Password.");
                }
                else
                {
                    context.HasUser = true;
                    context.ShowLoginBtn = false;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public void Logout()
        {
            context.user = null;
            context.tasks = null;

            context.HasUser = false;
            context.ShowLoginBtn = true;
            context.NoTasks = false;
        }
    }
}
