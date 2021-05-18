using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Services.Communication
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> ListAsync();
    }
}