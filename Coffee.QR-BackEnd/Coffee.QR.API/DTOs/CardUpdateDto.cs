using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class CardUpdateDto
    {
        public string Type { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
    }
}
