using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class CardEventDto
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public double Price { get; set; }
        public long EventId { get; set; }
    }
}
