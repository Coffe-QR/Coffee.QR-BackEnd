using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class OrderItem : Entity
    {
        public long Quantity { get; private set; }
        public long OrderId { get; private set; }
        public Order OrderOrigin { get; private set; }
        public long ItemId { get; private set; }
        public Item ItemPicked { get; private set; }


        public OrderItem(long quantity,long orderId, long itemId)
        {
            Quantity = quantity;
            OrderId = orderId;
            ItemId = itemId;
        }
    }
}
