using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _dbContext;
         public UserRepository(Context dbContext) {
            _dbContext = dbContext;
        }

        public User GetById(long id)
        {
            User user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if (user == null) throw new KeyNotFoundException("Not found.");
            return user;
        }
    }
}
