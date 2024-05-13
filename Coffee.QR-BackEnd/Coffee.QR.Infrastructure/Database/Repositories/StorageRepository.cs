using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly Context _dbContext;
        public StorageRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Storage Create(Storage storage)
        {
            _dbContext.Storages.Add(storage);
            _dbContext.SaveChanges();
            return storage;
        }

        public List<Storage> GetAll()
        {
            return _dbContext.Storages.ToList();
        }

        public Storage Delete(long eventId)
        {
            var eventToDelete = _dbContext.Storages.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.Storages.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }
    }
}
