using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IErrorSupplyRepository
    {
        ErrorSupply Create(ErrorSupply errorSupply);
        List<ErrorSupply> GetAllForSupply(long supplyId);
    }
}
