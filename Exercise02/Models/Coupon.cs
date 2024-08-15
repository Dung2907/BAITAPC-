using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal? DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public int TimesUsed { get; set; } = 0;
        public int? MaxUsage { get; set; }
        public decimal? OrderAmountLimit { get; set; }
        public DateTime? CouponStartDate { get; set; }
        public DateTime? CouponEndDate { get; set; }
    }
}