using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Supplier
    {
        public Guid Id { get; set; } // UUID for the supplier

        public string SupplierName { get; set; } // Name of the supplier

        public string Company { get; set; } // Company name of the supplier (optional)

        public string PhoneNumber { get; set; } // Phone number of the supplier

        public string AddressLine1 { get; set; } // Address line 1

        public string AddressLine2 { get; set; } // Address line 2 (optional)

        public int CountryId { get; set; } // Foreign key to Country

        public string City { get; set; } // City of the supplier

        public string Note { get; set; } // Additional notes about the supplier

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when created

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp when updated

        public Guid? CreatedBy { get; set; } // Foreign key to StaffAccount for who created the supplier

        public Guid? UpdatedBy { get; set; } // Foreign key to StaffAccount for who last updated the supplier

        // Navigation properties
        public StaffAccount CreatedByStaffAccount { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
        public Country Country { get; set; }
    }
}