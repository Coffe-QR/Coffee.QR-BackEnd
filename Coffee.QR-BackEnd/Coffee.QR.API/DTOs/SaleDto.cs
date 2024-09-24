using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Coffee.QR.API.DTOs
{
    public class SaleDto
    {
        public long Id { get; set; }    
        public long Pieces { get; set; }
        public long Discount { get; set; }
        public long CompanyId { get; set; }
    }
}
