using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class AttributeValue
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public string AttributeValueText { get; set; }
        public string Color { get; set; }
        public Attribute Attribute { get; set; }
        public ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
    }
}