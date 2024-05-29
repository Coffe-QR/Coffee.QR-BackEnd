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
    public class CardSaleReportService : CrudService<CardSaleReportDto, CardSaleReport>, ICardSaleReportService
    {
        private readonly ICardSaleRepository _cardSaleRepository;

        public CardSaleReportService(ICrudRepository<CardSaleReport> crudRepository,IMapper mapper,ICardSaleRepository cardSaleRepository) : base(crudRepository,mapper)
        {
               _cardSaleRepository = cardSaleRepository;
        }
        public Result<CardSaleReportDto> CreateReport(CardSaleReportDto cardSaleReportDto)
        {
            try {
                var report = _cardSaleRepository.Create(new CardSaleReport(CreateReportPdf(cardSaleReportDto), (ReportType)Enum.Parse(typeof(ReportType), cardSaleReportDto.Type.ToString(), true), cardSaleReportDto.Date, cardSaleReportDto.LocalId));

                     CardSaleReportDto resultDto = new CardSaleReportDto
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
            throw new NotImplementedException();
        }

       
        private string CreateReportPdf(CardSaleReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\Test" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(reportDto.Type.ToString() + " report!"));


            //List<ItemDto> dtos = BestItems(reportDto, 2020);

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
            
            /*foreach (var item in dtos)
            {
                table.AddCell(item.Name);
                table.AddCell(item.Description);
                table.AddCell(item.Price.ToString("C")); // Formatiraj kao valuta
                table.AddCell(item.Quantity.ToString()); //item.Quantity.ToString());
            }*/

            // Dodaj tabelu u dokument
            doc.Add(table);

            // Zatvori dokument
            doc.Close();


            doc.Close();
            return path;
        }
    }
}
