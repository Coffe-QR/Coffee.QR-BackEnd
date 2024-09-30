using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using Stripe.Treasury;
using System.Diagnostics;

namespace Coffee.QR.Core.Services
{
    public class ReportService : CrudService<ReportDto, Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly ISupplyRepository _supplyRepository;

        public ReportService(ICrudRepository<Report> crudRepository, IMapper mapper, IReportRepository reportRepository, IItemRepository itemRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IContractRepository contractRepository, IContractItemRepository contractItemRepository, ISupplyRepository supplyRepository)
            : base(crudRepository, mapper)
        {
            _reportRepository = reportRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _contractRepository = contractRepository;
            _contractItemRepository = contractItemRepository;
            _supplyRepository = supplyRepository;   
        }

        public Result<ReportDto> CreateReport(ReportDto reportDto)
        {
            try
            {
                var report = _reportRepository.Create(new Report(CreateReportPdfProfit(reportDto), (ReportType)Enum.Parse(typeof(ReportType), reportDto.Type.ToString(), true), reportDto.Start, reportDto.LocalId));
                report.Kind = ReportKind.PROFIT;

                ReportDto resultDto = new ReportDto
                {
                    Id = report.Id,
                    Path = report.Path,
                  //  Date = report.Date,
                  //  Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), report.Type.ToString(), true),
                    LocalId = report.LocalId,   
                };
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ReportDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<ReportDto>> GetAllReports()
        {
            try
            {
                var reports = _reportRepository.GetAll();
                var reportDtos = reports.Select(r => new ReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
                //    Date = r.Date,
                 //   Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), r.Type.ToString(), true),
                    LocalId = r.LocalId,
                }).ToList();

                return Result.Ok(reportDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }

        public Result<List<ReportDto>> GetAllForLocalProfit(long localId)
        {
            try
            {
                var reports = _reportRepository.GetAll().FindAll(r => r.LocalId == localId && r.Kind == ReportKind.PROFIT);
                var reportDtos = reports.Select(r => new ReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
               //    Date = r.Date,
               //     Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), r.Type.ToString(), true),
                    LocalId = r.LocalId,
                }).ToList();

                return Result.Ok(reportDtos);

            }
            catch (Exception e)
            {
                return Result.Fail<List<ReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }


        public bool DeleteReport(long reportId)
        {
            var reportToDelete = _reportRepository.Delete(reportId);
            return reportToDelete != null;
        }

        private List<ItemDto> BestItems(ReportDto reportDto, int year)
        {

            List<ItemDto> items = new List<ItemDto>();
       //     if(reportDto.Type == ReportTypeDto.YEARLY)
        //    { 
                foreach (var order in _orderRepository.GetAll().FindAll(o => o.LocalId == reportDto.LocalId))
                {
                    foreach (var orderItem in _orderItemRepository.GetItemsForOrder(order.Id))
                    {

                        ItemDto item = items.Find(i => i.Id == orderItem.ItemId);
                        if (item != null)
                        {
                            item.Quantity += orderItem.Quantity;
                        }
                        else
                        {
                            var domain = _itemRepository.GetItem(orderItem.ItemId);
                            ItemDto dto = new ItemDto()
                            {
                                Id = domain.Id,
                                Name = domain.Name,
                                Description = domain.Description,
                                Quantity = orderItem.Quantity,
                                Price = domain.Price,
                                Picture = domain.Picture,
                                Type = (ItemTypeDto)Enum.Parse(typeof(ItemTypeDto), domain.Type.ToString(), true),
                            };
                            items.Add(dto);
                        }
                    }
          //      }
            }
            
            return items;
        }

        private string CreateReportPdfProfit(ReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Test" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(reportDto.Type.ToString() +  " report!"));


            List<ItemDto> dtos = BestItems(reportDto, 2020);
            doc.Add(new Paragraph("Items List"));
            doc.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 5f, 2f, 2f });

            table.AddCell("Name");
            table.AddCell("Description");
            table.AddCell("Price");
            table.AddCell("Quantity");

            foreach (var item in dtos)
            {
                table.AddCell(item.Name);
                table.AddCell(item.Description);
                table.AddCell(item.Price.ToString("C")); 
                table.AddCell(item.Quantity.ToString()); 
            }

            doc.Add(table);

            doc.Close();


            doc.Close();
            return "/Pdfs/Test" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
        }

        public Result<List<ReportDto>> GetAllForLocalCost(long localId)
        {
            try
            {
                var reports = _reportRepository.GetAll().FindAll(r => r.LocalId == localId && r.Kind == ReportKind.COST);
                var reportDtos = reports.Select(r => new ReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
                 //   Date = r.Date,
                  //  Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), r.Type.ToString(), true),
                    LocalId = r.LocalId,
                }).ToList();

                return Result.Ok(reportDtos);

            }
            catch (Exception e)
            {
                return Result.Fail<List<ReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }

        public Result<ReportDto> CreateCostReport(ReportDto reportDto)
        {
            try
            {
                var report = _reportRepository.Create(new Report(CreateReportPdfCost(reportDto), (ReportType)Enum.Parse(typeof(ReportType), reportDto.Type.ToString(), true), reportDto.Start, reportDto.LocalId));
                report.Kind = ReportKind.COST;

                ReportDto resultDto = new ReportDto
                {
                    Id = report.Id,
                    Path = report.Path,
               //     Date = report.Date,
                //    Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), report.Type.ToString(), true),
                    LocalId = report.LocalId,   
                };
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ReportDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private string CreateReportPdfCost(ReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Cost" + reportDto.Type + "_" + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(reportDto.Type.ToString() + " report!"));


            doc.Add(new Paragraph("Contracts"));
            doc.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 5f, 2f, 2f });

            table.AddCell("Contract");
            table.AddCell("Description");
            table.AddCell("Frequency");
            table.AddCell("Price");

            double fullPrice = 0;

            foreach (var item in _contractRepository.GetAll().FindAll(c => c.LocalId == reportDto.LocalId))
            {
                double price = _contractItemRepository.GetPriceForContract(item.Id);
                table.AddCell(item.Id.ToString());
                table.AddCell(item.Description);
                table.AddCell(item.Frequency.ToString());
                //if (item.Frequency == Frequency.WEAKLY) price *= 4;
                table.AddCell(price.ToString("C"));
                fullPrice += price;
            }

            doc.Add(new Paragraph("\n"));
            doc.Add(new Paragraph("Full price: " + fullPrice));
            doc.Add(new Paragraph("\n"));

            doc.Add(table);

            doc.Close();
            return "/Pdfs/Cost" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
        }



        public Result<ReportDto> CreateNewReport(ReportDto reportDto)
        {
            try
            {
                Report report = MapToDomain(reportDto);
                List<Order> orders = _orderRepository.GetOrdersByLocalId(1).FindAll(o => o.Date >= reportDto.Start && o.Date <= reportDto.End);

                double earned = 0;
                double costed = 0;
                double orderNumber = orders.Count;
                
                foreach (var order in orders)
                {
                    earned += order.Price;
                }

                List<Supply> supplies = _supplyRepository.GetAllForLocalId(1);
                foreach(Supply supply in supplies)
                {
                    costed += supply.TotalPrice;
                }

                string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\REPORT" + "_" + reportDto.Start.ToString("dd-MM-yyyy") + "_" + reportDto.End.ToString("dd-MM-yyyy") + "_" + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                var titleFont = FontFactory.GetFont("Arial", 18); // Font i veličina
                doc.Add(new Paragraph("Izveštaj o Narudžbama", titleFont) { Alignment = Element.ALIGN_CENTER });

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("Od " + reportDto.Start + " do " + reportDto.End));
                doc.Add(new Paragraph("\n"));

                doc.Add(new Paragraph("Zaradjeno: " + earned));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("Troskovi: " + costed));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("Broj narudzbina: " + orderNumber));



                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 3f, 3f, 2f, 2f, 2f });

                table.AddCell("Item");
                table.AddCell("Quantity");
                table.AddCell("Price per unit");
                table.AddCell("Price");
                table.AddCell("Order code");

                foreach (var order in orders)
                {
                    string hash = CreateSHA256Hash(order.Id + " " + order.Date);
                    foreach (var orderItem in order.OrderItems)
                    {
                        Item item = _itemRepository.GetItem(orderItem.ItemId);
                        table.AddCell(item.Name);
                        table.AddCell(orderItem.Quantity.ToString());
                        table.AddCell(item.Price.ToString("C"));
                        table.AddCell((orderItem.Quantity * item.Price).ToString("C"));
                        table.AddCell(hash);
                    }
                }

                doc.Add(new Paragraph("\n"));

                doc.Add(table);

                doc.Close();
                report.LocalId = 1;
                report.Path = "pdfs/REPORT" + "_" + reportDto.Start.ToString("dd-MM-yyyy") + "_" + reportDto.End.ToString("dd-MM-yyyy") + "_" + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
                _reportRepository.Create(report);
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                return MapToDto(report);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ReportDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private string CreateSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a hexadecimal string
                var stringBuilder = new StringBuilder();
                foreach (var b in bytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        public Result<List<ReportDto>> GetNewReport()
        {
            try
            {
                List<Report> reports = _reportRepository.GetAll().FindAll(r => r.LocalId == 1);
                return MapToDto(reports);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<List<ReportDto>>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
