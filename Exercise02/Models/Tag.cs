using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Tag
    {
        public Guid Id { get; set; } // UUID for the tag

        public string TagName { get; set; } // Name of the tag

        public string Icon { get; set; } // Optional icon for the tag

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when created

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp when updated

        public Guid? CreatedBy { get; set; } // Foreign key to StaffAccount for who created the tag

        public Guid? UpdatedBy { get; set; } // Foreign key to StaffAccount for who last updated the tag

        // Navigation properties
        public StaffAccount CreatedByStaffAccount { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
    }
}