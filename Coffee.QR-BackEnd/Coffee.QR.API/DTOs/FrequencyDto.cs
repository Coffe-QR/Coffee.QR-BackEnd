using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class FrequencyDto
    {
        public long Id { get; set; }
        public string Unit { get; set; }
        public long UnitQuantity { get; set; }
    }
}
