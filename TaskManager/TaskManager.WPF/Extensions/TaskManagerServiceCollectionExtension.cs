using TaskManager.WPF;
using TaskManager.Infrastructure.Data;
using TaskManager.WPF.Controllers;
using TaskManager.Core.Services.UserService;
using TaskManager.WPF.DataContexts;

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
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped(typeof(IUserService), typeof(UserService));

            return services;
        }
    }
}
