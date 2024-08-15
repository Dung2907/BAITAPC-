using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Sell
    {
         public int Id { get; set; } 

        public Guid ProductId { get; set; } 

        public decimal Price { get; set; } 

        public int Quantity { get; set; } 
        public Product Product { get; set; }
    }
}