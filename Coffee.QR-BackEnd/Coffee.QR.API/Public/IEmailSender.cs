using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IEmailSender
    {
        Result SendEmailWithAttachment(string emailDestination, string emailSubject, string emailBody, string attachmentName);
    }
}
