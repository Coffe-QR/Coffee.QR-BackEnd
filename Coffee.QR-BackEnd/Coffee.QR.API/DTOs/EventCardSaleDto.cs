using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class EventCardSaleDto
    {
        public string EventName { get; set; }
        public string CardName { get; set; }
        public double CardPrice { get; set; }
        public int PurchasedCount { get; set; }
        public double TotalMoney { get; set; }
    }
}
