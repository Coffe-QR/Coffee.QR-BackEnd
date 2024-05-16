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
    public class OrderService : CrudService<OrderDto, Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;


        public OrderService(ICrudRepository<Order> crudRepository, IMapper mapper, IOrderRepository orderRepository)
            : base(crudRepository, mapper)
        {
            _orderRepository = orderRepository;
        }

        public Result<OrderDto> CreateOrder(OrderDto orderDto)
        {
            try
            {
                var ordert = _orderRepository.Create(new Order(orderDto.Price, orderDto.Description, orderDto.TableId, orderDto.LocalId));

                OrderDto resultDto = new OrderDto
                {
                    Price = ordert.Price,
                    Description = ordert.Description,
                    TableId = ordert.TableId,
                    LocalId = ordert.LocalId,
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
                    LocalId = o.LocalId,
                }).ToList();

                return Result.Ok(orderDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<OrderDto>>("Failed to retrieve events").WithError(e.Message);
            }
        }


        public bool DeleteOrder(long orderId)
        {
            var orderToDelete = _orderRepository.Delete(orderId);
            return orderToDelete != null;
        }
    }
}
