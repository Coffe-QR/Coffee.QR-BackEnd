using Coffee.QR.BuildingBlocks.Core.Domain;
using iTextSharp.text.xml.simpleparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class ContractItem : Entity
    {
        public long ContractId { get; private set; }
        public Contract Contract { get; private set; }
        public long ItemId { get; private set; }
        public Item Item { get; private set; }
        public long Quantity { get; private set; }
        public double Price { get; private set; }

        public ContractItem(long contractId, long itemId, long quantity, double price)
        {
            ContractId = contractId;
            ItemId = itemId;
            Quantity = quantity;
            Price = price;
        }
    }
}
