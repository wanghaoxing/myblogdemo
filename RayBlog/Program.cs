using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RayBlog.Infrastructure.Database;
using Serilog;

namespace RayBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Debug()
       .WriteTo.Console()
       .WriteTo.File("log.txt",
           rollingInterval: RollingInterval.Day,
           rollOnFileSizeLimit: true)
       .CreateLogger();
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var mycontext = services.GetRequiredService<MyContext>();
                    MyContextSeed.SeedAsync(mycontext, loggerFactory).Wait();

                }
                catch (Exception e)
                {

                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "发生错误");

                }


            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               // .UseStartup<Startup>();
               .UseStartup(typeof(StartupDevelopment).GetTypeInfo().Assembly.FullName)
                .UseSerilog();
    }
}
