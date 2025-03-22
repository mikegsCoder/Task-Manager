namespace TaskManager.Infrastructure.Constants
{
    internal static class DatabaseConstants
    {
        // User model:
        public const int User_Username_MaxLength = 15;
        public const int User_FirstName_MaxLength = 15;
        public const int User_LastName_MaxLength = 15;

        // UserTask model:
        public const int UserTask_Description_MaxLength = 75;

        // Reamrk model:
        public const int Remark_Content_MaxLength = 75;

        // Category model:
        public const int Category_Name_MaxLength = 10;

        // Status model:
        public const int Status_Name_MaxLength = 10;
    }
}
