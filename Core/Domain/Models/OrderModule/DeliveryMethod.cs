﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public string ShortName { get; set; } = null!;
        public string Description { get; set; } = null!; 
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; } = null!;


    }
}
