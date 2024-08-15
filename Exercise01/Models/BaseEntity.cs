using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    public class BaseEntity
    {
        [Column("created_at")] // Xác định tên cụ thể của cột cho CreatedAt
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")] // Xác định tên cụ thể của cột cho UpdatedAt
        public DateTime UpdatedAt { get; set; }
    }
}
