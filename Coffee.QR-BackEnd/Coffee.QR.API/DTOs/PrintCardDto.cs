using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class PrintCardDto
    {
        /*
        eventImage: this.eventImage,
        eventName: this.eventNameForTicket,
        eventDateTime: this.eventDateTime,
        ticketPrice: this.ticketPrice,
        position: this.selectedSeats[0].label,
         */
        public string EventImage { get; set; }
        public string EventName { get; set; }
        public string EventDateTime { get; set; }
        public long TicketPrice { get; set; }
        public string Position { get; set; }

    }
}
