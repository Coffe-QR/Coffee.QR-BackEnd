using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Context _dbContext;
        public NotificationRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Notification Create(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
            return notification;
        }

        public List<Notification> GetAll()
        {
            return _dbContext.Notifications.ToList();
        }

        public List<Notification> GetAllActive(long localId)
        {
            return _dbContext.Notifications
                      .Where(n => n.LocalId == localId && n.IsActive)
                      .ToList();
        }

        public void UpdateNotificationIsActive(long notificationId, bool isActive)
        {
            var notification = _dbContext.Notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                notification = notification.SetIsActive(notification,isActive);
                _dbContext.SaveChanges();
            }
        }

        public Notification Delete(long notificationId)
        {
            var notificationToDelete = _dbContext.Notifications.Find(notificationId);
            if (notificationToDelete != null)
            {
                _dbContext.Notifications.Remove(notificationToDelete);
                _dbContext.SaveChanges();
            }
            return notificationToDelete;
        }
    }
}
