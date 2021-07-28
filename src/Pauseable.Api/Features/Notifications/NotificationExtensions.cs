using System;
using Pauseable.Api.Models;

namespace Pauseable.Api.Features
{
    public static class NotificationExtensions
    {
        public static NotificationDto ToDto(this Notification notification)
        {
            return new ()
            {
                NotificationId = notification.NotificationId
            };
        }
        
    }
}
