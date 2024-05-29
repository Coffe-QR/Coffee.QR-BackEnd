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
    public class EventCardSaleData
    {
        public string EventName { get; set; }
        public string CardName { get; set; }
        public double CardPrice { get; set; }
        public int PurchasedCount { get; set; }
        public double TotalMoney { get; set; }
    }

    public class CardSaleReportService : CrudService<CardSaleReportDto, CardSaleReport>, ICardSaleReportService
    {
        private readonly ICardSaleRepository _cardSaleRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICardUserRepository _cardUserRepository;
        private readonly IEventRepository _eventRepository;

        public CardSaleReportService(ICrudRepository<CardSaleReport> crudRepository, IMapper mapper, ICardSaleRepository cardSaleRepository, ICardRepository cardRepository, ICardUserRepository cardUserRepository, IEventRepository eventRepository) : base(crudRepository, mapper)
        {
            _cardSaleRepository = cardSaleRepository;
            _cardRepository = cardRepository;
            _cardUserRepository = cardUserRepository;
            _eventRepository = eventRepository;

        }
        public Result<CardSaleReportDto> CreateReport(CardSaleReportDto cardSaleReportDto)
        {
            try
            {
                var report = _cardSaleRepository.Create(new CardSaleReport(CreateReportPdf(cardSaleReportDto), cardSaleReportDto.Date, cardSaleReportDto.UserId));

                CardSaleReportDto resultDto = new CardSaleReportDto
                {
                    Id = report.Id,
                    Path = report.Path,
                    Date = report.Date,
                    UserId = report.UserId,
                };
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CardSaleReportDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool DeleteReport(long cardSaleReportId)
        {
            var reportToDelete = _cardSaleRepository.Delete(cardSaleReportId);
            return reportToDelete != null;
        }

        public Result<List<CardSaleReportDto>> GetAllReports()
        {
            try
            {
                var reports = _cardSaleRepository.GetAll();
                var reportDtos = reports.Select(r => new CardSaleReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
                    Date = r.Date,
                    UserId = r.UserId,
                }).ToList();

                return Result.Ok(reportDtos);

            }
            catch (Exception e)
            {
                return Result.Fail<List<CardSaleReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }


        private async Task<List<EventCardSaleData>> GetEventCardSaleData(long authorId)
        {
            var events = _eventRepository.GetAllByUserId(authorId);
            var result = new List<EventCardSaleData>();

            foreach (var eventItem in events)
            {
                var cards = _cardRepository.GetAllByEventId(eventItem.Id);

                foreach (var card in cards)
                {
                    var purchases = _cardUserRepository.GetAll()
                                                       .Where(cu => cu.CardId == card.Id);

                    var purchasedCount = purchases.Sum(p => p.Quantity);
                    var totalMoney = purchases.Sum(p => p.Amount);

                    result.Add(new EventCardSaleData
                    {
                        EventName = eventItem.Name,
                        CardName = card.Type,
                        CardPrice = card.Price,
                        PurchasedCount = (int)purchasedCount,
                        TotalMoney = (double)totalMoney
                    });
                }
            }

            return result;
        }

        private string CreateReportPdf(CardSaleReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\CardSaleReport" + reportDto.UserId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            // Retrieve detailed card sale data for the author
            var detailedData = GetEventCardSaleData(reportDto.UserId).Result;

            // Add date and time to the top right
            PdfPTable dateTable = new PdfPTable(2);
            dateTable.WidthPercentage = 100;
            dateTable.SetWidths(new float[] { 8f, 2f });
            dateTable.AddCell(new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER });
            dateTable.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            doc.Add(dateTable);

            doc.Add(new Paragraph("Card Sales Report"));
            doc.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(5); // 5 columns for Event Name, Card Name, Card Price, Purchased Count, Total Money
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 3f, 2f, 2f, 2f });

            // Add column headers
            table.AddCell("Event Name");
            table.AddCell("Card Name");
            table.AddCell("Card Price");
            table.AddCell("Purchased Count");
            table.AddCell("Total Money");

            // Fill the table with data and calculate total money
            double totalMoneySum = 0;
            foreach (var data in detailedData)
            {
                table.AddCell(data.EventName);
                table.AddCell(data.CardName);
                table.AddCell(data.CardPrice.ToString("C"));
                table.AddCell(data.PurchasedCount.ToString());
                table.AddCell(data.TotalMoney.ToString("C"));
                totalMoneySum += data.TotalMoney;
            }

            // Add total row
            table.AddCell(new PdfPCell(new Phrase("Total")) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT });
            table.AddCell(totalMoneySum.ToString("C"));

            // Add the table to the document
            doc.Add(table);

            // Close the document
            doc.Close();

            return path;
        }






    }
}
