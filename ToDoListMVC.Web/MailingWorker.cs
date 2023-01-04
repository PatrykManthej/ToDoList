using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using ToDoListMVC.Application.Interfaces;

namespace ToDoListMVC.Web
{
    public class MailingWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MailingWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var hourSpan = 24 - DateTime.Now.Hour;
                var numberOfHours = hourSpan;

                if (hourSpan == 24)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();

                    try
                    {
                        await mailService.SendNotificationMailAsync();
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    finally
                    {
                        scope.Dispose();
                    }

                    numberOfHours = 24;
                }
                await Task.Delay(TimeSpan.FromHours(numberOfHours), stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
