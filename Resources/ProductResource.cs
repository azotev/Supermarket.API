using System.Collections.Generic;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Resources
{
    public class ProductResource
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public List<int> OrderId { get; set; } = new List<int>();
    }
}
