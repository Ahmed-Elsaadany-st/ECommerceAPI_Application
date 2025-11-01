using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModule;
using Servieces.Abstractions;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class OrderService(IBasketRepository _basketRepo, IMapper _mapper,IUntiOfWork _untiOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            //Map Address To  OrderAddress 
            var OrderAddress=_mapper.Map<ShippingAddressDto,ShippingAddress>(orderDto.Address);
            //Get basket
            var basket = await _basketRepo.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
            //Create Order Item List
            List<OrderItem> orderItems = [];
            var ProductRepository = _untiOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var Product = await ProductRepository.GetByIdAsync(item.Id)?? throw new ProductNotFoundException(item.Id);

                var OrderItem = new OrderItem()
                {
                    product=new ProductItemOrdered()
                    {
                        ProductId=Product.Id,
                        PictureUrl=Product.PictureUrl,
                        ProductName=Product.Name,
                    },
                    Price=Product.Price,
                    Quantity=item.Quantity,
                };

                orderItems.Add(OrderItem);
            }
            //Get Delivery Method
            var DeliveryMethod=await _untiOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(orderDto.DeliveryMethodId)??throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);


            //Calculate SubTotal
            var SubTotalo = orderItems.Sum(I => I.Quantity * I.Price);
            //Create Order
            var order = new Order(Email,orderItems,DeliveryMethod,OrderAddress,SubTotalo);
            await _untiOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _untiOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
