using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostBuilderConsoleSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new HostBuilder();

            builder.ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("appsettings.json", true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddSingleton<IDateService, DateService>()
                        .AddScoped<IPrinter, ConsolePrinter>()
                        .AddHostedService<HostedService>();
                });

            var host = builder.Build();

            host.Run();
        }
    }
}