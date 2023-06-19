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
    }
}
