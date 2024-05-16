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
    public class CardRepository : ICardRepository
    {
        private readonly Context _dbContext;

        public CardRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public Card Create(Card card)
        {
            _dbContext.Cards.Add(card);
            _dbContext.SaveChanges();
            return card;
        }

        public List<Card> GetAll()
        {
            return _dbContext.Cards.ToList();
        }

        public Card Delete(long cardId)
        {
            var card = _dbContext.Cards.Find(cardId);
            if (card != null)
            {
                _dbContext.Cards.Remove(card);
                _dbContext.SaveChanges();
            }
            return card;
        }

        
        public List<Card> GetAllByEventId(long eventId)
        {
            return _dbContext.Cards.Where(c => c.EventId == eventId).ToList();
        }

        public async Task<Card> GetByIdAsync(long id)
        {
            return await _dbContext.Cards.FindAsync(id);
        }
    }
}
