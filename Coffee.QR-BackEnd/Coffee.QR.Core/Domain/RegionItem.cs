using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class RegionItem : Entity
    {
        public long RegionId { get; set; }
        public long ItemId { get; set; }

        public RegionItem(long regionId, long itemId)
        {
            RegionId = regionId;
            ItemId = itemId;
        }
    }
}
