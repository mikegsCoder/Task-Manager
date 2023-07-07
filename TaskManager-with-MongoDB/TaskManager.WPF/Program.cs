using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.WPF.InitialSeed;

namespace TaskManager.WPF
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddApplicationServices();
                })
                .Build();

            Seeder.SeedAsync(host).GetAwaiter();

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
