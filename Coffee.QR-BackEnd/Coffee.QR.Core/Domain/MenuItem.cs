using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class MenuItem : Entity
    {
        public long MenuId { get; set; }
        public long ItemId { get; set; }


        public MenuItem(long menuId, long itemId)
        {
            MenuId = menuId;
            ItemId = itemId;
        }
    }
}
