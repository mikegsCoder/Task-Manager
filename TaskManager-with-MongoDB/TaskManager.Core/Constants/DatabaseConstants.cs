using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Constants
{
    public static class DatabaseConstants
    {
        // Database name:
        public const string DatabaseName = "taskManager";

        // DB collections names:
        public const string CategoryCollectionName = "categories";
        public const string StatusCollectionName = "statuses";
        public const string UserCollectionName = "users";
        public const string TaskCollectionName = "tasks";
        public const string RemarkCollectionName = "remarks";
    }
}
