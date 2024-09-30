using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class SupplyItem : Entity
    {
        public long SupplyId { get; set; }
        public Supply Supply { get; set; }  
        public long ItemId { get; set; }
        public Item Item { get; set; }
        public long Quantity { get; set; }  
        public double Price { get; set; }
        public SupplyItem(long supplyId, long itemId, long quantity, double price)
        {
            SupplyId = supplyId;
            ItemId = itemId;
            Quantity = quantity;
            Price = price;
        }

        public SupplyItem(SupplyItem si, long supplyId)
        {
            Id = 0;
            SupplyId = supplyId;
            ItemId = si.ItemId;
            Item = si.Item;
            Quantity = si.Quantity;
            Price = si.Price;
        }
    }
}
