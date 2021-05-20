using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;

namespace Supermarket.API.Domain.Services.Communication
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> GetAsync(int id);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
