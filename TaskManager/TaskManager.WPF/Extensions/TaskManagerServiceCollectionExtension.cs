using TaskManager.WPF;
using TaskManager.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TaskManagerServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<App>();
            services.AddScoped<MainWindow>();
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }
    }
}
