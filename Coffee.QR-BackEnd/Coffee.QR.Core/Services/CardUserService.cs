using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Interfaces;
using FluentResults;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class CardUserService : CrudService<CardDto,Card>, ICardUserService 
    {
        private readonly ICardUserRepository _cardUserRepository;
        private readonly IEmailSender _emailSender;

        public CardUserService(ICrudRepository<Card> crudRepository, IMapper mapper, ICardUserRepository carduserRepository, IEmailSender emailSender) : base(crudRepository, mapper)
        {
            _cardUserRepository = carduserRepository;
            _emailSender = emailSender;
        }

        public Result<CardUserDto> CreateCardUser(CardUserDto cardUserDto)
        {
            try
            {
                var carduser = _cardUserRepository.Create(new CardUser(cardUserDto.CardId, cardUserDto.UserId, cardUserDto.Quantity,cardUserDto.Amount,cardUserDto.Currency,cardUserDto.PaymentStatus,cardUserDto.PayPalPaymentIntentId));

                CardUserDto resultDto = new CardUserDto
                {
                    CardId = cardUserDto.CardId,
                    UserId = cardUserDto.UserId,
                    Quantity = cardUserDto.Quantity,
                    Amount = cardUserDto.Amount,
                    Currency = cardUserDto.Currency,
                    PaymentStatus = cardUserDto.PaymentStatus,
                    PayPalPaymentIntentId = cardUserDto.PayPalPaymentIntentId,
                    
                };
                //OVDE STAVI IF cardUserDto.PaymentStatus=="COMPLETED" -> POSALJI MEJL I KARTE
                if(cardUserDto.PaymentStatus == "COMPLETED")
                {
                    
                    _emailSender.SendEmail(cardUserDto.receiverEmail, "Coffee.QR - Bought Ticket", "You successfully bought your tickets on Coffee.QR");
                }

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CardUserDto>("Invalid argument: " + e.Message);
            }
        }

        private string CreateCardPdf(CardSaleReportDto reportDto)
        {
            string vr = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");

            string path = "..\\Coffee.QR-BackEnd\\Resources\\Tickets\\Ticket_" + reportDto.UserId + "_" + vr + ".pdf";
            Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            // Set fonts
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, BaseColor.DARK_GRAY);
            var subtitleFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.GRAY);
            var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
            var cellFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            var totalFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);


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




        public async Task<bool> DeleteCardUserAsync(long cardUserId)
        {
            try
            {
                await _cardUserRepository.DeleteAsync(cardUserId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Result<List<CardUserDto>>> GetAllCardUsersAsync()
        {
            try
            {
                var cardUsers = await _cardUserRepository.GetAllAsync();
                var cardUserDtos = cardUsers.Select(c => new CardUserDto
                {
                    CardId = c.CardId,
                    UserId = c.UserId,
                    Quantity = c.Quantity,
                    Amount = c.Amount,
                    Currency = c.Currency,
                    PaymentStatus = c.PaymentStatus,
                    PayPalPaymentIntentId = c.PayPalPaymentIntentId
                }).ToList();

                return Result.Ok(cardUserDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<CardUserDto>>("Failed to retrieve card users").WithError(e.Message);
            }
        }

        public async Task<Result<CardUserDto>> GetCardUserByIdAsync(long cardUserId)
        {
            try
            {
                var cardUser = await _cardUserRepository.GetByIdAsync(cardUserId);
                if (cardUser == null)
                    return Result.Fail<CardUserDto>("CardUser not found");

                var cardUserDto = new CardUserDto
                {
                    CardId = cardUser.CardId,
                    UserId = cardUser.UserId,
                    Quantity = cardUser.Quantity,
                    Amount = cardUser.Amount,
                    Currency = cardUser.Currency,
                    PaymentStatus = cardUser.PaymentStatus,
                    PayPalPaymentIntentId = cardUser.PayPalPaymentIntentId
                };

                return Result.Ok(cardUserDto);
            }
            catch (Exception e)
            {
                return Result.Fail<CardUserDto>("Failed to retrieve card user").WithError(e.Message);
            }
        }

        public async Task UpdateCardUserAsync(CardUserDto cardUserDto)
        {
            try
            {
                CardUserDto resultDto = new CardUserDto
                {
                    CardId = cardUserDto.CardId,
                    UserId = cardUserDto.UserId,
                    Quantity = cardUserDto.Quantity,
                    Amount = cardUserDto.Amount,
                    Currency = cardUserDto.Currency,
                    PaymentStatus = cardUserDto.PaymentStatus,
                    PayPalPaymentIntentId = cardUserDto.PayPalPaymentIntentId,
                };
//                await _cardUserRepository.UpdateAsync(resultDto);
            }
            catch (ArgumentException e)
            {
                // Consider more specific error handling or logging
            }
        }

    }
}
