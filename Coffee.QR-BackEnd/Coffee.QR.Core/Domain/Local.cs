using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Local : Entity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateOnly DateOfStartingPartnership { get; set; }
        public Boolean IsActive { get; set; }

        public Local(string name, string city, DateOnly dateOfStartingPartnership, bool isActive)
        {
            Name = name;
            City = city;
            DateOfStartingPartnership = dateOfStartingPartnership;
            IsActive = isActive;
        }
    }
}
