using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Sale : Entity
    {
        public long Pieces { get; set; }    
        public long Discount { get; set; }  
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public Sale(long pieces, long discount, long companyId)
        {
            Pieces = pieces;
            Discount = discount;
            CompanyId = companyId;
        }

    }
}
