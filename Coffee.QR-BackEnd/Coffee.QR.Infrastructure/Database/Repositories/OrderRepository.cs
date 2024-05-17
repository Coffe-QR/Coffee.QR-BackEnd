﻿using Coffee.QR.Core.Domain;
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
    }
}