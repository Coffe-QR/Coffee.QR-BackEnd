using Coffee.QR.API.DTOs;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Coffee.QR.API.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Mime;

namespace Coffee.QR.Core.Services
{
    public class EmailSender : IEmailSender
    {
        public Result SendEmail(string emailDestination, string emailSubject, string emailBody)
        {
            string filePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
            string templatePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/emailTemplateRedesigned.html";


            string jsonString = File.ReadAllText(filePath);
            string templateString = File.ReadAllText(templatePath);


            EmailCredentialsDto credentials = JsonSerializer.Deserialize<EmailCredentialsDto>(jsonString);

            SmtpClient smtpClient = new SmtpClient(credentials.SmtpServer)
            {
                Port = credentials.Port,
                Credentials = new NetworkCredential(credentials.SenderEmail, credentials.SenderPassword),
                EnableSsl = true,
            };

            string mailBody = templateString.Replace("{UserName}", "Customer").Replace("{TicketDetails}", emailBody);

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(credentials.SenderEmail, "Coffee.QR"),
                Subject = emailSubject,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestination);

            string logoPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/logoWhite.png";
            string logoFatmanPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/fatmanqrLogo.png";
            string realCoffeeBannerPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/realCoffeeBanner.png";

            if (File.Exists(logoPath) && File.Exists(logoFatmanPath) && File.Exists(realCoffeeBannerPath))
            {
                LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "logoWhite",
                    TransferEncoding = TransferEncoding.Base64
                };

                LinkedResource logoFatman = new LinkedResource(logoFatmanPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "fatmanqrLogo",
                    TransferEncoding = TransferEncoding.Base64
                };

                LinkedResource coffeeBanner = new LinkedResource(realCoffeeBannerPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "realCoffeeBanner",
                    TransferEncoding = TransferEncoding.Base64
                };

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(mailBody, null, MediaTypeNames.Text.Html);
                avHtml.LinkedResources.Add(logo);
                avHtml.LinkedResources.Add(logoFatman);
                avHtml.LinkedResources.Add(coffeeBanner);

                mailMessage.AlternateViews.Add(avHtml);
            }

            try
            {
                // Send email
                smtpClient.Send(mailMessage);
                return null; // Success
            }
            catch (Exception e)
            {
                // Handle error
                return Result.Fail(FailureCode.EmailError);
            }
            finally
            {
                // Clean up
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }


        public Result SendEmailWithAttachment(string emailDestination, string emailSubject, string emailBody, string attachmentName)
        {
            string filePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
            string attachmentFilePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Tickets/" + attachmentName;
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

        /*        public Result SendEmailWithAttachments(string emailDestination, string emailSubject, string emailBody, List<string> attachmentNames)
                {
                    string filePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
                    string templatePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/emailTemplate.html";
                    string jsonString = File.ReadAllText(filePath);
                    string templateString = File.ReadAllText(templatePath);

                    EmailCredentialsDto credentials = JsonSerializer.Deserialize<EmailCredentialsDto>(jsonString);

                    SmtpClient smtpClient = new SmtpClient(credentials.SmtpServer)
                    {
                        Port = credentials.Port,
                        Credentials = new NetworkCredential(credentials.SenderEmail, credentials.SenderPassword),
                        EnableSsl = true,
                    };

                    // Replace placeholders in the template
                    string mailBody = templateString.Replace("{UserName}", "Customer").Replace("{TicketDetails}", emailBody);

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(credentials.SenderEmail, "Coffee.QR"),
                        To = { emailDestination },
                        Subject = emailSubject,
                        Body = mailBody,
                        IsBodyHtml = true,
                    };

                    // Embedding images
                    string logoPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/logoWhite.png"; // Adjust path as necessary
                    string logoFatmanPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/fatmanqrLogo.png"; // Adjust path as necessary

                    if (File.Exists(logoPath) && File.Exists(logoFatmanPath))
                    {
                        LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Png)
                        {
                            ContentId = "logoWhite",
                            TransferEncoding = TransferEncoding.Base64
                        };
                        LinkedResource logoFatman = new LinkedResource(logoFatmanPath, MediaTypeNames.Image.Png)
                        {
                            ContentId = "fatmanqrLogo",
                            TransferEncoding = TransferEncoding.Base64
                        };

                        AlternateView avHtml = AlternateView.CreateAlternateViewFromString(mailBody, null, MediaTypeNames.Text.Html);
                        avHtml.LinkedResources.Add(logo);
                        avHtml.LinkedResources.Add(logoFatman);

                        mailMessage.AlternateViews.Add(avHtml);
                    }




                    try
                    {
                        foreach (var attachmentName in attachmentNames)
                        {
                            if (!string.IsNullOrEmpty(attachmentName) && File.Exists(attachmentName))
                            {
                                Attachment attachment = new Attachment(attachmentName);
                                mailMessage.Attachments.Add(attachment);
                            }
                        }

                        smtpClient.Send(mailMessage);
                        return null;
                    }
                    catch (Exception e)
                    {
                        return Result.Fail(FailureCode.EmailError);
                    }
                    finally
                    {
                        mailMessage.Dispose();
                        smtpClient.Dispose();
                    }
                }*/

        public Result SendEmailWithAttachments(string emailDestination, string emailSubject, string emailBody, List<string> attachmentNames)
        {
            string filePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/appEmailSettings.json";
            string templatePath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/emailTemplateRedesigned.html";

            // Read email settings and HTML template
            string jsonString = File.ReadAllText(filePath);
            string templateString = File.ReadAllText(templatePath);

            // Deserialize email settings from JSON
            EmailCredentialsDto credentials = JsonSerializer.Deserialize<EmailCredentialsDto>(jsonString);

            // Configure SMTP client
            SmtpClient smtpClient = new SmtpClient(credentials.SmtpServer)
            {
                Port = credentials.Port,
                Credentials = new NetworkCredential(credentials.SenderEmail, credentials.SenderPassword),
                EnableSsl = true,
            };

            // Replace placeholders in the HTML template
            string mailBody = templateString.Replace("{UserName}", "Customer").Replace("{TicketDetails}", emailBody);

            // Create mail message
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(credentials.SenderEmail, "Coffee.QR"),
                Subject = emailSubject,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailDestination);

            // Embedding images using Content-ID (CID)
            string logoPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/logoWhite.png";
            string logoFatmanPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/fatmanqrLogo.png";
            string realCoffeeBannerPath = "../../Coffee.QR-BackEnd/Coffee.QR-BackEnd/Resources/Images/realCoffeeBanner.png";

            if (File.Exists(logoPath) && File.Exists(logoFatmanPath) && File.Exists(realCoffeeBannerPath))
            {
                LinkedResource logo = new LinkedResource(logoPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "logoWhite",
                    TransferEncoding = TransferEncoding.Base64
                };

                LinkedResource logoFatman = new LinkedResource(logoFatmanPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "fatmanqrLogo",
                    TransferEncoding = TransferEncoding.Base64
                };

                LinkedResource coffeeBanner = new LinkedResource(realCoffeeBannerPath, MediaTypeNames.Image.Png)
                {
                    ContentId = "realCoffeeBanner",
                    TransferEncoding = TransferEncoding.Base64
                };

                // Create alternate view for HTML email and add linked resources
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(mailBody, null, MediaTypeNames.Text.Html);
                avHtml.LinkedResources.Add(logo);
                avHtml.LinkedResources.Add(logoFatman);
                avHtml.LinkedResources.Add(coffeeBanner);

                // Attach alternate view to mail message
                mailMessage.AlternateViews.Add(avHtml);
            }

            try
            {
                // Attachments
                foreach (var attachmentName in attachmentNames)
                {
                    if (!string.IsNullOrEmpty(attachmentName) && File.Exists(attachmentName))
                    {
                        Attachment attachment = new Attachment(attachmentName);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                // Send email
                smtpClient.Send(mailMessage);
                return null; // Success
            }
            catch (Exception e)
            {
                // Handle error
                return Result.Fail(FailureCode.EmailError);
            }
            finally
            {
                // Clean up
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }


    }
}
