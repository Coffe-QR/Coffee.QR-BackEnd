using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class UpdateCategoryRequestDto
    {
        public string EventKey { get; set; }
        public List<string> ObjectIds { get; set; }
        public string NewCategory { get; set; }
    }



}
