using System;
using System.Collections.Generic;

#nullable disable

namespace Supermarket.API.Domain.Models
{
    public partial class OrderItemNote
    {
        public int NoteId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Note { get; set; }
    }
}
