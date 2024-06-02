using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum Frequency { MONTHLY, YEARLY, WEAKLY }
    public class Contract : Entity
    {
        public long LocalId { get; set; }   
        public long CompanyId { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public Frequency Frequency { get; set; }
        public long SupplyId { get; set; }

        public Contract(long localId, long companyId, string description, DateOnly date, Frequency frequency, long supplyId)
        {
            LocalId = localId;
            CompanyId = companyId;
            Description = description;
            Date = date;
            Frequency = frequency;
            SupplyId = supplyId;
        }
    }
}
