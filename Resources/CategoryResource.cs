using System.Collections.Generic;

namespace Supermarket.API.Resources
{
    public class CategoryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ProductNames { get; set; } = new List<string>();
    }
}
