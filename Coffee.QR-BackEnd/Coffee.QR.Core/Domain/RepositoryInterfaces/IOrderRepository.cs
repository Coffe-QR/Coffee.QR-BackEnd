﻿using System;
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
    }
}