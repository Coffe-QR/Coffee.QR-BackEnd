using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class ReceiptDto
    {
        public long Id { get; set; }
        public string Path { get;set; }
        public DateOnly Date { get; set; }
        public long OrderId { get; set; }
        public long WaiterId { get; set; }
    }
}
