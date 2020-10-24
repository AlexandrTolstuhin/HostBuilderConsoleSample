using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HostBuilderConsoleSample
{
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
}