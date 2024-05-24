using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IOrderItemService
    {
        Result<OrderItemDto> CreateOrderItem(OrderItemDto orderItemDto);
        Result<List<OrderItemDto>> GetAllOrderItems();
        bool DeleteOrderItem(long orderItemId);
        Result<List<ItemDto>> GetAllForOrder(long orderId);
        Result<List<OrderItemDto>> GetByOrderId(long orderId);
        Result<long> getQuantityByOrderIdAndItemId(long orderId, long itemId);
    }
}
