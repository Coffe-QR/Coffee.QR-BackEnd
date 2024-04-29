using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Event : Entity
    {
        public string Name { get; private set; }
        public DateTime DateTime { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }

        public Event(string name,DateTime dateTime, string description, string image)
        {
            Name = name;
            DateTime = dateTime;
            Description = description;
            Image = image;
        }
    }
}
