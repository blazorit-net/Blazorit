using Blazorit.ConsoleApp.Programs.ECommerce;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blazorit.ConsoleApp.Services.ECommerce
{
    internal sealed class ConsoleHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IECommerceRepository _ecomRepo;

        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IECommerceRepository ecomRepo
            )
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _ecomRepo = ecomRepo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        _logger.LogInformation("Payload from HostedService!");

                        ProgramCode program = new ProgramCode(_ecomRepo); //DI via constructor
                        await program.Main(); // Simulate real work is being done

                        //await Task.Delay(1000);
                        _logger.LogInformation("Payload is finished in HostedService!");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unhandled exception in payload!");
                    }
                    finally
                    {
                        _appLifetime.StopApplication(); // Stop the application once the work is done
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
