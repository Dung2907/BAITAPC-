using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise02.Models
{
    public class Notification
    {
        public Guid Id { get; set; } // UUID for the notification

        public Guid AccountId { get; set; } // Foreign key to StaffAccount

        public string Title { get; set; } // Title of the notification

        public string Content { get; set; } // Content of the notification

        public bool? Seen { get; set; } // Indicates if the notification has been seen

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when created

        public DateTime? ReceiveTime { get; set; } // Timestamp when the notification was received

        public DateTime? NotificationExpiryDate { get; set; } // Expiry date for the notification

        // Navigation property
        public StaffAccount Account { get; set; }
    }
}