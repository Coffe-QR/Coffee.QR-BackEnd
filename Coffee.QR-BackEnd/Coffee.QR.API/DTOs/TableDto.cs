using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class TableDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Capacity { get; set; }
        public Boolean IsSmokingArea { get; set; }
        public long LocalId { get; set; }
    }
}
