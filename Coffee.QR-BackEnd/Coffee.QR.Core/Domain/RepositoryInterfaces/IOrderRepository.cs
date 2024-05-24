using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        List<Order> GetAll();
        Order Delete(long orderId);
        List<Order> GetActiveOrdersByLocalId(long localId);
        void UpdateOrderIsActive(long orderId, bool isActive);
        Order GetById(long orderId);
    }
}
