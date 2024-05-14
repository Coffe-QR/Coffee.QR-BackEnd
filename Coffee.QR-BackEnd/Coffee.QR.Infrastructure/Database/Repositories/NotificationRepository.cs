using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
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
