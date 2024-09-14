using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IRegionItemRepository
    {
        RegionItem Create(RegionItem regionItem);
        List<RegionItem> GetAll();
        RegionItem Delete(long regionItemId);
        bool DeleteByRegionIdAndItemId(long regionId, long itemId);
        List<RegionItem> GetAllByRegionId(long regionId);

    }
}
