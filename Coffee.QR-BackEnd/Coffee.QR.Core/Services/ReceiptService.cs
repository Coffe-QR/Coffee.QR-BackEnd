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

namespace Coffee.QR.Core.Services
{
    public class ReceiptService : CrudService<ReceiptDto, Receipt>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IUserRepository _userRepository;

        public ReceiptService(ICrudRepository<Receipt> crudRepository, IMapper mapper, IReceiptRepository receiptRepository, IOrderRepository orderRepository, ILocalRepository localRepository, IUserRepository userRepository) : base(crudRepository, mapper)
        {
            _receiptRepository = receiptRepository;
            _orderRepository = orderRepository;
            _localRepository = localRepository;
            _userRepository = userRepository;
        }

        public Result<ReceiptDto> CreateReceipt(ReceiptDto receiptDto)
        {
            try
            {
                var receipt = _receiptRepository.Create(new Receipt(CreateReceiptPdf(receiptDto), DateOnly.FromDateTime(DateTime.Now), receiptDto.OrderId, receiptDto.WaiterId));

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

        private string CreateReceiptPdf(ReceiptDto receiptDto)
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
            doc.Add(new Paragraph("--------------------------------------------------------"));
            User waiter = _userRepository.GetById(receiptDto.WaiterId);
            doc.Add(new Paragraph("Id kupca                                                 20"));
            doc.Add(new Paragraph("Konobar                                            " + waiter.FirstName));
            doc.Add(new Paragraph("--------------PROMET - PRODAJA--------------"));
            doc.Add(new Paragraph("                              Artikli                             "));
            doc.Add(new Paragraph("================================="));
            doc.Add(new Paragraph("Naziv                    Cena        Kol.     Ukupno"));
            /*
            List<ItemDto> dtos = BestItems(reportDto, 2020);
            // Dodaj naslov dokumenta
            doc.Add(new Paragraph("Items List"));
            doc.Add(new Paragraph("\n"));

            // Kreiraj tabelu sa četiri kolone
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 5f, 2f, 2f });

            // Dodaj zaglavlja kolona
            table.AddCell("Name");
            table.AddCell("Description");
            table.AddCell("Price");
            table.AddCell("Quantity");

            // Popuni tabelu podacima iz liste
            foreach (var item in dtos)
            {
                table.AddCell(item.Name);
                table.AddCell(item.Description);
                table.AddCell(item.Price.ToString("C")); // Formatiraj kao valuta
                table.AddCell(item.Quantity.ToString()); //item.Quantity.ToString());
            }

            // Dodaj tabelu u dokument
            doc.Add(table);

            // Zatvori dokument


            doc.Close();
            */
            doc.Close();
            return path;
        }
    }
}
