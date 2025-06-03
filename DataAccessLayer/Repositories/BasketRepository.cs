using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using ProjectAPI.DataAccessLayer.Data.Models;
using StackExchange.Redis;

namespace ProjectAPI.DataAccessLayer.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
            => await _database.KeyDeleteAsync(id);

        public async Task<Basket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if (value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<Basket?>(value);
        }

        public async Task<Basket?> UpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket ,timeToLive ?? TimeSpan.FromDays(10));
            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
;        }
    }
}
