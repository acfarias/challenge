using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Domain.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void AddNotification(string key, string menssage)
        {
            _notifications.Add(new Notification(key, menssage));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
                _notifications.Add(new Notification(error.ErrorCode, error.ErrorMessage));
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }
    }
}