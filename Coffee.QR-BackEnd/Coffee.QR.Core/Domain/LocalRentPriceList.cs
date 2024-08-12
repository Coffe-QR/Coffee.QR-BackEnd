using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class LocalRentPriceList : Entity
    {
        public Local local { get; private set; }
        public long LocalId { get; set; }
        public double Price { get; set; }
        public DateOnly PricingDate { get; set; }
        public bool IsActive {  get; set; }

        public LocalRentPriceList(long localId,double price,DateOnly pricingDate, bool isActive)
        {
            LocalId = localId;
            Price = price;
            PricingDate = pricingDate;
            IsActive = isActive;
        }

    }
}
