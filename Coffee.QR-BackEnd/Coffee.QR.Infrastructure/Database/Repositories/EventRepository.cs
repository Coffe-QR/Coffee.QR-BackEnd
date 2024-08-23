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
    public class EventRepository : IEventRepository
    {
        private readonly Context _dbContext;
        public EventRepository(Context dbContext)
        {
                _dbContext = dbContext;
        }

        public Event Create(Event @event)
        {
            _dbContext.Events.Add(@event);
            _dbContext.SaveChanges();
            return @event;
        }

        public List<Event> GetAll()
        {
            return _dbContext.Events.ToList();
        }

        public Event Delete(long eventId)
        {
            var eventToDelete = _dbContext.Events.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.Events.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }

        public List<Event> GetAllByUserId(long userId)
        {
            return _dbContext.Events.Where(e => e.UserId == userId).ToList();
        }

        public async Task<Event> GetByIdAsync(long id)
        {
            return await _dbContext.Events.FindAsync(id);
        }

        public async Task<IEnumerable<DateTime>> GetEventDatesByLocalId(long localId)
        {
            return await _dbContext.Events
                .Where(e => e.LocalId == localId)
                .Select(e => e.DateTime.Date) // Ensuring we only get the Date part
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByLocalId(long localId)
        {
            return await _dbContext.Events
                .Where(e => e.LocalId == localId)
                .ToListAsync();
        }

        public List<Event> GetFutureEvents()
        {
            return _dbContext.Events
                .Where(e => e.DateTime > DateTime.UtcNow && !e.Name.StartsWith("RENT"))
                .OrderBy(e => e.DateTime) 
                .ToList();
        }

        public List<Event> GetFutureEventsByUserId(long userId)
        {
            return _dbContext.Events
                .Where(e => e.UserId == userId && e.DateTime > DateTime.UtcNow && !e.Name.StartsWith("RENT"))
                .OrderBy(e => e.DateTime)
                .ToList();
        }


    }
}
