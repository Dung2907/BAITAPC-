using Example03;
using Microsoft.EntityFrameworkCore;

namespace Example03
{
    public class EmployeeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=Example03;User Id=sa;password=sa;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=true");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}