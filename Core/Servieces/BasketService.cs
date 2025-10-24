using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Servieces.Abstractions;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class BasketService(IBasketRepository _BasketRepo, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> CreateOrUpdateBasketAsync(BasketDto basket)
        {
           var CustomerBasket=_mapper.Map<BasketDto,CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _BasketRepo.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Can not Update Or Create this basket now , please try again later!");
            }
        }

        public async Task<BasketDto?> GetBasketAsync(string key)
        {
            var basket = await _BasketRepo.GetBasketAsync(key);
            if (basket is not null)
            {
                return _mapper.Map<CustomerBasket,BasketDto>(basket);
            }
            else
            {
                throw new BasketNotFoundException(key);
            }
        }
        public async Task<bool> DeleteBasketAsync(string key)=>await _BasketRepo.DeleteBasketAsync(key);
        
    }
}
