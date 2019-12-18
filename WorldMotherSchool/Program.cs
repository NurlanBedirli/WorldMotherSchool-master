using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorldMotherSchool.Core.SeedAsync;
using WorldMotherSchool.Models;

namespace WorldMotherSchool
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
           IWebHost hosting =  CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = hosting.Services.CreateScope())
            {
                using (SchoolDbContext dbContext = scope.ServiceProvider.GetRequiredService<SchoolDbContext>())
                {
                    await Seed.InvokeAsync(scope,dbContext);
                }
            }
            await hosting.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
