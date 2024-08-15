using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string OrderId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}