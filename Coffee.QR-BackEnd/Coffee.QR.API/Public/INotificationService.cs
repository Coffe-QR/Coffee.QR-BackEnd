using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface INotificationService
    {
        Result<NotificationDto> CreateNotification(NotificationDto notificationDto);
        Result<List<NotificationDto>> GetAllNotifications();
        bool DeleteNotification(long notificationId);
        Result<List<NotificationDto>> GetAllActiveNotifications(long localId);
        public void DeactivateNotification(long notificationId);
    }
}
