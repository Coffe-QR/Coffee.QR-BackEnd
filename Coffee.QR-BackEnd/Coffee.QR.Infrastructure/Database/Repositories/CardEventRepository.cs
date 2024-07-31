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
    public class CardEventRepository : ICardEventRepository
    {
        private readonly Context _dbContext;

        public CardEventRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public CardEvent Create(CardEvent cardEvent)
        {
            _dbContext.CardEvents.Add(cardEvent);
            _dbContext.SaveChanges();
            return cardEvent;
        }

        public List<CardEvent> GetAll()
        {
            return _dbContext.CardEvents.ToList();
        }

        public CardEvent Delete(long cardEventId)
        {
            var cardEvent = _dbContext.CardEvents.Find(cardEventId);
            if (cardEvent != null)
            {
                _dbContext.CardEvents.Remove(cardEvent);
                _dbContext.SaveChanges();
            }
            return cardEvent;
        }

        public List<CardEvent> GetAllByEventId(long eventId)
        {
            return _dbContext.CardEvents.Where(c => c.EventId == eventId).ToList();
        }

        public async Task<CardEvent> GetByIdAsync(long id)
        {
            return await _dbContext.CardEvents.FindAsync(id);
        }

        public async Task<CardEvent> GetCardEventByCardIdAsync(long cardId)
        {
            return await _dbContext.Set<CardEvent>().Include(ce => ce.Card).FirstOrDefaultAsync(ce => ce.CardId == cardId);
        }
        public void UpdateCardEvent(CardEvent cardEvent)
        {
            _dbContext.Update(cardEvent);
            _dbContext.SaveChanges();
        }

    }
}
