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
using iTextSharp.text.pdf.draw;
using System.Diagnostics;

namespace Coffee.QR.Core.Services
{

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


        private async Task<List<EventCardSaleDto>> GetEventCardSaleData(long authorId)
        {
            var events = _eventRepository.GetAllByUserId(authorId);
            var result = new List<EventCardSaleDto>();

            foreach (var eventItem in events)
            {
                var cards = _cardRepository.GetAllByEventId(eventItem.Id);

                foreach (var card in cards)
                {
                    var purchases = _cardUserRepository.GetAll()
                                                       .Where(cu => cu.CardId == card.Id);

                    var purchasedCount = purchases.Sum(p => p.Quantity);
                    var totalMoney = purchases.Sum(p => p.Amount);

                    result.Add(new EventCardSaleDto
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
            string vr = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");

            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\CardSaleReport_" + reportDto.UserId + "_" + vr + ".pdf";
            Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            // Set fonts
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, BaseColor.DARK_GRAY);
            var subtitleFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.GRAY);
            var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
            var cellFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            var totalFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);

            // Add date and time to the top right
            PdfPTable dateTable = new PdfPTable(2);
            dateTable.WidthPercentage = 100;
            dateTable.SetWidths(new float[] { 8f, 2f });
            dateTable.AddCell(new PdfPCell(new Phrase("Card Sales Report", titleFont)) { Border = Rectangle.NO_BORDER });
            dateTable.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), subtitleFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            doc.Add(dateTable);

            // Add a horizontal line
            doc.Add(new Chunk(new LineSeparator()));

            // Add half of new row bottom margin (adjust the height as needed)
            doc.Add(new Paragraph("\n"));

            // Retrieve detailed card sale data for the author
            var detailedData = GetEventCardSaleData(reportDto.UserId).Result;

            PdfPTable table = new PdfPTable(5); // 5 columns for Event Name, Card Name, Card Price, Purchased Count, Total Money
            table.WidthPercentage = 100;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            table.SetWidths(new float[] { 3f, 3f, 2f, 2f, 2f });

            // Add column headers
            PdfPCell cell;
            cell = new PdfPCell(new Phrase("Event Name", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Card Name", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Card Price", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Quantity", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Total Money", headerFont)) { BackgroundColor = BaseColor.DARK_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);

            // Fill the table with data and calculate total money
            double totalMoneySum = 0;
            string lastEventName = null;
            bool alternateRow = false;

            foreach (var data in detailedData)
            {
                if (data.EventName != lastEventName)
                {
                    alternateRow = !alternateRow;
                    lastEventName = data.EventName;
                }

                BaseColor rowColor = alternateRow ? new BaseColor(230, 230, 230) : BaseColor.WHITE;

                cell = new PdfPCell(new Phrase(data.EventName, cellFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = rowColor };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(data.CardName, cellFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = rowColor };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(data.CardPrice.ToString("$0.00"), cellFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5, BackgroundColor = rowColor };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(data.PurchasedCount.ToString(), cellFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5, BackgroundColor = rowColor };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(data.TotalMoney.ToString("$0.00"), cellFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5, BackgroundColor = rowColor };
                table.AddCell(cell);

                totalMoneySum += data.TotalMoney;
            }

            // Add total row without border
            cell = new PdfPCell(new Phrase("Total", totalFont)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5, Border = Rectangle.NO_BORDER };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(totalMoneySum.ToString("$0.00"), totalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5, Border = Rectangle.NO_BORDER };
            table.AddCell(cell);

            // Add the table to the document
            doc.Add(table);

            // Add a horizontal line
            doc.Add(new Chunk(new LineSeparator()));

            // Close the document
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

            return "/pdfs/CardSaleReport" + reportDto.UserId + '_' + vr + ".pdf";
            //return path;
        }



    }
}
