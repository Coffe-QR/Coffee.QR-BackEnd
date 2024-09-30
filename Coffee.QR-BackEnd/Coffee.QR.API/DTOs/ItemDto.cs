using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum ItemTypeDto
    {
        FOOD,
        DRINK,
        INGREDIENT
    };
    public class ItemDto
    {
        public long Id { get; set; }
        public ItemTypeDto Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public long Quantity { get; set; }
        public string CompanyName { get; set; }
        public long DaysDelivery { get; set; }
        public bool Reccomended { get; set; }
        public long? ExceptedQuantity { get; set; }

    }
}
