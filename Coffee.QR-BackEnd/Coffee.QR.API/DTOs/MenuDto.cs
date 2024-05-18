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
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public long CafeId { get; set; }

    }
}
