using Example07.Models;
using Microsoft.EntityFrameworkCore;
using TranAnhDung.API.DataAccess.Entity;

namespace TranAnhDung.API.DataAccess.Context
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ cho Category
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany() // Một danh mục cha có thể có nhiều danh mục con
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa danh mục cha khi danh mục con bị xóa

            modelBuilder.Entity<Category>()
                .HasOne(c => c.AddedByNavigation)
                .WithMany() // Một người dùng có thể thêm nhiều danh mục
                .HasForeignKey(c => c.AddedBy)
                .OnDelete(DeleteBehavior.SetNull); // Đặt giá trị AddedBy là null khi người dùng bị xóa

            // Cấu hình quan hệ cho Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Cat)
                .WithMany(c => c.Products) // Một danh mục có thể có nhiều sản phẩm
                .HasForeignKey(p => p.CatId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa danh mục khi sản phẩm bị xóa
        }
    }
}
