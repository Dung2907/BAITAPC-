using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Slideshow
    {
         public Guid Id { get; set; } // UUID for the slideshow

        public string Title { get; set; } // Title of the slideshow

        public string DestinationUrl { get; set; } // URL destination for the slideshow

        public string Image { get; set; } // Image URL or path

        public string Placeholder { get; set; } // Placeholder image URL or path

        public string Description { get; set; } // Description of the slideshow

        public string BtnLabel { get; set; } // Button label for the slideshow

        public int DisplayOrder { get; set; } // Order in which the slideshow should be displayed

        public bool Published { get; set; } = false; // Indicates if the slideshow is published

        public int Clicks { get; set; } = 0; // Number of clicks on the slideshow

        public string Styles { get; set; } // JSONB style information

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when created

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp when updated

        public Guid? CreatedBy { get; set; } // Foreign key to StaffAccount for who created the slideshow

        public Guid? UpdatedBy { get; set; } // Foreign key to StaffAccount for who last updated the slideshow

        // Navigation properties
        public StaffAccount CreatedByStaffAccount { get; set; }
        public StaffAccount UpdatedByStaffAccount { get; set; }
    }
}