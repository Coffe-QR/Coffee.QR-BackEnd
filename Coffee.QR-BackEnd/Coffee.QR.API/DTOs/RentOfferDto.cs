using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class RentOfferDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LocalId { get; set; }
        public double Price { get; set; }
        public DateTime DateTime { get; set; }
        public string RentOfferStatus { get; set; }
    }
}
