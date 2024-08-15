using Exercise01.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Exercise01.Context
{
    public class Exercise01Context : DbContext
    {
        public Exercise01Context()
        {
        }

        public Exercise01Context(DbContextOptions<Exercise01Context> options) : base(options)
        {
        }
public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
{
    OnBeforeSaving();
    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
}

private void OnBeforeSaving()
{
    var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

    foreach (var entry in entries)
    {
        var entity = (BaseEntity)entry.Entity;
        var now = DateTime.UtcNow; // Lấy giờ UTC

        if (entry.State == EntityState.Added)
        {
            // Đổi giờ từ UTC sang giờ của khu vực của bạn
            now = TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            
            entity.CreatedAt = now;
        }

        entity.UpdatedAt = now;
    }
}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<VerificationToken> VerificationTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(s => s.ProductId).HasName("PK_product_id");
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(v => v.CategoryId).HasName("PK_category_id");
                e.HasMany(p => p.Products)
                    .WithOne(c => c.Category)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(e =>
  {
      e.HasKey(u => u.UserId).HasName("PK_user_id");

      // Thêm mối quan hệ với Credential
      e.HasMany(u => u.Credentials)
          .WithOne(c => c.User)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.Restrict);
  });

            modelBuilder.Entity<Cart>(e =>
            {
                e.HasKey(c => c.CartId).HasName("PK_cart_id");
                e.HasOne(u => u.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Address>(e =>
            {
                e.HasKey(a => a.AddressId).HasName("PK_address_id");
                e.HasOne(u => u.User)
                    .WithMany()
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Like>(e =>
            {
                e.HasKey(l => new { l.UserId, l.ProductId }).HasName("PK_like_user_product");
                e.HasOne(u => u.User)
                    .WithMany()
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(p => p.Product)
                    .WithMany()
                    .HasForeignKey(l => l.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.OrderId).HasName("PK_order_id");

                // Đặt tên khóa ngoại là "FK_order_cart" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(c => c.Cart)
                    .WithMany()
                    .HasForeignKey(o => o.CartId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_order_cart");

                // Thêm mối quan hệ với Payment
                e.HasMany(o => o.Payments)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                // e.HasKey(oi => new { oi.ProductId, oi.OrderId }).HasName("PK_order_item_product_order");

                // // Đặt tên khóa ngoại là "FK_orderitem_order" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(oi => oi.Order)
                    .WithMany()
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_orderitem_order");

                // Đặt tên khóa ngoại là "FK_orderitem_product" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(oi => oi.Product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_orderitem_product");
            });
            modelBuilder.Entity<Payment>(e =>
            {
                e.HasKey(p => p.PaymentId).HasName("PK_payment_id");

                // Đặt tên khóa ngoại là "FK_payment_order" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(p => p.Order)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_payment_order");
            });
            modelBuilder.Entity<Credential>(e =>
            {
                e.HasKey(c => c.CredentialId).HasName("PK_credential_id");

                // Đặt tên khóa ngoại là "FK_credential_user" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(c => c.User)
                    .WithMany(u => u.Credentials)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_credential_user");
                e.HasMany(c => c.VerificationTokens)
    .WithOne(vt => vt.Credential)
    .HasForeignKey(vt => vt.CredentialId)
    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<VerificationToken>(e =>
            {
                e.HasKey(vt => vt.TokenId).HasName("PK_verification_token_id");

                // Đặt tên khóa ngoại là "FK_verificationtoken_credential" và thay thế DeleteBehavior.Restrict bằng DeleteBehavior.Cascade
                e.HasOne(vt => vt.Credential)
                    .WithMany(c => c.VerificationTokens)
                    .HasForeignKey(vt => vt.CredentialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_verificationtoken_credential");
            });
        }
    }
}
