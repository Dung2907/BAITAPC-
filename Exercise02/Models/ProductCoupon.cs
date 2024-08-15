using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ProductCoupon
    {
         public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CouponId { get; set; }
        public Product Product { get; set; }
        public Coupon Coupon { get; set; }
    }
}