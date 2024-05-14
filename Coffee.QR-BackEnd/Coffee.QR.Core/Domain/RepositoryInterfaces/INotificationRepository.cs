using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        Notification Create(Notification notification);
        List<Notification> GetAll();
        Notification Delete(long notificationId);
        List<Notification> GetAllActive(long localId);
        void UpdateNotificationIsActive(long notificationId, bool isActive);
    }
}
