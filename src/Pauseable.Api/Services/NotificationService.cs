using System;
using System.Reactive.Subjects;

namespace Pauseable.Api.Services
{
    public class NotificationService : INotificationService
    {

        private readonly BehaviorSubject<Models.Notification> _notification = new BehaviorSubject<Models.Notification>(null);

        public void Subscribe(Action<Models.Notification> onNext)
        {
            _notification.Subscribe(onNext);
        }

        public void OnNext(Models.Notification value)
        {
            _notification.OnNext(value);
        }

    }
}