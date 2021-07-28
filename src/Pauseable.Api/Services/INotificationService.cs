
using Pauseable.Api.Models;
using System;

namespace Pauseable.Api.Services
{
    public interface INotificationService
    {
        void Subscribe(Action<Notification> onNext);

        void OnNext(Notification value);
    }
}