using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.DataAccess.Entity
{   
    [Table("Department",Schema = "dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (TypeName = "INT")]
        public int DepartmentId { get; set; }
        [Required]
        [Column (TypeName = "varchar(50)")]
        public string DepartmentName { get; set; }
    }
}