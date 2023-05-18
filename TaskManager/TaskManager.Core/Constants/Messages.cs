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
        // UserController Register:
        public const string First_Name_Error_Msg = "First Name should be between {0} and {1} characters long.";
        public const string Last_Name_Error_Msg = "Last Name should be between {0} and {1} characters long.";
        public const string Username_Error_Msg = "Username should be between {0} and {1} characters long.";
        public const string Password_Error_Msg = "Password should be between {0} and {1} characters long.";
        public const string Repass_Error_Msg = "Passwords don't match.";
        public const string Input_Error_Msg = "Invalid Input!";
        public const string Username_NotAvailable_Error_Msg = "Username is not available.";
        public const string Registration_Success_Msg = "Successful registration.";
        // UserController Login:
        public const string Login_Input_Error_Msg = "Both fields are required.";
        public const string Login_Data_Error_Msg = "Invalid Username or Password.";
        public const string Login_Welcome_Title_Msg = "Welcome again!";
        public const string Login_Welcome_Text_Msg = "Welcome {0} {1}.";
        // UserController Logout:
        public const string Logout_Title_Msg = "See you {0}.";
        public const string Logout_Text_Msg = "See you later!";

        // TaskController:
        public const string Task_Description_Error_Msg = "Task description should be between {0} and {1} characters long.";

        // RemarkController:
        public const string Remark_Description_Error_Msg = "Remark description should be between {0} and {1} characters long.";
        
        // MainWindow:
        public const string Main_Window_Tello_Text = "Hello {0}!";
    }
}
