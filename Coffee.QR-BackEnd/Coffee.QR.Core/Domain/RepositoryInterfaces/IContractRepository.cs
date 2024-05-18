using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IContractRepository
    {
        Contract Create(Contract contract);
        List<Contract> GetAll();
        Contract Delete(long contractId);
    }
}
