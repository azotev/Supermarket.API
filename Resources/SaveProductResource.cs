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
        [Range(0, 100)]
        public short QuantityInPackage { get; set; }

        [Required]
        [Range(1, 5)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
