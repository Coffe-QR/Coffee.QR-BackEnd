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
        DRINK
    };
    public class Item : Entity
    {
        public ItemType Type { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(string type, string name, string description)
        {
            Type = (ItemType)Enum.Parse(typeof(ItemType), type, true);
            Name = name;
            Description = description;
        }
    }
}
