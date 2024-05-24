using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IOrderService
    {
        Result<OrderDto> CreateOrder(OrderDto orderDto);
        Result<List<OrderDto>> GetAllOrders();
        bool DeleteOrder(long orderId);
        Result<List<OrderDto>> getByLocalIdAndIsActive(long localId);
        public void DeactivateOrder(long orderId);
        Result<OrderDto> GetById(long orderId);
    }
}
