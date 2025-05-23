﻿using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

            // migrate Database on startup:
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();
            }

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
