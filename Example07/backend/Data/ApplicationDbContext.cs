using Microsoft.EntityFrameworkCore;
using Example07.Models;

namespace Example07.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id); // Define primary key
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.RefreshToken).HasDefaultValue("defaultToken");
                // Additional configuration if needed
            });
            
        }

        public DbSet<User> User { get; set; }
    }
}
