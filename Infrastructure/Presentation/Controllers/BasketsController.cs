using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiecManager _serviecManager) :ControllerBase
    {
        //Get Basket by id 
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket=await _serviecManager.basketService.GetBasketAsync(key);
            return Ok(basket);
        }
        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket=await _serviecManager.basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        //Delete Basket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
          var Result=  await _serviecManager.basketService.DeleteBasketAsync(key);
            return Ok(Result);
        }


    }
}
