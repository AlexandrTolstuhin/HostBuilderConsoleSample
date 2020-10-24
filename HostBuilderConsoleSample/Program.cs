using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace HostBuilderConsoleSample
{
    class Program
    {
        static void Main(string[] args)
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

    internal class HostedService : IHostedService
    {
        private readonly IPrinter _printer;
        private readonly IDateService _dateService;

        public HostedService(IPrinter printer, IDateService dateService)
        {
            _printer = printer;
            _dateService = dateService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                var date = _dateService.GetCurrentDateTime();

                _printer.PrintMessage(date.ToString("T"));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public interface IPrinter
    {
        void PrintMessage(string message);
    }

    class ConsolePrinter : IPrinter
    {
        public void PrintMessage(string message) => WriteLine(message);
    }

    public interface IDateService
    {
        DateTime GetCurrentDateTime();
    }

    class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}