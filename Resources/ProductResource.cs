using Supermarket.API.Domain.Models;

namespace Supermarket.API.Resources
{
    public class ProductResource
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Category Category { get; set; }
    }
}