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
        private readonly IItemRepository _itemRepository;

        public OrderItemService(ICrudRepository<OrderItem> crudRepository, IMapper mapper, IOrderItemRepository orderItemRepository, IItemRepository itemRepository)
            : base(crudRepository, mapper)
        {
            _orderItemRepository = orderItemRepository;
            _itemRepository = itemRepository;
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

        public Result<List<ItemDto>> GetAllForOrder(long orderId)
        {
            try
            {
                List<Item> items = new List<Item>();
                List<OrderItem> orderItems = _orderItemRepository.GetAllByOrderId(orderId);
                foreach (var orderItem in orderItems)
                {
                    items.Add(_itemRepository.GetById(orderItem.ItemId));
                }

                var itemDtos = items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), i.Type.ToString(), true),
                    Price = i.Price,
                    Picture = i.Picture,
                }).ToList();

                return Result.Ok(itemDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ItemDto>>("Failed to retrieve items").WithError(e.Message);
            }
        }

        public Result<List<OrderItemDto>> GetByOrderId(long orderId)
        {
            try
            {
                var orderItems = _orderItemRepository.GetAllByOrderId(orderId);
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
                return Result.Fail<List<OrderItemDto>>("Failed to retrieve orderItems").WithError(e.Message);
            }
        }

        public Result<long> getQuantityByOrderIdAndItemId(long orderId, long itemId)
        {
            try
            {
                OrderItem orderItem = _orderItemRepository.getQuantityByOrderIdAndItemId(orderId,itemId);
                if (orderItem != null)
                {
                    return Result.Ok(orderItem.Quantity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<long>("Failed to retrieve quantity").WithError(e.Message);
            }
        }
    }
}
