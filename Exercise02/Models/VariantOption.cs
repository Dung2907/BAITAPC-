using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class VariantOption
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? ImageId { get; set; }
        public Guid ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ComparePrice { get; set; }
        public decimal? BuyingPrice { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
        public bool Active { get; set; } = true;
        // Navigation properties
        public Gallery Image { get; set; }
        public Product Product { get; set; }
    }
}