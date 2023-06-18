using TaskManager.WPF;
using TaskManager.WPF.DataContexts;
using MongoDB.Driver;
using TaskManager.Core.Services.UserService;
using TaskManager.WPF.Controllers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TaskManagerServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<App>();
            services.AddScoped<MainWindow>();
            services.AddScoped<UserController>();
            services.AddScoped<MainWindowContext>();
            services.AddSingleton(typeof(IMongoClient), new MongoClient("mongodb://localhost:27017"));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            return services;
        }
    }
}
