using AutoMapper;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemsDto, string>
    {
        public string Resolve(OrderItem source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.product.PictureUrl))
            {
                return string.Empty;
            }
           
                var Url = $"{configuration.GetSection("URLS")["BaseUrl"]}{source.product.PictureUrl}";

            return Url ;
        }
    }
}
