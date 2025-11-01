﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public ShippingAddressDto Address { get; set; } = null!;



    }
}
