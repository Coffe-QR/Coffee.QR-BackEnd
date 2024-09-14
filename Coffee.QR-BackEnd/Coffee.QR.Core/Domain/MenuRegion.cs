using Coffee.QR.BuildingBlocks.Core.Domain;
using iTextSharp.text.xml.simpleparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class MenuRegion : Entity
    {
        public string Name { get; set; }
        public long MenuId { get; set; }
        public Menu Menu{ get; set;}

        public MenuRegion(string name, long menuId)
        {
            Name = name;
            MenuId = menuId;
        }
    }
}
