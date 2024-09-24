using Coffee.QR.BuildingBlocks.Core.Domain;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public long DaysDelivery { get; set; }
        public ICollection<Item> Items { get; } = [];
        public ICollection<Sale> Sales { get; } = [];
        public Company(string name)
        {
            Name = name;
        }
        public Company(string name, long daysDelivery)
        {
            Name = name;
            DaysDelivery = daysDelivery;
        }
    }
}
