using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public Boolean IsActive { get; set; }
        public long TableId { get; set; }
        public long LocalId { get; set; }
    }
}
