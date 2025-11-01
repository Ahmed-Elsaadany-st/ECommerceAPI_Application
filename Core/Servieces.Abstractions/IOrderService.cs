﻿using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Abstractions
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDto orderDto,string Email);
    }
}
