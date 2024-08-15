using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using API.DataAccess.Entity;
namespace API.DataAccess.Context
{
    public class EFDataContext:DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
        :base (options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}