using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Table : Entity
    {
        public string Name { get; private set; }
        public long Capacity { get; private set; }
        public Boolean IsSmokingArea { get; private set; }
        public long LocalId { get; private set; }
        public Local Place { get; private set; }

        public Table(string name, long capacity, Boolean isSmokingArea, long localId)
        {
            Name = name;
            Capacity = capacity;
            IsSmokingArea = isSmokingArea;
            LocalId = localId;
        }
    }
}
