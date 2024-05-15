using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class EventDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long UserId { get; set; }
        public long LocalId { get; set; }


    }
}
