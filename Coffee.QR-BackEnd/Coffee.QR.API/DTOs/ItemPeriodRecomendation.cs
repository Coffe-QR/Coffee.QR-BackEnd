using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class ItemPeriodRecomendation
    {
        public List<ItemDto> Items { get; set; }

        public string MostSoldItemForMonth { get; set; }
    }
}
