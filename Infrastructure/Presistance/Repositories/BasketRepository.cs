using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;

namespace Presistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated=await _database.StringSetAsync(JsonBasket,basket.Id,TimeToLive?? TimeSpan.FromDays(1));
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key) 
        {
            var basket = await _database.StringGetAsync(key);
            if (basket.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<CustomerBasket>(basket);

        }

        public async Task<bool> DeleteBasketAsync(string Id) => await _database.KeyDeleteAsync(Id);
       
    }
}
