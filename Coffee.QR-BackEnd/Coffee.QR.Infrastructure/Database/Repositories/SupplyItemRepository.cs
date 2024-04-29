using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class SupplyItemRepository : ISupplyItemRepository
    {
        private readonly Context _dbContext;
        public SupplyItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public SupplyItem Create(SupplyItem supply)
        {
            _dbContext.SupplyItems.Add(supply);
            _dbContext.SaveChanges();
            return supply;
        }

        public List<SupplyItem> GetAll()
        {
            return _dbContext.SupplyItems.ToList();
        }

        public SupplyItem Delete(long eventId)
        {
            var eventToDelete = _dbContext.SupplyItems.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.SupplyItems.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }
    }
}
