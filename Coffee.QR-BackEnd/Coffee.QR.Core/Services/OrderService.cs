using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class OrderService : CrudService<OrderDto, Domain.Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;


        public OrderService(ICrudRepository<Domain.Order> crudRepository, IMapper mapper, IOrderRepository orderRepository)
            : base(crudRepository, mapper)
        {
            _orderRepository = orderRepository;
        }

        public Result<OrderDto> CreateOrder(OrderDto orderDto)
        {
            try
            {
                var ordert = _orderRepository.Create(new Domain.Order(orderDto.Price, orderDto.Description, orderDto.TableId, orderDto.LocalId, DateOnly.FromDateTime(DateTime.Now), orderDto.IsActive));

                OrderDto resultDto = new OrderDto
                {
                    Id = ordert.Id,
                    Price = ordert.Price,
                    Description = ordert.Description,
                    TableId = ordert.TableId,
                    Date = ordert.Date,
                    LocalId = ordert.LocalId,
                    IsActive = ordert.IsActive,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<OrderDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<OrderDto>> GetAllOrders()
        {
            try
            {
                var orders = _orderRepository.GetAll();
                var orderDtos = orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Price = o.Price,
                    Description = o.Description,
                    TableId = o.TableId,
                    Date = o.Date,
                    LocalId = o.LocalId,
                    IsActive = o.IsActive,
                }).ToList();

                return Result.Ok(orderDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<OrderDto>>("Failed to retrieve orders").WithError(e.Message);
            }
        }


        public bool DeleteOrder(long orderId)
        {
            var orderToDelete = _orderRepository.Delete(orderId);
            return orderToDelete != null;
        }

        public Result<List<OrderDto>> getByLocalIdAndIsActive(long localId)
        {
            try
            {
                var orders = _orderRepository.GetActiveOrdersByLocalId(localId);
                var orderDtos = orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Price = o.Price,
                    Description = o.Description,
                    TableId = o.TableId,
                    Date = o.Date,
                    LocalId = o.LocalId,
                    IsActive = o.IsActive,
                }).ToList();

                return Result.Ok(orderDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<OrderDto>>("Failed to retrieve orders for local").WithError(e.Message);
            }
        }

        public void DeactivateOrder(long orderId)
        {
            _orderRepository.UpdateOrderIsActive(orderId, false);
        }

        public Result<ItemPeriodRecomendation> GetRecommendationForTimePeriod(ItemPeriodRecommendationRequest request) 
        {
            var ordersForPeriod = _orderRepository.GetAllOrdersForLocalForPeriod(request);

            var returnData = new ItemPeriodRecomendation { Items = new List<ItemDto>(), MostSoldItemForMonth = "" };
            foreach (var order in ordersForPeriod) 
            {
                foreach (var orderItem in order.OrderItems) 
                {
                    bool flag = false;
                    foreach (var itemDto in returnData.Items) 
                    {
                        if (orderItem.ItemId == itemDto.Id) 
                        {
                            itemDto.Quantity += orderItem.Quantity;
                            flag = true;
                        }
                    }
                    if (!flag) 
                    {
                        ItemDto newItemDto = new ItemDto
                        {
                            Id = orderItem.ItemId,
                            Type = orderItem.ItemPicked.Type != null ? (ItemTypeDto)orderItem.ItemPicked.Type : default,
                            Name = orderItem.ItemPicked.Name ?? "Unknown",
                            Description = orderItem.ItemPicked.Description ?? "No description",
                            Quantity = orderItem.Quantity,
                            Picture = orderItem.ItemPicked.Picture
                        };
                        returnData.Items.Add(newItemDto);
                    }
                }
            }

            string mostPopularItemName = "";
            long biggestQuantity = 0;

            foreach (ItemDto itemDto in returnData.Items) 
            {
                if (itemDto.Quantity > biggestQuantity) 
                {
                    biggestQuantity = itemDto.Quantity;
                    mostPopularItemName = itemDto.Name;
                }else if (itemDto.Quantity == biggestQuantity) 
                {
                    mostPopularItemName = mostPopularItemName + ", " + itemDto.Name;
                }
            }

            returnData.MostSoldItemForMonth = $"The most sold items: {mostPopularItemName} consider ordering more of that";

            if (returnData.Items.Count == 0) 
            {
                returnData.MostSoldItemForMonth = "You had no sold item in that period";
            }

            return returnData;
        }
    }
}
