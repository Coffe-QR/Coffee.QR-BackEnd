using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class OrderService : CrudService<OrderDto, Domain.Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocalUserRepository _localUserRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILocalRepository _localRepository;
        private const string dateFormat = "dd_MM_yyyy";


        public OrderService(ICrudRepository<Domain.Order> crudRepository, IMapper mapper, IOrderRepository orderRepository, IUserRepository userRepository, ILocalUserRepository localUserRepository, ITableRepository tableRepository, IEmailSender emailSender, ILocalRepository localRepository)
            : base(crudRepository, mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _localUserRepository = localUserRepository;
            _tableRepository = tableRepository;
            _emailSender = emailSender;
            _localRepository = localRepository;
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

        public Result<OrderDto> GetById(long orderId)
        {
            try
            {
                Domain.Order order = _orderRepository.GetById(orderId);
                if (order != null)
                {
                    OrderDto orderDto = new OrderDto
                    {
                        Id = order.Id,
                        Price = order.Price,
                        Description = order.Description,
                        TableId = order.TableId,
                        LocalId = order.LocalId,
                        Date = order.Date,
                        IsActive = order.IsActive
                    };
                    return Result.Ok(orderDto);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<OrderDto>("Failed to retrieve order").WithError(e.Message);
            }
        }

        public Result Export(long userId)
        {
            string settingsFilePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
            string settingsJsonString = File.ReadAllText(settingsFilePath);
            EmailCredentialsDto settings = JsonSerializer.Deserialize<EmailCredentialsDto>(settingsJsonString);
            User userForEmail = _userRepository.GetById(userId);
            if (userForEmail == null)
            {
                return Result.Fail(FailureCode.InvalidUser);
            }
            var userLocal = _localUserRepository.GetByUserId(userId);
            var local = _localRepository.GetById(userLocal.LocalId);
            var allOrdersForLocal = _orderRepository.GetOrdersByLocalId(userLocal.LocalId);
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Order_Information_For_Local_" + local.Name + "_By_User_" + userForEmail.FirstName + "_" + userForEmail.LastName + "_" + DateTime.Now.ToString(dateFormat) + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph("Orders report for local " + local.Name + ":"));
            doc.Add(new Paragraph("\n"));
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 5f, 5f, 3f});
            table.AddCell("Table name");
            table.AddCell("Date");
            table.AddCell("Price");
            foreach (var order in allOrdersForLocal)
            {
                var orderTable = _tableRepository.GetById(order.TableId); 
                table.AddCell(orderTable.Name);
                table.AddCell(order.Date.ToString(dateFormat));
                table.AddCell(order.Price.ToString() + "RSD");
            }
            doc.Add(table);
            doc.Close();
            _emailSender.SendEmailWithAttachment(userForEmail.Email, "Retrieving order information for local " + local.Name + " finished", "All order information:", path);
            File.Delete(path);
            return Result.Ok();
        }
    }
}
