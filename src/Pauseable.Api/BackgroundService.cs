using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pauseable.Api.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pauseable.Api
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly INotificationService _notificationService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public NotificationBackgroundService(INotificationService notificationService, IServiceScopeFactory serviceScopeFactory)
        {
            _notificationService = notificationService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public DateTime LastModified { get; set; }

        public int Interval { get; set; } = 5 * 100;
        protected async override Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _notificationService.OnNext(new()
                {

                });

                await Task.Delay(this.Interval);
            }
        }
    }
}