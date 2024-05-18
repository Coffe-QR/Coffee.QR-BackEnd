using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class CardUserDto
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public long UserId { get; set; }
        public long Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentStatus { get; set; }
        public string PayPalPaymentIntentId { get; set; }
    }
}
