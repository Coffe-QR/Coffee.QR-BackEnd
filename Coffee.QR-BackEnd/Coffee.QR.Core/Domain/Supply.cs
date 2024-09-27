using Coffee.QR.BuildingBlocks.Core.Domain;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum SupplyStatus
    {
        ORDERED,
        TAKEN, 
        MISTAKE,
        CONFIRMED
    }
    public class Supply : Entity
    {
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public double TotalPrice { get; set; }
        public SupplyStatus Status { get; set; }
        public DateOnly? Ordered { get; set; }
        public ICollection<ErrorSupply> ErrorSupplies { get; } = [];

        public Supply() {
            Id = 0;
        }
        public Supply(long companyId, double totalPrice, SupplyStatus supplyStatus)
        {
            CompanyId = companyId;
            TotalPrice = totalPrice;
            Status = supplyStatus;
        }

        public void Taken()
        {
            Status = SupplyStatus.TAKEN;
        }
        public void Confirm()
        {
            Status = SupplyStatus.CONFIRMED;
        }

    }
}
