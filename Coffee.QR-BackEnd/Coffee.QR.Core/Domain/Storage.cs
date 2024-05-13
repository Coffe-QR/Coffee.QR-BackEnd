using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Storage : Entity
    {
        public long CompanyId { get; set; }


        public Storage(long companyId)
        {
            CompanyId = companyId;
        }
    }
}
