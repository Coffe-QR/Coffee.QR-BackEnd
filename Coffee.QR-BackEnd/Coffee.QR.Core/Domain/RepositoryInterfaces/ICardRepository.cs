using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ICardRepository
    {
        Card Create(Card card);
        List <Card> GetAll();
        Card Delete(long cardId);
        List<Card> GetAllByEventId(long userId);
        Task<Card> GetByIdAsync(long id);
    }
}
