﻿using Coffee.QR.Core.Domain;
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

        public User? GetActiveByName(string username)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
        }

        public bool Exists(string username)
        {
            return _dbContext.Users.Any(user => user.Username == username);
        }

        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }
    }
}
