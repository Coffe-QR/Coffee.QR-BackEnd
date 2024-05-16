﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public class OrderItemDto
    {
        public long Id { get; set; }
        public double Quantity { get; set; }
        public long OrderId { get; set; }
        public long ItemId { get; set; }
    }
}
