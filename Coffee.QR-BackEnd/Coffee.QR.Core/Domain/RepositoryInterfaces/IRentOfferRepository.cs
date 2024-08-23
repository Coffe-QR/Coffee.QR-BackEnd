using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IRentOfferRepository
    {
        RentOffer Create(RentOffer rentOffer);
        RentOffer GetById(long id);
        IEnumerable<RentOffer> GetAll();
        RentOffer Update(RentOffer rentOffer);
        bool Delete(long id);
        IEnumerable<RentOffer> GetByLocalId(long localId);
    }
}
