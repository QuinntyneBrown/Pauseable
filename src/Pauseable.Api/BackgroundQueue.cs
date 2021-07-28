using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pauseable.Api
{
    public interface IBackgroundQueue
    {
        void Queue(List<Models.Notification> notifications);

        Task<List<Models.Notification>> DequeueAsync(
            CancellationToken cancellationToken);
    }

    public class BackgroundQueue : IBackgroundQueue
    {
        private ConcurrentQueue<List<Models.Notification>> _notifications =
            new ConcurrentQueue<List<Models.Notification>>();

        public void Queue(
            List<Models.Notification> notifications)
        {
            if (notifications == null)
            {
                throw new ArgumentNullException(nameof(notifications));
            }

            _notifications.Enqueue(notifications);

        }

        public async Task<List<Models.Notification>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            _notifications.TryDequeue(out var notifications);

            return notifications;
        }
    }
}