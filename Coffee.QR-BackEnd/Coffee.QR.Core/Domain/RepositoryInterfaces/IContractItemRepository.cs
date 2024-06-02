using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IContractItemRepository
    {
        ContractItem Create(ContractItem contract);
        List<ContractItem> GetAll();
        ContractItem Delete(long contractId);
        double GetPriceForContract(long contractId);
    }
}
