using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class SupplyItemDto
    {
        public long Id { get; set; }    
        public long SupplyId { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}
