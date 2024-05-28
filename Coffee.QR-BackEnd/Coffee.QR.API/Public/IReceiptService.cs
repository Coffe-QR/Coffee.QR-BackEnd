using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IReceiptService
    {
        Result<ReceiptDto> CreateReceipt(ReceiptDto receiptDto);
        Result<List<ReceiptDto>> GetAllForLocal(long localId);
        bool DeleteReceipt(long receiptId);
    }
}
