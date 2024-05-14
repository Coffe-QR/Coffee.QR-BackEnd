using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class LocalDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateOnly DateOfStartingPartnership { get; set; }
        public Boolean IsActive { get; set; }
    }
}
