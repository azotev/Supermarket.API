using System.ComponentModel.DataAnnotations;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int QuantityInStock { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
