using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Coffee.QR.Core.Domain;

using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Infrastructure.Database;

namespace Coffee.QR.Infrastructure.Repositories
{
    public class CardUserRepository : ICardUserRepository
    {
        private readonly Context _context;

        public CardUserRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CardUser>> GetAllAsync()
        {
            return await _context.CardUsers.ToListAsync();
        }

        public List<CardUser> GetAll()
        {
            return _context.CardUsers.ToList();
        }


        public async Task<CardUser> GetByIdAsync(long cardUserId)
        {
            return await _context.CardUsers
                .FirstOrDefaultAsync(c => c.CardId == cardUserId);
        }

        public CardUser Create(CardUser cardUser)
        {
            _context.CardUsers.Add(cardUser);
            _context.SaveChanges();
            return cardUser;
        }

        public async Task UpdateAsync(CardUser cardUser)
        {
            _context.Entry(cardUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long cardUserId)
        {
            var cardUser = await _context.CardUsers.FindAsync(cardUserId);
            if (cardUser != null)
            {
                _context.CardUsers.Remove(cardUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
