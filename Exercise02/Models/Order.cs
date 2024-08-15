using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Order
    {
        public string Id { get; set; }

        public Guid? CouponId { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? OrderStatusId { get; set; }

        public DateTime? OrderApprovedAt { get; set; }

        public DateTime? OrderDeliveredCarrierDate { get; set; }

        public DateTime? OrderDeliveredCustomerDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? UpdatedBy { get; set; }
        public Coupon Coupon { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
    }
}