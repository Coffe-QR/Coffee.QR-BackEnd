using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly Context _dbContext;
        public SupplyRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Supply Create(Supply supply)
        {
            _dbContext.Supplies.Add(supply);
            _dbContext.SaveChanges();
            return supply;
        }

        public List<Supply> GetAll()
        {
            return _dbContext.Supplies.ToList();
        }

        public Supply Delete(long eventId)
        {
            var eventToDelete = _dbContext.Supplies.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.Supplies.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }

        public Supply GetById(long supplyId)
        {
            return _dbContext.Supplies.Find(supplyId);
        }
    }
}
