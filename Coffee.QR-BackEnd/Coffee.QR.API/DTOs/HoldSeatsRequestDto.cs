using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class HoldSeatsRequestDto
    {
        public string EventName { get; set; }
        public List<string> SeatsToHold { get; set; }
    }
}
