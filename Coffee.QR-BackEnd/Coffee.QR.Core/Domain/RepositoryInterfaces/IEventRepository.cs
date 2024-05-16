using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IEventRepository
    {
        Event Create(Event @event);
        List<Event> GetAll();
        Event Delete(long eventId);
        List<Event> GetAllByUserId(long userId);
        Task<Event> GetByIdAsync(long id);

    }
}
