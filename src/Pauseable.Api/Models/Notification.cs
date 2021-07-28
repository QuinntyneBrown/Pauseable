using System;

namespace Pauseable.Api.Models
{
    public class Notification
    {
        public Guid NotificationId { get; private set; } = Guid.NewGuid();
        public DateTime Created { get; private set; } = DateTime.UtcNow;
    }
}
