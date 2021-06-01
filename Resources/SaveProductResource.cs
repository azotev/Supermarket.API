using System.ComponentModel.DataAnnotations;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
