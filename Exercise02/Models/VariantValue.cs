using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class VariantValue
    {
        public Guid Id { get; set; }
        public Guid VariantId { get; set; }
        public Guid ProductAttributeValueId { get; set; }
        public Variant Variant { get; set; }
        public ProductAttributeValue ProductAttributeValue { get; set; }
    }
}