﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.API.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
