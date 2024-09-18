using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public string Picture { get; set; }

        public Item(string name, string description, double price, string picture)
        {
            Name = name;
            Description = description;
            Price = price;
            Picture = picture;
        }
    }
}
