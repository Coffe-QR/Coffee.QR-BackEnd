using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class StorageItemRepository : IStorageItemRepository
    {
        private readonly Context _dbContext;
        public StorageItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public StorageItem Create(StorageItem storage)
        {
            _dbContext.StorageItems.Add(storage);
            _dbContext.SaveChanges();
            return storage;
        }

        public List<StorageItem> GetAll()
        {
            return _dbContext.StorageItems.ToList();
        }

        public StorageItem Delete(long eventId)
        {
            var eventToDelete = _dbContext.StorageItems.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.StorageItems.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }
    }
}
