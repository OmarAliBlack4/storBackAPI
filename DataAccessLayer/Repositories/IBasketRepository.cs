using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.DataAccessLayer.Repositories
{
    public interface IBasketRepository
    {
        public Task<Basket> GetBasketAsync(string id);
        public Task<Basket?> UpdateBasketAsync(Basket basket , TimeSpan? timeToLive = null);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
