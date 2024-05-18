using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum FrequencyDto { MONTHLY, YEARLY }
    public class ContractDto
    {
        public long Id { get; set; }    
        public long LocalId { get; set; }
        public long CompanyId { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public FrequencyDto Frequency { get; set; }
        public long SupplyId { get; set; }
    }
}
