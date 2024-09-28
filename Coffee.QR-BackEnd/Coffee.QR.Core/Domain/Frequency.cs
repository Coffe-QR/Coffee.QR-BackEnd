using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{

    public class Frequency : Entity
    {
        public Unit Unit { get; set; }
        public long UnitQuantity {  get; set; }
    }
}

public enum Unit
{
    DAY, 
    WEEK,
    MONTH,
    YEAR
}