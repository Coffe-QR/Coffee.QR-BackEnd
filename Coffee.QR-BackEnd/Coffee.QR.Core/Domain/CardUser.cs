using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class CardUser : Entity
    {
        public Card Card { get; private set; }
        public long CardId { get; private set; }
        public User User { get; private set; }
        public long UserId { get; private set; }
        public long Quantity { get; private set; }

        // Payment related fields
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD";
        public string PaymentStatus { get; private set; }
        public string PayPalPaymentIntentId { get; private set; }

        public CardUser(long cardId, long userId, long quantity, decimal amount, string currency, string paymentStatus, string payPalPaymentIntentId)
        {
            CardId = cardId;
            UserId = userId;
            Quantity = quantity;
            Amount = amount;
            Currency = currency;
            PaymentStatus = paymentStatus;
            PayPalPaymentIntentId = payPalPaymentIntentId;
        }
    }
}
