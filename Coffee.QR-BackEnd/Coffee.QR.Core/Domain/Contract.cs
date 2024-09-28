using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Contract : Entity
    {
        public long LocalId { get; set; }   
        public long CompanyId { get; set; }
        public string Description { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }

        public long FrequencyId { get; set; }
        public Frequency Frequency { get; set; }
        public long SupplyId { get; set; }

        public Contract(long localId, long companyId, string description, DateOnly start, DateOnly end, long frequencyId, long supplyId)
        {
            LocalId = localId;
            CompanyId = companyId;
            Description = description;
            Start = start;
            End = end;
            FrequencyId = frequencyId;
            SupplyId = supplyId;
        }

        public Contract(long localId, long companyId, string description, DateOnly date, long supplyId)
        {
            LocalId = localId;
            CompanyId = companyId;
            Description = description;
            SupplyId = supplyId;
        }
    }
}
