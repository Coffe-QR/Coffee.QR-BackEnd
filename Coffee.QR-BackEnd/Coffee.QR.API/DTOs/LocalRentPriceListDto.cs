using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class LocalRentPriceListDto
    {
        public long Id { get; set; }
        public long LocalId { get; set; }
        public double Price { get; set; }
        public DateOnly PricingDate { get; set; }
        public bool IsActive { get; set; }
    }
}
