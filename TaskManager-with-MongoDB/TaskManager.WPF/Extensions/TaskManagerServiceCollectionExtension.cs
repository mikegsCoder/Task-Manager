using TaskManager.WPF;
using TaskManager.WPF.DataContexts;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TaskManagerServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<App>();
            services.AddScoped<MainWindow>();
            services.AddScoped<MainWindowContext>();
            services.AddSingleton(typeof(IMongoClient), new MongoClient("mongodb://localhost:27017"));

            return services;
        }
    }
}
