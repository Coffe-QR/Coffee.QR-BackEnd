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
using System.Net.Mail;

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
                var carduser = _cardUserRepository.Create(new CardUser(
                    cardUserDto.CardId,
                    cardUserDto.UserId,
                    cardUserDto.Quantity,
                    cardUserDto.Amount,
                    cardUserDto.Currency,
                    cardUserDto.PaymentStatus,
                    cardUserDto.PayPalPaymentIntentId
                ));

                CardUserDto resultDto = new CardUserDto
                {
                    CardId = cardUserDto.CardId,
                    UserId = cardUserDto.UserId,
                    Quantity = cardUserDto.Quantity,
                    Amount = cardUserDto.Amount,
                    Currency = cardUserDto.Currency,
                    PaymentStatus = cardUserDto.PaymentStatus,
                    PayPalPaymentIntentId = cardUserDto.PayPalPaymentIntentId,
                    ReceiverEmail = cardUserDto.ReceiverEmail,

                    PrintCard = cardUserDto.PrintCard
                };

                if (cardUserDto.PaymentStatus == "COMPLETED")
                {
                    List<string> attachments = new List<string>();

                    for (int i = 0; i < cardUserDto.Quantity; i++)
                    {
                        string pdfPath = CreateCardPdf(cardUserDto.PrintCard); 
                        string attachmentFilePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Tickets/" + pdfPath;
                        attachments.Add(attachmentFilePath);
                    }

                    _emailSender.SendEmailWithAttachments(
                        cardUserDto.ReceiverEmail,
                        "Coffee.QR - Bought Ticket",
                        "You successfully bought tickets, they are in the section below",
                        attachments
                    );
                }

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<CardUserDto>("Invalid argument: " + e.Message);
            }
        }

        private string GenerateRandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string CreateCardPdf(PrintCardDto reportDto)
        {
            string base36Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString("x"); // Convert to hex for shorter output

            string randomSuffix = GenerateRandomAlphanumericString(2);

            string uniqueId = $"{base36Timestamp}{randomSuffix}";

            string attachmentName = $"Ticket_{reportDto.EventName}_{reportDto.Position}_{uniqueId}.pdf";

            string path = $"..\\Coffee.QR-BackEnd\\Resources\\Tickets\\{attachmentName}";

            Document doc = new Document(PageSize.A4, 0, 0, 0, 0);
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            if (reportDto.EventImage != "")
            {
                               
                string imagePath = $"..\\Coffee.QR-BackEnd\\Resources{reportDto.EventImage.Replace("/", "\\")}";
                iTextSharp.text.Image eventImage = iTextSharp.text.Image.GetInstance(imagePath);
                eventImage.ScaleToFit(doc.PageSize.Width, doc.PageSize.Height / 3); // Scale to fit full width
                eventImage.Alignment = Element.ALIGN_CENTER;
                doc.Add(eventImage);
            }

            // Add event name
            Paragraph eventName = new Paragraph(reportDto.EventName, new Font(Font.FontFamily.HELVETICA, 36, Font.BOLD))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 20,
                SpacingAfter = 20
            };
            doc.Add(eventName);

            // Add event date and time
            Paragraph eventDateTime = new Paragraph(reportDto.EventDateTime, new Font(Font.FontFamily.HELVETICA, 24, Font.NORMAL))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10
            };
            doc.Add(eventDateTime);

            // Add position
            Paragraph position = new Paragraph($"POSITION: {reportDto.Position}", new Font(Font.FontFamily.HELVETICA, 24, Font.NORMAL))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10
            };
            doc.Add(position);

            // Add price
            Paragraph price = new Paragraph($"PRICE: {reportDto.TicketPrice}$", new Font(Font.FontFamily.HELVETICA, 24, Font.NORMAL))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            doc.Add(price);

            // Add QR code
            string uniqueQrCodeText = $"{reportDto.EventName} {reportDto.EventDateTime} {reportDto.Position} ${reportDto.TicketPrice} {uniqueId}";
            BarcodeQRCode qrCode = new BarcodeQRCode(uniqueQrCodeText, 150, 150, null);
            iTextSharp.text.Image qrCodeImage = qrCode.GetImage();
            qrCodeImage.Alignment = Element.ALIGN_CENTER;
            doc.Add(qrCodeImage);

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

            return attachmentName;
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
                    Id = c.Id,
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

        public IEnumerable<CardUserDto> GetByUserId(long userId) 
        {
        var cardUsers = _cardUserRepository.GetByUserId(userId);
            return cardUsers.Select(cu => new CardUserDto
            {
                Id = cu.Id,
                CardId = cu.CardId,
                UserId = cu.UserId,
                Quantity = cu.Quantity,
                Amount = cu.Amount,
                Currency = cu.Currency,
                PaymentStatus = cu.PaymentStatus,
                PayPalPaymentIntentId = cu.PayPalPaymentIntentId,
                }).ToList();

        }


    }
}
