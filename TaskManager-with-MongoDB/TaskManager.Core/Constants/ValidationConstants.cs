using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Constants
{
    public static class ValidationConstants
    {
        // User:
        public const int User_FirstName_MinLength = 3;
        public const int User_FirstName_MaxLength = 15;
        public const int User_LastName_MinLength = 3;
        public const int User_LastName_MaxLength = 15;
        public const int User_Username_MinLength = 8;
        public const int User_Username_MaxLength = 15;
        public const int User_Password_MinLength = 6;
        public const int User_Password_MaxLength = 10;

        // UserTask:
        public const int UserTask_Description_MinLength = 5;
        public const int UserTask_Description_MaxLength = 75;

        // Reamrk:
        public const int Remark_Content_MinLength = 5;
        public const int Remark_Content_MaxLength = 75;
    }
}
