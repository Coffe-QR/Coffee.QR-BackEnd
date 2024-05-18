using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IItemRepository
    {
        Item Create(Item item);
        List<Item> GetAll();
        Item Delete(long itemId);
        Item GetItem(long itemId);
        Item GetById(long itemId);
        bool UpdateItem(Item item);
    }
}
