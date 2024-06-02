using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    internal class ContractItemRepository : IContractItemRepository
    {
        private readonly Context _dbContext;
        public ContractItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public ContractItem Create(ContractItem contractItem)
        {
            _dbContext.ContractItems.Add(contractItem);
            _dbContext.SaveChanges();
            return contractItem;
        }

        public List<ContractItem> GetAll()
        {
            return _dbContext.ContractItems.ToList();
        }

        public ContractItem Delete(long eventId)
        {
            var eventToDelete = _dbContext.ContractItems.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.ContractItems.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }

        public double GetPriceForContract(long contractId)
        {
            return _dbContext.ContractItems.Where(ci => ci.ContractId == contractId).Sum(ci => ci.Price);
        }
        
    }
}
