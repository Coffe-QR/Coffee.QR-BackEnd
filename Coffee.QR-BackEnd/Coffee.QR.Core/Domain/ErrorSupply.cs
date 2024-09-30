using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class ErrorSupply : Entity
    {
        public ErrorStatus Status { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
        public long SupplyId { get; set; }
        public Supply Supply { get; set; }
        public long ReceivedQuantity { get; set; }
        public long ExpectedQuantity { get; set; }
        public double? Price { get; set; }
        
        public ErrorSupply(ErrorStatus status, long itemId, long supplyId, long receivedQuantity, long expectedQuantity)
        {
            Status = status;
            ItemId = itemId;
            SupplyId = supplyId;
            ReceivedQuantity = receivedQuantity;
            ExpectedQuantity = expectedQuantity;
        }


    }
}
public enum ErrorStatus{
    MISSING,
    DAMAGED,
    MISSTAKE
}
