using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.API.Domain.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public short ShipperId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
