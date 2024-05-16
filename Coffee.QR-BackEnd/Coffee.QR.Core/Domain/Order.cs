using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Order : Entity
    {
        public double Price { get; set; }
        public string Description { get; private set; }
        public long TableId { get; private set; }
        public User Table { get; private set; }
        public long LocalId { get; private set; }
        public Local Local { get; private set; }

        public Order(double price,string description, long tableId, long localId)
        {
            Price = price;
            Description = description;
            TableId = tableId;
            LocalId = localId;
        }
    }
}
