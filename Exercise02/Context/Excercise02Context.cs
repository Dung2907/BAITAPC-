using Microsoft.EntityFrameworkCore;
using Exercise02.Models;
using Attribute = Exercise02.Models.Attribute;

namespace Exercise02.Context
{
    public partial class Exercise02Context : DbContext
    {
        public Exercise02Context()
        {
        }

        public Exercise02Context(DbContextOptions<Exercise02Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public virtual DbSet<AttributeValue> AttributeValues { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardItem> CardItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductCoupon> ProductCoupons { get; set; }
        public virtual DbSet<ProductShippingInfo> ProductShippingInfos { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<ShippingRate> ShippingRates { get; set; }
        public virtual DbSet<ShippingZone> ShippingZones { get; set; }
        public virtual DbSet<Slideshow> Slideshows { get; set; }
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Variant> Variants { get; set; }
        public virtual DbSet<VariantOption> VariantOptions { get; set; }
        public virtual DbSet<VariantValue> VariantValues { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttributeValue>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_product_attribute_values");
                entity.ToTable("product_attribute_values");

                // Cấu hình khóa ngoại đến ProductAttribute
                entity.HasOne(e => e.ProductAttribute)
                    .WithMany(p => p.ProductAttributeValues) // Đảm bảo ProductAttribute có thuộc tính ProductAttributeValues
                    .HasForeignKey(e => e.ProductAttributeId)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade

                // Cấu hình khóa ngoại đến AttributeValue
                entity.HasOne(e => e.AttributeValue)
                    .WithMany() // Đảm bảo AttributeValue không có thuộc tính ProductAttributeValues
                    .HasForeignKey(e => e.AttributeValueId)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade
            });
            // Cấu hình cho Variant
            modelBuilder.Entity<Variant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_variants_id");
                entity.ToTable("variants");

                // Cấu hình khóa ngoại đến Product
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Variants) // Đảm bảo rằng Product có một thuộc tính Variants
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade

                // Cấu hình khóa ngoại đến VariantOption
                entity.HasOne(e => e.VariantOptionEntity)
                    .WithMany() // Nếu VariantOption không có thuộc tính Variants, sử dụng WithMany()
                    .HasForeignKey(e => e.VariantOptionId)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade
            });
            // Cấu hình cho Tag
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tags_id");
                entity.ToTable("tags");

                // Cấu hình khóa ngoại đến StaffAccount
                entity.HasOne(e => e.CreatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade

                entity.HasOne(e => e.UpdatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.NoAction); // Sử dụng NoAction để tránh chu trình cascade
            });
            // Cấu hình cho Supplier
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_suppliers_id");
                entity.ToTable("suppliers");

                // Cấu hình khóa ngoại đến Country
                entity.HasOne(e => e.Country)
                    .WithMany()
                    .HasForeignKey(e => e.CountryId)
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn chu trình cascade

                // Cấu hình các khóa ngoại đến StaffAccount
                entity.HasOne(e => e.CreatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn chu trình cascade

                entity.HasOne(e => e.UpdatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn chu trình cascade
            });
            // Cấu hình cho Slideshow
            modelBuilder.Entity<Slideshow>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_slideshows_id");
                entity.ToTable("slideshows");
                // Cấu hình các khóa ngoại
                entity.HasOne(e => e.CreatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn chu trình cascade

                entity.HasOne(e => e.UpdatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Ngăn chặn chu trình cascade
            });
            // Cấu hình cho ShippingZone
            modelBuilder.Entity<ShippingZone>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_shipping_zones_id");
                entity.ToTable("shipping_zones");

                // Cấu hình các khóa ngoại
                entity.HasOne(e => e.CreatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade

                entity.HasOne(e => e.UpdatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade
            });
            // Cấu hình cho OrderStatus
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_order_statuses_id");
                entity.ToTable("order_statuses");

                // Cấu hình các khóa ngoại
                entity.HasOne(e => e.CreatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade

                entity.HasOne(e => e.UpdatedByStaffAccount)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade
            });
            // Cấu hình cho Attribute
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_attributes_id");
                entity.ToTable("attributes");

                // Cấu hình các khóa ngoại
                entity.HasOne(e => e.UpdatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade

                entity.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Sử dụng Restrict để tránh chu trình cascade
            });

            // Cấu hình cho Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_products_id");
                entity.ToTable("products");

                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

                entity.HasMany(p => p.ProductCategories)
                      .WithOne(pc => pc.Product)
                      .HasForeignKey(pc => pc.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasMany(p => p.ProductShippingInfos)
                      .WithOne(ps => ps.Product)
                      .HasForeignKey(ps => ps.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình cho Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_roles_id");
                entity.ToTable("roles");
            });

            // Cấu hình cho StaffAccount
            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_staffaccounts_id");
                entity.ToTable("staffaccounts");

                entity.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);              
            });

            // Cấu hình cho Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_categories_id");
                entity.ToTable("categories");

                entity.HasMany(c => c.ProductCategories)
                      .WithOne(pc => pc.Category)
                      .HasForeignKey(pc => pc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Parent)
                    .WithMany(e => e.Children)
                    .HasForeignKey(e => e.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình cho ProductCategory
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_product_categories_id");
                entity.ToTable("product_categories");

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình cho ProductShippingInfo
            modelBuilder.Entity<ProductShippingInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_product_shipping_info");
                entity.ToTable("product_shipping_info");

                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductShippingInfos)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Weight).HasColumnName("Weight");
                entity.Property(e => e.WeightUnit)
                    .HasMaxLength(10)
                    .HasColumnName("WeightUnit");
                entity.Property(e => e.Volume).HasColumnName("Volume");
                entity.Property(e => e.VolumeUnit)
                    .HasMaxLength(10)
                    .HasColumnName("VolumeUnit");
                entity.Property(e => e.DimensionWidth).HasColumnName("DimensionWidth");
                entity.Property(e => e.DimensionHeight).HasColumnName("DimensionHeight");
                entity.Property(e => e.DimensionDepth).HasColumnName("DimensionDepth");
                entity.Property(e => e.DimensionUnit)
                    .HasMaxLength(10)
                    .HasColumnName("DimensionUnit");
            });

            // Cấu hình cho Gallery
            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_galleries_id");
                entity.ToTable("galleries");

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Image).HasColumnName("Image");
                entity.Property(e => e.Placeholder).HasColumnName("Placeholder");
                entity.Property(e => e.IsThumbnail).HasColumnName("IsThumbnail");
                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
            });

            // Cấu hình cho ProductTag
            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.ProductId }).HasName("PK_product_tags_id");
                entity.ToTable("product_tags");

                entity.HasOne(e => e.Tag)
                    .WithMany()
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Thay đổi giá trị CreatedBy và UpdatedBy cho Product để tránh lỗi khóa ngoại
            modelBuilder.Entity<Product>().HasData(              
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Slug = "iphone-14-pro-max",
                    ProductName = "iPhone 14 Pro Max",
                    Sku = "880088",
                    SalePrice = 999.99m,
                    ComparePrice = 1099.99m,
                    BuyingPrice = 899.99m,
                    Quantity = 10,
                    ShortDescription = "Điện thoại thông minh cao cấp",
                    ProductDescription = "iPhone 14 Pro Max có màn hình lớn hơn và hệ thống camera được cải tiến.",
                    ProductType = "Smartphone",
                    Published = true,
                    Note = "Không có ghi chú bổ sung.",
                    CreatedBy = Guid.NewGuid(), // Cung cấp giá trị Guid hợp lệ
                    UpdatedBy = Guid.NewGuid()  // Cung cấp giá trị Guid hợp lệ
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Slug = "iphone-15-pro-max",
                    ProductName = "iPhone 15 Pro Max",
                    Sku = "444666",
                    SalePrice = 1099.99m,
                    ComparePrice = 1199.99m,
                    BuyingPrice = 999.99m,
                    Quantity = 5,
                    ShortDescription = "Điện thoại thông minh thế hệ mới nhất",
                    ProductDescription = "iPhone 15 Pro Max cung cấp công nghệ và tính năng tiên tiến.",
                    ProductType = "Smartphone",
                    Published = true,
                    Note = "Không có ghi chú bổ sung.",
                    CreatedBy = Guid.NewGuid(), // Cung cấp giá trị Guid hợp lệ
                    UpdatedBy = Guid.NewGuid()  // Cung cấp giá trị Guid hợp lệ
                }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
