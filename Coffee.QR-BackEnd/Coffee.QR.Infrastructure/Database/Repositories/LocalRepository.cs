using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly Context _dbContext;
        public LocalRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Local Create(Local local)
        {
            _dbContext.Locals.Add(local);
            _dbContext.SaveChanges();
            return local;
        }

        public List<Local> GetAll()
        {
            return _dbContext.Locals.ToList();
        }

        public Local Delete(long localId)
        {
            var localToDelete = _dbContext.Locals.Find(localId);
            if (localToDelete != null)
            {
                _dbContext.Locals.Remove(localToDelete);
                _dbContext.SaveChanges();
            }
            return localToDelete;
        }

        public async Task<Local> GetByIdAsync(long id)
        {
            return await _dbContext.Locals.FindAsync(id);
        }

        public Local GetById(long localId)
        {
            return _dbContext.Locals.Find(localId);
        }
    }
}
