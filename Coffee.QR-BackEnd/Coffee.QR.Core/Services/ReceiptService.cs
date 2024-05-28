using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Coffee.QR.Core.Services
{
    public class ReceiptService : CrudService<ReceiptDto, Receipt>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IItemRepository _itemRepository;

        public ReceiptService(ICrudRepository<Receipt> crudRepository, IMapper mapper, IReceiptRepository receiptRepository, IOrderRepository orderRepository, ILocalRepository localRepository, IUserRepository userRepository, IOrderItemRepository orderItemRepository, IItemRepository itemRepository) : base(crudRepository, mapper)
        {
            _receiptRepository = receiptRepository;
            _orderRepository = orderRepository;
            _localRepository = localRepository;
            _userRepository = userRepository;
            _orderItemRepository = orderItemRepository;
            _itemRepository = itemRepository;
        }

        public Result<ReceiptDto> CreateReceipt(ReceiptDto receiptDto,double datoPara)
        {
            try
            {
                var receipt = _receiptRepository.Create(new Receipt(CreateReceiptPdf(receiptDto,datoPara), DateOnly.FromDateTime(DateTime.Now), receiptDto.OrderId, receiptDto.WaiterId));

                ReceiptDto resultDto = new ReceiptDto
                {
                    Id = receipt.Id,
                    Path = receipt.Path,
                    Date = receipt.Date,
                    OrderId = receipt.OrderId,
                    WaiterId = receipt.WaiterId,
                };
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ReceiptDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<ReceiptDto>> GetAllForLocal(long localId)
        {
            try
            {
                var receipts = _receiptRepository.GetAll();
                List<Receipt> returnList = new List<Receipt>();
                foreach (var receipt in receipts) 
                {
                    Order order = _orderRepository.GetById(receipt.OrderId);
                    if (order.LocalId == localId) 
                    {
                        returnList.Add(receipt);
                    }
                }
                var receiptDtos = returnList.Select(r => new ReceiptDto
                {
                    Id = r.Id,
                    Path = r.Path,
                    Date = r.Date,
                    OrderId = r.OrderId,
                    WaiterId = r.WaiterId,
                }).ToList();

                return Result.Ok(receiptDtos);

            }
            catch (Exception e)
            {
                return Result.Fail<List<ReceiptDto>>("Failed to retrieve receipts").WithError(e.Message);
            }
        }


        public bool DeleteReceipt(long receiptId)
        {
            var receiptToDelete = _receiptRepository.Delete(receiptId);
            return receiptToDelete != null;
        }

        private List<ReceiptItemDto> OrderItems(long orderId)
        {
            var orderItems = _orderItemRepository.GetItemsForOrder(orderId);
            List<ReceiptItemDto> returnList= new List<ReceiptItemDto>();
            foreach (OrderItem orderItem in orderItems) 
            {
                Item item = _itemRepository.GetById(orderItem.ItemId);
                ReceiptItemDto receiptItemDto = new ReceiptItemDto
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = orderItem.Quantity
                };
                returnList.Add(receiptItemDto);
            }
            return returnList;

        }

        private string CreateReceiptPdf(ReceiptDto receiptDto,double moneyReceived)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Test" + "_Order" + receiptDto.OrderId + "_Date" + receiptDto.Date.ToString("dd_MM_yyyy") + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph("===========Fiskalni racun==========="));
            Random random = new Random();
            int randomNumber = random.Next(100000000, 1000000000);
            doc.Add(new Paragraph("                         " + randomNumber));
            Order order = _orderRepository.GetById(receiptDto.OrderId);
            Local local = _localRepository.GetById(order.LocalId);
            doc.Add(new Paragraph("                              " + local.Name));
            doc.Add(new Paragraph("                           " + local.City));
            doc.Add(new Paragraph("----------------------------------------------------------"));
            User waiter = _userRepository.GetById(receiptDto.WaiterId);
            doc.Add(new Paragraph("Id kupca                                                 20"));
            doc.Add(new Paragraph("Konobar                                            " + waiter.FirstName));
            doc.Add(new Paragraph("--------------PROMET - PRODAJA--------------"));
            doc.Add(new Paragraph("                              Artikli                             "));
            doc.Add(new Paragraph("=================================="));
            doc.Add(new Paragraph("Naziv                    Cena        Kol.     Ukupno"));
            List<ReceiptItemDto> dtos = OrderItems(receiptDto.OrderId);
            double priceSum = 0;
            foreach (ReceiptItemDto receiptItemDto in dtos) 
            {
                doc.Add(new Paragraph(receiptItemDto.Name + "                    " + receiptItemDto.Price + "           " + receiptItemDto.Quantity + "          " + receiptItemDto.Price*receiptItemDto.Quantity));
                priceSum = priceSum + receiptItemDto.Price * receiptItemDto.Quantity;
            }
            doc.Add(new Paragraph("------------------------------------------------------------"));
            doc.Add(new Paragraph("Za uplatu                                              " + priceSum));
            doc.Add(new Paragraph("Prenos na racun                                   " + moneyReceived));
            double change = moneyReceived - priceSum;
            doc.Add(new Paragraph("Povracaj                                               " + change));
            doc.Add(new Paragraph("=================================="));
            doc.Add(new Paragraph("Oznaka              Ime         Stopa         Porez"));
            doc.Add(new Paragraph("Dj                  O-PDV       20.00%       " + priceSum/5));
            doc.Add(new Paragraph("------------------------------------------------------------"));
            doc.Add(new Paragraph("Ukupan iznos poreza                         " + priceSum/5));
            doc.Add(new Paragraph("=================================="));
            DateTime now = DateTime.Now;
            doc.Add(new Paragraph("PFR Vreme                    " + now.ToString("dd.MM.yyyy HH:mm:ss")));
            doc.Add(new Paragraph("======KRAJ FISKALNOG RACUNA======"));

            doc.Close();

            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not open the PDF file.");
                Console.WriteLine(ex.Message);
            }
            return path;
        }
    }
}
