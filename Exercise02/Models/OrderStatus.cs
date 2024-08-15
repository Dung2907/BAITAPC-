using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class OrderStatus
    {
        public Guid Id { get; set; }

        public string StatusName { get; set; }

        public string Color { get; set; }

        public string Privacy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public StaffAccount CreatedByStaffAccount { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
    }
}