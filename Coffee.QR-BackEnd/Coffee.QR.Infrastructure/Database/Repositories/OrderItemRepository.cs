using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly Context _dbContext;
        public OrderItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            _dbContext.OrderItems.Add(orderItem);
            _dbContext.SaveChanges();
            return orderItem;
        }

        public List<OrderItem> GetAll()
        {
            return _dbContext.OrderItems.ToList();
        }

        public OrderItem Delete(long orderItemId)
        {
            var orderItemToDelete = _dbContext.OrderItems.Find(orderItemId);
            if (orderItemToDelete != null)
            {
                _dbContext.OrderItems.Remove(orderItemToDelete);
                _dbContext.SaveChanges();
            }
            return orderItemToDelete;
        }


        public List<OrderItem> GetItemsForOrder(long orderId)
        {
            return _dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        }

        public List<OrderItem> GetAllByOrderId(long orderId)
        {
            return _dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        }

        public OrderItem getQuantityByOrderIdAndItemId(long orderId, long itemId) 
        {
            return _dbContext.OrderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.ItemId == itemId);
        }


    }
}
