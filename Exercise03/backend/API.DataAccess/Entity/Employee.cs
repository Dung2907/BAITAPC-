using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranAnhDung.API.DataAccess.Entity
{
    [Table("Employee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "INT")]
        public int EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Mobile { get; set; }
        [Required]
        public bool IsPermanent { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }
        [Required]
        [Column(TypeName = "INT")]
        public int DepartmentId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? imageUrl { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
    }

}
