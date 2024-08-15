using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ProductAttributeValue
    {
        public Guid Id { get; set; }
        public Guid ProductAttributeId { get; set; }
        public Guid AttributeValueId { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public AttributeValue AttributeValue { get; set; }
    }
}