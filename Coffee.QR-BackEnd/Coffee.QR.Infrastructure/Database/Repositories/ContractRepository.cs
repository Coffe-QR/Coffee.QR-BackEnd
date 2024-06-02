using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    internal class ContractRepository : IContractRepository
    {
        private readonly Context _dbContext;
        public ContractRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Contract Create(Contract contract)
        {
            _dbContext.Contracts.Add(contract);
            _dbContext.SaveChanges();
            return contract;
        }

        public List<Contract> GetAll()
        {
            return _dbContext.Contracts.ToList();
        }

        public Contract Delete(long eventId)
        {
            var contractToDelete = _dbContext.Contracts.Find(eventId);
            if (contractToDelete != null)
            {
                _dbContext.Contracts.Remove(contractToDelete);
                _dbContext.SaveChanges();
            }
            return contractToDelete;
        }

        
    }
}
