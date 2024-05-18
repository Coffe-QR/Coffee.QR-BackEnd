using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class OrderDto
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public long TableId { get; set; }
        public long LocalId { get; set; }
        public DateOnly Date { get; set; }
        public Boolean IsActive { get; set; }
    }
}
