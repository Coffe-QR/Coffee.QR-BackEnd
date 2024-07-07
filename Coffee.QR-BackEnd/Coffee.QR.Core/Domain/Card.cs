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
        public string Type { get; private set; }
        public string? Note { get; private set; }
        public Local local { get; private set; }
        public long LocalId { get; private set; }

        public Card(string type, string note, long localId)
        {
            Type = type;
            Note = note;
            LocalId = localId;
        }
    }
}
