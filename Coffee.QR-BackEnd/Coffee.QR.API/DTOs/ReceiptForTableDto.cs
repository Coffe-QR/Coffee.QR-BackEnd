using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class ReceiptForTableDto
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public DateOnly Date { get; set; }
        public long TableId { get; set; }
        public long WaiterId { get; set; }
    }
}
