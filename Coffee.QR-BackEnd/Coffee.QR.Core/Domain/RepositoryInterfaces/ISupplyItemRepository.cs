using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ISupplyItemRepository
    {
        SupplyItem Create(SupplyItem supply);
        List<SupplyItem> GetAll();
        SupplyItem Delete(long supplyId);
    }
}
