using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum SupplyStatus
    {
        INPROGRESS,
        END,
        CREATED
    }
    public class Supply : Entity
    {
        public long CompanyId { get; set; }
        public double TotalPrice { get; set; }  
        public SupplyStatus Status { get; set; }

        public Supply() { }
        public Supply(long companyId, double totalPrice, SupplyStatus supplyStatus)
        {
            CompanyId = companyId;
            TotalPrice = totalPrice;
            Status = supplyStatus;
        }
    }
}
