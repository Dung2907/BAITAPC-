using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Variant
    {
        public Guid Id { get; set; }
        public string VariantOption { get; set; }
        public Guid ProductId { get; set; }
        public Guid VariantOptionId { get; set; }
        public Product Product { get; set; }
        public VariantOption VariantOptionEntity { get; set; }
    }
}