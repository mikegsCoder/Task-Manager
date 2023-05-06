using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Constants
{
    public static class Messages
    {
        // UserController:
        // UserController Login:
        public const string Login_Input_Error_Msg = "Both fields are required.";
        public const string Login_Data_Error_Msg = "Invalid Username or Password.";
        public const string Login_Welcome_Title_Msg = "Welcome again!";
        public const string Login_Welcome_Text_Msg = "Welcome {0} {1}.";

        // MainWindow:
        public const string Main_Window_Tello_Text = "Hello {0}!";
    }
}
