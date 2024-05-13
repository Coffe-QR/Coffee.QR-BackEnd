using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class StorageItem : Entity
    {
        public long StorageId { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }

        public StorageItem(long storageId, long itemId, long quantity)
        {
            StorageId = storageId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
