using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Receipt : Entity
    {
        public string Path { get; private set; }
        public DateOnly Date { get; private set; }
        public long OrderId { get; private set; }
        public Order Order { get; private set; }
        public long WaiterId { get; private set; }
        public User Waiter { get; private set; }

        public Receipt(string path, DateOnly date, long orderId, long waiterId)
        {
            Path = path;
            Date = date;
            OrderId = orderId;
            WaiterId = waiterId;
        }
    }
}
