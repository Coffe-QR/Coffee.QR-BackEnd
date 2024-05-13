using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class StorageItemDto
    {
        public long Id { get; set; }
        public long StorageId { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }
    }
}
