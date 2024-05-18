using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum MenuStatus
    {
        ACTIVE,
        WAITING,
        FINISHED
    }
    public class Menu : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public long CafeId { get; set; }

        public Menu(string name, string description, Boolean isActive, long cafeId)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            CafeId = cafeId;
        }
    }
}
