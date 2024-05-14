using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Notification : Entity
    {
        public string Message { get; private set; }
        public DateTime DateTime { get; private set; }
        public Boolean IsActive { get; private set; }
        public long TableId { get; private set; }
        public Table TableOrigin { get; private set; }
        public long LocalId { get; private set; }
        public Local Place { get; private set; }

        public Notification(string message, DateTime dateTime, Boolean isActive, long tableId, long localId)
        {
            Message = message;
            DateTime = dateTime;
            IsActive = isActive;
            TableId = tableId;
            LocalId = localId;
        }

        public Notification SetIsActive(Notification notification,bool isActive)
        {
            notification.IsActive = isActive;
            return notification;
        }
    }
}
