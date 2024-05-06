using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum SupplyStatusDto
    {
        INPROGRESS,
        END,
        CREATED
    }
    public class SupplyDto { 
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public double TotalPrice { get; set; }
        public SupplyStatusDto Status { get; set; }
    }
}
