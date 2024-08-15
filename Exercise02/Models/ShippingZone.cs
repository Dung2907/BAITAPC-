using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class ShippingZone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; } = false;
        public bool FreeShipping { get; set; } = false;
        public string RateType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public StaffAccount CreatedByStaffAccount { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
    }
}