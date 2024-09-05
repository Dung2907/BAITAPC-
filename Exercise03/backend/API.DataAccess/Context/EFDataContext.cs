using Microsoft.EntityFrameworkCore;
using TranAnhDung.API.DataAccess.Entity;

namespace TranAnhDung.API.DataAccess.Context
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
        : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }

}