using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.API.Domain.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public byte Status { get; set; }
        public string Comments { get; set; }
        public DateTime? ShippedDate { get; set; }
        public short? ShipperId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual OrderStatus StatusNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
