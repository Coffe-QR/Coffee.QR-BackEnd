using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum MenuStatusDto
    {
        ACTIVE,
        WAITING,
        FINISHED
    }
    public class MenuDto
    {
        public long Id { get; set; }    
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public MenuStatusDto Status { get; set; }
        public long CafeId { get; set; }

    }
}
