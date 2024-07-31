using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class CardEvent : Entity
    {
        public Card Card { get; private set; }
        public long CardId { get; private set; }
        public double Price { get; private set; }
        public Event @event { get; private set; }
        public long EventId { get; private set; }

        public void UpdatePrice(double newPrice) => Price = newPrice;
        public CardEvent(long cardId, double price, long eventId)
        {
            CardId = cardId;
            Price = price;
            EventId = eventId;
        }
    }
}
