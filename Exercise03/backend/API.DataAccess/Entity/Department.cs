using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranAnhDung.API.DataAccess.Entity
{
    [Table("Department", Schema = "dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "INT")]
        public int DepartmentId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DepartmentName { get; set; }
    }
}
