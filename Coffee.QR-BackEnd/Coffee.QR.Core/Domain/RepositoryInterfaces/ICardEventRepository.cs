using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ICardEventRepository
    {
        CardEvent Create(CardEvent cardEvent);
        List<CardEvent> GetAll();
        CardEvent Delete(long cardEventId);
        List<CardEvent> GetAllByEventId(long eventId);
        Task<CardEvent> GetByIdAsync(long id);
    }
}
