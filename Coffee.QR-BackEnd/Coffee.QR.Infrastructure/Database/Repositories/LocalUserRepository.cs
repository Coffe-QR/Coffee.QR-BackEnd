using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class LocalUserRepository : ILocalUserRepository
    {
        private readonly Context _dbContext;
        public LocalUserRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public LocalUser Create(LocalUser localUser)
        {
            _dbContext.LocalUsers.Add(localUser);
            _dbContext.SaveChanges();
            return localUser;
        }

        public LocalUser GetByUserId(long userId)
        {
            return _dbContext.LocalUsers.FirstOrDefault(l => l.UserId == userId);
        }

        public LocalUser Delete(long localUserId)
        {
            var localUserToDelete = _dbContext.LocalUsers.Find(localUserId);
            if (localUserToDelete != null)
            {
                _dbContext.LocalUsers.Remove(localUserToDelete);
                _dbContext.SaveChanges();
            }
            return localUserToDelete;
        }

        public List<LocalUser> GetAll()
        {
            return _dbContext.LocalUsers.ToList();
        }
    }
}
