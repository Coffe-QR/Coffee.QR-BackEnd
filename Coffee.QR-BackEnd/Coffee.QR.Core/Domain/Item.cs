using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum ItemType
    {
        FOOD, 
        DRINK,
        INGREDIENT
    };
    public class Item : Entity
    {
        public ItemType Type { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public string Picture { get; set; }

        public Item(ItemType type, string name, string description, double price, string picture)
        {
            Type = type;
            Name = name;
            Description = description;
            Price = price;
            Picture = picture;
        }
    }
}
