using Coffee.QR.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Coffee.QR.API.DTOs;
using System.Text.Json;
using Coffee.QR.API.Public;

namespace Coffee.QR.Core.Services
{
    public class EmailSender : IEmailSender
    {
        public Result SendEmailWithAttachment(string emailDestination, string emailSubject, string emailBody, string attachmentName)
        {
            string filePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
            string attachmentFilePath = attachmentName;
            string jsonString = File.ReadAllText(filePath);
            EmailCredentialsDto credentials = JsonSerializer.Deserialize<EmailCredentialsDto>(jsonString);

            SmtpClient smtpClient = new SmtpClient(credentials.SmtpServer)
            {
                Port = credentials.Port,
                Credentials = new NetworkCredential(credentials.SenderEmail, credentials.SenderPassword),
                EnableSsl = true,
            };

            MailMessage MailMessage = new MailMessage
            {
                From = new MailAddress(credentials.SenderEmail),
                To = { emailDestination },
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = true,
            };
            try
            {
                if (!string.IsNullOrEmpty(attachmentFilePath) && File.Exists(attachmentFilePath))
                {
                    Attachment attachment = new Attachment(attachmentFilePath);
                    MailMessage.Attachments.Add(attachment);
                }
                smtpClient.Send(MailMessage);
                return null;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.EmailError);
            }
            finally
            {
                MailMessage.Dispose();
                smtpClient.Dispose();

            }
        }
    }
}
