using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Exercise02.Models
{
    public class Attribute
    {
        public Guid Id { get; set; }
        public string AttributeName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public StaffAccount CreatedByUser { get; set; }
        public StaffAccount UpdatedByUser { get; set; }
    }
}