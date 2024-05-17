using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Card : Entity
    {
        public double Price { get; private set; }
        public string Type { get; private set; }
        public string? Note { get; private set; }
        public Event @event { get; private set; }
        public long EventId { get; private set; }

        public Card(double price, string type, string note, long eventId)
        {
            Price = price;
            Type = type;
            Note = note;
            EventId = eventId;
        }
    }
}
