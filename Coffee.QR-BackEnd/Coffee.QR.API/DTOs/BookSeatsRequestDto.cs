using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class BookSeatsRequestDto
    {
        public string EventName { get; set; }
        public List<string> SeatsToBook { get; set; }
    }
}
