using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ILocalRentPriceListRepository
    {
        LocalRentPriceList Create(LocalRentPriceList localRentPriceList);
        LocalRentPriceList CreateAndDeactivateExisting(LocalRentPriceList newLocalRentPriceList);
        LocalRentPriceList? GetActiveByLocalId(long localId);
        bool DeactivateAllByLocalId(long localId);
    }
}
