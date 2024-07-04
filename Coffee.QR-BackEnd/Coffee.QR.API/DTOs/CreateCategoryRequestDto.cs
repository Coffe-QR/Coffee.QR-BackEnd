using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class CreateCategoryRequestDto
    {
        public string ChartKey { get; set; }
        public string CategoryKeyName { get; set; }
    }
}
