using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ProductAttribute
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid AttributeId { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Attribute Attribute { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
    }
}