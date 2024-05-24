using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _dbContext;
        public OrderRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Order Create(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order;
        }

        public List<Order> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public Order Delete(long orderId)
        {
            var orderToDelete = _dbContext.Orders.Find(orderId);
            if (orderToDelete != null)
            {
                _dbContext.Orders.Remove(orderToDelete);
                _dbContext.SaveChanges();
            }
            return orderToDelete;
        }

        public List<Order> GetActiveOrdersByLocalId(long localId) 
        {
            return _dbContext.Orders.Where(o=>o.LocalId== localId && o.IsActive).ToList();
        }

        public void UpdateOrderIsActive(long orderId, bool isActive)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.IsActive = isActive;
                _dbContext.SaveChanges();
            }
        }

        public Order GetById(long orderId)
        {
            return _dbContext.Orders.Find(orderId);
        }
    }
}
