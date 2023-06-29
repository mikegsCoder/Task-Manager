using TaskManager.WPF;
using TaskManager.WPF.DataContexts;
using MongoDB.Driver;
using TaskManager.Core.Services.UserService;
using TaskManager.WPF.Controllers;
using TaskManager.Core.Services.Task;
using TaskManager.Core.Services.RemarkService;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TaskManagerServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<App>();
            services.AddScoped<MainWindow>();
            services.AddScoped<UserController>();
            services.AddScoped<TaskController>();
            services.AddScoped<MainWindowContext>();
            services.AddSingleton(typeof(IMongoClient), new MongoClient("mongodb://localhost:27017"));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(ITaskService), typeof(TaskService));
            services.AddScoped(typeof(IRemarkService), typeof(RemarkService));

            return services;
        }
    }
}
