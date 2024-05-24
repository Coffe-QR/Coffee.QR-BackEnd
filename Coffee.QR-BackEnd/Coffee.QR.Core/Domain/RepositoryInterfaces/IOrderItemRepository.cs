using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IOrderItemRepository
    {
        OrderItem Create(OrderItem orderItem);
        List<OrderItem> GetAll();
        OrderItem Delete(long orderItemId);
        List<OrderItem> GetItemsForOrder(long orderId);
        List<OrderItem> GetAllByOrderId(long orderId);
        OrderItem getQuantityByOrderIdAndItemId(long orderId, long itemId);
    }
}
