using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ISupplyRepository
    {
        Supply Create(Supply supply);
        List<Supply> GetAll();
        Supply Delete(long supplyId);

        Supply GetById(long supplyId);
    }
}
