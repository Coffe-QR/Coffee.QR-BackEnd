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

namespace Coffee.QR.Core.Services
{
    public class ReportService : CrudService<ReportDto, Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public ReportService(ICrudRepository<Report> crudRepository, IMapper mapper, IReportRepository reportRepository, IItemRepository itemRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
            : base(crudRepository, mapper)
        {
            _reportRepository = reportRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public Result<ReportDto> CreateReport(ReportDto reportDto)
        {
            try
            {
                var report = _reportRepository.Create(new Report(CreateReportPdf(reportDto), (ReportType)Enum.Parse(typeof(ReportType), reportDto.Type.ToString(), true), reportDto.Date, reportDto.LocalId));

                ReportDto resultDto = new ReportDto
                {
                    Id = report.Id,
                    Path = report.Path,
                    Date = report.Date,
                    Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), report.Type.ToString(), true),
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
                    Date = r.Date,
                    Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), r.Type.ToString(), true),
                    LocalId = r.LocalId,
                }).ToList();

                return Result.Ok(reportDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<ReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }

        public Result<List<ReportDto>> GetAllForLocal(long localId)
        {
            try
            {
                var reports = _reportRepository.GetAll().FindAll(r => r.LocalId == localId);
                var reportDtos = reports.Select(r => new ReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
                    Date = r.Date,
                    Type = (ReportTypeDto)Enum.Parse(typeof(ReportTypeDto), r.Type.ToString(), true),
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
            if(reportDto.Type == ReportTypeDto.YEARLY)
            { 
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
                }
            }
            
            return items;
        }

        private string CreateReportPdf(ReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Test" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(reportDto.Type.ToString() +  " report!"));


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


            doc.Close();
            return path;
        }


        
    }
}
