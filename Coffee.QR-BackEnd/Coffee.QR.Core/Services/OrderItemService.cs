using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class OrderItemService : CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(ICrudRepository<OrderItem> crudRepository, IMapper mapper, IOrderItemRepository orderItemRepository)
            : base(crudRepository, mapper)
        {
            _orderItemRepository = orderItemRepository;
        }

        public Result<OrderItemDto> CreateOrderItem(OrderItemDto orderItemDto)
        {
            try
            {
                var orderItemt = _orderItemRepository.Create(new OrderItem(orderItemDto.Quantity, orderItemDto.OrderId, orderItemDto.ItemId));

                OrderItemDto resultDto = new OrderItemDto
                {
                    Id = orderItemt.Id,
                    Quantity = orderItemDto.Quantity,
                    OrderId = orderItemt.OrderId,
                    ItemId = orderItemt.ItemId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<OrderItemDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<OrderItemDto>> GetAllOrderItems()
        {
            try
            {
                var orderItems = _orderItemRepository.GetAll();
                var orderItemDtos = orderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    Quantity = oi.Quantity,
                    OrderId = oi.OrderId,
                    ItemId = oi.ItemId,
                }).ToList();

                return Result.Ok(orderItemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<OrderItemDto>>("Failed to retrieve orderItem").WithError(e.Message);
            }
        }


        public bool DeleteOrderItem(long orderItemId)
        {
            var orderItemToDelete = _orderItemRepository.Delete(orderItemId);
            return orderItemToDelete != null;
        }
    }
}
