using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class ErrorSupplyDto
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public long ItemId { get; set; }
        public long SupplyId { get; set; }
        public long? ReceivedQuantity { get; set; }
        public long ExpectedQuantity { get; set; }
        public double? Price { get; set;}
    }
}


