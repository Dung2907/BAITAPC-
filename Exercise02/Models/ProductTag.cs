using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ProductTag
    {
        public Guid TagId { get; set; } // Foreign key to Tag

        public Guid ProductId { get; set; } // Foreign key to Product

        // Navigation properties
        public Tag Tag { get; set; }
        public Product Product { get; set; }
    }
}