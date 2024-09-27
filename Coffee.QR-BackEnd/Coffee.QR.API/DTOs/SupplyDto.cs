using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum SupplyStatusDto
    {
        ORDERED,
        TAKEN,
        MISTAKE,
        CONFIRMED
    }
    public class SupplyDto { 
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public double TotalPrice { get; set; }
        public SupplyStatusDto Status { get; set; }
        public string CompanyName { get; set; }
        public DateOnly? Ordered { get; set; }

    }
}
