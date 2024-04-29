using Coffee.QR.BuildingBlocks.Core.Domain;
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

        public Company(string name)
        {
            Name = name;
        }
    }
}
