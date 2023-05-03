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
    }
}
